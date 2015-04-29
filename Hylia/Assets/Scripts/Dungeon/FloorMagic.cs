using UnityEngine;
using System.Collections;

public class FloorMagic : MonoBehaviour {

	public int magicRegeneration = 4;
	public float everyXSeconds = 0.5f;


	void OnTriggerStay2D(Collider2D c) {

		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {

			c.GetComponent<CollectableManager> ().addMagicThroughTime  ((int)CollectableItem.CollectableType.Magic, magicRegeneration, everyXSeconds);

		}
		
	}


}
