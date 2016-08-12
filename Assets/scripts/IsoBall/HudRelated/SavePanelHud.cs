using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace IsoBall {
    public class SavePanelHud : MonoBehaviour {

        public StartMenuManager manager;
        [Tooltip("Add here the different Slots")]
        public SaveSlotManager[] saveSlot;
        [Tooltip("the Savenames that are where using")]
        public string[] saveStateName;
        [Tooltip("Buttons to turn The Slots active")]
        public Button[] buttonList;
        [Tooltip("Reference to Resetpanel")]
        public ResetAcceptPanelHud acceptPanel;

        private string actualSlot;

        // Use this for initialization
        void Start() {
            if(saveSlot.Length != saveStateName.Length) {
                Debug.Log("Make sure u have saveState Assign");
            }
        }

        //Get the Actual SaveState
        public int getActualSaveState() {
            int _acutalState = saveStringToInt();
            return _acutalState;
        }

        //Controls with PanelState Buttons are shown Active or Deactive
        public void setPanelsActive(int _activePanel) {
            int _slot = _activePanel;
            for(int i = 0; i < saveSlot.Length; i++) {
                if(i == _slot) {
                    saveSlot[i].setActive(true);
                } else {
                    saveSlot[i].setActive(false);
                }
            }
        }

        //Return the according int from the SaveGame String
        public int saveStringToInt() {
            actualSlot = SaveObject.saveName;
            for(int i = 0; i < saveStateName.Length; i++) {
                if(actualSlot == saveStateName[i]) {
                    return i;
                }
            }
            return default(int);
        }

        //What happens when the a SavePanel Slot Button is Pressed
        public void ButtonPressed(SaveSlotManager _saveManager) {
            int _slot = _saveManager.getSlot();
            GameMaster.setSaveState(saveStateName[_slot]);
            manager.pushOnlyButtons();
            acceptPanel.setStatus(false);
        }
        //What happens if the DelButton is Pressed
        public void ButtonDelPressed(SaveSlotManager _saveManager) {
            int _slot = _saveManager.getSlot();
            acceptPanel.setSlot(saveStateName[_slot]);
            acceptPanel.setStatus(true);
        }
        //Reload all PanelData
        public void refreshAll() {
            setPanelsActive(getActualSaveState());
            calcMedalCount();
            calcProgress();
            setTextDisc();
        }
        //Set a New SaveState
        public void setNewState() {
            setPanelsActive(getActualSaveState());
        }
        //Reload only the Medal and ProgressBars
        public void reloadData() {
            calcMedalCount();
            calcProgress();
            setTextDisc();
        }
        // Setter for Progresspanel
        public void calcProgress() {
            for(int i = 0; i < saveSlot.Length; i++) {
                float _value = GameMaster.calcGameProgress(saveStateName[i]);
                saveSlot[i].setProgress(_value);
            }
        }
        // Setter fror Medalpanel
        public void calcMedalCount() {
            for(int i = 0; i < saveSlot.Length; i++) {
                for(int j = 0; j < saveSlot[i].GetComponent<SaveSlotManager>().medalText.Length; j++) {
                        int _value = GameMaster.calcAllMedals(j + 1, saveStateName[i]); 
                        saveSlot[i].setMedalText(j, _value);
                }
            }
        }

        // Set the AcceptPanel back if its open
        public void BackButton(AniMenuController _menuTo) {
            if(acceptPanel.getIsOpen()) {
                acceptPanel.Back();
            }
            manager.ShowMenu(_menuTo);
        }

        //Set discriptionText from Save
        void setTextDisc() {
            for(int i = 0; i < saveSlot.Length; i++) {
                string _discription = SaveObject.Get<SaveData>().saveState[i].saveDiscription;
                saveSlot[i].setTextDisc(_discription);
            }
        }

    }
}
