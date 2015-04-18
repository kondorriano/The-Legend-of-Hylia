using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuShareNavigation : MonoBehaviour {

	public AudioClip share;


	Image myItem;
	Image otherItem;

	MenuNavigation menu;
	MenuNavigation otherMenu;

	AudioSource myAudio;
	private int id;

	void setId(int myId) {
		id = myId;
	}
	// Use this for initialization
	void Start () {

		myAudio = GetComponent<AudioSource> ();

		myItem = transform.Find ("MenuShare/MyItem").GetComponent<Image> ();
		otherItem = transform.Find ("MenuShare/OtherItem").GetComponent<Image> ();

		menu = GetComponent<MenuNavigation> ();
		otherMenu = GameObject.FindGameObjectWithTag ("Player" + ((id%2)+1)).transform.Find("Canvas").GetComponent<MenuNavigation>();
	}
	
	// Update is called once per frame
	void Update () {
		int myId = menu.getItemSelectedId ();
		myItem.sprite = Items.itemList[myId].menuSprite;
		myItem.color = (myItem.sprite == null) ? new Color(1,1,1,0) : Color.white;

		int otherId = otherMenu.getItemSelectedId ();
		otherItem.sprite = Items.itemList[otherId].menuSprite;
		otherItem.color = (otherItem.sprite == null) ? new Color(1,1,1,0) : Color.white;

		if (Input.GetButtonDown ("360_Y" + id)) {
			if(menu.isActive() && otherMenu.isActive() && GetComponent<MenusControl>().isActiveShare()
			   && (myItem.sprite != null) && (otherItem.sprite != null)) {
				menu.setItemSelectedId(otherId);
				otherMenu.setItemSelectedId(myId);

				myAudio.Stop();
				myAudio.volume = 1;
				myAudio.clip = share;
				myAudio.Play();
			}
		}


	}
}
