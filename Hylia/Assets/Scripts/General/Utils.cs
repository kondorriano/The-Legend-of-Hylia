using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour {

	public static Color moonColor = new Color (0, 0, 0, 0.5f);
	public static Color sunColor = new Color(1,1,0,0.5f);
	public static Color hurtColor = new Color(1,0,0,0.5f);

	public enum ColliderType {
		All = 0,
		Boomerang = 1,
		Bombs = 2,
		Arrows = 4,
		Sword = 5,
		Length = 6
	}

	public enum ItemType {
		None = 0,
		Boomerang = 1,
		Bombs = 2,
		Bow = 3,
		MirrorShield = 4,
		MoonPearl = 5,
		SunPearl = 6,
		Length = 7		
	}

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
}
