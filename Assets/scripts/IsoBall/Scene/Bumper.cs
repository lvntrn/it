using UnityEngine;
using System.Collections;

namespace IsoBall {
    public class Bumper : MonoBehaviour {

        public Animation anim; // Reference to Animation
        public Vector3 forceMult;  // ForceMultiplayer
        public ForceMode forceType;  // Type of Force

        private AudioSource audioSource;


        void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        public void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "Player") {
                PlayerBall player = other.GetComponent<PlayerBall>();
                // Calculate the DirectionVector between this Object and the Player
                Vector3 _forceDir = player.gameObject.transform.position - transform.position;
                _forceDir.Normalize();
                _forceDir.Scale(forceMult);
                player.pControl.rb.velocity = Vector3.zero;
                player.pControl.rb.AddForce(_forceDir, forceType);
                anim.Play();
            }
        }

        public void OnTriggerExit(Collider other) {
            if(other.gameObject.tag == "Player") {
                audioSource.pitch = Random.Range(0.92f, 1.08f);
                audioSource.Play();

            }
        }
    }
}
