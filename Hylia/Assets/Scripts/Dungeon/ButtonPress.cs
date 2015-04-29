using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {
	
	public Utils.EnemyAreaType area = Utils.EnemyAreaType.All;
	bool active = false;
	ButtonEvent myEvent;

	void Start() {
		myEvent = GetComponent<ButtonEvent> ();
	}


	void OnTriggerEnter2D(Collider2D c) {
		if(active) return;
		if (c.gameObject.tag == "Player1" || c.gameObject.tag == "Player2") {
			EquipedItem item = c.GetComponent<EquipedItem> ();
			
			if ((area == Utils.EnemyAreaType.Normal || area == Utils.EnemyAreaType.Sun || area == Utils.EnemyAreaType.NotMoon) && item.getMoonMode ())
				return;
			if ((area == Utils.EnemyAreaType.Normal || area == Utils.EnemyAreaType.Moon || area == Utils.EnemyAreaType.NotSun) && item.getSunMode ())
				return;
			if ((area == Utils.EnemyAreaType.Sun || area == Utils.EnemyAreaType.Moon || area == Utils.EnemyAreaType.NotNormal) 
				&& !item.getMoonMode () && !item.getSunMode ())
				return;
			
			active = true;
			GetComponent<Animator> ().SetBool ("Active", active);
			GetComponent<AudioSource> ().Play ();
			activateStuff ();

		} else if (c.gameObject.tag == "Block") {
			PushableBlock pB = c.GetComponent<PushableBlock> ();

			if(area == Utils.EnemyAreaType.Normal && (pB.blockArea == Utils.EnemyAreaType.NotNormal || pB.blockArea == Utils.EnemyAreaType.Moon || pB.blockArea == Utils.EnemyAreaType.Sun)) return;
			if(area == Utils.EnemyAreaType.Sun && (pB.blockArea == Utils.EnemyAreaType.NotSun || pB.blockArea == Utils.EnemyAreaType.Moon || pB.blockArea == Utils.EnemyAreaType.Normal)) return;
			if(area == Utils.EnemyAreaType.Moon && (pB.blockArea == Utils.EnemyAreaType.NotMoon || pB.blockArea == Utils.EnemyAreaType.Normal || pB.blockArea == Utils.EnemyAreaType.Sun)) return;
			if(area == Utils.EnemyAreaType.NotNormal && pB.blockArea == Utils.EnemyAreaType.Normal) return;
			if(area == Utils.EnemyAreaType.NotMoon && pB.blockArea == Utils.EnemyAreaType.Moon) return;
			if(area == Utils.EnemyAreaType.NotSun && pB.blockArea == Utils.EnemyAreaType.Sun) return;
			
			active = true;
			GetComponent<Animator> ().SetBool ("Active", active);
			GetComponent<AudioSource> ().Play ();
			activateStuff ();
		}
		
	}

	protected void activateStuff() {
		if (myEvent != null)
			myEvent.performEvent ();
	}
	
	
}
