using UnityEngine;
using System.Collections;

namespace IsoBall {
    public class Pickup : MonoBehaviour {

        public int score = 1;
        public Transform particleEffect;
        public AudioClip pickUpSound;


        void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "Player") {
                //If Player do this
                IsoBallMaster.AddScore(score);
                IsoBallMaster.PlayClipAt(pickUpSound, transform.position,1.1f,1f);
                IsoBallMaster.DestroyPickup(GetComponent<Pickup>());
            }
        }
    }
}
