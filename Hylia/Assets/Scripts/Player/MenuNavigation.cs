using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour {

	public AudioClip popUp;
	public AudioClip popOut;
	public AudioClip moveCursor;

	int itemSelectedIndex = 0;
	bool active = false;
	Transform[] items;
	Animator anim;
	AudioSource audio;
	private int id;


	void setId(int myId) {
		id = myId;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		Transform menu = transform.Find ("Menu");
		items = new Transform[menu.childCount];
		int index = 0;
		foreach (Transform item in menu) {
			items [index] = item;
			++index;
		}
		items[itemSelectedIndex].GetComponent<Image>().color = Color.white;


	}
	bool up = false;
	bool down = false;
	bool right = false;
	bool left = false;
	// Update is called once per frame
	void Update () {
		if (active) {
			float xAxis = Input.GetAxis ("360_HorPAD"+id);
			float yAxis = Input.GetAxis ("360_VerPAD"+id);

			int sum = 0;
			if(yAxis == 1) {
				if(!up)	sum += -3;
				up = true;
			} else up = false;

			if(yAxis == -1) {
				if(!down) sum += 3;
				down = true;
			} else down = false;

			if(xAxis == 1) {
				if(!right) sum += 1;
				right = true;
			} else right = false;

			if(xAxis == -1) {
				if(!left) sum += -1;
				left = true;
			} else left = false;

			if(sum != 0) {
				items[itemSelectedIndex].GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,1f);
				itemSelectedIndex = (items.Length + itemSelectedIndex + sum)%items.Length;
				items[itemSelectedIndex].GetComponent<Image>().color = Color.white;

				audio.Stop();
				audio.volume = 1;
				audio.clip = moveCursor;
				audio.Play();
			}
		}

		if (Input.GetButtonDown ("360_Start"+id)) {
			active = !active;

			audio.Stop();
			audio.volume = 1;
			audio.clip = (active) ? popUp : popOut;
			audio.Play();

		}
		anim.SetBool ("Active", active);
	}

	public bool isActive() {
		return active;
	}

	public Sprite getItemSelected() {

		return (active) ? items[itemSelectedIndex].GetComponent<Image>().sprite : null;
	}

	public void setItemSelected(Sprite spr) {		
		items[itemSelectedIndex].GetComponent<Image>().sprite = spr;
	}
}
