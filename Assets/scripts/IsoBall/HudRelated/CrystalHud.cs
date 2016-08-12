using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


namespace IsoBall {
    public class CrystalHud : MonoBehaviour {

        [Header("Generation Related")]
        [Tooltip("CrystalPrefab")]
        public RectTransform CrystalPrefab;
        [Tooltip("Parent the Crystals to")]
        public RectTransform CrystalPanel;
        [Tooltip("Offset from null")]
        public Vector3 offset;
        [Tooltip("Place the one here")]
        public Vector3 posNext;
        [Tooltip("Hint if now Crystals are there")]
        public Text epicHint;

        [Header("Animation Related")]
        [Tooltip("Dont do this:D")]
        public bool animate;
        [Tooltip("Animate Ease")]
        public iTween.EaseType easeType;
        [Tooltip("Animate TransitionTime")]
        public float transitionTime = 1f;
        [Tooltip("TransistionTime for next Crystals")]
        public float nextTime = 2f;
        [Tooltip("Ease for next Crystals")]
        public iTween.EaseType easeTypeNext;

        private List<RectTransform> crystalList; // List this the UI Elements to Display
        private int crystalCount; // actualLive that is been Displayed
        private bool noScore = true;


        void Start() {
            crystalList = new List<RectTransform>();
            this.crystalCount = IsoBallMaster.isoBallMaster.crystalCount;
            if(crystalCount == 0) {
                epicHint.text = "Reach the Exit!";
                epicHint.gameObject.SetActive(true);
                noScore = true;
            } else {
                noScore = false;
                generateList();
            }
          
        }

       public void generateList() {
            for(int i = 0; i < crystalCount; i++) {
                RectTransform _crystal = Instantiate(CrystalPrefab) as RectTransform;
                _crystal.SetParent(CrystalPanel, false);
                _crystal.anchoredPosition = (posNext * i) + offset;
                if(animate) {
                    // iTween.ScaleBy(_crystal, iTween.Hash("y", Random.Range(-8f, 8f), "easeType", "linear", "loopType", "pingPong", "time", transitionTime*0.1f));
                    iTween.RotateBy(_crystal.gameObject, iTween.Hash("y", 1f, "easeType", easeType, "loopType", "loop", "time", Random.Range(transitionTime - 3f, transitionTime + 3f)));
                }
                crystalList.Add(_crystal);
            }

        }
        
        //Reset the List ,set all to Active
        public void setActive() {
            for(int i = 0; i < crystalList.Count; i++) {
                crystalList[i].gameObject.SetActive(true);
            }
        }

        // remove the first and animate the last
        public void setCount(int _value) {
            crystalCount = _value;
            if(crystalCount < 0 || noScore) {
                return;
            }
            crystalList[crystalCount - 1].GetComponent<Animator>().SetBool("Death", true);
            iTween.ScaleTo(crystalList[crystalCount-1].gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", easeTypeNext, "time", 1f));
            Destroy(crystalList[crystalCount - 1].gameObject, 1f);
            for(int i = crystalCount; i < crystalList.Count; i++) {
                iTween.MoveBy(crystalList[i].gameObject, iTween.Hash("x", -posNext.x*0.75, "easeType", easeTypeNext, "time", nextTime));
            }
        }

        //Getter
        public int getCount() {
            return this.crystalCount;
        }
    }
}
