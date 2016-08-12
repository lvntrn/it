using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace IsoBall {
    public class ScenePanelHud : MonoBehaviour {

        public StartMenuManager manager;
        public RectTransform levelPrefab;
        public RectTransform slotPanel;
        public RectTransform bonusPanel;
        public Color[] medalColor;
        public int world = 0;

        private List<RectTransform> levelList;

        void Awake() {
        }

        // Use this for initialization
        void Start() {
            levelList = new List<RectTransform>();
            generateList();
        }

        public void newList() {
            for(int d = 0; d < levelList.Count; d++) {
                Destroy(levelList[d].gameObject);
            }
            levelList.Clear();
        }

        public void AddListener(Button _button, string _value) {
            _button.onClick.AddListener(() => onButton(_value));
        }

        public void onButton(string _value) {
            GameMaster.changeLevel(_value);
        }

        public void updateList() {

            for(int i = 0; i < levelList.Count; i++) {
                SlotPanelHud _script = levelList[i].GetComponent<SlotPanelHud>() as SlotPanelHud;
                _script.setMedal(medalColor[GameMaster.getMedal(world, i)], GameMaster.getMedal(world, i));

                bool _isBeaten = GameMaster.getIsBeaten(world, i);
                _script.setIsBeaten(_isBeaten);
                _script.setClickable(_isBeaten);
            }

            int _lastLevel = GameMaster.getLastPlayableLevel(world);
            if(GameMaster.getBonusMark(world, _lastLevel) == false) {
                levelList[_lastLevel].GetComponent<SlotPanelHud>().setClickable(true);
            }
        }

        public void generateList() {
            int _maxCount = GameMaster.calcMaxLevels(world);
            for(int i = 0; i < _maxCount; i++) {

                int _bonusCount = 0;
                RectTransform _slot = Instantiate(levelPrefab) as RectTransform;
                SlotPanelHud _script = _slot.GetComponent<SlotPanelHud>() as SlotPanelHud;
                _slot.name = "LevelSlot";

                int _medal = GameMaster.getMedal(world, i);
                _script.setMedal(medalColor[_medal], _medal);

                bool _isBeaten = GameMaster.getIsBeaten(world, i);
                _script.setIsBeaten(_isBeaten);
                _script.setClickable(_isBeaten);

                if(GameMaster.getBonusMark(world, i)) {
                    _bonusCount++;
                    _slot.transform.SetParent(bonusPanel.transform, false);
                    _script.setText(_bonusCount, true);
                } else {
                    _slot.transform.SetParent(slotPanel.transform, false);
                    _script.setText(i, false);
                }

                AddListener(_script.button, GameMaster.getSceneName(world, i));
                levelList.Add(_slot);
            }

            int lastLevel = GameMaster.getLastPlayableLevel(world);
            if(GameMaster.getBonusMark(world, lastLevel) == false) {
                levelList[lastLevel].GetComponent<SlotPanelHud>().setClickable(true);
            }
        }
    }
}
