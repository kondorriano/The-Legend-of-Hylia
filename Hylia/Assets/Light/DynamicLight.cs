using UnityEngine;
using System.Collections;
using System.Collections.Generic;		// This allows for the use of lists, like <GameObject>
using pseudoSinCos;


public class verts
{
	public float angle {get;set;}
	public int location {get;set;} // 1= left end point    0= middle     -1=right endpoint
	public Vector2 pos {get;set;}
	public bool endpoint { get; set;}

}


public class DynamicLight : MonoBehaviour {
	
	// Public variables
	public Material lightMaterial;
	//public PolygonCollider2D[] allMeshes;									// Array for all of the meshes in our scene

	public List<verts> allVertices = new List<verts>();								// Array for all of the vertices in our meshes
	public float lightRadius = 20f;
	public Color lightColor = new Color (1f, 1f, 0.7f, 0.7f);
	public int lightSegments = 8;


	public LayerMask lightableLayer;



	
	// Private variables
	Mesh lightMesh;													// Mesh for our light mesh
	Renderer myRenderer;

	// Called at beginning of script execution
	void Start () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, LightController.lightPosition);
		PseudoSinCos.initPseudoSinCos();
		
		//-- Step 1: obtain all active meshes in the scene --//
		//---------------------------------------------------------------------//

		MeshFilter meshFilter = (MeshFilter)gameObject.AddComponent(typeof(MeshFilter));				// Add a Mesh Filter component to the light game object so it can take on a form
		myRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;		// Add a Mesh Renderer component to the light game object so the form can become visible
		//gameObject.name = "2DLight";
		//renderer.material.shader = Shader.Find ("Transparent/Diffuse");							// Find the specified type of material shader
		//myRenderer.sharedMaterial = lightMaterial;	
		myRenderer = GetComponent<Renderer> ();
		myRenderer.material = lightMaterial;
		// Add this texture
		lightMesh = new Mesh();																	// create a new mesh for our light mesh
		meshFilter.mesh = lightMesh;															// Set this newly created mesh to the mesh filter
		lightMesh.name = "Light Mesh";															// Give it a name
		lightMesh.MarkDynamic ();



	}

	void Update(){

		//getAllMeshes();
		setShader ();
		setLight ();
		renderLightMesh ();
		//resetBounds ();

	}


	/*
	void getAllMeshes(){
		allMeshes = FindObjectsOfType(typeof(PolygonCollider2D)) as PolygonCollider2D[];
	}
	*/

	void resetBounds(){
		Bounds b = lightMesh.bounds;
		b.center = Vector3.zero;
		lightMesh.bounds = b;
	}

	void setShader() {
		myRenderer.material.SetFloat ("_UVXScale", 1f / lightRadius);
		myRenderer.material.SetFloat ("_UVYScale", 1f / lightRadius);
		myRenderer.material.SetColor ("_Color", lightColor);


	}

	bool ContainsPoint (Vector2[] polyPoints, Vector2 p) { 
		int j = polyPoints.Length-1; 
		bool inside = false; 
		for (int i = 0; i < polyPoints.Length; j = i++) { 
			if ( ((polyPoints[i].y <= p.y && p.y < polyPoints[j].y) || (polyPoints[j].y <= p.y && p.y < polyPoints[i].y)) && 
			    (p.x < (polyPoints[j].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[j].y - polyPoints[i].y) + polyPoints[i].x)) 
				inside = !inside; 
		} 
		return inside; 
	}

	Vector2[] getPolygonPoints(Transform lightableObject) {
		PolygonCollider2D mf = null;
		BoxCollider2D bc = null;
		Vector2[] polyPoints;

		mf = lightableObject.GetComponent<PolygonCollider2D>();
		if (mf != null) {
			polyPoints = new Vector2[mf.GetTotalPointCount ()];
			for (int i = 0; i < polyPoints.Length; i++) {		
				polyPoints[i] = lightableObject.TransformPoint(mf.points[i]);
			}

			return polyPoints;
		} else {
			polyPoints = new Vector2[4];
			bc = lightableObject.GetComponent<BoxCollider2D>();

			polyPoints[0] = lightableObject.TransformPoint(new Vector2(bc.offset.x+bc.size.x*0.5f, bc.offset.y+bc.size.y*0.5f));
			polyPoints[1] = lightableObject.TransformPoint(new Vector2(bc.offset.x+bc.size.x*0.5f, bc.offset.y-bc.size.y*0.5f));
			polyPoints[2] = lightableObject.TransformPoint(new Vector2(bc.offset.x-bc.size.x*0.5f, bc.offset.y+bc.size.y*0.5f));
			polyPoints[3] = lightableObject.TransformPoint(new Vector2(bc.offset.x-bc.size.x*0.5f, bc.offset.y-bc.size.y*0.5f));

			return polyPoints;
			
		}


	}

	void setLight () {

		bool sortAngles = false;

		allVertices.Clear();// Since these lists are populated every frame, clear them first to prevent overpopulation

	



		//--Step 2: Obtain vertices for each mesh --//
		//---------------------------------------------------------------------//
	
		// las siguientes variables usadas para arregla bug de ordenamiento cuando
		// los angulos calcuados se encuentran en cuadrantes mixtos (1 y 4)
		bool lows = false; // check si hay menores a -0.5
		bool his = false; // check si hay mayores a 2.0
		float magRange = 0.15f;

		List <verts> tempVerts = new List<verts>();

		for (int m = 0; m < LightController.lightableObjectsList.Count; m++) {
		//for (int m = 0; m < 1; m++) {
			int count;

			tempVerts.Clear();
			PolygonCollider2D mf = null;
			BoxCollider2D bc = null;
			Vector2[] bcPoints = new Vector2[4];;
			mf = LightController.lightableObjectsList[m].GetComponent<PolygonCollider2D>();
			if(mf != null) count = mf.GetTotalPointCount();
			else {
				bc = LightController.lightableObjectsList[m].GetComponent<BoxCollider2D>();
				count = 4;

				bcPoints[0] = new Vector2(bc.offset.x+bc.size.x*0.5f, bc.offset.y+bc.size.y*0.5f);
				bcPoints[1] = new Vector2(bc.offset.x+bc.size.x*0.5f, bc.offset.y-bc.size.y*0.5f);
				bcPoints[2] = new Vector2(bc.offset.x-bc.size.x*0.5f, bc.offset.y+bc.size.y*0.5f);
				bcPoints[3] = new Vector2(bc.offset.x-bc.size.x*0.5f, bc.offset.y-bc.size.y*0.5f);

			}

			// las siguientes variables usadas para arregla bug de ordenamiento cuando
			// los angulos calcuados se encuentran en cuadrantes mixtos (1 y 4)
			lows = false; // check si hay menores a -0.5
			his = false; // check si hay mayores a 2.0


			for (int i = 0; i < count; i++) {								   // ...and for ever vertex we have of each mesh filter...
				
				verts v = new verts();
				// Convert to world space
				Vector2 worldPoint;
				if(mf != null)worldPoint = mf.transform.TransformPoint(mf.points[i]);
				else worldPoint = bc.transform.TransformPoint(bcPoints[i]);


				if((worldPoint-(Vector2) transform.position).magnitude <= lightRadius) {
					Vector2 origen = transform.position;
					// Reforma fecha 24/09/2014 (ultimo argumento lighradius X worldPoint.magnitude (expensivo pero preciso))
					RaycastHit2D ray = Physics2D.Raycast(origen, (worldPoint - origen).normalized, (worldPoint - origen).magnitude, lightableLayer);


					if(ray){
						v.pos = ray.point;
						if( worldPoint.sqrMagnitude >= (ray.point.sqrMagnitude - magRange) && worldPoint.sqrMagnitude <= (ray.point.sqrMagnitude + magRange) )
							v.endpoint = true;
							
					}else{
						v.pos = worldPoint;
						v.endpoint = true;
					}

					Debug.DrawLine(transform.position, new Vector3(v.pos.x,v.pos.y, transform.position.z), Color.white);	

					//--Convert To local space for build mesh (mesh craft only in local vertex)
					v.pos = transform.InverseTransformPoint(v.pos); 
					//--Calculate angle
					v.angle = getVectorAngle(true,v.pos.x, v.pos.y);
				


					// -- bookmark if an angle is lower than 0 or higher than 2f --//
					//-- helper method for fix bug on shape located in 2 or more quadrants
					if(v.angle < 0f )
						lows = true;
					
					if(v.angle > 2f)
						his = true;
					

					//--Add verts to the main array
					if((v.pos).sqrMagnitude <= lightRadius*lightRadius){
						tempVerts.Add(v);

					}
						
					if(sortAngles == false)
						sortAngles = true;

				}
			
			}




			// Indentify the endpoints (left and right)
			if(tempVerts.Count > 0){

				sortList(tempVerts); // sort first

				int posLowAngle = 0; // save the indice of left ray
				int posHighAngle = 0; // same last in right side

				//Debug.Log(lows + " " + his);

				if(his == true && lows == true){  //-- FIX BUG OF SORTING CUANDRANT 1-4 --//
					float lowestAngle = -1f;//tempVerts[0].angle; // init with first data
					float highestAngle = tempVerts[0].angle;


					for(int d=0; d<tempVerts.Count; d++){



						if(tempVerts[d].angle < 1f && tempVerts[d].angle > lowestAngle){
							lowestAngle = tempVerts[d].angle;
							posLowAngle = d;
						}

						if(tempVerts[d].angle > 2f && tempVerts[d].angle < highestAngle){
							highestAngle = tempVerts[d].angle;
							posHighAngle = d;
						}
					}


				}else{
					//-- convencional position of ray points
					// save the indice of left ray
					posLowAngle = 0; 
					posHighAngle = tempVerts.Count-1;

				}


				tempVerts[posLowAngle].location = 1; // right
				tempVerts[posHighAngle].location = -1; // left



				//--Add vertices to the main meshes vertexes--//
				allVertices.AddRange(tempVerts); 
				//allVertices.Add(tempVerts[0]);
				//allVertices.Add(tempVerts[tempVerts.Count - 1]);



				// -- r ==0 --> right ray
				// -- r ==1 --> left ray
				for(int r = 0; r<2; r++){

					//-- Cast a ray in same direction continuos mode, start a last point of last ray --//
					Vector2 fromCast = new Vector2();
					bool isEndpoint = false;

					if(r==0){
						fromCast = transform.TransformPoint(tempVerts[posLowAngle].pos);
						isEndpoint = tempVerts[posLowAngle].endpoint;

					}else if(r==1){
						fromCast = transform.TransformPoint(tempVerts[posHighAngle].pos);
						isEndpoint = tempVerts[posHighAngle].endpoint;
					}





					if(isEndpoint == true){

						Vector2 dir = (fromCast - (Vector2) transform.position);
						fromCast += dir.normalized*0.1f;

						bool notInside = true;
						for(int i = 0; i < LightController.lightableObjectsList.Count && notInside; ++i) {
							notInside = !ContainsPoint(getPolygonPoints(LightController.lightableObjectsList[i]), fromCast);
						}

						if(notInside) {	
							
							float mag = (lightRadius);//-dir.magnitude);// - fromCast.magnitude;
							RaycastHit2D rayCont = Physics2D.Raycast(fromCast, dir.normalized, mag, lightableLayer);
							//Debug.DrawLine(fromCast, dir.normalized*mag ,Color.green);

							
							Vector2 hitp;
							if(rayCont){
								if((rayCont.point-(Vector2)transform.position).magnitude >= lightRadius) hitp = transform.TransformPoint( dir.normalized * mag);
								else hitp = rayCont.point;
							}else{
								hitp = transform.TransformPoint( dir.normalized * mag);
							}

							Debug.DrawLine(new Vector3(fromCast.x,fromCast.y, transform.position.z), new Vector3(hitp.x,hitp.y, transform.position.z), Color.green);	

							verts vL = new verts();
							vL.pos = transform.InverseTransformPoint(hitp);

							vL.angle = getVectorAngle(true,vL.pos.x, vL.pos.y);
							allVertices.Add(vL);
						}
					}


				}


			}

			
		}
		




		//--Step 3: Generate vectors for light cast--//
		//---------------------------------------------------------------------//

		int theta = 0;
		//float amount = (Mathf.PI * 2) / lightSegments;
		int amount = 360 / lightSegments;



		for (int i = 0; i < lightSegments; i++)  {

			theta =amount * (i);
			if(theta == 360) theta = 0;

			verts v = new verts();
			//v.pos = new Vector3((Mathf.Sin(theta)), (Mathf.Cos(theta)), 0); // in radians low performance
			v.pos = new Vector2((PseudoSinCos.SinArray[theta]), (PseudoSinCos.CosArray[theta])); // in dregrees (previous calculate)

			v.angle = getVectorAngle(true,v.pos.x, v.pos.y);
			v.pos *= lightRadius;
			v.pos +=  (Vector2) transform.position;


			Vector2 origen = (Vector2) transform.position;

			RaycastHit2D ray = Physics2D.Raycast(origen,v.pos - origen,lightRadius, lightableLayer);
			//Debug.DrawRay(transform.position, v.pos - transform.position, Color.white);

			if (!ray){

				//Debug.DrawLine(transform.position, v.pos, Color.white);

				v.pos = transform.InverseTransformPoint(v.pos);
				allVertices.Add(v);

			}
		 
		}




		//-- Step 4: Sort each vertice by angle (along sweep ray 0 - 2PI)--//
		//---------------------------------------------------------------------//
		if (sortAngles == true) {
			sortList(allVertices);
		}
		//-----------------------------------------------------------------------------


		//--auxiliar step (change order vertices close to light first in position when has same direction) --//
		float rangeAngleComparision = 0.00001f;
		for(int i = 0; i< allVertices.Count-1; i+=1){
			
			verts uno = allVertices[i];
			verts dos = allVertices[i +1];

			// -- Comparo el angulo local de cada vertex y decido si tengo que hacer un exchange-- //
			if(uno.angle >= dos.angle-rangeAngleComparision && uno.angle <= dos.angle + rangeAngleComparision){
				
				if(dos.location == -1){ // Right Ray
					
					if(uno.pos.sqrMagnitude > dos.pos.sqrMagnitude){
						allVertices[i] = dos;
						allVertices[i+1] = uno;
						//Debug.Log("changing left");
					}
				}
				

				// ALREADY DONE!!
				if(uno.location == 1){ // Left Ray
					if(uno.pos.sqrMagnitude < dos.pos.sqrMagnitude){
						
						allVertices[i] = dos;
						allVertices[i+1] = uno;
						//Debug.Log("changing");
					}
				}
				
				
				
			}


		}



	}

	void renderLightMesh(){
		//-- Step 5: fill the mesh with vertices--//
		//---------------------------------------------------------------------//
		
		//interface_touch.vertexCount = allVertices.Count; // notify to UI
		
		Vector3 []initVerticesMeshLight = new Vector3[allVertices.Count+1];
		
		initVerticesMeshLight [0] = Vector3.zero;
		
		
		for (int i = 0; i < allVertices.Count; i++) { 
			//Debug.Log(allVertices[i].angle);
			initVerticesMeshLight [i+1] = new Vector3(allVertices[i].pos.x, allVertices[i].pos.y, 0);
			
			//if(allVertices[i].endpoint == true)
			//Debug.Log(allVertices[i].angle);
			
		}
		
		lightMesh.Clear ();
		lightMesh.vertices = initVerticesMeshLight;
		
		Vector2 [] uvs = new Vector2[initVerticesMeshLight.Length];
		for (int i = 0; i < initVerticesMeshLight.Length; i++) {
			uvs[i] = new Vector2(initVerticesMeshLight[i].x, initVerticesMeshLight[i].y);		
		}
		lightMesh.uv = uvs;
		
		// triangles
		int idx = 0;
		int [] triangles = new int[(allVertices.Count * 3)];
		for (int i = 0; i < (allVertices.Count*3); i+= 3) {
			
			triangles[i] = 0;
			triangles[i+1] = idx+1;
			
			
			if(i == (allVertices.Count*3)-3){
				//-- if is the last vertex (one loop)
				triangles[i+2] = 1;	
			}else{
				triangles[i+2] = idx+2; //next next vertex	
			}
			
			idx++;
		}
		
		
		lightMesh.triangles = triangles;

		//lightMesh.RecalculateNormals();
		GetComponent<MeshCollider> ().sharedMesh = null;
		GetComponent<MeshCollider> ().sharedMesh = lightMesh;
	}

	void sortList(List<verts> lista){
			lista.Sort((item1, item2) => (item2.angle.CompareTo(item1.angle)));
	}

	void drawLinePerVertex(){
		for (int i = 0; i < allVertices.Count; i++)
		{
			if (i < (allVertices.Count -1))
			{
				Debug.DrawLine(allVertices [i].pos , allVertices [i+1].pos, new Color(i*0.02f, i*0.02f, i*0.02f));
			}
			else
			{
				Debug.DrawLine(allVertices [i].pos , allVertices [0].pos, new Color(i*0.02f, i*0.02f, i*0.02f));
			}
		}
	}

	float getVectorAngle(bool pseudo, float x, float y){
		float ang = 0;
		if(pseudo == true){
			ang = pseudoAngle(x, y);
		}else{
			ang = Mathf.Atan2(y, x);
		}
		return ang;
	}
	
	float pseudoAngle(float dx, float dy){
		// Hight performance for calculate angle on a vector (only for sort)
		// APROXIMATE VALUES -- NOT EXACT!! //
		float ax = Mathf.Abs (dx);
		float ay = Mathf.Abs (dy);
		float p = dy / (ax + ay);
		if (dx < 0){
			p = 2 - p;

		}
		return p;
	}

}