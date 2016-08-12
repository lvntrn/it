using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AniMenuController : MonoBehaviour {

	
    public Vector2 startPos;
    private Animator m_anim;
    private RectTransform rTrans;

    public bool IsOpen{
		get { return m_anim.GetBool("isOpen");}
		set { this.m_anim.SetBool ("isOpen", value);}
	}

    public void Awake(){
		m_anim = GetComponent<Animator>();
        // so set the Menu right when scene Starts
        rTrans = (RectTransform) transform.GetComponent<RectTransform>();
        rTrans.anchoredPosition = new Vector2(startPos.x, startPos.y);
    }
}
