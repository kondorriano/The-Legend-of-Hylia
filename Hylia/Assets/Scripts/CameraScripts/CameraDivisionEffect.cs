using UnityEngine;
using System.Collections;

public class CameraDivisionEffect : MonoBehaviour {
	public Transform player1;
	public Transform player2;

	public Transform camera1;
	public Transform camera2;

	RenderTexture camTex1;
	RenderTexture camTex2;

	Vector3 direction;

	bool renderMainCamera = true;
	bool move1 = true;
	bool move2 = true;

	public float cameraVelocity = 10f;
	private Vector2 screenSize = new Vector2(0, 0);

	void Start () {
		updateTextures ();
	}

	void updateTextures() {
		camTex1 = new RenderTexture (Screen.width, Screen.height, 32);
		camera1.GetComponent<Camera> ().targetTexture = camTex1;
		
		camTex2 = new RenderTexture (Screen.width, Screen.height, 32);
		camera2.GetComponent<Camera> ().targetTexture = camTex2;
		screenSize = new Vector2(Screen.width, Screen.height);
	}

	void Update () {
		Vector2 currentScreenSize = new Vector2(Screen.width, Screen.height);
		if(currentScreenSize != screenSize) updateTextures();
		Vector2 p1 = player1.position;
		Vector2 p2 = player2.position;

		Vector3 dir = (player2.position - player1.position);
		direction = new Vector3 (dir.x, dir.y, 0);

		Vector3 cameraPos = player1.position + dir/2f;// * (direction.magnitude / 2f);
		cameraPos.z = transform.position.z;
		transform.position = cameraPos;

		if ((Mathf.Abs (direction.x) < Camera.main.aspect * 2f * Camera.main.orthographicSize * 0.5f) &&
			(Mathf.Abs (direction.y) < 2f * Camera.main.orthographicSize * 0.5f)) {
			if(!renderMainCamera) {
				move1 = true;
				move2 = true;
			}
			renderMainCamera = true;
		} else {
			if(renderMainCamera) {
				move1 = true;
				move2 = true;
			}
			renderMainCamera = false;
		}

		dir.Normalize ();

		if (move1) {

			if (!renderMainCamera) cameraPos = player1.position + new Vector3 (dir.x * Camera.main.aspect * 2f * Camera.main.orthographicSize, dir.y * 2f * Camera.main.orthographicSize, 0) * 0.25f;
			cameraPos.z = camera1.position.z;

			Vector3 vel = (cameraPos-camera1.position).normalized * cameraVelocity*Time.deltaTime;
			if(vel.sqrMagnitude >= (cameraPos-camera1.position).sqrMagnitude) {
				camera1.position =  cameraPos;
				move1 = false;
			} else camera1.position += vel;
		} else {
			if (renderMainCamera) {
				camera1.position = cameraPos;
			} else {				
				cameraPos = player1.position + new Vector3 (dir.x * Camera.main.aspect * 2f * Camera.main.orthographicSize, dir.y * 2f * Camera.main.orthographicSize, 0) * 0.25f;
				cameraPos.z = camera1.position.z;
				camera1.position = cameraPos;
			}
		}

		if (move2) {
			if (!renderMainCamera) cameraPos = player2.position - new Vector3 (dir.x * Camera.main.aspect * 2f * Camera.main.orthographicSize, dir.y * 2f * Camera.main.orthographicSize, 0) * 0.25f;
			cameraPos.z = camera2.position.z;

			Vector3 vel = (cameraPos-camera2.position).normalized * cameraVelocity*Time.deltaTime;
			if(vel.sqrMagnitude >= (cameraPos-camera2.position).sqrMagnitude) {
				camera2.position =  cameraPos;
				move2 = false;
			} else camera2.position += vel;
		} else {
			if (renderMainCamera) {
				camera2.position = cameraPos;
			} else {				
				cameraPos = player2.position - new Vector3 (dir.x * Camera.main.aspect * 2f * Camera.main.orthographicSize, dir.y * 2f * Camera.main.orthographicSize, 0) * 0.25f;
				cameraPos.z = camera2.position.z;
				camera2.position = cameraPos;
			}
		}
		/*
		if (renderMainCamera) {
			camera1.position = cameraPos;
			camera2.position = cameraPos;
		} else {

			dir.Normalize ();
			cameraPos = player1.position + new Vector3 (dir.x * Camera.main.aspect * 2f * Camera.main.orthographicSize, dir.y * 2f * Camera.main.orthographicSize, 0) * 0.25f;
			cameraPos.z = camera1.position.z;
			camera1.position = cameraPos;

			cameraPos = player2.position - new Vector3 (dir.x * Camera.main.aspect * 2f * Camera.main.orthographicSize, dir.y * 2f * Camera.main.orthographicSize, 0) * 0.25f;
			cameraPos.z = camera2.position.z;		
			camera2.position = cameraPos;
		}
		 */		
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

	public Shader shader;

	static Material m_Material = null;
	protected Material material {
		get {
			if (m_Material == null) {
				m_Material = new Material (shader);
				m_Material.hideFlags = HideFlags.DontSave;
			}
			return m_Material;
		}
	}

			
	public void OnRenderImage(RenderTexture src, RenderTexture dest) {

		if(renderMainCamera && !move1 && !move2) {
			Graphics.Blit(src, dest);
		} else {
			material.SetTexture ("_Cam1", camTex1);
			material.SetTexture ("_Cam2", camTex2);

			material.SetVector ("_Direction", direction);
			material.SetFloat("_Width", Screen.width);
			material.SetFloat("_Heigth", Screen.height);

			Graphics.Blit(src, dest, material);
		}
	}

	public bool getRenderMainCamera() {
		return renderMainCamera && !move1 && !move2;
	}
}