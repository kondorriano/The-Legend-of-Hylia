using UnityEngine;
using System.Collections;

public class ButtonSwitch : MonoBehaviour {


	public Utils.ColliderType affectedBy = Utils.ColliderType.All;
	bool active = false;
	ButtonEvent[] myEvent;

	void Start() {
		myEvent = GetComponents<ButtonEvent> ();
	}
	
	void OnTriggerEnter2D(Collider2D c) {
		ItemCollider iC = c.gameObject.GetComponent<ItemCollider>();
		if(iC == null) return;

		if (affectedBy == Utils.ColliderType.All || iC.myColliderType == Utils.ColliderType.All || affectedBy == iC.myColliderType) {
			active = !active;
			GetComponent<Animator> ().SetBool ("Active", active);
			GetComponent<AudioSource> ().Play ();
			activateStuff ();
		}

		
	}

	void activateStuff() {
		if (myEvent.Length < 2)
			return;
		if (active) {
			if (myEvent[0] != null)
				myEvent[0].performEvent ();
		} else {
			if (myEvent[1] != null)
				myEvent[1].performEvent ();
		}
	}

	
	
}
