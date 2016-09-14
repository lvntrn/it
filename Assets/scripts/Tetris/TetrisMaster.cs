using UnityEngine;
using System.Collections;

namespace Tetris {
    public class TetrisMaster : MonoBehaviour {

        //Create a Static instance of this Class
        public static TetrisMaster tetrisMaster;
        public Transform[] baseList; // List with all used Obstacles
        public Vector3 spawnPos; //SpawnPoint
        public Vector3 incomeSpeed; // Speed the Obstacles is Coming to the Player
        public float zDestruct; // the zValue that Destruct the Obstacle
        public Transform hud; //Hud Reference
        public float spawnPause; // SpawnDelay for new Obstacles

//        private BaseMover actualBase; // Reference to the new/Actual Obstacle
        private int actualBaseInt = 0; //Int of the Actual Obstacle
        [HideInInspector]
        public bool playable = false; //Run GameLogic

        private TetrisHud gameHud;
        private Transform mainCam;
        private CameraShake cameraShake;


        void Awake() {
            // Make this Static
			//Debug.Log(this);
//			Debug.Log(tetrisMaster);
            if(tetrisMaster == null) {
                tetrisMaster = this;
            } else if(tetrisMaster != this) {
                Destroy(gameObject);
            }

            this.mainCam = Camera.main.transform;
            this.cameraShake = mainCam.GetComponent<CameraShake>();
            this.gameHud = hud.GetComponent<TetrisHud>();

        }

        void Start() {
            if(this.cameraShake == null) {
                Debug.LogError("No Camerashake reference!");
            }
        }

        void Update() {
//            if(actualBase.transform.position.z < zDestruct) {
//            }

        }



        //Spawn a Obstacle and set Score, set movedelay
        public IEnumerator nextBase() {
            actualBaseInt++;
            if(actualBaseInt >= baseList.Length) {
                actualBaseInt = 0;
            }

            yield return new WaitForSeconds(spawnPause);
           // actualBase.setMoveObject(true);

        }

        //Set the actuall CameraReference
        public static void SetCamera(Camera _cam, CameraShake _shake) {
            tetrisMaster.mainCam = _cam.transform;
            tetrisMaster.cameraShake = _shake;
        }

        //Getter Setter
        static public void setPlayable(bool _value) {
            tetrisMaster.playable = _value;
//          
        }
        static public void AddScore(int _score) {
            tetrisMaster.gameHud.addScore(_score);
        }

        static public void reduceLive(int _value) {
            tetrisMaster.gameHud.reduceLive(_value);
        }
    }
}
