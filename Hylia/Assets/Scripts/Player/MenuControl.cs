using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour {

	public AudioClip popUpShare;


	private int id;

	bool isRight = false;
	Transform otherPlayer;
	Transform player;
	CameraDivisionEffect cam;
	Animator anim;
	AudioSource audio;


	bool previousActiveShare = false;
	Animator animShare;



	void setId(int myId) {
		id = myId;
	}
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();

		cam = Camera.main.GetComponent<CameraDivisionEffect> ();
		player = GameObject.FindGameObjectWithTag ("Player" + id).transform;
		otherPlayer = GameObject.FindGameObjectWithTag ("Player" + ((id%2)+1)).transform;

		Transform menuShare = transform.Find ("MenuShare");
		animShare = menuShare.GetComponent<Animator> ();


	}
	
	// Update is called once per frame
	void Update () {
		bool activeShare = false;

		float x = otherPlayer.position.x - player.position.x;

		if (cam.getRenderMainCamera ()) {
			if(x > 0) isRight = true;
			else isRight = false;

			if((otherPlayer.position-player.position).sqrMagnitude <= 50) activeShare = true;
		} else {
			if(x > 0) isRight = false;
			else isRight = true;
		}

		if (!previousActiveShare && activeShare && anim.GetBool("Active")) {
			audio.Stop();
			audio.clip = popUpShare;
			audio.Play();
		}
		previousActiveShare = activeShare;

		anim.SetBool ("IsRight", isRight);
		animShare.SetBool ("Active", activeShare);


	}
}
