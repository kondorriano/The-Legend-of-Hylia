using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour {

	public float distance = 0.75f;

	private int id;
	SpriteRenderer mySprite;

	
	void setId(int myId) {
		id = myId;
	}
	
	// Use this for initialization
	void Start () {
		mySprite = GetComponent<SpriteRenderer> ();

		mySprite.enabled = false;
		
		Vector2 bowPosition = new Vector2 (0, 1).normalized *distance;
		transform.position = transform.parent.position + new Vector3 (bowPosition.x, bowPosition.y, bowPosition.y*0.1f);
	}
	
	// Update is called once per frame
	void Update () {

		float xAxis = Input.GetAxis ("Horizontal"+id);
		float yAxis = Input.GetAxis ("Vertical"+id);
		if (Mathf.Abs (xAxis) < 0.15f) xAxis = 0;
		if (Mathf.Abs (yAxis) < 0.15f) yAxis = 0;
		
		Vector2 bowPosition = new Vector2 (xAxis, yAxis).normalized *distance;
		
		if(bowPosition.magnitude > 0) transform.position = transform.parent.position + new Vector3 (bowPosition.x, bowPosition.y, bowPosition.y*0.1f);
		Vector2 dir = ((Vector2)(transform.position - transform.parent.position)).normalized;
		float angle = Mathf.Atan2 (dir.x, dir.y);
		angle = angle / Mathf.PI * 180;
		
		transform.eulerAngles = new Vector3(0, 0, -angle + 90);
	}
	
	public void setBow(bool active) {
		mySprite.enabled = active;
	}
}
