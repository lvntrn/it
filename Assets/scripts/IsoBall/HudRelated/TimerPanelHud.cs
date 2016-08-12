using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace IsoBall {
    public class TimerPanelHud : MonoBehaviour {

        [Header("Reference to Text")]
        public Text timeMin;
        public Text timeSec;

        [Header("Animation Related")]
        [Tooltip("Enable Animation")]
        public bool changeColor = true;
        [Tooltip("0.No Medal 1.Gold 2.Silver 3.Bronze 4.Failure")]
        public Color[] medalColor;
        [Tooltip("TransitionTime between States")]
        public float transitionTime = 2f;
        [Tooltip("GoldmedalTime * this, to start Anim")]
        public float startDelayMulti = 0.33f;
        [Tooltip("BronzemedalTime * this, to start last Color")]
        public float lastMedalMulti = 2f;

        private float startDelay = 0f;
        private float rawTime;
        private int min = 0;
        private float sec = 0f;
        private int medalValue = 0;
        private float lastMedalTime;

        void Start() {
            timeMin.text = "0";
            timeSec.text = "0.<size=56>0</size><size=52>0</size><size=48>0</size>";
            float _goldmedal = IsoBallMaster.isoBallMaster.medalTime[0];
            float _bronzeMedal = IsoBallMaster.isoBallMaster.medalTime[2];
            startDelay = _goldmedal * startDelayMulti;
            lastMedalTime = _bronzeMedal * lastMedalMulti;
        }

        void Update() {
            //Get Actual Medal and Transistion to Color
            if(changeColor) {
                if(sec > startDelay) {
                    if(rawTime < lastMedalTime) {
                        medalValue = IsoBallMaster.isoBallMaster.calcMedal(rawTime);
                    } else {
                        medalValue = 4;
                    }
                    // Animate
                    iTween.ColorUpdate(timeMin.gameObject, iTween.Hash("color", medalColor[medalValue], "time", transitionTime));
                    iTween.ColorUpdate(timeSec.gameObject, iTween.Hash("color", medalColor[medalValue], "time", transitionTime));
                } 
            }
        }

        public void calcTimePanel(float _actualTime) {
            rawTime = _actualTime;

            min = Mathf.FloorToInt(rawTime / 60);
            sec = rawTime % 60;

            if(min >= 1) {
                timeMin.gameObject.SetActive(true);
                timeMin.text = min.ToString("0");
            }
            timeSec.text = sec.ToString("0.<size=56>0</size><size=52>0</size><size=48>0</size>");
        }
    }
}
