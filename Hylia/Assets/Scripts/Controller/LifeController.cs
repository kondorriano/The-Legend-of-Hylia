using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LifeController : MonoBehaviour {

	public int hearts;
	public RectTransform heartContainer;
	public Sprite[] heartSprites;
	int lifePoints;
	Image[] heartContainers;

	bool active = true;
	Animator anim;

	private int id;
	void setId(int myId) {
		id = myId;
	}
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		hearts = Mathf.Clamp (hearts, 1,10);
		heartContainers = new Image[hearts];
		for (int i = 0; i < hearts; ++i) {
			Vector3 position = heartContainer.anchoredPosition;
			if(i < 5) position.y = -1;
			else position.y = 1;
			position.x = 2*(i%5);

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

	void addLifePoints(int points) {
		lifePoints += points;
		lifePoints = Mathf.Clamp (lifePoints, 0, hearts * 4);
		updateHearts ();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.LeftControl))addLifePoints(-1);
		if(Input.GetKeyDown(KeyCode.RightControl))addLifePoints(1);

		if (Input.GetButtonDown ("360_Select"+id)) {
			active = !active;			
		}
		anim.SetBool ("Active", active);
	}
}
