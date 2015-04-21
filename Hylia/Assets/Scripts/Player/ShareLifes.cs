using UnityEngine;
using System.Collections;

public class ShareLifes : MonoBehaviour {

	void checkDead(Transform player) {
		if (player.GetComponent<PlayerDeadControl> ().getDead () 
		    && !transform.parent.GetComponent<PlayerDeadControl> ().getDead ()) {
			SecondMenuController otherSMC = player.Find("StuffCanvas").GetComponent<SecondMenuController>();
			SecondMenuController mySMC = transform.parent.Find("StuffCanvas").GetComponent<SecondMenuController>();

			int lifes = mySMC.getLifePoints();
			otherSMC.addLifePoints(lifes/2);
			mySMC.addLifePoints(-otherSMC.getLifePoints());
		}

	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.transform.parent == transform.parent || c.transform == transform.parent)
			return;

		if (c.gameObject.tag == "PlayerCollider") {
			checkDead(c.transform.parent);
		} else if (c.gameObject.tag == "Player1" ||
			c.gameObject.tag == "Player2") {
			checkDead(c.transform);
		}
	}
}
