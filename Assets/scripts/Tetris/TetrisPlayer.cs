using UnityEngine;
using System.Collections;

namespace Tetris {
    public class TetrisPlayer : MonoBehaviour {

		public bool  getHit = false;
		public bool  reduceLive = false;

		public int leftwall = 0;
		//public int wallsTillWin = 7;

		void OnTriggerStay(Collider _other) {
			if (_other.gameObject.tag == "Obstacle") {
				Debug.Log("hit obstacle1");
				getHit = true;
				reduceLive = true;
			}

		}

		void OnTriggerEnter(Collider _other){
			if (_other.gameObject.tag == "Wall") {
				//leftwall++;
				/*Debug.Log ("wand verlassen");*/
				//Win ();
			}
		}

//		void Win(){
//		if (leftwall == wallsTillWin) {
//			Debug.Log ("Spiel gewonnen Hurra!");
//		}
//		}

		void OnTriggerExit(Collider _other)
		{

			if(_other.gameObject.tag == "Wall") {
				/*Debug.Log("left wall");*/

				leftwall++;
				Debug.Log (leftwall);

				if (getHit == true) {
					/*Debug.Log ("no points");*/
					getHit = false;
					/*Debug.Log ("wand verlassen");*/
				} 
				else {
					/*Debug.Log ("plus 10punkte");*/
					TetrisMaster.AddScore(10);

				}
				if (reduceLive == true) {
					/*Debug.Log ("reduce live");*/
					TetrisMaster.reduceLive(1);
					reduceLive = false;	

				}
				if (leftwall == 7) {
					Debug.Log ("Spiel gewonnen Hurra!");
				}
					

			}

		}

	
    }
}

