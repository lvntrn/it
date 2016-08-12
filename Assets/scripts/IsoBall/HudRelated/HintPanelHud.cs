using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace IsoBall {
    public class HintPanelHud : MonoBehaviour {

        public Text hintText;
        public IsoGameHud manager;
        public AniMenuController hintAnim;

        private bool isTutorial;
        private bool isBeaten;
        private int level;
        private bool enable = true;
        private bool isTriggered;


        void Start() {
            isTutorial = IsoBallMaster.getTutorialStatus();
            level = IsoBallMaster.isoBallMaster.level;

            switch(level) {
                case 0:
                    hintText.text = "Rotate Camera with <size=16> (Default) </size> <b>Q E</b> and <b>Y X</b> \n Zoom Camera with <size=16> (Default) </size> <b>R</b> and <b>F</b>";
                    break;
                case 1:
                    hintText.text = "Collect <b>all</b> Crystals to Open the Exit";
                    break;
                case 2:
                    hintText.text = "Use Checkpoints to Respawn \n Try Jumping with <size=16> (Default) </size> <b>Space</b>";
                    break;
                case 3:
                    hintText.text = "Use <size=16> (Default) </size> <b>Tab</b> to \n Restart the Level";
                    break;
                case 4:
                    hintText.text = "This <b>Symbol</b> means, <b>NoRespawn</b>";
                    break;
            }
        }

        public void Update() {
        }

        public void EnterTrigger() {

            if(enable) {
                if(isTutorial && !isBeaten) {
                    isTriggered = true;
                    hintAnim.IsOpen = isTriggered;
                } else if(!isTutorial) {
                    isTriggered = true;
                    hintAnim.IsOpen = isTriggered;
                }
            }
        }

        public void StayTrigger() {
           // isTriggered = true;
        }

        public void ExitTrigger() {
            if(isTutorial && !isBeaten) {
                isTriggered = false;
                hintAnim.IsOpen = isTriggered;
            } else if(!isTutorial) {
                isTriggered = false;
                hintAnim.IsOpen = isTriggered;
            }
        }

        public void setIsBeaten(bool _value) {
            isBeaten = _value;
        }

        public void setCrystalCount(int _score) {
            if(level == 1) {
                if(_score >= 1) {
                    enable = false;
                }
            }
        }

        void OnGUI() {
            if(IsoBallMaster.isoBallMaster.debugMode == true) {
                GUI.Label(new Rect(Screen.width - 150, 230, 300, 20), "IsTutorialLevel = " + isTutorial);
                GUI.Label(new Rect(Screen.width - 150, 250, 300, 20), "IsBeaten = " + isBeaten);
                GUI.Label(new Rect(Screen.width - 150, 270, 300, 20), "IsNoRespawn = ");
            }
        }

    }
}
