using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {

	//use a list to store the planets that is controled by newtwon's gravitaiton law
	//becuase we might need to add/delete elements from this list
	
	public float GravityConstant = 10f;
	public List<RageCircleMono> planets = new List<RageCircleMono> ();
	
	static Transform _t;
	
	
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () 
	{
		int c = planets.Capacity;
		for (int i = 0;i<c;i++) {
			for (int j = 0;j<c;j++){
				if (j==i) continue;
				else {
					ApplyGravity(planets[i],planets[j]);
				}
			}
		}
	}
	
	void ApplyGravity(RageCircleMono A, RageCircleMono B)
	{
		//This is how to get the distance vector between two objects.
		Vector3 dist = B.Tf.position - A.Tf.position;
		float r = dist.sqrMagnitude;

		float force = (GravityConstant * A.Mass * B.Mass) / r;
		
		//Then, just apply the forces
		A.Rb2D.AddForce (dist.normalized * force);
		B.Rb2D.AddForce (-dist.normalized * force);
	}
	
	public static RageCircleMono AddGenericPlanet(){
		Transform genericPlanet = Resources.Load("Circle",typeof(Transform)) as Transform;
		if (genericPlanet!=null) {
			Transform t = Instantiate(genericPlanet,Vector3.zero,Quaternion.identity) as Transform;
			RageCircleMono r = t.GetComponent<RageCircleMono>();
			//if (r)
			//TODO add to planet list
			
			return r;
		} else {
			return null;
		}
	}
}
