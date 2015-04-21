using UnityEngine;
using System.Collections;

public class PlayerDeadControl : MonoBehaviour {

	public AudioClip death;
	public AudioClip revive;


	bool dead = false;
	Animator anim;
	AudioSource myAudio;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		myAudio = GetComponent<AudioSource> ();
	}

	public bool getDead() {
		return dead;
	}

	public void setDead(int lifePoints) {
		if (lifePoints <= 0) {
			if(!dead) {
				dead = true;
				spreadDeath();
			}
		} else {
			if(dead) {
				dead = false;
				spreadLife();
			}
		}

	}

	void spreadDeath() {
		anim.SetTrigger ("Dying");
		anim.SetBool ("Dead", dead);
		myAudio.Stop();
		myAudio.clip = death;
		myAudio.Play();

	}

	void spreadLife() {
		anim.SetBool ("Dead", dead);
		myAudio.Stop();
		myAudio.clip = revive;
		myAudio.Play();
	}
	

}
