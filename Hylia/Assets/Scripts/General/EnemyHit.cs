using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour {

	public enum EnemyHitType {
		None = 0,
		Foot = 1,
		Body = 2,
		Both = 3	
	}

	public int power = 4;
	public float knockback = 5;
	public EnemyHitType hitReach;



	void OnTriggerEnter2D(Collider2D c) {
		Vector2 force;

		if (c.gameObject.tag == "PlayerCollider") {
			if(hitReach == EnemyHitType.Body || hitReach == EnemyHitType.Both) {
				force = (Vector2)(c.transform.position-transform.position);
				c.transform.parent.GetComponent<HitEvent> ().hurt (power, force.normalized*knockback);
			}
		} else if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			if(hitReach == EnemyHitType.Foot || hitReach == EnemyHitType.Both) {
				OffsetPositionObject oPO = c.transform.GetComponent<OffsetPositionObject>();

				force = (Vector2)(c.transform.TransformPoint (oPO.getOffsetPosition())-transform.position);
				c.GetComponent<HitEvent> ().hurt  (power, force.normalized*knockback);
			}
		}

	}

	void OnCollisionEnter2D(Collision2D c) {
		Vector2 force;

		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			if(hitReach == EnemyHitType.Foot || hitReach == EnemyHitType.Both) {
				OffsetPositionObject oPO = c.transform.GetComponent<OffsetPositionObject>();
				
				force = (Vector2)(c.transform.TransformPoint (oPO.getOffsetPosition())-transform.position);
				c.transform.GetComponent<HitEvent> ().hurt  (power, force.normalized*knockback);
			}
		}

	}

	void OnTriggerStay2D(Collider2D c) {
		Vector2 force;
		
		if (c.gameObject.tag == "PlayerCollider") {
			if(hitReach == EnemyHitType.Body || hitReach == EnemyHitType.Both) {
				force = (Vector2)(c.transform.position-transform.position);
				c.transform.parent.GetComponent<HitEvent> ().hurt (power, force.normalized*knockback);
			}
		} else if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			if(hitReach == EnemyHitType.Foot || hitReach == EnemyHitType.Both) {
				OffsetPositionObject oPO = c.transform.GetComponent<OffsetPositionObject>();
				
				force = (Vector2)(c.transform.TransformPoint (oPO.getOffsetPosition())-transform.position);
				c.GetComponent<HitEvent> ().hurt  (power, force.normalized*knockback);
			}
		}
		
	}
	
	void OnCollisionStay2D(Collision2D c) {
		Vector2 force;
		
		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			if(hitReach == EnemyHitType.Foot || hitReach == EnemyHitType.Both) {
				OffsetPositionObject oPO = c.transform.GetComponent<OffsetPositionObject>();
				
				force = (Vector2)(c.transform.TransformPoint (oPO.getOffsetPosition())-transform.position);
				c.transform.GetComponent<HitEvent> ().hurt  (power, force.normalized*knockback);
			}
		}
		
	}
}
