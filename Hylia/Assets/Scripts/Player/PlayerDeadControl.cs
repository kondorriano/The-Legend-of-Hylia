using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDeadControl : MonoBehaviour {

	public int timeToRespawn = 21;
	public AudioClip death;
	public AudioClip revive;



	bool dead = false;
	float counter = 0;
	Transform respawnPoint;
	Text deathCounter;


	Animator anim;
	AudioSource myAudio;



	// Use this for initialization
	void Start () {
		respawnPoint = transform.parent;
		deathCounter = transform.Find ("StuffCanvas/DeathCounter").GetComponent<Text> ();
		deathCounter.text = "";
		anim = GetComponent<Animator> ();
		myAudio = GetComponent<AudioSource> ();

	}

	void Update() {
		if (dead) {
			counter -= Time.deltaTime;
			deathCounter.text = "" + ((int)counter / 10) + "" + ((int)counter % 10);

			if(counter <= 0) {
				//mirar si rupias?
				SecondMenuController smc = transform.Find("StuffCanvas").GetComponent<SecondMenuController>();
				smc.addLifePoints(smc.getMaxLifePoints());
				transform.position = respawnPoint.position;
				//Respawn

			}
		}
	}

	public bool getDead() {
		return dead;
	}

	public void setDead(int lifePoints) {
		if (lifePoints <= 0) {
			if(!dead) {
				spreadDeath();
			}
		} else {
			if(dead) {
				spreadLife();
			}
		}

	}

	void spreadDeath() {
		dead = true;
		counter = (float)timeToRespawn;

		//ActivateTime
		anim.SetTrigger ("Dying");
		anim.SetBool ("Dead", dead);
		myAudio.Stop();
		myAudio.clip = death;
		myAudio.Play();

	}

	void spreadLife() {
		dead = false;
		counter = 0;
		deathCounter.text = "";
		//DeactivateTime
		anim.SetBool ("Dead", dead);
		myAudio.Stop();
		myAudio.clip = revive;
		myAudio.Play();
	}

	void setRespawnPoint(Transform respawn) {
		respawnPoint = respawn;
	}
	

}
