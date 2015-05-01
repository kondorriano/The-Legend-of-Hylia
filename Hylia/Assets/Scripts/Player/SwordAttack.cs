using UnityEngine;
using System.Collections;

public class SwordAttack : MonoBehaviour {

	public AudioClip[] swordSounds;
	bool attack = false;
	Animator anim;
	AudioSource swordAudio;

	private int id;
	void setId(int myId) {
		id = myId;
	}
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		swordAudio = transform.Find ("Sword").GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("360_X" + id)) {
			if(!attack) {
				swordAudio.Stop();
				swordAudio.clip = swordSounds[Random.Range(0, swordSounds.Length)];
				swordAudio.Play();

			}
			anim.SetTrigger("Attack");
			attack = true;
		}
	}

	public void stopAttack(){
		attack = false;
	}

	public bool getAttack(){
		return attack;
	}
}
