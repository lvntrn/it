using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {


	public float speed = 15.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	 public void Move(){
		Vector3 moveAmount = Vector3.back * speed * Time.deltaTime;
		transform.Translate (moveAmount);
	}
}
