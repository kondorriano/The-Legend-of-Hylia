using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuShareNavigation : MonoBehaviour {

	public AudioClip share;


	Image myItem;
	Image otherItem;

	MenuNavigation menu;
	MenuNavigation otherMenu;

	AudioSource audio;
	private int id;

	void setId(int myId) {
		id = myId;
	}
	// Use this for initialization
	void Start () {

		audio = GetComponent<AudioSource> ();

		myItem = transform.Find ("MenuShare/MyItem").GetComponent<Image> ();
		otherItem = transform.Find ("MenuShare/OtherItem").GetComponent<Image> ();

		menu = GetComponent<MenuNavigation> ();
		otherMenu = GameObject.FindGameObjectWithTag ("Player" + ((id%2)+1)).transform.Find("Canvas").GetComponent<MenuNavigation>();
	}
	
	// Update is called once per frame
	void Update () {
		myItem.sprite = menu.getItemSelected ();
		myItem.color = (myItem.sprite == null) ? new Color(1,1,1,0) : Color.white;

		otherItem.sprite = otherMenu.getItemSelected ();
		otherItem.color = (otherItem.sprite == null) ? new Color(1,1,1,0) : Color.white;

		if (Input.GetButtonDown ("360_Y" + id)) {
			if(menu.isActive() && otherMenu.isActive() && GetComponent<MenusControl>().isActiveShare()) {
				menu.setItemSelected(otherItem.sprite);
				otherMenu.setItemSelected(myItem.sprite);

				audio.Stop();
				audio.volume = 1;
				audio.clip = share;
				audio.Play();
			}
		}


	}
}
