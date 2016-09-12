using UnityEngine;
using System.Collections;

namespace Tetris {
    public class HudHealthPanel : MonoBehaviour {

        public Transform[] heartList; // List this the UI Elements to Display
        public int actualLive = 3; // actualLive that is been Displayed


        void Start() {
            actualLive = heartList.Length;

        }

        //Reset the List ,set all to Active
        public void setActive() {
            for(int i = 0; i < heartList.Length; i++) {
                heartList[i].gameObject.SetActive(true);
            }
        }

        // set the Live and Deactivate all Objects Above this Count
        public void setLive(int _value) {
            actualLive = _value;
            if(_value == heartList.Length) {
                setActive();
            }
            if(actualLive >= 0)
                heartList[_value].gameObject.SetActive(false);
            else {
                //Dead
				//Application.LoadLevel (0);
				Dying();

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
