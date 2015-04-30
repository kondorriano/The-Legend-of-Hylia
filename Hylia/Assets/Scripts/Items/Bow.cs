using UnityEngine;
using System.Collections;


public class Bow : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	public float distance  = 35;
	public float speed = 10;

	public Sprite halfArrow;

	//float counter = 0f;
	Vector2 startPosition;
	Vector2 endPosition = Vector3.zero;
	Transform myPlayer;
	Rigidbody2D myRigidbody;
	float timer = -20f;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (((Vector2)transform.position - startPosition).sqrMagnitude > distance) {
			destroyArrow();
		}
		if (timer > 0) {
			timer -= Time.deltaTime;
			if (timer <= 0) destroyArrow();
		}
	}

	void OnTriggerEnter(Collider c) {
		myRigidbody.velocity = new Vector2(0, 0);
		spriteRenderer.sprite = halfArrow;
		timer = 2f;
	}

	void destroyArrow() {
		Destroy (gameObject);
	}

	public void InitArrow(Transform player, Vector2 dir) {
		myPlayer = player;
		startPosition = (Vector2)transform.position;
		myRigidbody = GetComponent<Rigidbody2D> ();
		endPosition = (Vector2)transform.position+dir*distance;

		float angle = Mathf.Atan2 (dir.x, dir.y);
		angle = angle / Mathf.PI * 180;

		transform.eulerAngles = new Vector3(0, 0, -angle);
		myRigidbody.velocity = dir * speed;
	}
}
