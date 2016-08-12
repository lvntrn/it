using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace IsoBall {
    public class PlayerBall : MonoBehaviour {

        public float killY;
        [Header("OnScale Event Settings")]
        public Vector3 targetScale = new Vector3(0.05f, 0.05f, 0.05f);
        public float scaleSpeedDown = 0.6f;
        public float scaleSpeedUp = 2f;
        public string easeType = "easeInOutExpo";

        [Header("DecoStuff")]
        public AudioClip[] pClip;
        
        [HideInInspector]
        public BallController pControl;
        private bool isShrinked = false;
        private bool isScaling = false;
        private int status = 0;
        private bool isDead = false;
        private bool noRespawn = false;
        private AudioSource pAudio;


        void Awake() {
            pControl = GetComponent<BallController>();
            pAudio = GetComponent<AudioSource>();
        }

        void Start() {
            noRespawn = IsoBallMaster.getNoRespawn();
        }

        // Update is called once per frame
        void Update() {

            // Kill Player when fall off
            if(killY > transform.position.y) {
                if(noRespawn) {
                    if(!isDead) {
                        scaleSpeedDown *= 0.75f;
                        scaleDown();
                        Invoke("RestartLv", scaleSpeedDown);
                        isDead = true;
                    }
                } else {
                    if(!isDead) {
                        scaleSpeedDown *= 0.75f;
                        scaleDown();
                        Invoke("Respawn", scaleSpeedDown);
                        isDead = true;
                    }
                }
            }


            // Check and Set Variables in Scaling Process
            if(isScaling == true) {
                switch(status) {
                    case 0:
                        if(transform.localScale.x <= targetScale.x) {
                            isShrinked = true;
                            status = 1;
                        }
                        break;
                    case 1:
                        if(transform.localScale.x >= 1f) {
                            isScaling = false;
                            isShrinked = false;
                            status = 0;
                        }
                        break;
                }
            }
        }

        public void JumpEffect() {
            int _random = Random.Range(0, pClip.Length);
            pAudio.clip = pClip[_random];
            pAudio.pitch = Random.Range(0.7f, 1.3f);
            
            pAudio.Play();
        }


        public void RestartLv() {
            IsoBallMaster.restartLevel();
        }

        public void Respawn() {
            IsoBallMaster.DestroyPlayerRespawn(GetComponent<PlayerBall>());
        }

        //Enables_Disables the PlayerController
        public void setPControl(bool _value) {
            pControl.setEnable(_value);
        }

        //Controll the Scaling, ScaleDown when Normal, ScaleUp when Shrink
        public void scalePlayer() {
            if(isShrinked == true) {
                scaleUp();
            }
            if(isShrinked == false) {
                scaleDown();
                isScaling = true;
            }
        }

        //Scale Player Down
        public void scaleDown() {
            iTween.ScaleTo(this.gameObject, iTween.Hash("x", targetScale.x, "y", targetScale.y, "z", targetScale.z, "easeType", easeType, "time", scaleSpeedDown));
        }

        //Scale Player UP
        public void scaleUp() {
            iTween.ScaleTo(this.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", easeType, "time", scaleSpeedUp));
        }

        public bool getShrinked() {
            return this.isShrinked;
        }
        public bool getIsScaling() {
            return this.isScaling;
        }
    }
}
