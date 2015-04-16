using UnityEngine;
using System.Collections;

public class TestItem : MonoBehaviour {
	public GameObject prefab;
	Movement mov;
	private string id;


	void setId(string myId) {
		id = myId;
	}

	// Use this for initialization
	void Start () {
		mov = GetComponent<Movement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("360_A"+id)) {
			GameObject item = (GameObject) Instantiate(prefab, transform.position, transform.rotation);

			Vector3 direction = mov.getWalkDirection();
			if(direction.magnitude == 0) {
				Movement.LookDirection dir = mov.getLooking();
				if(dir == Movement.LookDirection.Up)
					item.GetComponent<Boomerang>().InitBoomerang(transform, transform.up);
				if(dir == Movement.LookDirection.Down)
					item.GetComponent<Boomerang>().InitBoomerang(transform, -transform.up);
				if(dir == Movement.LookDirection.Left)
					item.GetComponent<Boomerang>().InitBoomerang(transform, -transform.right);
				if(dir == Movement.LookDirection.Right)
					item.GetComponent<Boomerang>().InitBoomerang(transform, transform.right);
			} else {
				item.GetComponent<Boomerang>().InitBoomerang(transform, direction.normalized);
			}
		}


	}
}
