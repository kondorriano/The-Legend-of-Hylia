using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	public enum LookDirection {
		Left,
		Right,
		Up,
		Down
	}

	public float initialSpeed = 7f;
	private float speed;
	private int id;
	bool movingLastFrame = false;
	Animator anim;
	Rigidbody2D myRigidbody;
	LookDirection looking = LookDirection.Right;
	Vector2 walkDirection = Vector2.zero;


	void setId(int myId) {
		id = myId;
	}

	void Start () {
		anim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
		looking = LookDirection.Up;
		speed = initialSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<PlayerDeadControl> ().getDead ()) {
			looking = LookDirection.Up;
			myRigidbody.velocity = Vector2.zero;
			return;
		}

		if (GetComponent<PlayerHurt> ().getHurted ()) {
			anim.SetBool ("Up", false);
			anim.SetBool ("Down", false);
			anim.SetBool ("Left", false);
			anim.SetBool ("Right", false);
			return;
		}

		bool up = false;
		bool down = false;
		bool right = false;
		bool left = false;
		float xAxis = Input.GetAxis ("Horizontal"+id);
		float yAxis = Input.GetAxis ("Vertical"+id);
		if (Mathf.Abs (xAxis) < 0.15f) xAxis = 0;
		if (Mathf.Abs (yAxis) < 0.15f) yAxis = 0;

		walkDirection = new Vector2 (xAxis, yAxis);

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

		anim.SetBool ("Up", up);
		anim.SetBool ("Down", down);
		anim.SetBool ("Left", left);
		anim.SetBool ("Right", right);
	}

	public LookDirection getLooking() {
		return looking;
	}

	public Vector2 getWalkDirection() {
		return walkDirection;
	}

	public void setSpeed(float multiplier) {
		speed = initialSpeed * multiplier;
	}
}
