using UnityEngine;
using System.Collections;

public class ColorManager : MonoBehaviour {
	Color moonColor = Color.black;
	Color sunColor = Color.yellow;
	Color hurtColor = Color.red;
	Color myColor;
	SpriteRenderer myRenderer;
	EquipedItem eI;
	PlayerHurt pH;


	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<SpriteRenderer> ();
		eI = GetComponent<EquipedItem> ();
		pH = GetComponent<PlayerHurt> ();

		moonColor.a = 0.5f;
		sunColor.a = 0.5f;
		hurtColor.a = 0.5f;

		myColor = myRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
		Color mainColor = myColor;
		if(eI.getSunMode()) mainColor = sunColor;
		else if(eI.getMoonMode()) mainColor = moonColor;

		if (pH.getHurted ())
			mainColor = Color.Lerp (mainColor, hurtColor, 0.5f);
		myRenderer.color = mainColor;

	}
}
