using UnityEngine;
using System.Collections;

public class EquipedItem : MonoBehaviour {

	Movement mov;
	MenuNavigation menu;
	Items.ItemType myItem;
	private int id;

	protected delegate void TypeCallback(); 
	protected TypeCallback[] typeCallbacks = new TypeCallback[(int) Items.ItemType.Length];

	void setId(int myId) {
		id = myId;
	}

	// Use this for initialization
	void Start () {
		mov = GetComponent<Movement> ();
		menu = transform.Find ("Canvas").GetComponent<MenuNavigation> ();
		InitCallBacks ();

	}
	
	// Update is called once per frame
	void Update () {
		myItem = (Items.ItemType) menu.getItemEquipedId ();

		TypeCallback callback = typeCallbacks [(int) myItem];
		if (callback != null) callback();
	}

	void InitCallBacks () {
		for (int i = 0; i < typeCallbacks.Length; i++)
			typeCallbacks[i] = null;
		
		typeCallbacks[(int)Items.ItemType.None] = new TypeCallback(OnNone);
		typeCallbacks[(int)Items.ItemType.Boomerang] = new TypeCallback(OnBoomerang);
		typeCallbacks[(int)Items.ItemType.Bombs] = new TypeCallback(OnBombs);
		typeCallbacks[(int)Items.ItemType.Bow] = new TypeCallback(OnBow);
		typeCallbacks[(int)Items.ItemType.MirrorShield] = new TypeCallback(OnMirrorShield);
		typeCallbacks[(int)Items.ItemType.MoonPearl] = new TypeCallback(OnMoonPearl);
		typeCallbacks[(int)Items.ItemType.SunPearl] = new TypeCallback(OnSunPearl);
	}

	protected void OnNone()
	{
		
	}

	protected void OnBoomerang()
	{
		if (Input.GetButtonDown ("360_A"+id)) {
			Vector2 direction = mov.getWalkDirection();
			Vector2 boomerangDirection = Vector2.zero;
			Vector3 boomerangPosition = transform.position;

			if(direction.magnitude == 0) {
				Movement.LookDirection dir = mov.getLooking();
				if(dir == Movement.LookDirection.Up) {
					boomerangDirection = transform.up;
					boomerangPosition += transform.up*0.25f + transform.forward*0.25f;
				}
				if(dir == Movement.LookDirection.Down) {
					boomerangDirection = -transform.up;
					boomerangPosition += -transform.up*0.25f - transform.forward*0.25f;
				}
				if(dir == Movement.LookDirection.Left)
					boomerangDirection = -transform.right;
				if(dir == Movement.LookDirection.Right)
					boomerangDirection = transform.right;
			} else {
				boomerangDirection = direction.normalized;
			}

			GameObject item = (GameObject) Instantiate(Items.itemList[(int) myItem].prefab, boomerangPosition, transform.rotation);
			item.GetComponent<Boomerang>().InitBoomerang(transform, boomerangDirection);
			item.transform.SetParent(transform.parent);
		}
	}

	protected void OnBombs()
	{
		
	}

	protected void OnBow()
	{
		
	}

	protected void OnMirrorShield()
	{
		
	}

	protected void OnMoonPearl()
	{
		
	}

	protected void OnSunPearl()
	{
		
	}
}
