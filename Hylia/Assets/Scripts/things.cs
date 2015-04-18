using UnityEngine;
using System.Collections;

public class things : MonoBehaviour {
	public LayerMask lightLayer;

	MeshCollider mC;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (transform.position, transform.forward * 100);
		if(Physics.Raycast(transform.position,  transform.forward, 100, lightLayer)) {
			Debug.Log("Luz!");
		} else Debug.Log("Sombra!");


	}
}
