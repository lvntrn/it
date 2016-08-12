using UnityEngine;
using System.Collections;

namespace SpaceBumb{
	public class ObjectMover : MonoBehaviour {
		
		public Vector3 trans;
		public Vector3 rotate;

		public bool moveObject = false;
		public bool rotateObject = false;
		public bool rotateRandom = false;
		public float tumble;

		private Vector3 random;
		
	void Start () {
		if (rotateRandom){
			random.x = Random.Range(-tumble,tumble);
			random.y = Random.Range(-tumble,tumble);
			random.z = Random.Range(-tumble,tumble);
		}
	}
	void Update () {
		if (moveObject)
		transform.Translate( new Vector3 (trans.x, trans.y, trans.z) * Time.deltaTime);
		if (rotateObject)
		transform.Rotate (new Vector3 (rotate.x, rotate.y, rotate.z) * Time.deltaTime);
		if (rotateObject && rotateRandom)
		transform.Rotate (new Vector3 (random.x, random.y, random.z) * Time.deltaTime);
		
		}
	}
}