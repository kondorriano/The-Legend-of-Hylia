using UnityEngine;
using System.Collections;

public class EnableEvent : ButtonEvent {

	public Transform enabledObject;
	bool enabled = false;

	public override void performEvent() {
		enabled = !enabled;
		enabledObject.GetComponent<Animator> ().SetBool ("Enabled", enabled);
	}
}
