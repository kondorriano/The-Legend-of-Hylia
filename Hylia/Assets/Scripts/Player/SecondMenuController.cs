using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SecondMenuController : MonoBehaviour {

	public int hearts;
	public RectTransform heartContainer;
	public Sprite[] heartSprites;
	public float showHeartsTime = 1f;
	int lifePoints;
	Image[] heartContainers;

	public int magic = 32;
	private int magicPoints;	
	Image magicBar;

	private int keys = 0;
	Text keyNumber;

	bool active = true;
	float counter = 0;
	Animator anim;

	private int id;
	void setId(int myId) {
		id = myId;
	}
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		InitLife ();
		InitMagic();
		InitKeys ();
	}

	void InitLife() {
		hearts = Mathf.Clamp (hearts, 1,8);
		heartContainers = new Image[hearts];
		for (int i = 0; i < hearts; ++i) {
			Vector3 position = heartContainer.anchoredPosition;
			if(i < 4) position.y = 1;
			else position.y = 3;
			position.x = -2*(3-(i%4));
			
			GameObject heart = (GameObject) Instantiate(heartContainer.gameObject, position, heartContainer.rotation);
			heart.transform.SetParent(transform, false);
			heartContainers[i] = heart.GetComponent<Image>();
			Color col = Color.white;
			col.a = 0.75f;
			heartContainers[i].color = col;
		}
		lifePoints = hearts * 4;
		updateHearts ();
	}

	void updateHearts() {
		for (int i = 1; i <= heartContainers.Length; ++i) {
			if(lifePoints > (i*4)-1) heartContainers[i-1].sprite = heartSprites[4];
			else if(lifePoints < (i*4)-3) heartContainers[i-1].sprite = heartSprites[0];
			else heartContainers[i-1].sprite = heartSprites[lifePoints%4];
		}
	}

	public void addLifePoints(int points) {
		lifePoints += points;
		lifePoints = Mathf.Clamp (lifePoints, 0, hearts * 4);
		updateHearts ();

		transform.parent.GetComponent<PlayerDeadControl>().setDead(lifePoints);

		if (!active) {
			active = true;
			counter = showHeartsTime;
		}
	}

	public int getLifePoints() {
		return lifePoints;
	}

	public int getMaxLifePoints() {
		return hearts * 4;
	}

	void InitMagic(){
		magicBar = transform.Find ("MagicBar").GetComponent<Image> ();
		magicPoints = magic;
		updateMagic ();
	}

	void updateMagic() {
		magicBar.fillAmount = (float) magicPoints / (float) magic;
	}
	
	public void addMagicPoints(int points) {
		magicPoints += points;
		magicPoints = Mathf.Clamp (magicPoints, 0, magic);
		updateMagic ();

		if (!active) {
			active = true;
			counter = showHeartsTime;
		}
	}

	void InitKeys() {
		keyNumber = transform.Find ("KeyNumber").GetComponent<Text> ();
	}

	void updateKeys () {
		keyNumber.text = "" + (keys / 10) + "" + (keys % 10);
	}
	
	public void addKey() {
		++keys;
		keys = Mathf.Clamp (keys, 0, 99);
		updateKeys ();

		if (!active) {
			active = true;
			counter = showHeartsTime;
		}
	}
	
	public bool useKey() {
		if (keys > 0) {
			--keys;
			updateKeys ();

			if (!active) {
				active = true;
				counter = showHeartsTime;
			}
			return true;
		}		
		return false;
	}
	
	public int getKeys() {
		return keys;
	}
	

	void Update() {
		if(Input.GetKeyDown(KeyCode.LeftControl)) addMagicPoints(-10);
		if(Input.GetKeyDown(KeyCode.RightControl)) addMagicPoints(10);

		if(Input.GetKeyDown(KeyCode.LeftControl)) addLifePoints(-1);
		if(Input.GetKeyDown(KeyCode.RightControl)) addLifePoints(1);

		if(Input.GetKeyDown(KeyCode.LeftControl)) useKey();
		if(Input.GetKeyDown(KeyCode.RightControl)) addKey();



		if (counter != 0) {
			counter -= Time.deltaTime;
			if(counter <= 0) {
				active = false;
				counter = 0;
			}
		}

		if (Input.GetButtonDown ("360_Select"+id)) {
			active = !active;
			counter = 0;
		}
		anim.SetBool ("Active", active);
	}
}
