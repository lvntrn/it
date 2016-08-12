using UnityEngine;
using System.Collections;

namespace IsoBall {
    public class Exit : MonoBehaviour {

        [Tooltip("Is Open, dont need to activate")]
        public bool exitOpen;
        [Tooltip("Dont Save for BonusState")]
        public bool bonus;
        [Tooltip("Override WarpLocation")]
        public string warpToLevel;
        //WIP
        [Tooltip("When Triggered how many it needs")]
        public int triggerCount = 1;
        public float warpDelay = 3f;
        public Vector3 effectOffset;
        public ParticleSystem particle;
        public Color particleColor;

        private ParticleSystem.EmissionModule emission;
        private int triggert = 0;
        private Animator aniExit;
        private AudioSource audioSource;


        void Awake() {
            aniExit = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            emission = particle.emission;
        }

        void Start() {
            particle.startColor = particleColor;
        }

        void Update() {
            //Link Animation Parameter
            aniExit.SetBool("ExitOpen", exitOpen);
            if(exitOpen) {
                emission.enabled = true;
            } else {
                emission.enabled = false;
            }
        }

        // if Exit is Open Load the Next Level with Delay
        void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "Player" && exitOpen == true) {
                exitOpen = false;
                PlayerBall _player = other.gameObject.GetComponent<PlayerBall>();
              
                //Make sure the Player Dont Move
                _player.setPControl(false);
                _player.scaleDown();
                audioSource.Play();
                _player.pControl.rb.Sleep();
                IsoBallMaster.DestroyPlayer(_player);
                //Spawn Default Effects
                IsoBallMaster.SpawnEffects(transform.position + effectOffset, IsoBallMaster.isoBallMaster.spawnfxMesh, warpDelay);
                IsoBallMaster.SpawnEffects(transform.position + effectOffset, IsoBallMaster.isoBallMaster.spawnParticlePrefab, warpDelay);

                //Enable WinPanel
                IsoBallMaster.setWinPanel(true);
                IsoBallMaster.enableTimer(false);
             
                //Calc and Save
                if(bonus) {
                    IsoBallMaster.bonusStage();
                } else {
                    IsoBallMaster.saveLevelProgress();
                }
                // Set Destination
                IsoBallMaster.setLevelEnd(true);
                if(warpToLevel != "") {
                    IsoBallMaster.setExit(warpToLevel);
                }
            }
        }

        public void TriggerExit() {
            triggert++;
            if(triggert >= triggerCount) {
                this.exitOpen = true;
            }
        }

        public void setExit(bool _exit) {
            this.exitOpen = _exit;
        }
    }
}
