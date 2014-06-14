using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using antilunchbox;// in order to use that singleton crap

public class WorldManager : Singleton<WorldManager> {

	//use a list to store the planets that is controled by newtwon's gravitaiton law
	//becuase we might need to add/delete elements from this list
	
	public float GravityConstant = 10f;
	public Color dayColor;
	public Color eklypsColor;
	public List<RageCircle> planets;
	
	static Camera _mainCamera;
	public static Camera MainCamera {
		get {
			if (_mainCamera == null ) {
				_mainCamera = Camera.main;
				return _mainCamera;
			} else return _mainCamera;
		}
	}

	//Singleton Initialization
	public static WorldManager Instance {
		get {
			return ((WorldManager)mInstance);
		} set {
			mInstance = value;
		}
	}

#region Properties

	bool _isEklypsing;
	public bool IsEklypsing {
		get {return _isEklypsing;}
		set {_isEklypsing = value;}
	}

#endregion
	
#region Unity Monobehavious
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_isEklypsing) {
			//because the Radius might change during runtime
			float D = Planet_Sun.Instance.RCircle.Radius+Planet_Player.Instance.RCircle.Radius;
			D *= D;
			Vector2 dv = Planet_Sun.Instance.RCircle.Pos2 - Planet_Player.Instance.RCircle.Pos2;
			MainCamera.backgroundColor = Color.Lerp(eklypsColor,dayColor,dv.sqrMagnitude/D);
		}
	}
	
	void FixedUpdate () 
	{
		
		int c = planets.Count;
		for (int i = 0;i<c;i++) {
			for (int j = 0;j<c;j++){
				if (j==i) continue;
				else {
					//ApplyGravity(planets[i],planets[j]);
				}
			}
		}
	
	}
#endregion

#region Methods

	//
	/*
	void ApplyGravity(RageCircle A, RageCircle B)
	{
		//This is how to get the distance vector between two objects.
		Vector3 dist = B.Tf.position - A.Tf.position;
		float r = dist.sqrMagnitude;

		float force = (GravityConstant * A.Mass * B.Mass) / r;
		
		//Then, just apply the forces
		A.Rb2D.AddForce (dist.normalized * force);
		B.Rb2D.AddForce (-dist.normalized * force);
	}*/

	//deprecated
	public RageCircle AddGenericPlanet(){
		Transform genericPlanet = Resources.Load("CircleMono",typeof(Transform)) as Transform;
		if (genericPlanet!=null) {
			Transform t = Instantiate(genericPlanet,Vector3.zero,Quaternion.identity) as Transform;
			if (t) {
				RageCircle r = t.GetComponent<RageCircle>();
				if (r) {
					planets.Add(r);
					
					return r;
				} else return null;
			} else return null;
			
		} else {
			Debug.Log ("Cannot find Circle prefab");
			return null;
		}
	}
#endregion

#region Camera Tweening
	public static LTDescr TweenBackgroundColor(Color c1, Color c2,float t){
		if (t<=0) return null;
		else {
			//_tempColor = new Vector3(c2.r,c2.r,c2.b);
			return LeanTween.value(
				MainCamera.gameObject,
				_tweenBackgroundColor,
				new Vector3(c1.r,c1.g,c1.b),
				new Vector3(c2.r,c2.g,c2.b),
				t);
		}
	}
	
	static void _tweenBackgroundColor (Vector3 x){
		MainCamera.backgroundColor = new Color(x.x,x.y,x.z);
	}
#endregion


}
