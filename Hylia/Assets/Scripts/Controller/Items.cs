using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {



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


	public static Item[] itemList = new Item[(int) Utils.ItemType.Length];

	bool condition;
	Transform player;

	protected delegate void TypeCallback(); 
	protected TypeCallback[] itemConditionCallbacks = new TypeCallback[(int) Utils.ItemType.Length];
	protected TypeCallback[] itemPreparationCallbacks = new TypeCallback[(int) Utils.ItemType.Length];

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
		
		itemConditionCallbacks[(int)Utils.ItemType.None] = new TypeCallback(NoneCondition);
		itemConditionCallbacks[(int)Utils.ItemType.Boomerang] = new TypeCallback(BoomerangCondition);
		itemConditionCallbacks[(int)Utils.ItemType.Bombs] = new TypeCallback(BombsCondition);
		itemConditionCallbacks[(int)Utils.ItemType.Bow] = new TypeCallback(BowCondition);
		itemConditionCallbacks[(int)Utils.ItemType.MirrorShield] = new TypeCallback(MirrorShieldCondition);
		itemConditionCallbacks[(int)Utils.ItemType.MoonPearl] = new TypeCallback(MoonPearlCondition);
		itemConditionCallbacks[(int)Utils.ItemType.SunPearl] = new TypeCallback(SunPearlCondition);

		itemPreparationCallbacks[(int)Utils.ItemType.None] = new TypeCallback(NonePreparation);
		itemPreparationCallbacks[(int)Utils.ItemType.Boomerang] = new TypeCallback(BoomerangPreparation);
		itemPreparationCallbacks[(int)Utils.ItemType.Bombs] = new TypeCallback(BombsPreparation);
		itemPreparationCallbacks[(int)Utils.ItemType.Bow] = new TypeCallback(BowPreparation);
		itemPreparationCallbacks[(int)Utils.ItemType.MirrorShield] = new TypeCallback(MirrorShieldPreparation);
		itemPreparationCallbacks[(int)Utils.ItemType.MoonPearl] = new TypeCallback(MoonPearlPreparation);
		itemPreparationCallbacks[(int)Utils.ItemType.SunPearl] = new TypeCallback(SunPearlPreparation);
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
		player.GetComponent<EquipedItem> ().disableShield ();
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
