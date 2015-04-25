using UnityEngine;
using System.Collections;

public class ButtonSwitch : MonoBehaviour {


	public Utils.EnemyAreaType area = Utils.EnemyAreaType.All;
	public Utils.ColliderType affectedBy = Utils.ColliderType.All;
	protected bool active = false;
	
	
	protected void OnTriggerEnter2D(Collider2D c) {

		//Get colliderArea
		//Get effect
		/*
		EquipedItem item = c.GetComponent<EquipedItem> ();
		if ((area == Utils.EnemyAreaType.Normal || area == Utils.EnemyAreaType.Sun || area == Utils.EnemyAreaType.NotMoon) && item.getMoonMode()) return;
		if ((area == Utils.EnemyAreaType.Normal || area == Utils.EnemyAreaType.Moon || area == Utils.EnemyAreaType.NotSun) && item.getSunMode()) return;
		if ((area == Utils.EnemyAreaType.Sun || area == Utils.EnemyAreaType.Moon || area == Utils.EnemyAreaType.NotNormal) 
		    && !item.getMoonMode () && !item.getSunMode ())	return;


		active = !active;
		GetComponent<Animator> ().SetBool ("Active", active);
		GetComponent<AudioSource>().Play();
		activateStuff();
			
		}
		*/
		
	}

	void activateStuff() {
		if (active) {

		} else {

		}
	}

	
	
}
