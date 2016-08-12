using UnityEngine;
using System.Collections;


namespace IsoBall {
    public class CameraFollowIso : MonoBehaviour {

        [Header("Main Parameters")]
        [Tooltip("Target to Follow")]
        public Transform target;
        [Tooltip("Smoothing of Camera Movment")]
        public float damping = 1;
        [Tooltip("Forces the Camera to Lock ahead on Movement (GameUnits)")]
        public float lookAheadFactor = 3;
        [Tooltip("Min Y the Camera should follow")]
        public float yPosClamp = -2;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        [Header("Rotate Parameters")]
        [Tooltip("Rotate EaseType")]
        public iTween.EaseType easeType;
        [Tooltip("Rotate Transition Time")]
        public float transitionTime;
        [Tooltip("Max Rotation Camera Angle in Procent")]
        public float maxRotation = 0.25f;

        [Header("Camera Angle")]
        public float[] angleTarget;
        [Tooltip("Zoom EaseType")]
        public iTween.EaseType angleEase;
        [Tooltip("Zoom Transition Time")]
        public float angleTransitionTime = 0.5f;
        [Tooltip("actual Startzoom = cameraPos")]
        public int actualAngle = 1; // actual Zoom the Cam is has

        [Header("Zoom Control")]
        public Vector3[] zoomTarget;
        [Tooltip("Zoom EaseType")]
        public iTween.EaseType zEase;
        [Tooltip("Zoom Transition Time")]
        public float zTransitionTime = 0.5f;
        [Tooltip("actual Startzoom = cameraPos")]
        public int actualZoom = 1; // actual Zoom the Cam is has

       

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
      
        private Camera cam; //Camera Reference
        private BallController pControl;
        private Transform cameraTransNull;
        private int actualDir; // actual Direction the Cam is facing
        private bool isRotating = false;
        private bool isTransision = false;

        void Awake() {
            SceneSetup();
        }
        void Start() {
            IsoBallMaster.SetCamera(this.cam, GetComponentInChildren<CameraShake>());
            getResetCamDir();
        }

        public void SceneSetup() {
            cameraTransNull = transform;
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            this.cam = Camera.main.GetComponent<Camera>();
            this.pControl = target.GetComponent<BallController>();
        }

        public void getResetCamDir() {
            setDirection(SaveObject.Get<SaveData>().world[IsoBallMaster.isoBallMaster.world].level[IsoBallMaster.isoBallMaster.level].actualCamDirection);
        }

        public void TransComp() {
            isTransision = false;
        }
        public void RotateComp() {
            isRotating = false;
        }

        private void Update() {

            // If no Target set, do nothing
            if(this.target == null) {
                return;
            }

            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if(updateLookAheadTarget) {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            } else {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y, yPosClamp, Mathf.Infinity), newPos.z);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }

        // Set a New Target, update Player
        public void setPlayer(Transform _playerTarget) {
            this.target = _playerTarget;
            this.pControl = _playerTarget.GetComponent<BallController>();
            pControl.setDirection(actualDir);
        }

        public void addAngle(int _step) {
            if(!isRotating) {
                actualAngle += _step;
                actualAngle = Mathf.Clamp(actualAngle, 0, angleTarget.Length - 1);
                angleCam(actualAngle);
            }
        }

        public void addZoom(int _step) {
            if(!isTransision) {
                actualZoom += _step;
                actualZoom = Mathf.Clamp(actualZoom, 0, zoomTarget.Length-1);
                zoomCam(actualZoom);
            }
        }

        //calculates Direction and send to Player
        private void addDirection(int _value) {
            actualDir += _value;
            if(actualDir > 3)
                actualDir = 0;
            else if(actualDir < 0)
                actualDir = 3;
            pControl.setDirection(actualDir);
        }

        //set Direction and send to Player
        public void setDirection(int _value) {
            actualDir = _value;
            switch(actualDir) {
                case 0:
                    cameraTransNull.rotation = Quaternion.Euler(new Vector3(angleTarget[actualAngle], 315f, 0f));
                    break;
                case 1:
                    cameraTransNull.rotation = Quaternion.Euler(new Vector3(angleTarget[actualAngle], 45f, 0f));
                    break;
                case 2:
                    cameraTransNull.rotation = Quaternion.Euler(new Vector3(angleTarget[actualAngle], 135f, 0f));
                    break;
                case 3:
                    cameraTransNull.rotation = Quaternion.Euler(new Vector3(angleTarget[actualAngle], 225f, 0f));
                    break;
            }
            pControl.setDirection(actualDir);
        }

        //Zoom Camera
        public void zoomCam(int _step) {
            if(!isTransision) {
                iTween.MoveTo(cam.gameObject, iTween.Hash("islocal", true, "x", zoomTarget[_step].x, "y", zoomTarget[_step].y, "z", zoomTarget[_step].z, "easetype", zEase, "time", zTransitionTime, "onComplete", "TransComp", "onCompleteTarget", transform.gameObject));
                isTransision = true;
            }
        }

        //Angle Camera
        public void angleCam(int _step) {
            if(!isRotating) {
                iTween.RotateTo(this.gameObject, iTween.Hash("islocal", true, "space", "world", "x", angleTarget[_step], "easetype", angleEase, "time", angleTransitionTime, "onComplete", "RotateComp"));
                isRotating = true;
            }
        }

        // Rotate Camera
        public void rotateCam(float _degree) {
            if(_degree < 0) {
                if(!isRotating) {
                    iTween.RotateBy(this.gameObject, iTween.Hash("islocal", true, "space", "world", "y", -maxRotation, "easeType", easeType, "time", transitionTime, "onComplete", "RotateComp"));
                    addDirection(-1);
                    isRotating = true;
                }
            } else {
                if(!isRotating) {
                    iTween.RotateBy(this.gameObject, iTween.Hash("islocal", true, "space", "world", "y", maxRotation, "easeType", easeType, "time", transitionTime, "onComplete", "RotateComp"));
                    addDirection(1);
                    isRotating = true;
                }
            }
        }

        public int getActualDir() {
            return actualDir;
        }

        public void OnGUI() {
            if(IsoBallMaster.isoBallMaster.debugMode == true) {
                GUI.Label(new Rect(20, 270, 300, 20), "actual Zoom = " + actualZoom);
                GUI.Label(new Rect(20, 290, 300, 20), "actual Angle = " + actualAngle);
            }
        }

    }
}
