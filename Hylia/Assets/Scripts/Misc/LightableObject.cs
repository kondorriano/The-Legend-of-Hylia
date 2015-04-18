using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PolygonCollider2D))]
public class LightableObject : MonoBehaviour {

	bool active = true;

	// Use this for initialization
	void Start () {
		if(active) addLightableObject ();
		else removeLightableObject();
	}


	public void addLightableObject() {
		LightController.lightableObjectsList.Add(transform);
		gameObject.layer = 0; //Default
	}

	public void removeLightableObject() {
		LightController.lightableObjectsList.Remove (transform);
		gameObject.layer = 2; //IgnoreRayCast
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.LeftControl)) {
			if(active) removeLightableObject();
			else addLightableObject ();
			active = !active;
		}
	}
}
