using UnityEngine;
using System.Collections;

namespace SpaceBumb{
	public class DestroyByTime : MonoBehaviour {
		
		public float lifetime;

		void Start () {
			Destroy (gameObject, lifetime);
		}
	}
}