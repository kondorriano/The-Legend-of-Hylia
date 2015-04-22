using UnityEngine;
using System.Collections;

public class GrabableItem : MonoBehaviour {

	Transform followingTarget;	

	// Update is called once per frame
	void Update () {
		if (followingTarget != null) {
			transform.position = followingTarget.position;
		}
	}

	public void setTarget(Transform target) {
		followingTarget = target;
	}

	void OnCollisionEnter2D(Collision2D c) {
		followingTarget = null;
	}
}
