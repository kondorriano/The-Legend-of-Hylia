using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PolygonCollider2D))]
public class LightableItem : MonoBehaviour {

	bool active = true;

	// Use this for initialization
	void Start () {
		addLightableObject ();
	}


	public void addLightableObject() {
		LightController.lightableObjectsList.Add(transform);
	}

	public void removeLightableObject() {
		LightController.lightableObjectsList.Remove (transform);
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.LeftControl)) {
			if(active) removeLightableObject();
			else addLightableObject ();
			active = !active;
		}
	}
}
