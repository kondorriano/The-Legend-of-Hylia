using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed = 10f;
	public string id = "1";

	Animator anim;
	Rigidbody2D myRigidbody;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool up = false;
		bool down = false;
		bool right = false;
		bool left = false;

		float xAxis = Input.GetAxis ("Horizontal"+id);
		float yAxis = Input.GetAxis ("Vertical"+id);


		if(yAxis > 0.15f) {
			up = true;
		}

		if(yAxis < -0.15f) {
			if(up) up = false;
			else down = true;
		}

		if(xAxis < -0.15f) {
			left = true;
		}

		if(xAxis > 0.15f) {
			if(left) left = false;
			else right = true;
		}

		if (Mathf.Abs (xAxis) < 0.15f) xAxis = 0;
		if (Mathf.Abs (yAxis) < 0.15f) yAxis = 0;

		//Movement
		myRigidbody.velocity = new Vector2 (xAxis, yAxis)*speed;
		//Animations
		//invertScale (left);

		anim.SetBool ("Up", up);
		anim.SetBool ("Down", down);
		anim.SetBool ("Left", left);
		anim.SetBool ("Right", right);

	}

	void invertScale(bool left) {
		float x;
		if(left) x = Mathf.Abs(transform.localScale.x) * -1;
		else x = Mathf.Abs(transform.localScale.x);
		
		transform.localScale = new Vector3 (x, transform.localScale.y, transform.localScale.z);		

	}
}
