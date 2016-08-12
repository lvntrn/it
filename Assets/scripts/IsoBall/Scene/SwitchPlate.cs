using UnityEngine;
using System.Collections;

namespace IsoBall {
    public class SwitchPlate : MonoBehaviour {


        public Transform[] targetList;         //Add Here Targets to Activate
        public Vector3 sinkpos;                //How far the Object Moves (down) if Pressed
        public float transitionTime = 0.5f;    //How fast is move Down
        public iTween.EaseType easeType;
        public bool stayActive = false;        //Only Once Trigger
        public AudioClip[] audioClipList;

        private bool isActive = false;
        private AudioSource audioSource;

        void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        public void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "Player") {
                if(isActive == false) {
                    //Set AudioClip and Play
                    audioSource.clip = audioClipList[0];
                    audioSource.Play();
                    //Animate this Plate itself
                    iTween.MoveTo(this.gameObject, iTween.Hash("islocal", true, "x", sinkpos.x, "y", sinkpos.y, "z", sinkpos.z, "easeType", easeType, "time", transitionTime));

                    //Make this for each Transform in TargetList
                    foreach(Transform transform in targetList) {
                        //Set here Parameters for other Triggerable Objects
                        if(transform.gameObject.tag == "Door") {
                            // If its a Door ,Trigger it
                            Door _door = transform.gameObject.GetComponent<Door>();
                            _door.TriggerDoor();
                        }
                        if(transform.gameObject.tag == "Exit") {
                            // If its a Exit ,Trigger it
                            Exit _exit = transform.gameObject.GetComponent<Exit>();
                            _exit.TriggerExit();
                        }
                    }
                    if(stayActive == true) {
                        isActive = true;
                    }
                }
            }
        }

        public void OnTriggerExit(Collider other) {
            if(other.gameObject.tag == "Player") {
                if(stayActive == false) {
                    //Set AudioClip and Play
                    audioSource.clip = audioClipList[1];
                    audioSource.Play();
                    //MoveBack to Local 0;
                    iTween.MoveTo(this.gameObject, iTween.Hash("islocal", true, "x", 0f, "y", 0f, "z", 0f, "easeType", easeType, "time", transitionTime));
                }
            }
        }
    }
}
