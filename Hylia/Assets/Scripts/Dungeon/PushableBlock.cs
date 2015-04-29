using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class PushableBlock : MonoBehaviour {

	public Utils.EnemyAreaType blockArea = Utils.EnemyAreaType.All;

}
