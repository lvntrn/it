using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace IsoBall {
    public class StartMenuManager : MonoBehaviour {

        public AniMenuController currentMenu;

        public Text versionsText;
        public OptionManager optionsPanel;
        public SavePanelHud savePanel;
        public ScenePanelHud[] scenePanelList;


        public void Start() {
            //Open this Menu
            ShowMenu(this.currentMenu);
            // Get SaveData Slot
            SaveObject.Load(GameMaster.getSaveState());
            // Get Version and Displayit
            setVersion(GameMaster.getGameVersion());
            // Push all Savedata into Panels
            pushsaveData();
        }

        //get new Menu though Button Events ,close the old and Open the new
        public void ShowMenu(AniMenuController _menu) {
            if(currentMenu != null) {
                currentMenu.IsOpen = false;
            }
            currentMenu = _menu;
            currentMenu.IsOpen = true;
        }

        //Trigger the PushSaveData into Panels functions
        public void pushsaveData() {
            optionsPanel.refreshAudioPanel();
            savePanel.refreshAll();
            updateAllScenePanels();
        }
       
        //Trigger only a ButtonUpdate (Savestate change)
        public void pushOnlyButtons() {
            optionsPanel.refreshAudioPanel();
            savePanel.setNewState();
            updateAllScenePanels();
        }
    
        //Loadlevel for Gui warper
        public void LoadLevel(string _level) {
            SaveObject.Save();
            GameMaster.changeLevel(_level);
        }
    
        //Set the VersionsText in StartMenu
        public void setVersion(string _text) {
            versionsText.text = _text;
        }

        public void updateAllScenePanels() {
            foreach(ScenePanelHud scenePabel in scenePanelList) {
                scenePabel.updateList();
            }
        }
    }
}
