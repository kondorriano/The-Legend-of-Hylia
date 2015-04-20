using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {

	public enum ItemType {
		None = 0,
		Boomerang = 1,
		Bombs = 2,
		Bow = 3,
		MirrorShield = 4,
		MoonPearl = 5,
		SunPearl = 6,
		Length = 7		
	}

	[System.Serializable]
	public class Item	{
		public Sprite menuSprite;
		public GameObject prefab;

		public Item(Sprite sprite, GameObject pref)
		{
			menuSprite = sprite;
			prefab = pref;
		}
	};

	private string prefabPath = "";
	private string spritePath = "";


	public static Item[] itemList = new Item[(int) ItemType.Length];

	bool condition;
	Transform player;

	protected delegate void TypeCallback(); 
	protected TypeCallback[] itemConditionCallbacks = new TypeCallback[(int) ItemType.Length];
	protected TypeCallback[] itemPreparationCallbacks = new TypeCallback[(int) ItemType.Length];

	void Awake() {
		itemList[0] = new Item (Resources.Load<Sprite> (spritePath + "None"), Resources.Load<GameObject> (prefabPath + "None"));
		itemList[1] = new Item (Resources.Load<Sprite> (spritePath + "Boomerang"), Resources.Load<GameObject> (prefabPath + "Boomerang"));
		itemList[2] = new Item (Resources.Load<Sprite> (spritePath + "Bombs"), Resources.Load<GameObject> (prefabPath + "Bombs"));
		itemList[3] = new Item (Resources.Load<Sprite> (spritePath + "Bow"), Resources.Load<GameObject> (prefabPath + "Bow"));
		itemList[4] = new Item (Resources.Load<Sprite> (spritePath + "MirrorShield"), Resources.Load<GameObject> (prefabPath + "MirrorShield"));
		itemList[5] = new Item (Resources.Load<Sprite> (spritePath + "MoonPearl"), Resources.Load<GameObject> (prefabPath + "MoonPearl"));
		itemList[6] = new Item (Resources.Load<Sprite> (spritePath + "SunPearl"), Resources.Load<GameObject> (prefabPath + "SunPearl"));

		InitCallBacks ();

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

	public bool getItemCondition(int itemId, Transform myPlayer) {
		condition = false;
		player = myPlayer;
		TypeCallback callback = itemConditionCallbacks [itemId];
		if (callback != null) callback();

		return condition;
	}

	public void setItemPreparation(int itemId, Transform myPlayer) {
		player = myPlayer;
		TypeCallback callback = itemPreparationCallbacks [itemId];
		if (callback != null) callback();
	}

}
