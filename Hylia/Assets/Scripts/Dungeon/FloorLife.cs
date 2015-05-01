using UnityEngine;
using System.Collections;

public class FloorLife : MonoBehaviour {

	public int lifeRegeneration = 1;
	public float everyXSeconds = 1f;


	void OnTriggerStay2D(Collider2D c) {

		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {

			c.GetComponent<CollectableManager> ().addLifeThroughTime  ((int)CollectableItem.CollectableType.Magic, lifeRegeneration, everyXSeconds);

		}
		
	}


}
