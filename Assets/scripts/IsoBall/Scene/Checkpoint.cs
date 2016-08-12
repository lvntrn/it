using UnityEngine;
using System.Collections;

namespace IsoBall {
    public class Checkpoint : MonoBehaviour {

        [Tooltip("Is Checkpoint Active")]
        public bool CheckPointActive;
        [Tooltip("Spawnpoint Correction Offset")]
        public Vector3 spawnOffset = new Vector3(0f,0.7f,0f);
        private Checkpoint_Manager cpMananger;

        private Animator checkPointAni;
        private AudioSource audioSource;
      
        private Transform spawnpoint;
        private bool movingCheckpoint;


        void Awake() {
            checkPointAni = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }


        public void setCpManager(Checkpoint_Manager _manager) {
            cpMananger = _manager;
        }


        void Start() {
            spawnpoint = cpMananger.spawnPoint;
            movingCheckpoint = cpMananger.getMovingSpawnpoint();
        }

        void Update() {
            //Link Animation Parameter
            checkPointAni.SetBool("CheckPointActive", CheckPointActive);
        }

        //Open the Checkpoint if Player Touches it, Set new Spawnpoint
        void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "Player" && CheckPointActive == false) {
                cpMananger.DisableAllCheckpoints();
                CheckPointActive = true;
                audioSource.Play();
                if(movingCheckpoint) {
                    spawnpoint.SetParent(gameObject.transform);
                    spawnpoint.position = this.transform.position + spawnOffset;
                } else {
                    spawnpoint.position = this.transform.position + spawnOffset;
                }   
            }
        }

        public void setCheckpoint(bool _active) {
            this.CheckPointActive = _active;
        }

        public bool getCheckpoint() {
            return this.CheckPointActive;
        }
    }
}
