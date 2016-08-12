using UnityEngine;
using System.Collections;

namespace Tetris {
    public class TetrisPlayer : MonoBehaviour {

        //TestObject as Representation of the Player
     //   private Rigidbody rb;

        void Start() {
         //   rb = GetComponent<Rigidbody>();
        }

        void OnCollisionEnter(Collision _other) {

            if(_other.gameObject.tag == "Obstacle") {
                //Debug.Log("Hit");
                TetrisMaster.reduceLive(1);
            }
        }
    }
}

