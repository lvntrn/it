using UnityEngine;
using System.Collections;

//A Warpper for iTween Script
public class iObjectMover : MonoBehaviour {

    [Header("Max Transition Distance from Null")]
    public Vector3[] trans;
    public Vector3 rotate;
  
    [Header("Ease and LoopType")]
    public iTween.EaseType easeType;
    public iTween.LoopType loopType;

    [Header("SpeedParameter")]
    [Tooltip("How Long it Takes")]
    public float transitionTime = 1f;
    [Tooltip("Wait for x sec befor going Back (Loop Only) Loop 1 Array")]
    public float delayTime = 1f;
    public float delayStart = 1f;

    [Header("Enable Skript")]
    public bool moveOn = false;
    [Tooltip("Only for length 1 Array")]
    public bool movefromCenter = false;
    public bool rotateOn = false;

    private int count;
    private int i = 0;
   
    // This Skript generates a iTweenSkript on GameObject at PlayTime with the above values
    void Start() {
        count = trans.Length;
        if(count == 1) {
            StartCoroutine("DelayS");
        } else {
            StartCoroutine("Delay");
        } 
    }

    public void Update() {
        if(rotateOn) {
            // Transform the Values * time on itself
            transform.Rotate(new Vector3(rotate.x, rotate.y, rotate.z) * Time.deltaTime);
        }
    }

    public void StartNext() {
        
    }

    public void MoveBack() {
        i--;
        if(i >= 0) {
            iTween.MoveBy(gameObject, iTween.Hash("x", -trans[i].x, "y", -trans[i].y, "z", -trans[i].z, "easeType", easeType, "delay", delayTime, "time", transitionTime, "onComplete", "MoveBack"));
        } else {
            MoveNext();
        }
    }

    public void MoveNext() {
        i++;
        if(i < count) {
            iTween.MoveBy(gameObject, iTween.Hash("x", trans[i].x, "y", trans[i].y, "z", trans[i].z, "easeType", easeType, "delay", delayTime, "time", transitionTime, "onComplete", "MoveNext"));
        } else {
            MoveBack();
        }
     
    }

    public IEnumerator Delay() {
        yield return new WaitForSeconds(delayStart);
        if(moveOn) {
            iTween.MoveBy(gameObject, iTween.Hash("x", trans[i].x, "y", trans[i].y, "z", trans[i].z, "easeType", easeType, "delay", delayTime, "time", transitionTime, "onComplete", "MoveNext"));
        }
    }


    public IEnumerator DelayS() {
        yield return new WaitForSeconds(delayStart);
            if(moveOn) {
                if(movefromCenter) {
                    iTween.MoveBy(gameObject, iTween.Hash("x", trans[0].x, "y", trans[0].y, "z", trans[0].z, "easeType", easeType, "delay", delayTime, "time", transitionTime, "onComplete", "MoveCenterBack"));
                } else {
                    iTween.MoveBy(gameObject, iTween.Hash("x", trans[0].x, "y", trans[0].y, "z", trans[0].z, "easeType", easeType, "loopType", loopType, "delay", delayTime, "time", transitionTime));
                }
            }
            if(rotateOn) {
                iTween.RotateBy(gameObject, iTween.Hash("x", rotate.x, "y", rotate.y, "z", rotate.z, "easeType", easeType, "loopType", loopType, "delay", delayTime, "time", transitionTime));
        }
    }


    public void MoveCenterBack() {
        iTween.MoveBy(gameObject, iTween.Hash("x", -trans[0].x, "y", -trans[0].y, "z", -trans[0].z, "easeType", easeType, "loopType", loopType, "delay", delayTime, "time", transitionTime));
    }

}
