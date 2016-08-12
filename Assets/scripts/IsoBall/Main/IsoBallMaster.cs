using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace IsoBall {
    public class IsoBallMaster : MonoBehaviour {

        //Create a Static instance of this Class
        public static IsoBallMaster isoBallMaster;

        [Header("Actual unique Level Info")]
        [Tooltip("Displayed LevelName in GUI")]
        public string levelDiscText;
        [Tooltip("Actual Scene ID (for Restart)")]
        public string sceneID;
        [Tooltip("Actual Level ID in Save")]
        public int level;
        [Tooltip("ActualWorld")]
        public int world;
        [Tooltip("Actual CrystallCount to Open Exit")]
        public int crystalCount;
        [Tooltip("0.Gold; 1.Silver; 2.Bronce;")]
        public float[] medalTime;
        [Tooltip("List of Level Exits")]
        public Exit[] exitList;
        [Tooltip("Select SaveSlot")]
        public string saveState = "save1";
        [Tooltip("Select Default next Exit")]
        public string exitDestination;
        [Tooltip("isTutorial Level")]
        public int musicNummer = 1;
        [Tooltip("isTutorial Level")]
        public bool tutorial = false;
        [Tooltip("isNoRespawn")]
        public bool noRespawn = false;

        [Header("Player Respawn")]
        [Tooltip("The Playerprefab to Spawn on Death")]
        public Transform playerPrefab;
        [Tooltip("Reference to Spawnpoint in Scene")]
        public Transform spawnPoint;
        [Tooltip("Wait x sec until Respawn")]
        public float spawnDelay = 1;
        [Tooltip("Default SpawnParticles to Use")]
        public Transform spawnParticlePrefab;
        [Tooltip("Default SpawnEffect Mesh to Use")]
        public Transform spawnfxMesh;

        [Header("unique References to Scene")]
        [Tooltip("Reference to Main Audio Mixer")]
        public AudioMixer mixer;
        [Tooltip("Reference to the GameHud")]
        public Transform hud;

        [Header("Debugmode, OnGui")]
        [Tooltip("use this variavle to Toggle OnGUi Debugging")]
        public bool debugMode = false;

        private PlayerBall actualPlayer; // PlayerReference
        private IsoGameHud gameHud; // Component of the HudTransform
        private Transform mainCam; // Camera Reference
        private CameraShake cameraShake; // Camera Shake Skript
        private float warpTime;

        private bool isPause = false;
        private float deltaTime = 0.0f;
        private bool levelEnd = false;

        [HideInInspector]
        public AudioSource musicSource;  // Main Music AudioSource 

        void Awake() {
            // Make this Static
            if(isoBallMaster == null) {
                isoBallMaster = this;
            } else if(isoBallMaster != this) {
                Destroy(gameObject);
            }

            //get SaveGame if not in Scene
            GameObject _saveObject = GameObject.FindGameObjectWithTag("SaveObject");
            if(_saveObject == null) {
                SaveObject.Initialize("SaveObject");
            }

            this.mainCam = Camera.main.transform;
            this.cameraShake = mainCam.GetComponent<CameraShake>();
            this.musicSource = GetComponent<AudioSource>();
            this.gameHud = hud.GetComponent<IsoGameHud>();
            actualPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerBall>();
        }


        void Start() {
            if(this.cameraShake == null) {
                Debug.LogError("No Camerashake reference!");
            }

            // Get SaveState we currently in
                saveState = SaveObject.Get<SaveData>().actualSaveSlot;

            GameMaster.setMusic(musicNummer);

            // Make sure the Game is Running at Start (Obsolete)
            Time.timeScale = 1;
            isPause = false;
            //PlayerStart Effects
            actualPlayer.transform.localScale = actualPlayer.targetScale;
            actualPlayer.scaleUp();
            TeleportPlayerFX(3.5f);
            spawnPoint.GetComponent<AudioSource>().Play();

            //Load SaveState and Push
            SaveObject.Load(saveState);
            pushSaveData();
        }

        void Update() {
            if(debugMode) {
                deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            }

            if(levelEnd == false) {
                //Pause Input
                bool _pause = Input.GetButtonDown("Cancel");
                if(_pause == true) {
                    if(getGamePause() == true) {
                        gamePause(false);
                    } else {
                        gamePause(true);
                    }
                }
            } 
        }

        //Pause Switch
        public void gamePause(bool _value) {
            if(_value == true) {
                //Freeze Time, enable Pausemenu
                gameHud.setPausePanelActive(_value);
                isPause = _value;
                Time.timeScale = 0;
            } else {
                // Restore Changes
                gameHud.setPausePanelActive(_value);
                isPause = _value;
                Time.timeScale = 1;
            }
        }

        public static void setLevelEnd(bool _value) {
            isoBallMaster.levelEnd = _value;
        }


        //Get Called when save is Loaded or Refreshed
        public void pushSaveData() {
            //Get Values from Save
            float _bestTime = SaveObject.Get<SaveData>().world[world].level[level].bestTime;
            int _lastMedal = SaveObject.Get<SaveData>().world[world].level[level].medal;
            bool _isBeaten = SaveObject.Get<SaveData>().world[world].level[level].isBeaten;
            
            //gameHud
            gameHud.setLeveldiscText(levelDiscText);
            // SetBestTimePanel
            gameHud.setBestTime_Best(_bestTime);
            gameHud.setMedal_Best(_lastMedal);
            // SetPausePanel
            gameHud.setMedal_Pause(_lastMedal);
            gameHud.setYourBestTime_Pause(_bestTime);
            gameHud.setMedalTime_Pause(medalTime);
            gameHud.setNextButtonPause(_isBeaten);
            // SetWinPanel
            gameHud.setMedalTime_Win(medalTime);
            gameHud.setIsBeaten_Hint(_isBeaten);
        }

        public static void setExit(string _exit) {
            isoBallMaster.exitDestination = _exit;
        }

        // Change the Levelscene
        public static void nextLevel() {
            //GameMaster.ChangeMusic(1);
            IsoBallMaster.saveActualDir(true);
            SceneManager.LoadScene(isoBallMaster.exitDestination); 
        }

        // Static Change the Level with Delay
        public static void nextLevel(float _delay) {
            isoBallMaster.warpTime = _delay;
            isoBallMaster.StartCoroutine(isoBallMaster.LevelWarp());
        }

        //Level Delay
        public IEnumerator LevelWarp() {
            yield return new WaitForSeconds(warpTime);
            restartLevel();
        }

        public static void restartLevel() {
            IsoBallMaster.saveActualDir(false);
            SceneManager.LoadScene(IsoBallMaster.isoBallMaster.sceneID);
        }

        public static void backToMenu() {
            IsoBallMaster.saveActualDir(true);
            GameMaster.setMusic(0);
            SceneManager.LoadScene(GameMaster.gameMaster.startScene);
        }

        // NonSave Bonus Funktion WIP
        public static void bonusStage() {
            // Get old Variables
            int _lastMedal = SaveObject.Get<SaveData>().world[isoBallMaster.world].level[isoBallMaster.level].medal;

            //Calculate
            float _actualTime = isoBallMaster.gameHud.getTime();

            //Set some Endgame Variables
            isoBallMaster.gameHud.setYourTime_Win(_actualTime);
            isoBallMaster.gameHud.setMedal_Win(_lastMedal, 0);
            isoBallMaster.gameHud.setBonusStage_Win(true);
        }

        public static void saveLevelProgress() {
            // Get old Variables
            bool _isBeaten = SaveObject.Get<SaveData>().world[isoBallMaster.world].level[isoBallMaster.level].isBeaten;
            int _lastMedal = SaveObject.Get<SaveData>().world[isoBallMaster.world].level[isoBallMaster.level].medal;

            //Calculate
            float _actualTime = isoBallMaster.gameHud.getTime();
            int _actualMedal = isoBallMaster.calcMedal(_actualTime);
            float _calcTime = isoBallMaster.calcTime(_actualTime, _isBeaten);
            int _calcMedal = isoBallMaster.calcMedal(_calcTime);

            //Set some Endgame Variables
            isoBallMaster.gameHud.setYourTime_Win(_actualTime);
            isoBallMaster.gameHud.setMedal_Win(_lastMedal, 0);
            isoBallMaster.gameHud.setMedal_Win(_actualMedal, 1);

            //Push to Save
            SaveObject.Get<SaveData>().world[isoBallMaster.world].level[isoBallMaster.level].bestTime = _calcTime;
            SaveObject.Get<SaveData>().world[isoBallMaster.world].level[isoBallMaster.level].medal = _calcMedal;
            SaveObject.Get<SaveData>().world[isoBallMaster.world].level[isoBallMaster.level].sceneName = isoBallMaster.sceneID;
            SaveObject.Get<SaveData>().world[isoBallMaster.world].level[isoBallMaster.level].isBeaten = true;
            
            // Save to File and Refresh
            SaveObject.Save();
            isoBallMaster.pushSaveData();
        }
       
        //Calculate new Time
        public float calcTime(float _actualTime,bool _beaten) {
            float _savedTime = SaveObject.Get<SaveData>().world[world].level[level].bestTime;
            if(_beaten == false) {
                // Also new Besttime
                gameHud.setMassage_Win(true);
                return _actualTime;
            } else if(_actualTime < _savedTime) {
                // New Best Time
                gameHud.setMassage_Win(true);
                return _actualTime;
            }
            // no new Time
            gameHud.setMassage_Win(false);
            return _savedTime;        
        }
      
        //Calculate new Medal
        public int calcMedal(float _actualTime) {
            if(medalTime.Length >= 0 && medalTime.Length > 2) {
                if(_actualTime >= 0 && _actualTime < medalTime[0]) {
                    return 1;
                } else if(_actualTime > medalTime[0] && _actualTime < medalTime[1]) {
                    return 2;
                } else if(_actualTime > medalTime[1] && _actualTime < medalTime[2]) {
                    return 3;
                } else if(_actualTime > medalTime[2]) {
                    return 0;
                }
            }
            Debug.LogWarning("Plz set MedalTimes in IsoBallMaster");
            return 0;
        }

       public static PlayerBall getPlayer() {
            return isoBallMaster.actualPlayer;
        }

        //Set The Camera back to the actual Player, if Player get Lost
        public static void SetCamera(Camera _cam, CameraShake _shake) {
            isoBallMaster.mainCam = _cam.transform;
            isoBallMaster.cameraShake = _shake;
        }

        //Static Methode to Change Camera
        public static void RotateCamera(float _degree) {
            CameraFollowIso _cam = isoBallMaster.mainCam.parent.GetComponent<CameraFollowIso>();
            _cam.rotateCam(_degree);
        }

        public static void AngleCamera(int _step) {
            CameraFollowIso _cam = isoBallMaster.mainCam.parent.GetComponent<CameraFollowIso>();
            _cam.addAngle(_step);
        }

        public static void ZoomCamera(int _step) {
            CameraFollowIso _cam = isoBallMaster.mainCam.parent.GetComponent<CameraFollowIso>();
            _cam.addZoom(_step);            
        }

        public static void saveActualDir(bool _reset) {
            if(_reset) {
                SaveObject.Get<SaveData>().world[isoBallMaster.world].level[isoBallMaster.level].actualCamDirection = 0;
                SaveObject.Save();
            } else {
                int _actualDir = isoBallMaster.mainCam.parent.GetComponent<CameraFollowIso>().getActualDir();
                SaveObject.Get<SaveData>().world[isoBallMaster.world].level[isoBallMaster.level].actualCamDirection = _actualDir;
                SaveObject.Save();
            }

        }

        //Controll PlayerController
        public static void SetPlayable(bool _value) {
            isoBallMaster.actualPlayer.setPControl(_value);
        }

        //Controll if the Timer should run
        public static void enableTimer(bool _value) {
            isoBallMaster.gameHud.timerIsRunning(_value);
        }

        //Respawn Player -> Follow Destroy Player
        public IEnumerator RespawnPlayer(float _time) {
            yield return new WaitForSeconds(this.spawnDelay);

            Transform _playerClone = Instantiate(this.playerPrefab, this.spawnPoint.position, spawnPoint.rotation) as Transform;
            actualPlayer = _playerClone.GetComponent<PlayerBall>();
            actualPlayer.transform.localScale = actualPlayer.targetScale;
            actualPlayer.scaleUp();
            spawnPoint.GetComponent<AudioSource>().Play();
            actualPlayer.name = "Player";
            this.mainCam.transform.GetComponentInParent<CameraFollowIso>().setPlayer(_playerClone);
            this.TeleportPlayerFX(3.5f);
        }

        //Add Specific Effects to Position Static
        public static void SpawnEffects(Vector3 _position, Transform _effectPrefab, float _timeToDestroy) {
            Transform _spawnEffectClone = Instantiate(_effectPrefab, _position, Quaternion.Euler(Vector3.zero)) as Transform;
            Destroy(_spawnEffectClone.gameObject, _timeToDestroy);
        }

        //Add Default RespawnEffects to Player Respawn
        public void TeleportPlayerFX(float _timeToDestroy) {
            Transform _spawnFXmeshClone = Instantiate(isoBallMaster.spawnfxMesh, isoBallMaster.spawnPoint.position, isoBallMaster.spawnPoint.rotation) as Transform;
            Transform _spawnParticleClone = Instantiate(isoBallMaster.spawnParticlePrefab, isoBallMaster.spawnPoint.position, isoBallMaster.spawnPoint.rotation) as Transform;
            Destroy(_spawnFXmeshClone.gameObject, _timeToDestroy);
            Destroy(_spawnParticleClone.gameObject, _timeToDestroy);
        }

        //Kill the Player
        public static void DestroyPlayerRespawn(PlayerBall _player) {
            Destroy(_player.gameObject);
            isoBallMaster.StartCoroutine(isoBallMaster.RespawnPlayer(isoBallMaster.spawnDelay));
        }
        public static void DestroyPlayer(PlayerBall _player) {
            float _scaleTime = _player.scaleSpeedDown;
            Destroy(_player.gameObject,_scaleTime);
        }


        //Kill a Pickup
        public static void DestroyPickup(Pickup _pickup) {
            Transform _pickupParticleClone = Instantiate(_pickup.particleEffect, _pickup.transform.position, _pickup.transform.rotation) as Transform;
            Destroy(_pickupParticleClone.gameObject, 5f);
            Destroy(_pickup.transform.parent.gameObject);
        }

        //some static Warppers
        public static void AddScore(int _score) {
            isoBallMaster.gameHud.addScore(_score);
        }

        public static void setWinPanel(bool _value) {
            isoBallMaster.gameHud.setWinPanelActive(_value);
        }

        public static Exit[] getExitList() {
            return isoBallMaster.exitList;
        }

        public static bool getGamePause() {
            return isoBallMaster.isPause;
        }

        public static bool getTutorialStatus() {
            return isoBallMaster.tutorial;
        }

        public static bool getNoRespawn() {
            return isoBallMaster.noRespawn;
        }

        //Play a Sound with a Clip at Specific Position
        public static AudioSource PlayClipAt(AudioClip _clip, Vector3 _pos) {
            GameObject tempGO = new GameObject("TempAudio"); // create the temp object
            tempGO.transform.position = _pos; // set its position
            AudioSource aSource = tempGO.AddComponent<AudioSource>();// add an audio source
            aSource.clip = _clip; // define the clip
            aSource.outputAudioMixerGroup = isoBallMaster.mixer.FindMatchingGroups("Sound")[0];
            // set other aSource properties here, if desired
            aSource.spatialBlend = 1f;
            aSource.Play(); // start the sound
            Destroy(tempGO, _clip.length); // destroy object after clip duration
            return aSource; // return the AudioSource reference
        }

        public static AudioSource PlayClipAt(AudioClip _clip, Vector3 _pos, float _volume, float _pitch) {
            GameObject tempGO = new GameObject("TempAudio"); // create the temp object
            tempGO.transform.position = _pos; // set its position
            AudioSource aSource = tempGO.AddComponent<AudioSource>();// add an audio source
            aSource.clip = _clip; // define the clip
            aSource.outputAudioMixerGroup = isoBallMaster.mixer.FindMatchingGroups("Sound")[0];
            // set other aSource properties here, if desired
            aSource.volume = _volume;
            aSource.pitch = _pitch;
            aSource.spatialBlend = 1f;
            aSource.Play(); // start the sound
            Destroy(tempGO, _clip.length); // destroy object after clip duration
            return aSource; // return the AudioSource reference
        }

        public void OnGUI() {
            if(debugMode == true) {
                GUI.Label(new Rect(Screen.width - 150, 5, 300, 20), "DebugMode = " + debugMode.ToString());
                GUI.Label(new Rect(Screen.width - 150, 80, 300, 20), "Pause = " + isPause);
                GUI.Label(new Rect(Screen.width - 150, 160, 300, 20), "GoldMedal = " + medalTime[0] + " sec");
                GUI.Label(new Rect(Screen.width - 150, 180, 300, 20), "SilverMedal = " + medalTime[1]+ " sec");
                GUI.Label(new Rect(Screen.width - 150, 200, 300, 20), "BronceMedal = " + medalTime[2] + " sec");
             
                //FPS Counter
                GUIStyle style = new GUIStyle();
                Rect rect = new Rect(0, 0, 0, 0);
                style.alignment = TextAnchor.UpperLeft;
                style.fontSize = Screen.height * 2 / 120;
                style.normal.textColor = new Color(0f, 1f, 0f, 1.0f);
                float msec = deltaTime * 1000.0f;
                float fps = 1.0f / deltaTime;
                string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
                GUI.Label(rect, text, style);

            }
            if(GUI.Button(new Rect(Screen.width - 200, Screen.height - 60, 200, 30), "DebugMode"))
                debugMode = !debugMode;
        }
    }
}
