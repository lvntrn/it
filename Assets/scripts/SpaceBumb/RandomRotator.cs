using UnityEngine;
using System.Collections;

namespace SpaceBumb{
	public class RandomRotator : MonoBehaviour {
		
		public float tumble;

		void Start () {
			Rigidbody rb = GetComponent<Rigidbody> ();
			rb.angularVelocity = Random.insideUnitSphere * tumble; 
		}
	}
}