using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	public enum LookDirection {
		Left,
		Right,
		Up,
		Down
	}

	public float speed = 10f;
	private int id;
	bool movingLastFrame = false;
	Animator anim;
	Rigidbody2D myRigidbody;
	LookDirection looking = LookDirection.Right;
	Vector3 walkDirection = Vector3.zero;


	void setId(int myId) {
		id = myId;
	}

	void Start () {
		anim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
		looking = LookDirection.Up;
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

		walkDirection = new Vector3 (xAxis, yAxis, 0);

		up = (yAxis >= 0.15f);
		down = (yAxis <= -0.15f);
		left = (xAxis <= -0.15f);
		right = (xAxis >= 0.15f);

		bool isMoving = (up || down || left || right);

		bool sameDir;
		if (looking == LookDirection.Up) sameDir = up;
		else if (looking == LookDirection.Down) sameDir = down;
		else if (looking == LookDirection.Left) sameDir = left;
		else sameDir = right;

		if (!movingLastFrame || !sameDir) {
			if (up)
				looking = LookDirection.Up;
			if (down)
				looking = LookDirection.Down;
			if (left)
				looking = LookDirection.Left;
			if (right)
				looking = LookDirection.Right;
		}

		movingLastFrame = isMoving;

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

	public LookDirection getLooking() {
		return looking;
	}

	public Vector3 getWalkDirection() {
		return walkDirection;
	}
}
