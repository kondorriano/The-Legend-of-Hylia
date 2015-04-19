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

	protected delegate void TypeCallback(); 
	protected TypeCallback[] itemConditionCallbacks = new TypeCallback[(int) Items.ItemType.Length];
	protected TypeCallback[] itemPreparationCallbacks = new TypeCallback[(int) Items.ItemType.Length];

	bool condition;
	Transform player;


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
		InitCallBacks ();

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
			if(menu.isActive() && otherMenu.isActive() && GetComponent<MenusControl>().isActiveShare()) {
				condition = false;

				player = menu.transform.parent;
				TypeCallback callback = itemConditionCallbacks [myId];
				if (callback != null) callback();
				bool condition1 = condition;

				player = otherMenu.transform.parent;
				callback = itemConditionCallbacks [otherId];
				if (callback != null) callback();
				bool condition2 = condition;

				if(condition1 && condition2) {
					player = menu.transform.parent;
					callback = itemPreparationCallbacks [myId];
					if (callback != null) callback();

					player = otherMenu.transform.parent;
					callback = itemPreparationCallbacks [otherId];
					if (callback != null) callback();


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

	void InitCallBacks () {
		for (int i = 0; i < itemConditionCallbacks.Length; i++) {
			itemConditionCallbacks [i] = null;
			itemPreparationCallbacks [i] = null;
		}
		
		itemConditionCallbacks[(int)Items.ItemType.None] = new TypeCallback(NoneCondition);
		itemConditionCallbacks[(int)Items.ItemType.Boomerang] = new TypeCallback(BoomerangCondition);
		itemConditionCallbacks[(int)Items.ItemType.Bombs] = new TypeCallback(BombsCondition);
		itemConditionCallbacks[(int)Items.ItemType.Bow] = new TypeCallback(BowCondition);
		itemConditionCallbacks[(int)Items.ItemType.MirrorShield] = new TypeCallback(MirrorShieldCondition);
		itemConditionCallbacks[(int)Items.ItemType.MoonPearl] = new TypeCallback(MoonPearlCondition);
		itemConditionCallbacks[(int)Items.ItemType.SunPearl] = new TypeCallback(SunPearlCondition);

		itemPreparationCallbacks[(int)Items.ItemType.None] = new TypeCallback(NonePreparation);
		itemPreparationCallbacks[(int)Items.ItemType.Boomerang] = new TypeCallback(BoomerangPreparation);
		itemPreparationCallbacks[(int)Items.ItemType.Bombs] = new TypeCallback(BombsPreparation);
		itemPreparationCallbacks[(int)Items.ItemType.Bow] = new TypeCallback(BowPreparation);
		itemPreparationCallbacks[(int)Items.ItemType.MirrorShield] = new TypeCallback(MirrorShieldPreparation);
		itemPreparationCallbacks[(int)Items.ItemType.MoonPearl] = new TypeCallback(MoonPearlPreparation);
		itemPreparationCallbacks[(int)Items.ItemType.SunPearl] = new TypeCallback(SunPearlPreparation);
	}

	protected void NoneCondition()
	{
		condition = false;
	}
	protected void BoomerangCondition()
	{
		condition = player.GetComponent<EquipedItem> ().getBoomerang ();
	}
	protected void BombsCondition()
	{
		condition = true;
	}
	protected void BowCondition()
	{
		condition = true;
	}
	protected void MirrorShieldCondition()
	{
		condition = true;
	}
	protected void MoonPearlCondition()
	{
		condition = true;
	}
	protected void SunPearlCondition()
	{
		condition = true;
	}



	protected void NonePreparation()
	{
		
	}
	protected void BoomerangPreparation()
	{
		
	}
	protected void BombsPreparation()
	{
		
	}
	protected void BowPreparation()
	{
		
	}
	protected void MirrorShieldPreparation()
	{
		
	}
	protected void MoonPearlPreparation()
	{
		player.GetComponent<EquipedItem> ().setMoonMode (false);
	}
	protected void SunPearlPreparation()
	{
		player.GetComponent<EquipedItem> ().setSunMode (false);
	}


}
