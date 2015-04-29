using UnityEngine;
using System.Collections;

public class CollectableItem : MonoBehaviour {

	public enum CollectableType {
		Key = 0,
		Rupee = 1,
		Bombs = 2,
		Arrow = 3,
		Magic = 4,
		Heart =  5,
		Length = 6
	}

	public CollectableType type;
	public int quantity;

	Transform target;

	void OnDestroy() {
		if(target != null) target.GetComponent<CollectableManager>().addCollectable((int) type, quantity);
		//else Debug.Log("Wat");
	}

	void OnTriggerEnter2D(Collider2D c) {		 
		if (c.gameObject.tag == "Player1" ||
			c.gameObject.tag == "Player2") {
			target = c.transform;
			Destroy(gameObject);
		} else if (c.gameObject.tag == "PlayerCollider") {
			target = c.transform.parent;;
			Destroy(gameObject);
		}

	}
}
