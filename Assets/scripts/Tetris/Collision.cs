using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {

	// wenn spieler durch loch passt
	void OnTriggerEnter (Collider col) {
		// punkte gehn hoch
		switch(col.tag){

		case"ObstacleEins":
			//TetrisMaster.reduceLive(1);

			//countText.UpdateMessage ("Einen Punkt mehr erlangt.");
			break;

		}
	
	}
	// wenn spieler durch loch berührt
	//void OnCollisionEnter (Collider col) {
		// leben geht weg
		//switch(col.gameObject.tag){
		//case"Obstacle":
			//udHealthPanel.UpdateMessage("Einen Punkt mehr erlangt.");
			//break;
		
	//}
//}
}