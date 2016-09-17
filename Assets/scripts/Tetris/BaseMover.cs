using UnityEngine;
using System.Collections;

namespace Tetris {
	public class BaseMover : MonoBehaviour {

		public bool moveObject = false; // Move the Object
		private Vector3 trans;


		void Update() {
			if(moveObject) {
				// Transform Object *Time
				transform.Translate(new Vector3(trans.x, trans.y, trans.z) * Time.deltaTime);
			}
		}

		//Setter Getter
		public void setMoveObject(bool _value) {
			this.moveObject = _value;
		}

		public void setSpeed(Vector3 _speed) {
			this.trans = _speed;
		}
	}
}