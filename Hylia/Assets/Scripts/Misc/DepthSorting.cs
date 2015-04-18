using UnityEngine;
using System.Collections;

public class DepthSorting : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		foreach (Transform child in transform) {
			OffsetPositionObject oPO = child.GetComponent<OffsetPositionObject>();
			Vector3 pos = (oPO == null) ? child.position : child.TransformPoint (oPO.getOffsetPosition());

			pos = new Vector3(child.position.x, child.position.y, pos.y * 0.25f);
			child.position = pos;
		}
	}
}
