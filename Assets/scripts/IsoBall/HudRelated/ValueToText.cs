using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace IsoBall {
    public class ValueToText : MonoBehaviour {

        public Text target;

        public void SendProcent(float _value) {

            target.text = _value.ToString("0 %");
        }


    }
}

