using UnityEngine;
using System.Collections;

public class EquipedItem : MonoBehaviour {

	public AudioClip activatePearl;
	public AudioClip deactivatePearl;
	public AudioClip dropBomb;

	public LayerMask lightLayer;
	private bool moonMode = false;
	private bool sunMode = false;
	public float pearlUseTime = 0.4f;
	float pearlCounter = 0;

	private bool haveBoomerang = true;


	Movement mov;
	MenuNavigation menu;
	SecondMenuController menu2;
	Shield shield;
	Bow bow;

	AudioSource myAudio;
	Utils.ItemType myItem;
	private int id;

	protected delegate void TypeCallback(); 
	protected TypeCallback[] typeCallbacks = new TypeCallback[(int) Utils.ItemType.Length];

	void setId(int myId) {
		id = myId;
	}

	// Use this for initialization
	void Start () {
		mov = GetComponent<Movement> ();
		menu = transform.Find ("Canvas").GetComponent<MenuNavigation> ();
		menu2 = transform.Find ("StuffCanvas").GetComponent<SecondMenuController> ();

		shield = transform.Find ("Shield").GetComponent<Shield> ();
		bow = transform.Find ("BowItem").GetComponent<Bow> ();

		myAudio = GetComponent<AudioSource> ();
		InitCallBacks ();

	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<PlayerDeadControl> ().getDead ()) {
			setMoonMode(false);
			setSunMode(false);
			return;
		}

		myItem = (Utils.ItemType) menu.getItemEquipedId ();

