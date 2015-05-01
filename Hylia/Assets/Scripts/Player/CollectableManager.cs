using UnityEngine;
using System.Collections;

public class CollectableManager : MonoBehaviour {

	public AudioClip keyAudio;
	public AudioClip itemAudio;
	public AudioClip magicAudio;
	public AudioClip rupeeAudio;



	SecondMenuController menu;

	AudioSource myAudio;

	protected delegate void TypeCallback(); 
	protected TypeCallback[] addCollectableCallbacks = new TypeCallback[(int) CollectableItem.CollectableType.Length];

	int quantity;
	float magicCounter = 0;
	float lifeCounter = 0;

	// Use this for initialization
	void Start () {
		menu = transform.Find ("StuffCanvas").GetComponent<SecondMenuController> ();

		myAudio = GetComponent<AudioSource> ();
		InitCallBacks ();

	}

	void InitCallBacks () {
		for (int i = 0; i < addCollectableCallbacks.Length; i++) {
			addCollectableCallbacks [i] = null;
		}
		
		addCollectableCallbacks[(int)CollectableItem.CollectableType.Key] = new TypeCallback(AddKey);
		addCollectableCallbacks[(int)CollectableItem.CollectableType.Rupee] = new TypeCallback(AddRupee);
		addCollectableCallbacks[(int)CollectableItem.CollectableType.Bombs] = new TypeCallback(AddBomb);
		addCollectableCallbacks[(int)CollectableItem.CollectableType.Arrow] = new TypeCallback(AddArrow);
		addCollectableCallbacks[(int)CollectableItem.CollectableType.Magic] = new TypeCallback(AddMagic);
		addCollectableCallbacks[(int)CollectableItem.CollectableType.Heart] = new TypeCallback(AddHeart);
	}

	protected void AddKey()
	{
		menu.addKey ();
		myAudio.Stop();
		myAudio.clip = keyAudio;
		myAudio.Play();
	}

	protected void AddRupee()
	{
		GeneralCanvasController.rupees += quantity;
		myAudio.Stop();
		myAudio.clip = rupeeAudio;
		myAudio.Play();
	}

	protected void AddBomb()
	{
		GeneralCanvasController.bombs += quantity;
		myAudio.Stop();
		myAudio.clip = itemAudio;
		myAudio.Play();
	}

	protected void AddArrow()
	{
		GeneralCanvasController.arrows += quantity;
		myAudio.Stop();
		myAudio.clip = itemAudio;
		myAudio.Play();

	}

	protected void AddMagic()
	{
		menu.addMagicPoints (quantity);
		myAudio.Stop();
		myAudio.clip = magicAudio;
		myAudio.Play();
	}

	protected void AddHeart()
	{
		menu.addLifePoints (quantity*4);
		myAudio.Stop();
		myAudio.clip = itemAudio;
		myAudio.Play();
	}

	public void addCollectable(int id, int itemQuantity) {
		quantity = itemQuantity;

		TypeCallback callback = addCollectableCallbacks [id];
		if (callback != null) callback();
	}

	public void addMagicThroughTime(int id, int itemQuantity, float time) {
		if(magicCounter > 0) return;
		quantity = itemQuantity;		
		AddMagic ();
		magicCounter = time;
	}

	public void addLifeThroughTime(int id, int itemQuantity, float time) {
		if(lifeCounter > 0) return;
		quantity = itemQuantity;		
		AddHeart ();
		lifeCounter = time;
	}

	void Update() {
		if (magicCounter > 0) {
			magicCounter -= Time.deltaTime;
		}

		if (lifeCounter > 0) {
			lifeCounter -= Time.deltaTime;
		}
	}


}
