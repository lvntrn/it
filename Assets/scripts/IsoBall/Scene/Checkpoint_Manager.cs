using UnityEngine;
using System.Collections;

namespace IsoBall {
    public class Checkpoint_Manager : MonoBehaviour {

        //This Class make sure its only One Checkpoint Enabled
        [Tooltip("Add Here Checkpoints as ChildObjects")]
        public Checkpoint[] checkpointList;
        [Tooltip("Add Here GlobalSpawnpoint to Use")]
        public Transform spawnPoint;
        [Tooltip("If SpawnPoint is Moving, need to set it to Parent")]
        public bool movingSpawnpoint;

        void Awake() {
            foreach(Checkpoint checkpoint in checkpointList) {
                checkpoint.setCpManager(this);
            }
        }

        public void setMovingSpawnpoint(bool value) {
            movingSpawnpoint = value;
        }

        public bool getMovingSpawnpoint() {
            return movingSpawnpoint;
        }

        public void DisableAllCheckpoints() {
            foreach(Checkpoint checkpoint in checkpointList) {
                checkpoint.setCheckpoint(false);
            }
        }
    }


}
