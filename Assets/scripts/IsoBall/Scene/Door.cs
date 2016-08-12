using UnityEngine;
using System.Collections;

namespace IsoBall {
    public class Door : MonoBehaviour {

        //PositionStorage
        public Vector3 isOpenPos;
        public iTween.EaseType easeType;
        public float transitionTime = 0.5f;

        private bool isOpen = false;

        [HideInInspector]
        public Vector3 startPos;

        void Start() {
            this.startPos = this.transform.position;
        }

        //Animation
        public void TriggerDoor() {
            if(isOpen == true) {
                iTween.MoveTo(this.gameObject, iTween.Hash("islocal", true, "x", 0f, "y", 0f, "z", 0f, "easeType", easeType, "time", transitionTime));
                isOpen = false;
            } else {
                iTween.MoveTo(this.gameObject, iTween.Hash("islocal", true, "x", isOpenPos.x, "y", isOpenPos.y, "z", isOpenPos.z, "easeType", easeType, "time", transitionTime));
                isOpen = true;
            }
        }
    }
}
