using UnityEngine;
using System.Collections;

public class ColorManager : MonoBehaviour {
	Color myColor;
	SpriteRenderer myRenderer;
	EquipedItem eI;
	PlayerHurt pH;


	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<SpriteRenderer> ();
		eI = GetComponent<EquipedItem> ();
		pH = GetComponent<PlayerHurt> ();

		myColor = myRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
		Color mainColor = myColor;
		if(eI.getSunMode()) mainColor = Utils.sunColor;
		else if(eI.getMoonMode()) mainColor = Utils.moonColor;

		if (pH.getHurted ())
			mainColor = Color.Lerp (mainColor, Utils.hurtColor, 0.5f);
		myRenderer.color = mainColor;

	}
}
