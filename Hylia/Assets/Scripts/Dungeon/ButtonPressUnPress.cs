using UnityEngine;
using System.Collections;

public class ButtonPressUnPress : MonoBehaviour {
	
	bool active = false;
	int numberPressing = 0;
	ButtonEvent[] myEvent;

	void Start() {
		myEvent = GetComponents<ButtonEvent> ();
	}


	void OnTriggerEnter2D(Collider2D c) {
		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2" || c.gameObject.tag == "Block") {
			++numberPressing;
			if(numberPressing == 1) {
				active = true;
				GetComponent<Animator> ().SetBool ("Active", active);
				GetComponent<AudioSource> ().Play ();
				activateStuff ();
			}
		}		



		
	}

	void OnTriggerExit2D(Collider2D c) {
		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2" || c.gameObject.tag == "Block") {
			--numberPressing;
			if(numberPressing == 0) {
				active = false;
				GetComponent<Animator> ().SetBool ("Active", active);
				activateStuff ();
			}
		}		



		
	}

	protected void activateStuff() {
		if (myEvent.Length < 1)
			return;
		if (myEvent.Length == 1) {
			if (myEvent[0] != null)
				myEvent[0].performEvent ();
			return;
		}

		if (active) {
			if (myEvent[0] != null)
				myEvent[0].performEvent ();
		} else {
			if (myEvent[1] != null)
				myEvent[1].performEvent ();
		}
	}
	
	
}
