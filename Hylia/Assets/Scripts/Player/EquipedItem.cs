﻿using UnityEngine;
using System.Collections;

public class EquipedItem : MonoBehaviour {

	public AudioClip activatePearl;
	public AudioClip deactivatePearl;

	public LayerMask lightLayer;
	private bool moonMode = false;
	private bool sunMode = false;


	Movement mov;
	MenuNavigation menu;
	AudioSource myAudio;
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
		myAudio = GetComponent<AudioSource> ();
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
		if (Input.GetButtonDown ("360_A"+id)) {
			Movement.LookDirection dir = mov.getLooking();
			Vector3 offset = new Vector3(0.0f,-0.50f,0.0f) ;;
			Vector3 bombPosition = transform.position + offset; 
			
			GameObject item = (GameObject) Instantiate(Items.itemList[(int) myItem].prefab, bombPosition, transform.rotation);
			item.GetComponent<Bomb>().InitBomb(transform);
			item.transform.SetParent(transform.parent);
		}
	}

	protected void OnBow()
	{
		
	}

	protected void OnMirrorShield()
	{
		
	}

	void setMoonMode(bool moon) {
		moonMode = moon;
		if (moonMode) {
			GetComponent<LightableObject> ().removeLightableObject ();
			Color col = Color.black;
			col.a = 0.5f;
			GetComponent<SpriteRenderer>().color = col;
			GetComponent<Movement>().setSpeed(1.6f);
			myAudio.Stop();
			myAudio.clip = activatePearl;
			myAudio.Play();

		} else {
			GetComponent<LightableObject> ().addLightableObject ();
			GetComponent<SpriteRenderer>().color = Color.white;
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
			Debug.Log("MoonMode");
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

		}

	}

	void setSunMode(bool sun) {
		sunMode = sun;
		if (sunMode) {
			GetComponent<LightableObject> ().removeLightableObject ();
			Color col = Color.yellow;
			col.a = 0.5f;
			GetComponent<SpriteRenderer>().color = col;
			GetComponent<Movement>().setSpeed(1.6f);
			myAudio.Stop();
			myAudio.clip = activatePearl;
			myAudio.Play();
		} else {
			GetComponent<LightableObject> ().addLightableObject ();
			GetComponent<SpriteRenderer>().color = Color.white;
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
			Debug.Log("sunMode");
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

			
		}
	}
}
