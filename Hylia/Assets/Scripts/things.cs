using UnityEngine;
using System.Collections;

public class things : MonoBehaviour {

	MeshCollider mC;
	// Use this for initialization
	void Start () {
		mC = GameObject.FindGameObjectWithTag ("Player").GetComponent<MeshCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray test = new Ray(transform.position, -transform.forward);
		Debug.DrawRay (transform.position, -transform.forward*10f);

		if(mC.Raycast(test, out hit, 10f)) {
			Debug.Log(hit.collider.gameObject.name);
		}


	}
}
