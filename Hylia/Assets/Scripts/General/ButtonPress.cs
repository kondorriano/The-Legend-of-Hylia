using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {
	
	public Utils.EnemyAreaType area = Utils.EnemyAreaType.All;
	protected bool active = false;


	protected void OnTriggerEnter2D(Collider2D c) {
		if(active) return;
		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			EquipedItem item = c.GetComponent<EquipedItem> ();
			
			if ((area == Utils.EnemyAreaType.Normal || area == Utils.EnemyAreaType.Sun || area == Utils.EnemyAreaType.NotMoon) && item.getMoonMode()) return;
			if ((area == Utils.EnemyAreaType.Normal || area == Utils.EnemyAreaType.Moon || area == Utils.EnemyAreaType.NotSun) && item.getSunMode()) return;
			if ((area == Utils.EnemyAreaType.Sun || area == Utils.EnemyAreaType.Moon || area == Utils.EnemyAreaType.NotNormal) 
			    && !item.getMoonMode () && !item.getSunMode ())	return;
			
			active = true;
			GetComponent<Animator> ().SetBool ("Active", active);
			GetComponent<AudioSource>().Play();
			activateStuff();

		}
		
	}

	protected void activateStuff() {

	}
	
	
}
