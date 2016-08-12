using UnityEngine;
using System.Collections;

//A Warpper for iTween Script
public class iObjectMoverPath : MonoBehaviour {

    [Header("Path")]
    public string pathName;

    [Header("Ease and LoopType")]
    public iTween.EaseType easeType;
    public iTween.LoopType loopType;

    [Header("SpeedParameter")]
    [Tooltip("How Long it Takes")]
    public float transitionTime = 1f;
    [Tooltip("Wait for x sec befor going Back (Loop Only)")]
    public float delayTime = 1f;
    public float delayStart = 1f;

    [Header("Enable Skript")]
    public bool moveOn = false;
    public bool rotateOn = false;
   

    // This Skript generates a iTweenSkript on GameObject at PlayTime with the above Path
    void Start() {
        StartCoroutine("Delay", delayStart);
    }

    public IEnumerator Delay(float _sec) {
        yield return new WaitForSeconds(_sec);
        if(moveOn) {
            iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "easeType", easeType, "loopType", loopType, "delay", delayTime, "time", transitionTime));
        }
        if(rotateOn) {
            iTween.RotateTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "easeType", easeType, "loopType", loopType, "delay", delayTime, "time", transitionTime));
        }
    }

}
