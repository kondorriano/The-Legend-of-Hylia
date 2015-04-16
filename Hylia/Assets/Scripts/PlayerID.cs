using UnityEngine;
using System.Collections;

public class PlayerID : MonoBehaviour {
	public string id = "1";
	// Use this for initialization
	void Awake () {
		gameObject.BroadcastMessage ("setId", id);
	}
}
