using UnityEngine;
using System.Collections;

namespace IsoBall {
    public class Rotator : MonoBehaviour {

        public int rotateX = 15;
        public int rotateY = 30;
        public int rotateZ = 45;


        void Update() {
            // Transform the Values * time on itself
            transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime);
        }
    }
}
