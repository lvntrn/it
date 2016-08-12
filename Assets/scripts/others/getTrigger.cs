using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class getTrigger : MonoBehaviour {

    // Simple Trigger Forwarder
    public GameObject sendToThis;
    public string withToCollideTag;

    public void OnTriggerEnter(Collider other) {
            if(other != null) {
                if(other.tag == withToCollideTag) {
                    sendToThis.BroadcastMessage("EnterTrigger");
                }
            }
    }


    public void OnTriggerStay(Collider other) {
            if(other != null) {
                if(other.tag == withToCollideTag) {
                    sendToThis.BroadcastMessage("StayTrigger");
                }
            }
    }

    public void OnTriggerExit(Collider other) {
        if(other != null) {
            if(other.tag == withToCollideTag) {
                sendToThis.BroadcastMessage("ExitTrigger");
            }
        }
    }
}
