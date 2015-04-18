using UnityEngine;
using System.Collections;

public class MenusControl : MonoBehaviour {

	public AudioClip popUpShare;


	private int id;

	bool isRight = false;
	Transform otherPlayer;
	Transform player;
	CameraDivisionEffect cam;
	Animator anim;
	AudioSource myAudio;


	bool previousActiveShare = false;
	Animator animShare;



	void setId(int myId) {
		id = myId;
	}
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		myAudio = GetComponent<AudioSource> ();

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
			if((otherPlayer.position-player.position).sqrMagnitude <= 10) activeShare = true;
		}
		if(x > 0) isRight = false;
		else isRight = true;

		if (!previousActiveShare && activeShare && anim.GetBool("Active")) {
			myAudio.Stop();
			myAudio.volume = 0.5f;
			myAudio.clip = popUpShare;
			myAudio.Play();
		}
		previousActiveShare = activeShare;

		anim.SetBool ("IsRight", isRight);
		animShare.SetBool ("Active", activeShare);


	}

	public bool isActiveShare() {
		return previousActiveShare;
	}
}
