using UnityEngine;
using System.Collections;

public class Boomerang : MonoBehaviour {

	public float rotationSpeed = 800f;
	public float distance  = 10;
	public float speed = 10;
	public float deceleration = 1;
	
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
			myRigidbody.velocity = Vector2.Lerp(myRigidbody.velocity, ((Vector2)myPlayer.position-(Vector2)transform.position).normalized*speed, counter*deceleration);
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

	void changeDirection(Vector2 dir) {
		endPosition = (Vector2)transform.position + dir * (endPosition - (Vector2)transform.position).magnitude;
		myRigidbody.velocity = dir * speed;

	}

	void bounceBoomerang(Vector2 dir) {
		state = 1;
		counter = 0;
		myRigidbody.velocity = dir * Mathf.Min (myRigidbody.velocity.magnitude*1.5f,speed);


	}

	void destroyBoomerang() {
		myPlayer.GetComponent<EquipedItem> ().activateBoomerang ();
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D c) {

		if (c.gameObject.tag == "Shield") {
			c.GetComponent<AudioSource>().Play();
			Vector2 direction = ((Vector2)(c.transform.position-c.transform.parent.position)).normalized;

			if(state == 0) changeDirection(direction);
			else bounceBoomerang(direction);
			return;
		} 

		if (c.transform.parent == myPlayer || c.transform == myPlayer) {
			if(state == 2) destroyBoomerang ();
			return;
		}

		GrabableItem gI;
		if (c.gameObject.tag == "PlayerCollider") {
			gI = c.transform.parent.GetComponent<GrabableItem> ();
			if(gI != null) {
				gI.setTarget(transform);
				state = 2;
			}
			return;
		} 

		gI = c.GetComponent<GrabableItem> ();
		if (c.gameObject.tag == "Player1" ||
		    c.gameObject.tag == "Player2") {
			if(gI != null) {
				gI.setTarget(transform);
				state = 2;
			}			
		} else if(gI != null) gI.setTarget(transform);
		else if(!c.isTrigger) state = 2;
	}
}
