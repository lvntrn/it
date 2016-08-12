using UnityEngine;
using System.Collections;

namespace IsoBall {
    public class SpeedRail : MonoBehaviour {

        public Vector3 forceDirection;
        public ForceMode forceMode;
        public bool animateUV = true;
        public Vector2 UVOffset;
        public Transform particleLeavePrefab;
        public Vector3 particleOffset;

        private new Renderer renderer;
        private Vector2 UVPos = new Vector2(0f, 0f);
        private AudioSource audioSource;


        void Start() {
            renderer = GetComponent<Renderer>();
            audioSource = GetComponent<AudioSource>();
        }

        void Update() {
            //Animate UVs if True
            if(animateUV == true) {
                UVPos.x = Time.time * UVOffset.x;
                UVPos.y = Time.time * UVOffset.y;
                renderer.material.SetTextureOffset("_MainTex", new Vector2(UVPos.x, UVPos.y));
            }
        }

        void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "Player") {
                BallController _player = other.gameObject.GetComponent<BallController>() as BallController;
                //Add Force to Player
                _player.rb.velocity = new Vector3(0f, 0f, 0f);
                _player.rb.AddForce(forceDirection, forceMode);

            }
        }

        void OnTriggerExit(Collider other) {
            if(other.gameObject.tag == "Player") {
                audioSource.Play();
                //Add Particle Effects
                Transform _pickupParticleClone = Instantiate(particleLeavePrefab, transform.position + particleOffset, transform.rotation) as Transform;
                Destroy(_pickupParticleClone.gameObject, 3f);
            }
        }
    }
}
