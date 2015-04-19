using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PolygonCollider2D))]
public class LightableObject : MonoBehaviour {

	bool active = true;

	// Use this for initialization
	void Start () {
		active = !active;
		if(!active) addLightableObject ();
		else removeLightableObject();
	}


	public void addLightableObject() {
		if(active) return;
		LightController.lightableObjectsList.Add(transform);
		gameObject.layer = 9; //Lightable
		active = true;
	}

	public void removeLightableObject() {
		if(!active) return;
		LightController.lightableObjectsList.Remove (transform);
		gameObject.layer = 0; //Default
		active = false;

	}

}
