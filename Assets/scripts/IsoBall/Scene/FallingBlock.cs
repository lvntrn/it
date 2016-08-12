using UnityEngine;
using System.Collections;

namespace IsoBall {
    public class FallingBlock : MonoBehaviour {


        public string contactTag;
        public bool useGravity = false;
        public Vector3 forceToApply;
        public ForceMode forceMode;
        public AudioClip[] clip;
        
        private Rigidbody rb;
        private bool isTriggered;

        void Awake() {
            rb = GetComponent<Rigidbody>();
        }
        void Start() {
            rb.useGravity = useGravity;
            rb.Sleep();
        }

        void FixedUpdate() {
            if(isTriggered) {
                if(useGravity) {

                } else {
                    rb.AddForce(forceToApply,forceMode);
                }   
            }
        }

        public void playSound(bool _ignoretrigger) {
            if(_ignoretrigger) {
                int _random = Random.Range(0, clip.Length);
                IsoBallMaster.PlayClipAt(clip[_random], transform.position, 0.45f, Random.Range(0.3f, 0.7f));
            } else {
                if(!isTriggered) {
                    int _random = Random.Range(0, clip.Length);
                    IsoBallMaster.PlayClipAt(clip[_random], transform.position, 0.8f, Random.Range(0.9f, 1.1f));
                }
            }
        }

        public void OnCollisionEnter(Collision _coll) {
            if(_coll.gameObject.tag == contactTag) {
                rb.isKinematic = false;
                playSound(false);
                isTriggered = true;
            }
            if(_coll.gameObject.tag == "Obstacle") {
                playSound(true);
            }
        }
    }

}
