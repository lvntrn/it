using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace IsoBall {
    public class SlotPanelHud : MonoBehaviour {

        public Image medalImage;
        public Text levelNummer;
        public Button button;
        public Image checkMark;
        public int medal;
        public bool bonusMark;
        public bool isBeaten;
        public bool interactable;    


        public void setText(int _nummer, bool _mark) {
            bonusMark = _mark;
            int sum = _nummer;
             sum += 1; 
            if(bonusMark) {
                levelNummer.text = "<b>"+_nummer.ToString() + "</b><size=26>B</size>";
            } else {
                levelNummer.text = "<b>"+sum.ToString()+"</b>";
            } 
        }

        public void setMedal(Color _color, int _medal) {
            medalImage.color = _color;
            medal = _medal;
        }

        public void setClickable(bool _value) {
            interactable = _value;
            button.interactable = interactable;
        }

        public void setIsBeaten(bool _value) {
            isBeaten = _value;
            checkMark.gameObject.SetActive(isBeaten);
        }
    }
}
