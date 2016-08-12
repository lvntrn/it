using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace IsoBall {
    public class IsoGameHud : MonoBehaviour {
        public CrystalHud crystalPanel;
        public BestTimePanelHud bestTimePanel;
        public WinPanelHud winPanel;
        public PausePanelHud pausePanel;
        public TimerPanelHud timerPanel;
        public HintPanelHud hintPanel;
        public Text countText;
        public Text timerText;
        public Text startCounterText;
        public Text leveldiscText;
        public Transform leveldiscBGImage;
        public int startCountDown = 3;
        public float countDownTime = 2f; // how fast it counts Down

        private int medalStatus;
        private int score = 0;
        private int maxScore = 1;
        private bool isRunning = false;  // Update the Timer
        private float internalTime;

        void Start() { 
            IsoBallMaster.SetPlayable(false);
            maxScore = IsoBallMaster.isoBallMaster.crystalCount;
            StartCoroutine(CountDown());
        }

        public IEnumerator HideCountDownText() {
            //Enable PlayerControlls, Start Countdown, Disables Countdowntext
            isRunning = true;
            IsoBallMaster.SetPlayable(true);
            yield return new WaitForSeconds(countDownTime);
            startCounterText.gameObject.SetActive(false);
        }

        // Running until Countdown is 0
        public IEnumerator CountDown() {
            startCounterText.text = startCountDown.ToString("0");
            yield return new WaitForSeconds(countDownTime);
            startCountDown--;
            if(startCountDown <= 0) {
                startCounterText.text = "Start";
                StartCoroutine(HideCountDownText());
            } else {
                StartCoroutine(CountDown());
            }
        }

        void Update() {
            // Calculate Timer and send to Panel
            if(isRunning) {
               internalTime += Time.deltaTime;
               timerPanel.calcTimePanel(internalTime);
            }   
        }

        // IsoGameHud Setters/Getters
        // Controls if Score = maxScore then opens the Exit
        public void addScore(int _score) {
            score += _score;

            // Send Score
            hintPanel.setCrystalCount(score);
            crystalPanel.setCount(score);

            if(score >= maxScore) {
                countText.gameObject.SetActive(true);
                Exit[] _exit = IsoBallMaster.getExitList();
                foreach(Exit exit in _exit) {
                    exit.setExit(true);
                }
            }
        }

        public void timerIsRunning(bool _active) {
            this.isRunning = _active;
        }

        public float getTime() {
            return internalTime;
        }
  
        public void setLeveldiscText(string _string) {
            if(_string == "") {
                leveldiscBGImage.gameObject.SetActive(false);
            } else {
                leveldiscBGImage.gameObject.SetActive(true);
                leveldiscText.text = _string;
            }
        }

      
        //Enable:Disable Menus\Panles
        public void setWinPanelActive(bool _value) {
          //winPanel.gameObject.SetActive(_value);
            winPanel.GetComponent<AniMenuController>().IsOpen = _value;
        }
        public void setPausePanelActive(bool _value) {
            //pausePanel.gameObject.SetActive(_value);
            pausePanel.GetComponent<AniMenuController>().IsOpen = _value;
            setBestTimePanelActive(!_value);
        }
        public void setNextButtonPause(bool _value) {
            pausePanel.setNextButton(_value);
        }
        public void setBestTimePanelActive(bool _value) {
            //bestTimePanel.gameObject.SetActive(_value);
            bestTimePanel.GetComponent<AniMenuController>().IsOpen = _value;
        }

        // PanelMenus and Setters
        //BestTimePanelSetters
        public void setBestTime_Best(float _time) {
            bestTimePanel.setYourBestTime(_time);
        }
        public void setMedal_Best(int _value) {
            bestTimePanel.setMedalStatus(_value);
        }

        //WinPanelSetters
        public void setMassage_Win(bool _value) {
            winPanel.setMassage(_value);
        }
        public void setYourTime_Win(float _value) {
            winPanel.setYourTime(_value);
        }
        public void setMedalTime_Win(float[] _array) {
            winPanel.setMedalTime(_array);
        }
        public void setBonusStage_Win(bool _value) {
            winPanel.setBonus(_value);
        }
        public void setMedal_Win(int _value, int _for) {
            winPanel.setMedalStatus(_value, _for);
        }

        //PausePanelSetters
        public void setYourBestTime_Pause(float _value) {
            pausePanel.setYourBestTime(_value);
        }
        public void setMedalTime_Pause(float[] _array) {
            pausePanel.setMedalTime(_array);
        }
        public void setMedal_Pause(int _value) {
            pausePanel.setMedalStatus(_value);
        }
        //HintPanelSetters
        public void setIsBeaten_Hint(bool _value) {
            hintPanel.setIsBeaten(_value);
        }


        //Button Warper
        public void backToMenu() {
            Time.timeScale = 1;
            IsoBallMaster.backToMenu();
        }
        public void restart() {
            IsoBallMaster.restartLevel();
        }
        public void nextLevel() {
            IsoBallMaster.nextLevel();
        }

        //DebugMode
        void OnGUI() {
            if(IsoBallMaster.isoBallMaster.debugMode == true) {
                GUI.Label(new Rect(Screen.width - 150, 120, 300, 20), "ScoreCount = " + score + " / " + IsoBallMaster.isoBallMaster.crystalCount);
            }
        }
    }
}