		TypeCallback callback = typeCallbacks [(int) myItem];
		if (callback != null) callback();
	}

	void InitCallBacks () {
		for (int i = 0; i < typeCallbacks.Length; i++)
			typeCallbacks[i] = null;
		
		typeCallbacks[(int)Utils.ItemType.None] = new TypeCallback(OnNone);
		typeCallbacks[(int)Utils.ItemType.Boomerang] = new TypeCallback(OnBoomerang);
		typeCallbacks[(int)Utils.ItemType.Bombs] = new TypeCallback(OnBombs);
		typeCallbacks[(int)Utils.ItemType.Bow] = new TypeCallback(OnBow);
		typeCallbacks[(int)Utils.ItemType.MirrorShield] = new TypeCallback(OnMirrorShield);
		typeCallbacks[(int)Utils.ItemType.MoonPearl] = new TypeCallback(OnMoonPearl);
		typeCallbacks[(int)Utils.ItemType.SunPearl] = new TypeCallback(OnSunPearl);
	}

	protected void OnNone()
	{
		
	}

	public void activateBoomerang () {
		haveBoomerang = true;
	}

	public bool getBoomerang () {
		return haveBoomerang;
	}

	protected void OnBoomerang()
	{
		if (Input.GetButtonDown ("360_A"+id) && haveBoomerang) {
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

			haveBoomerang = false;
		}
	}

	protected void OnBombs()
	{
		if (Input.GetButtonDown ("360_A"+id)) {
			if(GeneralCanvasController.bombs <= 0) return;

			--GeneralCanvasController.bombs;
			Vector3 offset = transform.TransformPoint(GetComponent<OffsetPositionObject> ().getOffsetPosition ());
			Vector3 bombPosition = offset; 
			
			GameObject item = (GameObject) Instantiate(Items.itemList[(int) myItem].prefab, bombPosition, transform.rotation);
			item.transform.SetParent(transform.parent);
			myAudio.Stop();
			myAudio.clip = dropBomb;
			myAudio.Play();
		}
	}

	public void disableBow() {
		bow.setBow (false);
	}

	protected void OnBow()
	{
		bow.setBow (true);

		if (Input.GetButtonDown ("360_A"+id)) {
			if(GeneralCanvasController.arrows <= 0) return;

			--GeneralCanvasController.arrows;
			Vector2 direction = mov.getWalkDirection();
			Vector2 arrowDirection = Vector2.zero;

			if(direction.magnitude == 0) {
				Movement.LookDirection dir = mov.getLooking();
				if(dir == Movement.LookDirection.Up) {
					arrowDirection = transform.up;
				}
				if(dir == Movement.LookDirection.Down) {
					arrowDirection = -transform.up;
				}
				if(dir == Movement.LookDirection.Left)
					arrowDirection = -transform.right;
				if(dir == Movement.LookDirection.Right)
					arrowDirection = transform.right;
			} else {
				arrowDirection = direction.normalized;
			}
			
			GameObject item = (GameObject) Instantiate(Items.itemList[(int) myItem].prefab, bow.transform.position, transform.rotation);
			item.GetComponent<Arrow>().InitArrow(transform,arrowDirection);
			item.transform.SetParent(transform.parent);
		}
	}

	public void disableShield() {
		shield.setShield (false);
	}

	protected void OnMirrorShield()
	{
		shield.setShield (true);
	}

	public bool getMoonMode() {
		return moonMode;
	}

	public void setMoonMode(bool moon) {
		if(moonMode == moon) return;
		moonMode = moon;
		if (menu2.getMagicPoints () <= 0) moonMode = false;

		if (moonMode) {
			menu2.addMagicPoints(-5);
			pearlCounter = pearlUseTime;

			GetComponent<LightableObject> ().removeLightableObject ();

			GetComponent<Movement>().setSpeed(1.6f);
			myAudio.Stop();
			myAudio.clip = activatePearl;
			myAudio.Play();

		} else {
			GetComponent<LightableObject> ().addLightableObject ();
			GetComponent<Movement>().setSpeed(1);
			myAudio.Stop();
			myAudio.clip = deactivatePearl;
			myAudio.Play();
		}
	}

	protected void OnMoonPearl()
	{
		if (Input.GetButtonDown ("360_A" + id)) setMoonMode(!moonMode);

		if (moonMode) {

			pearlCounter -= Time.deltaTime;
			if(pearlCounter <= 0) {
				menu2.addMagicPoints(-2);
				pearlCounter += pearlUseTime;
			}

			bool onShadow = true;

			PolygonCollider2D myMesh = GetComponent<PolygonCollider2D> ();

			for (int i = 0; i < myMesh.GetTotalPointCount() && onShadow; i++) {
				Vector2 localPoint = myMesh.points [i];
				localPoint = localPoint + (localPoint - (Vector2)GetComponent<OffsetPositionObject> ().getOffsetPosition ()).normalized * 0.01f;

				Vector2 worldPoint = myMesh.transform.TransformPoint (localPoint);

				Vector3 position = new Vector3 (worldPoint.x, worldPoint.y, LightController.lightPosition - 1);
				Debug.DrawRay (position, transform.forward * 2);
				
				if (Physics.Raycast (position, transform.forward, 20, lightLayer)) onShadow = false;
			}

			if(!onShadow) setMoonMode(false);
			if (menu2.getMagicPoints () <= 0) setMoonMode(false);


		}

	}

	public bool getSunMode() {
		return sunMode;
	}

	public void setSunMode(bool sun) {
		if(sunMode == sun) return;
		sunMode = sun;

		if (menu2.getMagicPoints () <= 0) sunMode = false;

		if (sunMode) {
			menu2.addMagicPoints(-5);
			pearlCounter = pearlUseTime;

			GetComponent<LightableObject> ().removeLightableObject ();

			GetComponent<Movement>().setSpeed(1.6f);
			myAudio.Stop();
			myAudio.clip = activatePearl;
			myAudio.Play();
		} else {
			GetComponent<LightableObject> ().addLightableObject ();
			GetComponent<Movement>().setSpeed(1);
			myAudio.Stop();
			myAudio.clip = deactivatePearl;
			myAudio.Play();
		}
	}

	protected void OnSunPearl()
	{
		if (Input.GetButtonDown ("360_A" + id)) setSunMode(!sunMode);
		
		if (sunMode) {

			pearlCounter -= Time.deltaTime;
			if(pearlCounter <= 0) {
				menu2.addMagicPoints(-2);
				pearlCounter += pearlUseTime;
			}

			bool onLight = false;
			
			PolygonCollider2D myMesh = GetComponent<PolygonCollider2D> ();
			
			for (int i = 0; i < myMesh.GetTotalPointCount() && !onLight; i++) {
				Vector2 localPoint = myMesh.points [i];
				localPoint = localPoint + (localPoint - (Vector2)GetComponent<OffsetPositionObject> ().getOffsetPosition ()).normalized * 0.01f;
				
				Vector2 worldPoint = myMesh.transform.TransformPoint (localPoint);
				
				Vector3 position = new Vector3 (worldPoint.x, worldPoint.y, LightController.lightPosition - 1);
				Debug.DrawRay (position, transform.forward * 2);
				
				if (Physics.Raycast (position, transform.forward, 20, lightLayer)) onLight = true;
			}
			
			if(!onLight) setSunMode(false);
			if (menu2.getMagicPoints () <= 0) setSunMode(false);


			
		}
	}
}
