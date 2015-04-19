using UnityEngine;
using System.Collections;

public class Boomerang : MonoBehaviour {

	public float rotationSpeed = 800f;
	public float distance  = 10;
	public float speed = 10;
	
	int state = 0;

	float counter = 0f;
	Vector2 startPosition;
	Vector2 endPosition = Vector3.zero;
	Transform myPlayer;
	Rigidbody2D myRigidbody;
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (-Vector3.forward * rotationSpeed * Time.deltaTime);
		if (state == 0) {
			if ((endPosition - startPosition).sqrMagnitude <= ((Vector2)transform.position - startPosition).sqrMagnitude)
				state = 1;
		}

		if (state == 1) {
			myRigidbody.velocity = Vector2.Lerp(myRigidbody.velocity, ((Vector2)myPlayer.position-(Vector2)transform.position).normalized*speed, counter);
			counter += Time.deltaTime;
			//myRigidbody.velocity += (Vector2) (myPlayer.position-transform.position).normalized*acceleration*Time.deltaTime;
			if(counter >= 1) state = 2;
			if(((Vector2)myPlayer.position-(Vector2)transform.position).sqrMagnitude <= 0.1f) destroyBoomerang();

		} 
		if (state == 2) {
			myRigidbody.velocity = ((Vector2)myPlayer.position-(Vector2)transform.position).normalized*speed;
			if(((Vector2)myPlayer.position-(Vector2)transform.position).sqrMagnitude <= 0.1f) destroyBoomerang();
		}
	}

	public void InitBoomerang(Transform player, Vector2 dir) {
		myPlayer = player;
		startPosition = (Vector2)transform.position;
		myRigidbody = GetComponent<Rigidbody2D> ();
		endPosition = (Vector2)transform.position+dir*distance;
		
		myRigidbody.velocity = dir * speed;
	}

	void destroyBoomerang() {
		myPlayer.GetComponent<EquipedItem> ().activateBoomerang ();
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D c) {
		Debug.Log(c.gameObject.name);
		if (c.transform.parent == myPlayer && state == 2) {
			destroyBoomerang();
		} else if (c.gameObject.tag != "Grabable" &&
		           c.gameObject.tag != "Player" &&
		           c.gameObject.tag != "Player1" &&
		           c.gameObject.tag != "Player2") state = 2;
	}
}
