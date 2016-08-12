using UnityEngine;
using System.Collections;

namespace IsoBall {
    public class MedalStatus : MonoBehaviour {

        //Only for easyer Anim
        [Tooltip("Animatior Propaties")]
        private Animator medalAni;
        private int medalStatus = 0;


	    void Awake () {
            medalAni = GetComponent<Animator>();
	    }

	    public void setStatus (int _value) {
            this.medalStatus = _value;
            medalAni.SetInteger("status", this.medalStatus);
	    }
        public int getStatus() {
            return this.medalStatus;
        }
    }
}

