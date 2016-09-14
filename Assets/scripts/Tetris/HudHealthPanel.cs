using UnityEngine;
using System.Collections;

namespace Tetris {
    public class HudHealthPanel : MonoBehaviour {

        public Transform[] heartList; // List this the UI Elements to Display
        public int actualLive = 2; // actualLive that is been Displayed


        void Start() {
            actualLive = heartList.Length;

        }

        //Reset the List ,set all to Active
//        public void setActive() {
//            for(int i = 0; i < heartList.Length; i++) {
//                heartList[i].gameObject.SetActive(true);
//            }
//        }

        // set the Live and Deactivate all Objects Above this Count
        public void setLive(int _value) {
            actualLive = _value;
            if(_value == heartList.Length) {
                //setActive();
            }
			if (actualLive > 0) {
				heartList [_value].gameObject.SetActive (false);
			}
            else {
                //Dead
				//Application.LoadLevel (0);
				Dying();
<<<<<<< HEAD
				Debug.Log ("Dying");
=======
>>>>>>> d32356b70e833ee7f6ff862f9079df96d820f2e4
            }
        }

        //Getter
        public int getLive() {
            return this.actualLive;
        }

		void Dying (){
			Application.LoadLevel (3);
			//Application.LoadLevel ("PauseMenu");

			}
//
//
//		void StartGame (){
//			Application.LoadLevel (0);
//		}
    }
}
