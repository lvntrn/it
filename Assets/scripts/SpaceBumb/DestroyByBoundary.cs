using UnityEngine;
using System.Collections;

namespace SpaceBumb{
	public class DestroyByBoundary : MonoBehaviour {

		void OnTriggerExit(Collider other) {
			Destroy (other.gameObject);
		}
	}
}