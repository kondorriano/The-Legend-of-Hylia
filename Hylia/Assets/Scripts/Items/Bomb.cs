using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	public float timer = 3.0f;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0.0f){
			Destroy (gameObject);
			Instantiate(explosion, transform.position,Quaternion.identity);
		}
	}
}
