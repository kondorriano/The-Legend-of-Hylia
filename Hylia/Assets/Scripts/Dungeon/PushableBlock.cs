using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class PushableBlock : MonoBehaviour {

	public Utils.EnemyAreaType blockArea = Utils.EnemyAreaType.All;

	Rigidbody2D myRigidbody;
	AudioSource myAudio;

	void Start() {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAudio = GetComponent<AudioSource> ();
	}

	void Update() {
		if (myRigidbody.velocity.sqrMagnitude > 0.2f) {
			if(!myAudio.isPlaying) myAudio.Play ();
		} else myAudio.Stop ();
	}
}
