using UnityEngine;
using System.Collections;

public abstract class HitEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void hurt(int damage, Vector2 force, EnemyHit.EnemyAreaType area) {

	}


}
