using UnityEngine;
using System.Collections;

namespace IsoBall{
	public class Teleporter : MonoBehaviour {

        [Header("Teleporter Location NullObject")]
        public Transform teleporterNull;
        [Header("Particle Effects")]
        public Transform particleEffect;
        public Transform fxMesh;
        public Vector3 effectOffset;

		private PlayerBall curPlayer;
		private bool active = false;
        private bool once = false;
        private bool once2 = false;
        private AudioSource asOrigin;
        private AudioSource asDestination;


        void Start () {
            asOrigin = GetComponent<AudioSource>();
            asDestination = teleporterNull.GetComponent<AudioSource>();
        }
		
		void Update () {

			if (active == true){

                // If Player is Teleporting to this
				curPlayer.pControl.rb.velocity = new Vector3(0f,0f,0f);
				curPlayer.pControl.rb.Sleep();
                // Add Starteffects
                if (once == false) {
                    IsoBallMaster.SpawnEffects(gameObject.transform.position + effectOffset, particleEffect, 3.5f);
                    IsoBallMaster.SpawnEffects(gameObject.transform.position + effectOffset, fxMesh, 3.5f);
                    asOrigin.Play();
                    once = true;
                }
            
                // If Player get is Shrinked make this additionaly
				if(curPlayer.getShrinked() == true){
                    // Set Players new Position
					curPlayer.gameObject.transform.position = teleporterNull.transform.position;
                    //Add Effects Once
                    if (once2 == false) {
                        IsoBallMaster.SpawnEffects(teleporterNull.transform.position, particleEffect, 3.5f);
                        IsoBallMaster.SpawnEffects(teleporterNull.transform.position, fxMesh, 3.5f);
                        asDestination.Play();
                        once2 = true;
                    }
                    // Scale the Player Up
                    curPlayer.scalePlayer();
				}
                // if Player is normalsize reset this Process
                if(curPlayer.getShrinked() == false && curPlayer.getIsScaling() == false){
                    curPlayer.pControl.setEnable(true);
                    curPlayer.pControl.rb.WakeUp();
					active = false;
                    once = false;
                    once2 = false;
				}
			}
		}

        //Player Enters Trigger
		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag == "Player") 
			{
				curPlayer = other.gameObject.GetComponent<PlayerBall>() as PlayerBall;
                //Activate Scaling
                curPlayer.scalePlayer();
                //Disable Controls
                curPlayer.pControl.setEnable(false);
                //Start Teleporting
                active = true;
			}
		}
	}
}
