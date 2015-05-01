using UnityEngine;
using System.Collections;

public class SpawnItems : MonoBehaviour {

	public Transform[] items;
	public float minTime = 3f;

	public float maxTime = 9f;
	public Vector3 offset = Vector3.zero;

	float counter;

	Transform spawnedItem;



	// Use this for initialization
	void Start () {
		counter = Random.Range (minTime, maxTime);
		spawnedItem = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnedItem == null) {
			counter -= Time.deltaTime;
			if(counter <= 0) {
				Vector3 pos = transform.position+offset;
				pos.z = 0;
				spawnedItem = (Transform) Instantiate(items[Random.Range(0,items.Length)], pos, transform.rotation);
				counter = Random.Range (minTime, maxTime);
			}
		} else counter = Random.Range (minTime, maxTime);

	}
}
