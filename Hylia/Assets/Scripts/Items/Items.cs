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

	void Awake() {
		itemList[0] = new Item (Resources.Load<Sprite> (spritePath + "None"), Resources.Load<GameObject> (prefabPath + "None"));
		itemList[1] = new Item (Resources.Load<Sprite> (spritePath + "Boomerang"), Resources.Load<GameObject> (prefabPath + "Boomerang"));
		itemList[2] = new Item (Resources.Load<Sprite> (spritePath + "Bombs"), Resources.Load<GameObject> (prefabPath + "Bombs"));
		itemList[3] = new Item (Resources.Load<Sprite> (spritePath + "Bow"), Resources.Load<GameObject> (prefabPath + "Bow"));
		itemList[4] = new Item (Resources.Load<Sprite> (spritePath + "MirrorShield"), Resources.Load<GameObject> (prefabPath + "MirrorShield"));
		itemList[5] = new Item (Resources.Load<Sprite> (spritePath + "MoonPearl"), Resources.Load<GameObject> (prefabPath + "MoonPearl"));
		itemList[6] = new Item (Resources.Load<Sprite> (spritePath + "SunPearl"), Resources.Load<GameObject> (prefabPath + "SunPearl"));
	}

}
