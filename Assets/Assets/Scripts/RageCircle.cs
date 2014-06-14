//Yves Wang @Project Eklyps
//Rage Circle Mono is a component that utilizes ragesplines functinality
//and make it adpted to Eklyps' needs.
//it provides the basic circle-specific features like,
//most of its tweening features, collison detection, 
//any features that are common among circular entities in Eklyps
//note that it dose not handle physics stuff, it goes into 
//another script which handles common physics event 


using UnityEngine;
using System.Collections;

public class RageCircle : MonoBehaviour {
	
	public float minAntiAliasWidth = 0.005f;
	public float maxAntiAliasWidth = 5f;
	public float density = 10f;
	
	float AntiAliasPlaftformFactor;
	
	//mass = density * pi * r^2
	float mass;
	//Rigidbody2D rb2D;
	
	float antiAliasWidth; //recorde the original fineset width
	float radius;
	Transform thisTransform;
	
	IRageSpline spline;

#region Unity Monobehavious
	void Awake(){
		spline = this.GetComponent<RageSpline>() as IRageSpline;
		thisTransform = this.transform;
		//rb2D = this.rigidbody2D;
		#if UNITY_EDITOR
		AntiAliasPlaftformFactor = 1f;
		#endif
		#if UNITY_STANDALONE
		AntiAliasPlaftformFactor = 1f;
		#endif
		#if UNITY_IPHONE
		AntiAliasPlaftformFactor = 0.5f;
		#endif
		
	}
	void Start () {
		float t = Vector3.Distance (spline.GetPositionWorldSpace (0), thisTransform.position);
		radius = t;
		float w = spline.GetAntialiasingWidth();
		spline.SetAntialiasingWidth(w * AntiAliasPlaftformFactor);
		//antiAliasFactor = radius * spline.GetAntialiasingWidth();
		antiAliasWidth = spline.GetAntialiasingWidth();
		mass = density * radius * radius;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
#endregion

#region Properties
	//TODO: Fix physics glithces when setting radius by altering its scale
	//or we can manually calculate the point
	public float Radius {
		get {return radius;}
		// 0 means nothing
		set {
			if (value<0f) {
				return;
			} else {
				//physics and anti-w is scale accordingly
				float s = value / radius;
				thisTransform.localScale *= s;
				antiAliasWidth /= s;
				radius = value;
				//update mass
				mass = density * radius * radius;
				//update anti-alias width and refresh mesh
				if (( antiAliasWidth > minAntiAliasWidth) || ( antiAliasWidth < maxAntiAliasWidth )) {
					spline.SetAntialiasingWidth(antiAliasWidth);
					spline.RefreshMesh(true,false,false);
				}
			}
		}
	}

	public bool IsTweening {
		get {return LeanTween.isTweening(this.gameObject);}
	}
	
	public float Mass {
		get {return mass;}
	}
	
	/*
	public Rigidbody2D Rb2D {
		get {
			if (rb2D == null ) {
				rb2D = this.GetComponent<Rigidbody2D>();
				if (rb2D == null) return null; else return rb2D;
			} else {
				return rb2D;
			}
		}
	}*/
	
	public Transform Tf {
		get {
			if (thisTransform != null) {
				return thisTransform;
			} else {
				thisTransform = this.transform;
				return thisTransform;
			}
		}
	}

	public Vector2 Pos2 {
		get {
			return new Vector2 (Tf.position.x,Tf.position.y);
		}
	}
#endregion
#region Tween Radius

	public LTDescr TweenRadius(float r1, float r2, float t) {
		if ((r1<0f)||(r2<0f)||(t<=0)) {
			return null;
		} else {
			return LeanTween.value(this.gameObject,_tweenRadius,r1,r2,t);
		}
	}
	
	void _tweenRadius(float x){
		this.Radius = x;
	}
	
	public LTDescr TweenRadius(float r,float t){
		return TweenRadius(this.Radius,r,t);
	}
	
	
#endregion

#region Tween Fill Color
	//alpha is ignored and put in another tween
	public LTDescr TweenFillColor(Color c, Color c2, float t){
		if (spline.GetFill () == RageSpline.Fill.Solid) {
			return 
				LeanTween.value (
					this.gameObject, 
					_tweenFillColor, 
					new Vector3 (c.r, c.g,c.b), 
					new Vector3 (c2.r, c2.g, c2.b), 
					t);
		} else {
			Debug.Log(this.name+ " : Only Solid fill can be tweened");
			return null;
		}
	}

	void _tweenFillColor(Vector3 x){
		spline.SetFillColor1 (new Color (x.x, x.y, x.z));
	}

	public LTDescr TweenFillColor(Color c,float t){
		return TweenFillColor (spline.GetFillColor1 (), c, t);
	}

#endregion

#region Tween Alpha
	public LTDescr TweenAlpha(float a1, float a2, float t){
		if (spline.GetFill () == RageSpline.Fill.Solid) {
			_fillColor = spline.GetFillColor1();
			return LeanTween.value (this.gameObject, _tweenAlpha, a1, a2, t);
		} else {
			Debug.Log(this.name+ " : Only Solid fill can be tweened");
			return null;
		}
	}
	Color _fillColor;
	void _tweenAlpha(float x){
		_fillColor.a = x;
		spline.SetFillColor1(_fillColor);
	}
#endregion


}
