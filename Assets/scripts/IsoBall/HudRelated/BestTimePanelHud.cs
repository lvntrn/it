using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace IsoBall {
    public class BestTimePanelHud : MonoBehaviour {

        public MedalStatus medalAnim;
        public Text yourBestTime;
        public RectTransform permInfo;

        private bool isNoRespawn;
        private int medalStatus;

        void Start() {
            isNoRespawn = IsoBallMaster.getNoRespawn();
            setPermInfo(isNoRespawn);
        }

        // Update medalAnim 
        void Update() {
            medalAnim.setStatus(medalStatus);
        }

        // Set MedalAnim
        public void setMedalStatus(int _value) {
            medalStatus = _value;
        }

        // Set Actual Time
        public void setYourBestTime(float _time) {
            // Get Sec, Display at Min and Sec. (Cosmetic)
            int _min = Mathf.FloorToInt(_time / 60);
            float _sec = _time % 60;
            if(_time == 0) {
                yourBestTime.text = "<size=24>No Best Time</size>";
            } else if(_min == 0) {
                yourBestTime.text = "Best Time: \n" + _sec.ToString("<size=28>0.000</size>") + " <size=12>sec</size>";
            } else {
                yourBestTime.text = "Best Time: \n" + _min.ToString("<size=28>0</size>") + " <size=12>min</size> " + _sec.ToString(
                    "<size=28>0.000</size>") + "  <size=12>sec</size>";
            }
        }

        void setPermInfo(bool _value) {
            permInfo.gameObject.SetActive(_value);
        }
    }
}
