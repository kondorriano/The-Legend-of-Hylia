using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour {

	public enum EnemyHitType {
		None = 0,
		Foot = 1,
		Body = 2,
		Both = 3	
	}

	public enum EnemyAreaType {
		Normal = 0,
		Sun = 1,
		Moon = 2,
		NotNormal = 3,
		NotSun = 4,
		NotMoon = 5,
		All = 6
	}

	public int power = 4;
	public float knockback = 5;
	public EnemyHitType hitReach;
	public EnemyAreaType areaReach = EnemyAreaType.All;



	void OnTriggerEnter2D(Collider2D c) {
		Vector2 force;

		if (c.gameObject.tag == "PlayerCollider") {
			if(hitReach == EnemyHitType.Body || hitReach == EnemyHitType.Both) {
				force = (Vector2)(c.transform.position-transform.position);
				c.transform.parent.GetComponent<HitEvent> ().hurt (power, force.normalized*knockback, areaReach);
			}
		} else if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			if(hitReach == EnemyHitType.Foot || hitReach == EnemyHitType.Both) {
				OffsetPositionObject oPO = c.transform.GetComponent<OffsetPositionObject>();

				force = (Vector2)(c.transform.TransformPoint (oPO.getOffsetPosition())-transform.position);
				c.GetComponent<HitEvent> ().hurt  (power, force.normalized*knockback, areaReach);
			}
		}

	}

	void OnCollisionEnter2D(Collision2D c) {
		Vector2 force;

		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			if(hitReach == EnemyHitType.Foot || hitReach == EnemyHitType.Both) {
				OffsetPositionObject oPO = c.transform.GetComponent<OffsetPositionObject>();
				
				force = (Vector2)(c.transform.TransformPoint (oPO.getOffsetPosition())-transform.position);
				c.transform.GetComponent<HitEvent> ().hurt  (power, force.normalized*knockback, areaReach);
			}
		}

	}

	void OnTriggerStay2D(Collider2D c) {
		Vector2 force;
		
		if (c.gameObject.tag == "PlayerCollider") {
			if(hitReach == EnemyHitType.Body || hitReach == EnemyHitType.Both) {
				force = (Vector2)(c.transform.position-transform.position);
				c.transform.parent.GetComponent<HitEvent> ().hurt (power, force.normalized*knockback, areaReach);
			}
		} else if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			if(hitReach == EnemyHitType.Foot || hitReach == EnemyHitType.Both) {
				OffsetPositionObject oPO = c.transform.GetComponent<OffsetPositionObject>();
				
				force = (Vector2)(c.transform.TransformPoint (oPO.getOffsetPosition())-transform.position);
				c.GetComponent<HitEvent> ().hurt  (power, force.normalized*knockback, areaReach);
			}
		}
		
	}
	
	void OnCollisionStay2D(Collision2D c) {
		Vector2 force;
		
		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			if(hitReach == EnemyHitType.Foot || hitReach == EnemyHitType.Both) {
				OffsetPositionObject oPO = c.transform.GetComponent<OffsetPositionObject>();
				
				force = (Vector2)(c.transform.TransformPoint (oPO.getOffsetPosition())-transform.position);
				c.transform.GetComponent<HitEvent> ().hurt  (power, force.normalized*knockback, areaReach);
			}
		}
		
	}
}
