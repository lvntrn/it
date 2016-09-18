using UnityEngine;
using System.Collections;

namespace Tetris {
	
    public class TetrisPlayer : MonoBehaviour {

		private Rigidbody rb;
		public float speed;

		public bool  getHit = false;
		public bool  reduceLive = false;

		public int leftwall = 0;
		//public int wallsTillWin = 7;

		void Start () {
			rb = GetComponent<Rigidbody> ();
		}

		void FixedUpdate () {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (movement * speed);
		}

		void OnTriggerStay(Collider _other) {
			if (_other.gameObject.tag == "Obstacle") {
				Debug.Log("hit obstacle1");
				getHit = true;
				reduceLive = true;
			}				
		}

//		void OnTriggerEnter(Collider _other){
//			if (_other.gameObject.tag == "Wall") {
//				//leftwall++;
//				/*Debug.Log ("wand verlassen");*/
//				//Win ();
//			}
//		}

		void OnTriggerExit(Collider _other) {
			if(_other.gameObject.tag == "Wall") {
				/*Debug.Log("left wall");*/

				leftwall++;
				Debug.Log (leftwall);

				if (getHit == true) {
					/*Debug.Log ("no points");*/
					getHit = false;
					/*Debug.Log ("wand verlassen");*/
				} else {
					/*Debug.Log ("plus 10punkte");*/
					TetrisMaster.AddScore(10);
				}
				if (reduceLive == true) {
					/*Debug.Log ("reduce live");*/
					TetrisMaster.reduceLive(1);
					reduceLive = false;	
				}
				if (leftwall == 9) {
					Debug.Log ("Spiel gewonnen Hurra!");
					Win ();
				}					
			}
		}

		void Win() {
			Application.LoadLevel(4);
		}
      }				
    }


