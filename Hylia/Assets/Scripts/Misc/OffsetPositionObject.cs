using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider2D))]
public class OffsetPositionObject : MonoBehaviour {

	private Vector3 offset = Vector2.zero;

	void Start() {

		PolygonCollider2D myMesh = GetComponent<PolygonCollider2D> ();
		if (myMesh != null) {
			Vector2 max = myMesh.points [0];
			Vector2 min = myMesh.points [0];

			for (int i = 1; i < myMesh.GetTotalPointCount(); i++) {
				Vector2 point = myMesh.points [i];
				if (max.x < point.x)
					max.x = point.x;
				if (max.y < point.y)
					max.y = point.y;

				if (min.x > point.x)
					min.x = point.x;
				if (min.y > point.y)
					min.y = point.y;
			}
			offset = new Vector3 (min.x + ((max.x - min.x) * 0.5f), min.y + ((max.y - min.y) * 0.5f), 0);
		} else {
			BoxCollider2D myBox = GetComponent<BoxCollider2D> ();
			offset = myBox.offset;

		}
	}

	public Vector3 getOffsetPosition() {
		return offset;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere (transform.TransformPoint (offset), 0.1f);
	}
}
