using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace IsoBall {
    public class BallController : MonoBehaviour {

        [Header("Physics Related")]
        [Tooltip("maxSpeed Player can Move")]
        public float speed = 6f;
        [Tooltip("max Height the Player can Jump")]
        public float jumpPower = 5.25f;
        [Tooltip("Distance RayCast from Center travel")]
        public float distToGround = 0.6f;
        [Tooltip("Jump Delay in Sec")]
        public float jumpDelay = 0.85f;

        [Header("Controlrelated")]
        [Tooltip("Enable the Calculation for Input")]
        public bool enable = true;
        [Tooltip("Enable Jumping")]
        public bool enableJump = true;

        [Header("Send Camera Data")]
        public bool enableCamRotation = true;
        public bool enableZoom = true;
        public bool enableCamAngle = true;

        [Tooltip("Axis Multiply")]
        public float axisMult = 1f;

        [HideInInspector]
        public Rigidbody rb;
        private Vector3 movement;
        private int direction = 0;
        private bool isJumped;
        private bool isColl;

        private PlayerBall player;

        void Awake() {
            rb = GetComponent<Rigidbody>();
            player = GetComponent<PlayerBall>();
        }

        void FixedUpdate() {
            if(enable == true) {
                //Get Input
                float _moveHorizontal = Input.GetAxis("Horizontal");
                float _moveVertical = Input.GetAxis("Vertical");


                // Calculate Direction
                switch(direction) {
                    case 0: //North
                        movement = new Vector3(_moveVertical * axisMult, 0f, -_moveHorizontal * axisMult);
                        //movement = new Vector3(_moveHorizontal * axisMult, gravMult, _moveVertical * axisMult);
                        break;
                    case 1: //East
                        movement = new Vector3(-_moveHorizontal * axisMult, 0f, -_moveVertical * axisMult);
                        //  movement = new Vector3(_moveVertical * axisMult, gravMult, -_moveHorizontal * axisMult);        
                        break;
                    case 2: //South
                        movement = new Vector3(-_moveVertical * axisMult, 0f, _moveHorizontal * axisMult);
                        //movement = new Vector3(-_moveHorizontal * axisMult, gravMult, -_moveVertical * axisMult);
                        break;
                    case 3: //West
                        movement = new Vector3(_moveHorizontal * axisMult, 0f, _moveVertical * axisMult);
                        //movement = new Vector3(-_moveVertical * axisMult, gravMult, _moveHorizontal * axisMult);
                        break;
                }

                //Add the Calculation Force
                rb.AddTorque(movement * speed);

                if(enableJump) {
                    if(Input.GetButton("Fire1")) {
                        if(!isJumped && isColl) {

                            RaycastHit _hit;
                            Ray groundRay = new Ray(transform.position, Vector3.down);

                            if(Physics.Raycast(groundRay, out _hit, distToGround)) {
                                //Get the Normal from Ground to calc the jumpdirection
                                Vector3 _normal = _hit.normal;
                                _normal.Normalize();
                                _normal *= jumpPower;
                                rb.AddForce(_normal, ForceMode.Impulse);
                                player.JumpEffect();
                                StartCoroutine("jumpDelayReset");
                                isJumped = true;
                            }
                        }
                    }

                }

            }



            //Reset Scene
            bool _reset = Input.GetButtonDown("Restart");
            if(_reset) {
                IsoBallMaster.restartLevel();
            }

            //RotateCam
            if(enableCamRotation) {
                float _rotateCam = Input.GetAxis("RotateCam");
                if(_rotateCam > 0 || _rotateCam < 0) {
                    IsoBallMaster.RotateCamera(_rotateCam);
                }
            }

            if(enableZoom) {
                float _zoom = Input.GetAxis("CamZoom");
                float _scrollwheel = Input.GetAxis("Mouse ScrollWheel");
                if(_zoom > 0f || _scrollwheel > 0f) {
                    IsoBallMaster.ZoomCamera(-1);
                }
                if(_zoom < 0f || _scrollwheel < 0f) {
                    IsoBallMaster.ZoomCamera(1);
                }
            }

            if(enableZoom) {
                float _angle = Input.GetAxis("CamAngle");
                if(_angle > 0f) {
                    IsoBallMaster.AngleCamera(-1);
                }
                if(_angle < 0f) {
                    IsoBallMaster.AngleCamera(1);
                }
            }
        }

        public void OnCollisionEnter(Collision collision) {

        }

        public void OnCollisionStay(Collision collision) {
            isColl = true;
        }

        public void OnCollisionExit(Collision collision) {
            isColl = false;
        }

        public IEnumerator jumpDelayReset() {
            yield return new WaitForSeconds(jumpDelay);
            isJumped = false;
        }

        //Some OnScreen Debug
        public void OnGUI() {
            if(IsoBallMaster.isoBallMaster.debugMode == true) {
                GUI.Label(new Rect(20, 120, 300, 20), "player.velocity.x = " + rb.velocity.x);
                GUI.Label(new Rect(20, 140, 300, 20), "player.velocity.z = " + rb.velocity.z);
                GUI.Label(new Rect(20, 160, 300, 20), "player.movement.x = " + movement.x);
                GUI.Label(new Rect(20, 180, 300, 20), "player.movement.z = " + movement.z);
                GUI.Label(new Rect(20, 200, 300, 20), "player.totalforce.x = " + movement.x * speed);
                GUI.Label(new Rect(20, 220, 300, 20), "player.totalforce.z = " + movement.z * speed);
                GUI.Label(new Rect(20, 250, 300, 20), "actual Direction = " + direction);

                GUI.Label(new Rect(20, 320, 300, 20), "PlayerColl = " + isColl);
                GUI.Label(new Rect(20, 340, 300, 20), "PlayerisJump= " + isJumped);
            }
        }

        //Setter for Direction (0-3)
        public void setDirection(int _value) {
            direction = _value;
            if(direction > 3)
                direction = 3;
            else if(direction < 0)
                direction = 0;
        }

        // Enable_Disable the most Controls
        public void setEnable(bool _value) {
            this.enable = _value;
        }

    }
}
