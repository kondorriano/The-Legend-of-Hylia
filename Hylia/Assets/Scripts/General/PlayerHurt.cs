using UnityEngine;
using System.Collections;

public class PlayerHurt : HitEvent {

	public AudioClip hurtAudio;
	public float invincibleTime = 1f;
	public float knockbackTime = 0.3f;

	bool hurted = false;
	bool knockbacking = false;
	float invincibleCounter = 0;
	float knockbackCounter = 0;
	Vector2 knockbackForce;

	SecondMenuController menu;
	EquipedItem item;
	AudioSource myAudio;



	// Use this for initialization
	void Start () {
		menu = transform.Find ("StuffCanvas").GetComponent<SecondMenuController> ();
		item = GetComponent<EquipedItem> ();

		myAudio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (knockbacking) {
			GetComponent<Rigidbody2D> ().velocity = knockbackForce;
			knockbackForce = knockbackForce *0.9f;

			knockbackCounter -= Time.deltaTime;
			if(knockbackCounter <= 0) {
				knockbackCounter = 0;
				knockbacking = false;
			}

		}
		if (hurted && !GetComponent<PlayerDeadControl> ().getDead ()) {
			invincibleCounter -= Time.deltaTime;
			if(invincibleCounter <= 0) {
				invincibleCounter = 0;
				hurted = false;
				knockbacking = false;
			}
		}
	}

	public override void hurt(int damage, Vector2 force, EnemyHit.EnemyAreaType area) {
		if(hurted) return;

		if ((area == EnemyHit.EnemyAreaType.Normal || area == EnemyHit.EnemyAreaType.Sun || area == EnemyHit.EnemyAreaType.NotMoon) && item.getMoonMode()) return;
		if ((area == EnemyHit.EnemyAreaType.Normal || area == EnemyHit.EnemyAreaType.Moon || area == EnemyHit.EnemyAreaType.NotSun) && item.getSunMode()) return;
		if ((area == EnemyHit.EnemyAreaType.Sun || area == EnemyHit.EnemyAreaType.Moon || area == EnemyHit.EnemyAreaType.NotNormal) 
			&& !item.getMoonMode () && !item.getSunMode ())	return;


		myAudio.Stop();
		myAudio.clip = hurtAudio;
		myAudio.Play();
		menu.addLifePoints (-damage);
		if (force.sqrMagnitude > 0) {
			knockbacking = true;
			knockbackCounter = knockbackTime;
			knockbackForce = force;
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}

		hurted = true;
		invincibleCounter = invincibleTime;

	}

	public bool getHurted() {
		return knockbacking;
	}

}
