using UnityEngine;
using System.Collections;

public class CameraDivision : MonoBehaviour {
	public Transform cam1;
	public Transform cam2;
	public Transform line;

	Vector2 intersection1;
	Vector2 intersection2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 p1 = cam1.position;
		Vector2 p2 = cam2.position;
		getDivisionPoints (p1, p2, out intersection1, out intersection2);

		line.LookAt (intersection1);
		line.Rotate (Vector3.up * 90);
	}

	void getDivisionPoints(Vector2 p1, Vector2 p2, out Vector2 inter1, out Vector2 inter2) {
		float w = Screen.width;
		float h = Screen.height;

		if(p1.x == p2.x) {
			inter1 = new Vector2(w/2f,h);
			inter2 = new Vector2(w/2f,0f);
		} else if(p1.y == p2.y) {
			inter1 = new Vector2(0,h/2f);
			inter2 = new Vector2(w,h/2f);
		} else {
			float m =  -(p2.x-p1.x)/(p2.y-p1.y);
			float n = h/2f-(w*m)/2f;

			if(m==h/w) {
				inter1 = new Vector2(0,0);
				inter2 = new Vector2(w,h);
			} else if(m==-h/w) {
				inter1 = new Vector2(0,h);
				inter2 = new Vector2(w,0);
			} else if (-h/w < m && m < h/w) {
				inter1 = new Vector2(0,n);
				inter2 = new Vector2(w,n+w*m);
			} else {
				inter1 = new Vector2(-n/m , 0);
				inter2 = new Vector2(w + n/m , h);
			}
		}
	}
}