using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	public float timer = 3.0f;
	public GameObject explosion;

	Transform myPlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0.0f ){
			timer -= Time.deltaTime;
		}
		if (timer <= 0.0f){
			Destroy (gameObject);
			Instantiate(explosion, transform.position,Quaternion.identity);
		}
	}

	public void InitBomb(Transform player) {
		myPlayer = player;
	}
}
