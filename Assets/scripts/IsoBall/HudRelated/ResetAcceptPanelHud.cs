using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace IsoBall {
    public class ResetAcceptPanelHud : MonoBehaviour {

        public StartMenuManager manager;

        public Button accept;
        public Button back;
        public Text disc;
        public Animator anim;

        private string slotToReset;

        public bool IsOpen {
            get {
                return anim.GetBool("isOpen");
            }
            set {
                this.anim.SetBool("isOpen", value);
            }
        }

        public void setStatus(bool _value) {
            IsOpen = _value;
            accept.interactable = _value;
            back.interactable = _value;
            disc.text = "you are Really Sure? ->  <color=red>Reset " + slotToReset + " ?</color>";
        }

        public void setSlot(string _slot) {
            slotToReset = _slot;
        }

        public void Reset() {
            GameMaster.clearSaveData(slotToReset);
            manager.pushsaveData();
            setStatus(false);
        }

        public void Back() {
            setStatus(false);
        }

        public bool getIsOpen() {
            return IsOpen; 
        }
    }
}
