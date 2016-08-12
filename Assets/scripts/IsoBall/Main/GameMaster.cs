using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace IsoBall {
    public class GameMaster : MonoBehaviour {
        //Contain some Static Methodes so Manipulate and Play Music though game

        //Create a Static instance of this Class
        public static GameMaster gameMaster;
        [Header("Global AudioRelated")]
        public AudioClip[] audioClip; //AudioClip Array  Add generic Soundfiles here
        public AudioClip[] musicClip; //MusicClip Array  Add generic Musicfiles here
        public AudioMixer mixer; // The Main Audio Mixer

        [Header("GameReladed")]
        [Tooltip("This Game Version to Display in Canvas")]
        public string gameVersion;
        [Tooltip("This Scene Name")]
        public string startScene = "StartScreen";
        [Tooltip("The SaveState to Load first (temp)")]
        public string saveState;  // Note this will update itself

        [HideInInspector]
        public AudioSource musicSource; //Main MusicPlayer Source

        void Awake() {
            //Make this Static and Dont DestroyOnLoad
            //So this Class when Play though this Scene will Carry over and still Avabile in Game
            //Dont try even reference non static things in other scenes, it will u make cry
            if(gameMaster == null) {
                DontDestroyOnLoad(gameObject);
                gameMaster = this;
            } else if(gameMaster != this) {
                Destroy(gameObject);
            }

            //Make Sure initialize a SaveObject when nothing was there
            GameObject _saveObject = GameObject.FindGameObjectWithTag("SaveObject");
            if(_saveObject == null) {
                SaveObject.Initialize("SaveObject");
            }

            //Set musicSource
            this.musicSource = GetComponent<AudioSource>();
            //Get Actual last SaveState in Save to Load
            saveState = SaveObject.Get<SaveData>().actualSaveSlot;
            SaveObject.Load(saveState);

            //Set Time Running (Obsolete)
            Time.timeScale = 1;
        }

        // If Game was Closed
        public void Exit() {
            SaveObject.Save();
            Application.Quit();
        }

        //Load a Savegame with Stringinput
        public static void setSaveState(string _state) {
            gameMaster.saveState = _state;
            SaveObject.Load(gameMaster.saveState);
            SaveObject.Get<SaveData>().actualSaveSlot = gameMaster.saveState;
            SaveObject.Save();
        }

        //Delete a SaveGame and reset is to Default values
        public static void clearSaveData(string _state) {
            SaveObject.Load(_state);
            SaveObject.NewGame();
            SaveObject.Save();
        }

        //get the Actual SaveGame
        public static string getSaveState() {
            return gameMaster.saveState;
        }

        //Some Static Calculations
        // Get the Max WorldSaveArray
        public static int calcMaxWorlds() {
            int _count = SaveObject.Get<SaveData>().world.Length;
            return _count;
        }

        public static int calcAllMaxLevels() {
            int _maxCount = 0;
            for(int i = 0; i < calcMaxWorlds(); i++) {
                _maxCount += calcMaxLevels(i);
            }
            return _maxCount;
        }

        // Get the Max LevelSaveArray
        public static int calcMaxLevels(int _world) {
            int _count = SaveObject.Get<SaveData>().world[_world].level.Length;
            return _count;
        }

        //Get the Medal int from type (Gold 1,Silver 2,Bronze 3) from the spezific Savegame
        public static int calcAllMedals(int _typeOf, string _saveState) { // ToDo: add world addition
            int _maxMedal = 0;
            string _lastState = SaveObject.saveName;
            SaveObject.Load(_saveState);
            for(int i = 0; i < calcMaxWorlds(); i++) {
                for(int j = 0; j < calcMaxLevels(i); j++) {
                    int _medal = SaveObject.Get<SaveData>().world[i].level[j].medal;
                    if(_medal == _typeOf) {
                        _maxMedal++;
                    }
                }
            }
            SaveObject.Load(_lastState);
            return _maxMedal;
        }

        //Get Gameprocess in Procent from the spezific Savegame
        public static float calcGameProgress(string _saveState) {
            float _goldMedals = 0;
            float _isBeaten = 0;
            string _lastState = SaveObject.saveName;
            SaveObject.Load(_saveState);
            for(int i = 0; i < calcMaxWorlds(); i++) {
                for(int j = 0; j < calcMaxLevels(i); j++) {
                    float _medal = SaveObject.Get<SaveData>().world[i].level[j].medal;
                    bool _beat = SaveObject.Get<SaveData>().world[i].level[j].isBeaten;
                    if(_medal == 1) {
                        _goldMedals++;
                    }
                    if(_beat == true) {
                        _isBeaten++;
                    }
                }
            }

            float _maxlevels = calcAllMaxLevels();
            float _progress = ((_goldMedals / _maxlevels) + (_isBeaten / _maxlevels)) / 2;
            SaveObject.Load(_lastState);
            return _progress;
        }

        // Get if this level is a bonuslevel from Save
        public static bool getBonusMark(int _world, int _level) {
            bool _bonus = SaveObject.Get<SaveData>().world[_world].level[_level].bonus;
            return _bonus;
        }

        //Get the actual Scenename for this level from Save
        public static string getSceneName(int _world, int _level) {
            string _sceneName = SaveObject.Get<SaveData>().world[_world].level[_level].sceneName;
            return _sceneName;
        }

        //Get the actual Medal from this level from Save
        public static int getMedal(int _world, int _level) {
            int _medal = SaveObject.Get<SaveData>().world[_world].level[_level].medal;
            return _medal;
        }

        //Get if this level was beaten befor from Save
        public static bool getIsBeaten(int _world, int _level) {
            bool _isBeaten = SaveObject.Get<SaveData>().world[_world].level[_level].isBeaten;
            return _isBeaten;
        }

        //Get the Max Bonuslevel Count from Save
        public static int calcMaxBonusLevel(int _world) {
            int _maxCount = calcMaxLevels(_world);
            int _bonus = 0;
            for(int i = 0; i < _maxCount; i++) {
                if(getBonusMark(_world, i) == true) {
                    _bonus++;
                }
            }
            return _bonus;
        }

        //Figure out if the last playable level ,excluding bonuslevels;
        public static int getLastPlayableLevel(int _world) {
            int _maxCount = calcMaxLevels(_world);
            int _bonuslevel = calcMaxBonusLevel(_world);
            int _onlyNormalCount = _maxCount - _bonuslevel;
            int _level = 0;

            for(int i = 0; i < _onlyNormalCount; i++) {
                if(getIsBeaten(_world, i) == true && getBonusMark(_world, i) == false) {
                    _level++;
                }
            }
            return _level;
        }

        //Getter Setter to Drive the Mixer
        public static void setMusicVol(float _volume) {
            SaveObject.Get<SaveData>().musicVolume = _volume;
            gameMaster.mixer.SetFloat("musicVol", _volume);
        }

        public static void setSoundVol(float _volume) {
            SaveObject.Get<SaveData>().soundVolume = _volume;
            gameMaster.mixer.SetFloat("soundVol", _volume);
        }

        public static void muteSound(bool _value) {
            if(_value == false)
                gameMaster.mixer.SetFloat("soundVol", -80f);
        }

        public static void muteMusic(bool _value) {
            if(_value == false)
                gameMaster.mixer.SetFloat("musicVol", -80f);
        }

        //Set the Gamemusic, drived bei Scene IsoBallmaster Default
        public static void setMusic(int _number) {
            gameMaster.musicSource.clip = gameMaster.musicClip[_number];
            if(gameMaster.musicSource.isActiveAndEnabled) {
                if(!gameMaster.musicSource.isPlaying) {
                    gameMaster.musicSource.Play();
                }
            }
        }

        //Static Calls 
        public static void changeLevel(string _level) {
            SceneManager.LoadScene(_level);
        }

        //Setter and Getter
        public static string getGameVersion() {
            return gameMaster.gameVersion;
        }
    }
}
