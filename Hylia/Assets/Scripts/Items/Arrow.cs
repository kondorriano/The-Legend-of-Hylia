using UnityEngine;
using System.Collections;


public class Arrow : MonoBehaviour {


	public float speed = 10;
	public float timeAlive = 20f;

	public Sprite halfArrow;

	Transform myPlayer;
	Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		timeAlive -= Time.deltaTime;
		if (timeAlive <= 0)			Destroy (gameObject);
	}

	public void InitArrow(Transform player, Vector2 dir) {
		myPlayer = player;
		GetComponent<EnemyHit> ().setIgnoredPlayer (myPlayer);
		myRigidbody = GetComponent<Rigidbody2D> ();

		float angle = Mathf.Atan2 (dir.x, dir.y);
		angle = angle / Mathf.PI * 180;

		transform.eulerAngles = new Vector3(0, 0, -angle);
		myRigidbody.velocity = dir * speed;
	}

	void changeDirection(Vector2 dir) {
		float angle = Mathf.Atan2 (dir.x, dir.y);
		angle = angle / Mathf.PI * 180;
		
		transform.eulerAngles = new Vector3(0, 0, -angle);
		myRigidbody.velocity = dir * speed;
		
	}

	
	void OnTriggerEnter2D(Collider2D c) {
		
		if (c.gameObject.tag == "Shield") {
			c.GetComponent<AudioSource>().Play();
			Vector2 direction = ((Vector2)(c.transform.position-c.transform.parent.position)).normalized;			
			changeDirection(direction);
			myPlayer = c.transform.parent;
			GetComponent<EnemyHit> ().setIgnoredPlayer (myPlayer);

			return;
		} 

		if (c.transform.parent == myPlayer || c.transform == myPlayer) return;
		
		if (c.gameObject.tag == "PlayerCollider") {
			Destroy(gameObject);
			return;
		} 
		

		if (c.gameObject.tag == "Player1" ||
		    c.gameObject.tag == "Player2") {
			return;
		} 

		if (!c.isTrigger) {
			Vector2 pos = myRigidbody.velocity.normalized*GetComponent<BoxCollider2D>().bounds.size.y*0.5f;
			transform.position += new Vector3(pos.x, pos.y, 0);
			myRigidbody.velocity = Vector2.zero;
			GetComponent<SpriteRenderer>().sprite = halfArrow;
			GetComponent<BoxCollider2D>().enabled = false;
			GetComponent<AudioSource>().Play();
			timeAlive = 5f;
		}
	}
}
