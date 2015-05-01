using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GeneralCanvasController : MonoBehaviour {

	public static int rupees = 0;
	public static int bombs = 15;
	public static int arrows = 25;

	Text rupeesText;
	Text bombsText;
	Text arrowsText;

	void Start() {
		rupeesText = transform.Find("Canvas/Rupees/Text").GetComponent<Text>();
		bombsText = transform.Find("Canvas/Bombs/Text").GetComponent<Text>();
		arrowsText = transform.Find("Canvas/Arrows/Text").GetComponent<Text>();
	}

	void Update() {
		rupees = Mathf.Clamp (rupees, 0, 999);
		bombs = Mathf.Clamp (bombs, 0, 99);
		arrows = Mathf.Clamp (arrows, 0, 99);

		rupeesText.text = "" + (rupees / 100) + "" + ((rupees / 10)%10) + "" + (rupees % 10);
		bombsText.text = "" + (bombs / 10) + "" + (bombs % 10);
		arrowsText.text = "" + (arrows / 10) + "" + (arrows % 10);
	}
}
