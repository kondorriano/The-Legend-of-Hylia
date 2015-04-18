using UnityEngine;
using System.Collections;

public class DepthSorting : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform child in transform) {
			Vector3 pos = child.localPosition;
			pos.z = pos.y * 0.25f;
			child.localPosition = pos;
		}
	}
}
