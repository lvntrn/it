using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace IsoBall {
    public class WinPanelHud : MonoBehaviour {

        public MedalStatus[] medalAnim;
        public Text[] medalTimesList;
        public Text massageText;
        public Text bonusText;
        public Text nextButtonText;
        public Text yourTime;

        private int[] medalStatus;
        private float[] medalTime;
        private bool isNewTime = false;
        private bool isBonus = false;
      

        void Awake() {
            medalStatus = new int[medalAnim.Length];
        }

        // Update medalAnim 
        void Update() {
            medalAnim[0].setStatus(medalStatus[0]);
            medalAnim[1].setStatus(medalStatus[1]);
        }

        // Set MedalAnim
        public void setMedalStatus(int _value,int _for) {
            if(_for >= 0 && _for <= medalStatus.Length) {
                medalStatus[_for] = _value;
            }       
        }

        //Set Bonus
        public void setBonus(bool _value) {
            isBonus = _value;
            if(isBonus == true) {
                bonusText.gameObject.SetActive(true);
                nextButtonText.text = "Bonus";
            } else {
                bonusText.gameObject.SetActive(false);
                nextButtonText.text = "Next";
            }
        }

        //Set Massage
        public void setMassage(bool _value) {
            isNewTime = _value;
            if(isNewTime == true) {
                massageText.gameObject.SetActive(true);
            } else {
                massageText.gameObject.SetActive(false);
            }
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
        public void setYourTime(float _time) {
            // Get Sec, Display at Min and Sec. (Cosmetic)
            int _min = Mathf.FloorToInt(_time / 60);
            float _sec = _time % 60;
            if(_min == 0) {
                yourTime.text = "= " + _sec.ToString("0.000") + " <size=16>sec</size>";
            } else {
                yourTime.text = "= " + _min.ToString("0") + " <size=16>min</size> " + _sec.ToString("0.000") + " <size=16>sec</size>";
            }
        }
    }
}
