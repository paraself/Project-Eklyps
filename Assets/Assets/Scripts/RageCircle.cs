using UnityEngine;
using System.Collections;

//Draw a 2d circle using RageSpline on XY plane
//you have to manually refresh mesh in each late update
//it's a monohehaviour already

public class RageCircle : MonoBehaviour{

	float radius = 1f;
	Vector3 center = Vector3.zero;
	float width = 1f;
	public bool filled = true;
	public bool outlined = false;
	Color strokeColor = Color.white;
	Color fillColor = Color.red;

	public GameObject gameObject;
	public IRageSpline spline;

	Vector3 r,l,u,d,r2,l2,u2,d2;//radius parameters;

	//if we created the ragecircle from code, it's also supportable
	//this probably will be drprecated
#region Constructor for creating circles from code
	//default no-stroke, filled circle
	public RageCircle () {
		Init();
	}

	public RageCircle (Vector3 center, float radius, Color color) {
		this.center = center;
		this.radius = radius;
		this.fillColor = color;
		Init();
	}

	public RageCircle ( Vector3 center, float radius, float width, bool outlined, bool filled, Color color1, Color color2){
		this.center = center;
		this.radius = radius;
		this.outlined = outlined;
		this.filled = filled;
		this.fillColor = color2;
		this.strokeColor = color1;
		this.width = Mathf.Max(0f,width);
		spline.SetOutlineWidth(this.width);
		Init();
	}

	void Init(){
		//gameObject = new GameObject("RageCircle");
		//spline = gameObject.AddComponent<RageSpline>() as IRageSpline;
		gameObject = UnityEngine.Object.Instantiate(Resources.Load("Circle", typeof(GameObject))) as GameObject;
		spline = gameObject.GetComponent<RageSpline>() as IRageSpline;
		
		if (filled == true) {
			spline.SetFill(RageSpline.Fill.Solid);
			spline.SetFillColor1(fillColor);
		} else {
			spline.SetFill(RageSpline.Fill.None);
		}
		
		if (outlined == true ){
			spline.SetOutline(RageSpline.Outline.Loop);
			spline.SetOutlineColor1(strokeColor);
		} else {
			spline.SetOutline(RageSpline.Outline.None);
		}
		
		gameObject.transform.position = center;
		//spline.AddPoint(0.25f);
		//spline.AddPoint(0.75f);because we use the prefab, so no need to add extra points
		
		//Debug.Log("total points count = "+spline.GetPointCount());
		//Debug.Log(spline.GetPositionWorldSpace(1));
		
		UpdateRadiusAndPoints();


		spline.SetAntialiasingWidth(0.1f);
		spline.SetOptimize(true);
		spline.SetOptimizeAngle(4f);
		
		Debug.Log(spline.GetVertexCount());

		//gameObject.renderer.material = 
		
		spline.RefreshMesh();

		//we initiate an empty tween for furture use
		//LeanTween.value(this.gameObject,this.radius,this.radius,0f,
	}

	internal void UpdateRadiusAndPoints () {
		//r,l,u,d,r2,l2,u2,d2
		r = Vector3.right * radius;
		l = Vector3.left * radius;
		d = Vector3.down * radius;
		u = Vector3.up * radius;
		r2 = r * 0.54f;
		l2 = l * 0.54f;
		u2 = u * 0.54f;
		d2 = d * 0.54f;

		spline.SetPoint(0,r,u2,d2,false);
		spline.SetPoint(1,d,r2,l2,false);
		spline.SetPoint(2,l,d2,u2,false);
		spline.SetPoint(3,u,l2,r2,false);

		//spline.SetVertexCount(Mathf.Max(10,Mathf.RoundToInt(radius * radius * 0.2f)));
	}
#endregion

#region Unity Monobehaviours
	void Start(){
		//this.spline = this.GetComponent<RageSpline>() as IRageSpline;
		//gameObject = this.gameObject;
	}
	void Update(){
	}
#endregion

#region Methods

	public void RefreshMesh(){
		spline.RefreshMesh();
	}

	//public void RefreshMesh(bool refreshFillTriangulation, bool refreshNormals, bool refreshPhysics)
	public void RefreshMesh(bool a,bool b,bool c) {
		spline.RefreshMesh(a,b,c);
		//if find circle collider 2d, refresh it

	}

	public void TweenRadius(float _from,float _to,float _time){
		LeanTween.value(gameObject,changeRadius,_from,_to,_time).setEase(LeanTweenType.linear).setLoopPingPong();
	}

	//public void TweenScale(

	internal void changeRadius(float x){
		this.Radius = x;
		this.RefreshMesh();
	}
#endregion

	//public void RefreshMesh(bool refreshFillTriangulation, bool refreshNormals, bool refreshPhysics)
#region Get/Set
	//--Get/Set
	public float Radius {
		get {
			return this.radius;
		}
		set {
			if (value!=this.radius) {
				this.radius = Mathf.Max(0f,value);
				UpdateRadiusAndPoints();
			}

		}
	}

	public Vector3 Center {
		get {
			return this.center;
		}
		set {
			this.center = value;
		}
	}

	public Color FillColor {
		get {
			return this.fillColor;
		}
		set {
			this.fillColor = value;
			spline.SetFillColor1(this.fillColor);
		}
	}

	public Color OutlineColor {
		get {return this.strokeColor;}
		set {this.strokeColor = value;spline.SetOutlineColor1(this.strokeColor);}
	}

	public float Width {
		get { return this.width;}
		set { this.width = Mathf.Max (0f,value);spline.SetOutlineWidth(this.width);}
	}
#endregion

}
