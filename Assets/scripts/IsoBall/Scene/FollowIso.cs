using UnityEngine;
using System.Collections;


namespace IsoBall {
    public class FollowIso : MonoBehaviour {

        [Header("Main Parameters")]
        [Tooltip("Target to Follow")]
        public Transform target;
        [Tooltip("Smoothing of Null Movment")]
        public float damping = 1;
        [Tooltip("Forces the Null to Lock ahead on Movement (GameUnits)")]
        public float lookAheadFactor = 3;
        [Tooltip("Min Y the Null should follow")]
        public float yPosClamp = -2;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
        private Rigidbody rb;

        //private BallController pControl;
        private bool isSearchForPlayer = false;

        void Awake() {
            SceneSetup();
        }


        public void SceneSetup() {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
         //   pControl = target.GetComponent<BallController>();
            rb = GetComponent<Rigidbody>();
        }


        private void FixedUpdate() {

            // If no Target set, do nothing
            if(this.target == null) {
                if(isSearchForPlayer == false) {
                    StartCoroutine(findPlayer());
                    isSearchForPlayer = true;
                }
            } else {

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

                //transform.position = newPos;
                rb.MovePosition(newPos);

                m_LastTargetPosition = target.position;
            }
        }


        // Set a New Target, update Player
        public void setPlayer(Transform _playerTarget) {
            this.target = _playerTarget;
           // this.pControl = _playerTarget.GetComponent<BallController>();
        }

        public IEnumerator findPlayer() {
            yield return new WaitForSeconds(1.5f);
            PlayerBall _player = IsoBallMaster.getPlayer();
            if(_player == null) {
                StartCoroutine(findPlayer());
            } else {
                target = _player.gameObject.transform;
                isSearchForPlayer = false;
            }
        }
    }

}
