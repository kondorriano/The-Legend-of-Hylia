using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour {

	public AudioClip popUp;
	public AudioClip popOut;
	public AudioClip moveCursor;

	public Items.ItemType[] menuItems;


	int itemSelectedIndex = 0;
	bool active = false;
	Image[] items;
	RectTransform select;

	Animator anim;
	AudioSource myAudio;
	private int id;


	void setId(int myId) {
		id = myId;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		myAudio = GetComponent<AudioSource> ();
		Transform menu = transform.Find ("Menu");
		items = new Image[menu.childCount-1];
		int index = 0;
		foreach (Transform item in menu) {
			if(item.name != "Select") {
				items [index] = item.GetComponent<Image>();
				items [index].sprite = Items.itemList[(int)menuItems[index]].menuSprite;
				float alpha = (items[index].sprite == null) ? 0 : 1;
				items[index].color = new Color(0.5f,0.5f,0.5f,alpha);
				++index;
			} else {
				select = (RectTransform) item;
			}
		}
		items[itemSelectedIndex].color = new Color(1f,1f,1f,items[itemSelectedIndex].color.a);


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
				if(!up)	sum += -1;
				up = true;
			} else up = false;

			if(yAxis == -1) {
				if(!down) sum += 1;
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
				items[itemSelectedIndex].color = new Color(0.5f,0.5f,0.5f,items[itemSelectedIndex].color.a);
				itemSelectedIndex = (items.Length + itemSelectedIndex + sum)%items.Length;
				items[itemSelectedIndex].color = new Color(1f,1f,1f,items[itemSelectedIndex].color.a);

				select.anchoredPosition = ((RectTransform) items[itemSelectedIndex].transform).anchoredPosition;


				myAudio.Stop();
				myAudio.volume = 1;
				myAudio.clip = moveCursor;
				myAudio.Play();
			}
		}

		if (Input.GetButtonDown ("360_Start"+id)) {
			active = !active;

			myAudio.Stop();
			myAudio.volume = 1;
			myAudio.clip = (active) ? popUp : popOut;
			myAudio.Play();

		}
		anim.SetBool ("Active", active);
	}

	public bool isActive() {
		return active;
	}

	public int getItemSelectedId() {
		return (active) ? (int) menuItems[itemSelectedIndex] : (int) Items.ItemType.None;
	}

	public void setItemSelectedId(int itemId) {	
		menuItems [itemSelectedIndex] = (Items.ItemType) itemId;
		items[itemSelectedIndex].sprite = Items.itemList[itemId].menuSprite;
		Color col = items [itemSelectedIndex].color;
		col.a = (items[itemSelectedIndex].sprite == null) ? 0 : 1;
		items [itemSelectedIndex].color = col;
	}

	public int getItemEquipedId() {
		return (int) menuItems[itemSelectedIndex];
	}
}
