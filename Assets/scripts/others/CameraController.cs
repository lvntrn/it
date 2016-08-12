using UnityEngine;
using System.Collections;


//Old CameraController Obsolete
public class CameraController : MonoBehaviour {

    public GameObject player;
    public float lerpFactor = 0.1f;

    private Vector3 offset;
    private float nX, nY, nZ;

    void Start() {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate() {

        //nX = Mathf.Lerp(player.transform.position.x ,player.transform.position.x + offset.x, lerpFactor);
        //nY = Mathf.Lerp(player.transform.position.y ,player.transform.position.y + offset.y, lerpFactor);
        //nZ = 0f;//Mathf.Lerp(player.transform.position.z ,player.transform.position.z + offset.z, lerpFactor);

        transform.position = player.transform.position + offset;
    }
}
