using UnityEngine;
using System.Collections;

public class RigidbodyModifier : MonoBehaviour {

    public Vector3 centerOfMass;

    private Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Start () {
        rb.centerOfMass = centerOfMass;
	}	
}
