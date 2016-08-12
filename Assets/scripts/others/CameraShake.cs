using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public float shakeInvokeRep = 0.01f; //Default ShakeStrength
    private float shakeAmount = 0; //Default ShakeDuration
    private Vector3 startPos;  //StartPos of Main Camera

    private Camera mainCam; //Main Camera

    void Awake() {
        this.startPos = transform.position;
        this.mainCam = Camera.main;
    }

    public void Shake(float _amount, float _length) {
        this.shakeAmount = _amount;

        InvokeRepeating("DoShake", 0f, shakeInvokeRep);
        Invoke("StopShake", _length);
    }

    //For Debug Uses
    //void Update(){
    //	if (Input.GetKeyDown (KeyCode.T)){
    //		Shake (0.5f, 2.2f);
    //		Debug.Log ("Shake");
    //	}
    //}

    //Start Camera Shaking
    private void DoShake() {
        if(shakeAmount > 0) {
            Vector3 _CamPos = mainCam.transform.position;
            //Get Random Value
            float _offsetX = Random.value * this.shakeAmount * 2 - this.shakeAmount;
            float _offsetY = Random.value * this.shakeAmount * 2 - this.shakeAmount;
            _CamPos.x += _offsetX;
            _CamPos.y += _offsetY;
            //Set New CameraPos
            mainCam.transform.position = _CamPos;
        }
    }
    
    // Stop the Shaking if Camera Shakes
    private void StopShake() {
        CancelInvoke("DoShake");
        //Reset the Camera to Default Position
        mainCam.transform.localPosition = this.startPos;
    }
}
