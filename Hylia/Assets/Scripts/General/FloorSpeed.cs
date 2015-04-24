using UnityEngine;
using System.Collections;

public class FloorSpeed : MonoBehaviour {

	public float multiplierSpeed = 0.75f;
	public EnemyHit.EnemyAreaType areaReach = EnemyHit.EnemyAreaType.All;



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
			
			c.GetComponent<Movement> ().setEnviromentSpeed  (1, EnemyHit.EnemyAreaType.All);
			
		}
		
	}


}
