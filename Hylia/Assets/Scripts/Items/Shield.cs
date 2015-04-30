using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	public float distance = 0.75f;
	public Sprite[] sprites;

	private int id;
	SpriteRenderer mySprite;
	BoxCollider2D myCollider;


	void setId(int myId) {
		id = myId;
	}

	// Use this for initialization
	void Start () {
		mySprite = GetComponent<SpriteRenderer> ();
		myCollider = GetComponent<BoxCollider2D> ();

		myCollider.enabled = false;
		mySprite.enabled = false;

		Vector2 shieldPosition = new Vector2 (0, 1).normalized *distance;
		transform.position = transform.parent.position + new Vector3 (shieldPosition.x, shieldPosition.y, shieldPosition.y*0.1f);
		mySprite.sprite = sprites[4];

	}
	
	// Update is called once per frame
	void Update () {
		bool up = false;
		bool down = false;
		bool right = false;
		bool left = false;

		float xAxis = Input.GetAxis ("Horizontal"+id);
		float yAxis = Input.GetAxis ("Vertical"+id);
		if (Mathf.Abs (xAxis) < 0.15f) xAxis = 0;
		if (Mathf.Abs (yAxis) < 0.15f) yAxis = 0;

		Vector2 shieldPosition = new Vector2 (xAxis, yAxis).normalized *distance;

		if(shieldPosition.magnitude > 0) transform.position = transform.parent.position + new Vector3 (shieldPosition.x, shieldPosition.y, shieldPosition.y*0.1f);

		up = (yAxis >= 0.15f);
		down = (yAxis <= -0.15f);
		left = (xAxis <= -0.15f);
		right = (xAxis >= 0.15f);

		if (up) {
			if(left) mySprite.sprite = sprites[3];
			else if(right) mySprite.sprite = sprites[5];
			else mySprite.sprite = sprites[4];

		} else if (down) {
			if(left) mySprite.sprite = sprites[1];
			else if(right) mySprite.sprite = sprites[7];
			else mySprite.sprite = sprites[0];
		} else if (right) mySprite.sprite = sprites[6];
		else if (left) mySprite.sprite = sprites[2];

		myCollider.size = mySprite.bounds.size/(transform.localScale.x*transform.parent.localScale.x);


	}

	public void setShield(bool active) {
		myCollider.enabled = active;
		mySprite.enabled = active;
	}


}
