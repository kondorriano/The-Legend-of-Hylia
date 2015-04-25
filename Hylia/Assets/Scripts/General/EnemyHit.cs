using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour {

	public int power = 4;
	public float knockback = 5;
	public Utils.EnemyHitType hitReach;
	public Utils.EnemyAreaType areaReach = Utils.EnemyAreaType.All;



	void OnTriggerEnter2D(Collider2D c) {
		Vector2 force;

		if (c.gameObject.tag == "PlayerCollider") {
			if(hitReach == Utils.EnemyHitType.Body || hitReach == Utils.EnemyHitType.Both) {
				force = (Vector2)(c.transform.position-transform.position);
				c.transform.parent.GetComponent<HitEvent> ().hurt (power, force.normalized*knockback, areaReach);
			}
		} else if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			if(hitReach == Utils.EnemyHitType.Foot || hitReach == Utils.EnemyHitType.Both) {
				OffsetPositionObject oPO = c.transform.GetComponent<OffsetPositionObject>();

				force = (Vector2)(c.transform.TransformPoint (oPO.getOffsetPosition())-transform.position);
				c.GetComponent<HitEvent> ().hurt  (power, force.normalized*knockback, areaReach);
			}
		}

	}

	void OnCollisionEnter2D(Collision2D c) {
		Vector2 force;

		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			if(hitReach == Utils.EnemyHitType.Foot || hitReach == Utils.EnemyHitType.Both) {
				OffsetPositionObject oPO = c.transform.GetComponent<OffsetPositionObject>();
				
				force = (Vector2)(c.transform.TransformPoint (oPO.getOffsetPosition())-transform.position);
				c.transform.GetComponent<HitEvent> ().hurt  (power, force.normalized*knockback, areaReach);
			}
		}

	}

	void OnTriggerStay2D(Collider2D c) {
		Vector2 force;
		
		if (c.gameObject.tag == "PlayerCollider") {
			if(hitReach == Utils.EnemyHitType.Body || hitReach == Utils.EnemyHitType.Both) {
				force = (Vector2)(c.transform.position-transform.position);
				c.transform.parent.GetComponent<HitEvent> ().hurt (power, force.normalized*knockback, areaReach);
			}
		} else if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			if(hitReach == Utils.EnemyHitType.Foot || hitReach == Utils.EnemyHitType.Both) {
				OffsetPositionObject oPO = c.transform.GetComponent<OffsetPositionObject>();
				
				force = (Vector2)(c.transform.TransformPoint (oPO.getOffsetPosition())-transform.position);
				c.GetComponent<HitEvent> ().hurt  (power, force.normalized*knockback, areaReach);
			}
		}
		
	}
	
	void OnCollisionStay2D(Collision2D c) {
		Vector2 force;
		
		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			if(hitReach == Utils.EnemyHitType.Foot || hitReach == Utils.EnemyHitType.Both) {
				OffsetPositionObject oPO = c.transform.GetComponent<OffsetPositionObject>();
				
				force = (Vector2)(c.transform.TransformPoint (oPO.getOffsetPosition())-transform.position);
				c.transform.GetComponent<HitEvent> ().hurt  (power, force.normalized*knockback, areaReach);
			}
		}
		
	}
}
