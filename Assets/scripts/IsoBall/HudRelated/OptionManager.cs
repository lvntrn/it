using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace IsoBall {
    public class OptionManager : MonoBehaviour {

        public StartMenuManager manager;

        public Slider soundSlider;
        public Slider musicSlider;
        public Toggle soundToggle;
        public Toggle musicToggle;

        void Update() {
            if(musicSlider.value <= musicSlider.minValue) {
                musicToggle.isOn = false;
            } else {
                musicToggle.isOn = true;
            }

            if(soundSlider.value <= soundSlider.minValue) {
                soundToggle.isOn = false;
            } else {
                soundToggle.isOn = true;
            }
        }

        public void setSoundVolume(float _volume) {
            GameMaster.setSoundVol(_volume);
        }

        public void setMusicVolume(float _volume) {
            GameMaster.setMusicVol(_volume);
        }

        public void muteSound(bool _value) {
            GameMaster.muteSound(_value);
        }
        public void muteMusic(bool _value) {
            GameMaster.muteMusic(_value);
        }

        //Call on Refresh
        public void refreshAudioPanel() {
            soundSlider.value = SaveObject.Get<SaveData>().soundVolume;
            musicSlider.value = SaveObject.Get<SaveData>().musicVolume;
            GameMaster.gameMaster.mixer.SetFloat("soundVol", soundSlider.value);
            GameMaster.gameMaster.mixer.SetFloat("musicVol", musicSlider.value);

        }
    }
}
