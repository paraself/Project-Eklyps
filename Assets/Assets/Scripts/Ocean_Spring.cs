using UnityEngine;
using System.Collections;

public class Ocean_Spring : MonoBehaviour {
	
	//
	public int numberOfSprings = 100;
	int numberOfSprings_cache;
	
	//the values are critical, if set wrong, the syste will be blowde up
	public float k = 0.025f; //<0.025, 0.01
	public float damping = 0.3f; // >0.025
	public float spread = 0.2f;
	public int iterations = 5;
	
	
	
	//
	Ocean_Shape shape;
	Transform _thisTransform;
	GameObject _springHolder;
	
	Spring[] springs;
	
	//
	public Spring[] Springs {
		get {
			return springs;
		}
	}
	
	//
	private Transform Tf {
		get {
			if (_thisTransform) return _thisTransform; else {
				_thisTransform = this.transform;
				return _thisTransform;
			}
		}
	}
	
	
	#region Unity Monos
	void Awake(){
		shape = this.GetComponent<Ocean_Shape>();
		_springHolder = new GameObject ("SpringHolder");
		_springHolder.transform.position = this.transform.position;
	}
	
	void Start () {
		SpringInit();
	}
	
	// Update is called once per frame
	void Update () {
		SpringUpdate();
	}
	#endregion
	
	#region Methods
	
	void SpringInit(){
		NumberCheck();
		springs = new Spring[numberOfSprings];
		float d = 1f/numberOfSprings;
		float r = shape.Length/numberOfSprings/2f;//get the radius of trigger
		for (int i=0;i<numberOfSprings;i++){
			float h = shape.GetRadiusAtPosition(i*d);
			//h,v,t,k,d
			springs[i] = new Spring (shape.Pos2,h,0f,h,k,damping,shape.GetPos2(i*d),shape.GetDirection(i*d),_springHolder.transform,r);
		}
	}
	
	void SpringUpdate(){
		
		float d = 1f/numberOfSprings;
		for (int i=0;i < numberOfSprings;i++) {
			springs[i].TargetHeight = shape.GetRadiusAtPositionCycle(i*d);
			//springs[i].UpdateVerlet();
			springs[i].Update();
		}
		
		for (int j =0;j< iterations;j++) {
			
			for (int i=0;i< numberOfSprings;i++){
				if (i>0) {
					springs[i].leftDelta = spread * (springs[i].HeightDif - springs[i-1].HeightDif);
					springs[i-1].Speed += springs[i].leftDelta;
				}
				if (i<numberOfSprings-1){
					springs[i].rightDelta = spread *(springs[i].HeightDif - springs[i+1].HeightDif );
					springs[i+1].Speed += springs[i].rightDelta;
				}
				if (i==0) {
					springs[i].leftDelta = spread * (springs[i].HeightDif - springs[numberOfSprings-1].HeightDif);
					springs[numberOfSprings-1].Speed += springs[i].leftDelta;
				}
				if (i==numberOfSprings-1){
					springs[i].rightDelta = spread *(springs[i].HeightDif - springs[0].HeightDif );
					springs[0].Speed += springs[i].rightDelta;
				}
			}
			
			for (int i=0;i<numberOfSprings;i++){
				if (i>0){
					springs[i - 1].Height += springs[i].leftDelta ;
				}
				if (i<numberOfSprings-1){
					springs[i + 1].Height += springs[i].rightDelta ;
				}
				if (i==0) springs[numberOfSprings -1].Height += springs[i].leftDelta;
				if (i==numberOfSprings-1) springs[0].Height += springs[i].rightDelta;
			}
			
		}
	}
	
	void NumberCheck(){
		numberOfSprings = (int)Mathf.Clamp(numberOfSprings,0,Mathf.Infinity);
		iterations = Mathf.Clamp(iterations,1,1000);
		spread = Mathf.Clamp(spread,0f,Mathf.Infinity);
	}
	
	
	#endregion
}

//Spring struct
[System.Serializable]
public class Spring {
	
	private Vector2 center;
	float _height;
	float tolerance;
	//float HeightOld;
	float _speed;
	public float _targetHeight;
	public float HeightDif;
	private float K;
	private float Damping;
	public float leftDelta;
	public float rightDelta;
	public Vector2 Di;//from center to peremeter
	public Vector2 Pos2;
	public float a;
	
	private GameObject g;
	Transform _t;
	Rigidbody2D _rb;
	private CircleCollider2D trigger;
	
	public bool isDebug = false;
	
	public Vector3 Pos3 {
		get {
			return new Vector3 (Pos2.x,Pos2.y,0f);
		}
	}
	
	public float Height {
		get {
			return _height;
		}
		set {
			_height = Mathf.Clamp(value, TargetHeight - tolerance, TargetHeight+tolerance);
		}
	}
	public float TargetHeight {
		get {return _targetHeight;}
		set {
			_targetHeight = Mathf.Clamp(value,0f,Mathf.Infinity);
		}
	}
	public float Speed {
		get { return _speed;}
		set { _speed = Mathf.Clamp(value,-tolerance,tolerance);}
	}
	
	
	
	public Spring (Vector2 c,float h,float v,float t,float k,float d,Vector2 p,Vector2 di,Transform parent,float r){
		
		TargetHeight = t;
		Height = h;
		HeightOld = Height;

		Speed = v;
		
		HeightDif = Height - TargetHeight;
		K = Mathf.Clamp(k,0f,Mathf.Infinity);
		Damping = Mathf.Clamp(d,0f,Mathf.Infinity);
		leftDelta = 0f;
		rightDelta = 0f;
		Di = di.normalized;
		Pos2 = p;
		g = new GameObject ("SpringTrigger", typeof(CircleCollider2D),typeof(Rigidbody2D),typeof(Ocean_Trigger));
		_t = g.transform;
		_rb = g.rigidbody2D;
		_rb.isKinematic = true;
		trigger = g.GetComponent<CircleCollider2D>();
		g.GetComponent<Ocean_Trigger>().sp = this;
		trigger.isTrigger = true;
		trigger.radius = r;
		g.transform.parent = parent;
		g.transform.position = Pos3;
		
		Vector3 tp = this.Pos2 + Di * r;
		trigger.center = g.transform.InverseTransformPoint(tp);
		
		center = c;
		tolerance = TargetHeight / 5f;
	}
	
	public void Update(){
		HeightDif = Height - TargetHeight;
		a = - K * HeightDif - Damping * Speed;
		Height += Speed * Time.deltaTime;
		Speed += a * Time.deltaTime;
		_t.position = center+ Di * Height;
		

	}
	
	float HeightOld;
	public void UpdateVerlet(){
		float temp = Height;
		
		HeightDif = Height - TargetHeight;
		a = - K * HeightDif - Damping * Speed;
		
		Height = Height + 1f * (Height - HeightOld) + a * Time.deltaTime * Time.deltaTime;
		HeightOld = temp;
		
		//Height += Speed * Time.deltaTime;
		Speed += a * Time.deltaTime;
		_t.position = center+ Di * Height;
		
	}
	
	//apply positive force will act fr center to peremeter
	public void ApplyForce(float f){
		//f = Mathf.Clamp(f,-tolerance,tolerance);
		Speed += f;
	
	}
	
	public void ApplyForceVerlet(float f){
		//f = Mathf.Clamp(f,-tolerance,tolerance);
		HeightOld += f;
		
	}
	
	
	
}

