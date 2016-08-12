using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace IsoBall {
    public class SaveSlotManager : MonoBehaviour {

        public StartMenuManager manager;

        [Tooltip("Reference to MedalCountText")]
        public Text[] medalText;
        [Tooltip("Reference to MedalImage")]
        public Image[] medalImage;
        [Tooltip("Reference to ProcessSlider")]
        public Slider process;
        [Tooltip("with nummer this SaveSlot have")]
        public int slotNummer;
        [Tooltip("User can name this Text")]
        public string discription;
        [Tooltip("is this Slot Active?")]
        public bool isActive;
        [Tooltip("Reference to Inputfield")]
        public InputField discriptionText;
        [Tooltip("Reference to the Animator")]
        public Animator buttonAnim;

        private Text button;

        void Awake() {
            button = buttonAnim.gameObject.GetComponentInChildren<Text>();
        }

        public void setMedalText(int _for, int _count) {
            // Implement this while 0 is similar to 8
            if(_count == 0) {
                medalText[_for].text = "";
                medalImage[_for].gameObject.SetActive(false);
               // Color _actualColor = medalText[_for].color;
               // _actualColor.a = 0.1f;
               // medalImage[_for].color = _actualColor;
            } else {
                medalText[_for].text = _count.ToString("0");
            } 
        }

        public void setProgress(float _value) {
            process.value = _value;
        }

        public int getSlot() {
            return slotNummer;
        }

        public void setActive(bool _value) {
            isActive = _value;
            if(isActive) {
                buttonAnim.SetTrigger("Pressed");
                button.text = "Enabled";
            } else {
                buttonAnim.SetTrigger("Disabled");
                button.text = "Disabled";
            }

        }
       
        // Read Text from Textfield
        public void readTextEnd(string _string) {
            discription = _string;
            SaveObject.Get<SaveData>().saveState[slotNummer].saveDiscription = discription;
            SaveObject.Save();
        }
        // Set Text from Textfield
        public void setTextDisc(string _string) {
            discription = _string;
            discriptionText.text = discription;
        }

    }

}
