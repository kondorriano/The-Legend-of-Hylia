using UnityEngine;
using System.Collections;

public abstract class ButtonPress : MonoBehaviour {
	
	public EnemyHit.EnemyAreaType area = EnemyHit.EnemyAreaType.All;
	protected bool active = false;


	protected virtual void OnTriggerEnter2D(Collider2D c) {
		if(active) return;
		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			EquipedItem item = c.GetComponent<EquipedItem> ();
			
			if ((area == EnemyHit.EnemyAreaType.Normal || area == EnemyHit.EnemyAreaType.Sun || area == EnemyHit.EnemyAreaType.NotMoon) && item.getMoonMode()) return;
			if ((area == EnemyHit.EnemyAreaType.Normal || area == EnemyHit.EnemyAreaType.Moon || area == EnemyHit.EnemyAreaType.NotSun) && item.getSunMode()) return;
			if ((area == EnemyHit.EnemyAreaType.Sun || area == EnemyHit.EnemyAreaType.Moon || area == EnemyHit.EnemyAreaType.NotNormal) 
			    && !item.getMoonMode () && !item.getSunMode ())	return;
			
			active = true;
			GetComponent<Animator> ().SetBool ("Active", active);
			GetComponent<AudioSource>().Play();
			activateStuff();

		}
		
	}

	protected virtual void activateStuff() {

	}
	
	
}
