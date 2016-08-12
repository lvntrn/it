using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace IsoBall {
    public class PausePanelHud : MonoBehaviour {

        public MedalStatus medalAnim;
        public Text[] medalTimesList;
        public Button nextButton;
        public Text yourBestTime;

        private int medalStatus;
        private float[] medalTime;
        private bool isBeaten;

        // Update medalAnim 
        void Update() {
            medalAnim.setStatus(medalStatus);
        }

        // Set MedalAnim
        public void setMedalStatus(int _value) {
                medalStatus = _value;       
        }

        //Set Acutal Beat Times
        public void setMedalTime(float[] _array) {
            //Check if its in right Format
            if(_array.Length >= 0 && _array.Length > 2) {
                medalTime = _array;
            }

            //Make some Calculations
            int[] _min = new int[3];
            float[] _sec = new float[3];
            for(int i = 0; i < medalTime.Length; i++) {
                _min[i] = Mathf.FloorToInt(medalTime[i] / 60);
                _sec[i] = medalTime[i] % 60;
            }
            
            //Push in the Right Textflieds
            for(int i = 0; i < medalTimesList.Length; i++) {
                if(_min[i] == 0) {
                    medalTimesList[i].text = "< " + _sec[i].ToString("0.000") + " <size=16>sec</size>";
                } else {
                    medalTimesList[i].text = "< " + _min[i].ToString("0") + " <size=16>min</size> " + _sec[i].ToString("0.000") + " <size=16>sec</size>";
                }
            }    
        }

        // Set Actual Time
        public void setYourBestTime(float _time) {
            // Get Sec, Display at Min and Sec. (Cosmetic)
            int _min = Mathf.FloorToInt(_time / 60);
            float _sec = _time % 60;
            if(_time == 0) {
                yourBestTime.text = "No Best Time";
            } else if(_min == 0) {
                yourBestTime.text = "Best Time: \n" + _sec.ToString("0.000") + " <size=16>sec</size>";
            } else {
                yourBestTime.text = "Best Time: \n" + _min.ToString("0") + " <size=16>min</size> " + _sec.ToString("0.000") + " <size=16>sec</size>";
            }
        }

        // Check if Travel to Next Scene is Possible
        public void setNextButton(bool _value) {
            isBeaten = _value;
            if(isBeaten == false) {
                nextButton.interactable = false;
            } else {
                nextButton.interactable = true;
            }
        }
    }
}
