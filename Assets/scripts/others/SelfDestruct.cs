using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    public float maxZoneZ;

	void Update () {
        if(maxZoneZ > transform.position.y) {
            Destroy(this.gameObject);
        }
    }
}
