using UnityEngine;
using System.Collections;

public class FloorSpeed : MonoBehaviour {

	public float multiplierSpeed = 0.75f;
	public Utils.EnemyAreaType areaReach = Utils.EnemyAreaType.All;



	void OnTriggerEnter2D(Collider2D c) {

		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {

			c.GetComponent<Movement> ().setEnviromentSpeed  (multiplierSpeed, areaReach);

		}

	}
	

	void OnTriggerStay2D(Collider2D c) {

		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {

			c.GetComponent<Movement> ().setEnviromentSpeed  (multiplierSpeed, areaReach);

		}
		
	}


	void OnTriggerExit2D(Collider2D c) {
		
		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			
			c.GetComponent<Movement> ().setEnviromentSpeed  (1, Utils.EnemyAreaType.All);
			
		}
		
	}


}
