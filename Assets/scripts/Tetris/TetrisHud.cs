using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Tetris {
    public class TetrisHud : MonoBehaviour {

        public Text countText;
        public Text winText;
        public Text startCounterText;
        public HudHealthPanel healthPanel;

        public int score = 0; //Acutal Displayed Score

        public int startCountDown = 3;
        public float countDownTime = 2f; // how fast it counts Down


        void Start() {
            addScore(0);
            winText.text = "";
            TetrisMaster.setPlayable(false);
            StartCoroutine(CountDown());
        }

        public IEnumerator HideCountDownText() {
            //Enable PlayerControlls, Disables Countdowntext
            TetrisMaster.setPlayable(true);
            startCounterText.text = "";
            startCounterText.gameObject.SetActive(false);
            yield return new WaitForSeconds(countDownTime);
        }

        // Running until Countdown is 0
        public IEnumerator CountDown() {
            startCounterText.text = startCountDown.ToString("0");
            yield return new WaitForSeconds(countDownTime);
            startCountDown--;
            if(startCountDown <= 0) {
                startCounterText.text = "Start";
                StartCoroutine(HideCountDownText());
				//Moving.Move ();
            } else {
                StartCoroutine(CountDown());
            }
        }

        // Add Score
        public void addScore(int _score) {
            score += _score;
            countText.text = "Score: " + score.ToString();
        }

        // Reduce Live by This Value
        public void reduceLive(int _value) {
            int _acutalLive = healthPanel.getLive();
            _acutalLive -= _value;
            healthPanel.setLive(_acutalLive);
        }
    }
}
