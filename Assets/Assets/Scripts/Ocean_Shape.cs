using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class Ocean_Shape : MonoBehaviour {

	//the inner radius of the ocean
	public float radius_inner = 100f;
	float _radius_inner_cache;
	
	//the outter radius of the ocean
	public float radius_outter = 150f;
	float _radius_outter_cache;
	
	//the number of points, dont change this in runtime
	public int numberOfPoints = 10;
	int numberOfPoints_cache;

	Transform _thisTransform;
	Spline _spline;
	SplineNode[] _points;
	
	// how fast does the shape rotate for a phase. Positive->clockwise
	// in 0-1 space, per second
	public float cycleSpeed; 
	float cycleLength;
	public float cyclePos = 0f;

#region Properties
	//internal reference of the Spline
	private Spline Spline {
		get {
			if (_spline==null) {
				_spline = this.GetComponent<Spline>();
				if (_spline) return _spline; else return null;
			} else {
				return _spline;
			}
		}
	}
	
	public Vector2 Pos2 {
		get {
			return new Vector2(Tf.position.x,Tf.position.y);
		}
	}
	
	private Vector2 Pos3 {
		get {
			return Tf.position;
		}
	}
	
	private Transform Tf {
		get {
			if (_thisTransform) return _thisTransform; else {
				_thisTransform = this.transform;
				return _thisTransform;
			}
		}
	}
	
	public float Length {
		get {
			return Spline.Length;
		}
	}
	
	
#endregion
	
#region Unity Monos
	
	void Awake (){
		_spline = this.GetComponent<Spline>();
		cycleLength = 1f/numberOfPoints;
	}
	
	void Start () {
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		ValueCheck();
		RemoveNullNodes();//double check so that we dont have too many nodes
		//points number changed
		if (numberOfPoints!=numberOfPoints_cache){
			numberOfPoints_cache = numberOfPoints;
			SplineRegenerator(numberOfPoints);
		}
		
		if (radius_inner!=_radius_inner_cache){
			_radius_inner_cache = radius_inner;
			SplineRegenerator(numberOfPoints);
		}
		if (radius_outter!=_radius_outter_cache){
			_radius_outter_cache = radius_outter;
			SplineRegenerator(numberOfPoints);
		}
		
		if (Application.isPlaying) {
			cyclePos += cycleSpeed * Time.deltaTime;
			if (cyclePos>cycleLength) cyclePos = cyclePos - cycleLength;
		}
		
		
	}
#endregion

#region Methods
	void ValueCheck(){
		
		numberOfPoints = Mathf.Clamp(numberOfPoints,2,50);
		radius_outter = Mathf.Clamp(radius_outter,radius_inner,100000f);
		radius_inner = Mathf.Clamp(radius_inner,0f,radius_outter);
		cycleSpeed = Mathf.Clamp(cycleSpeed,-1000f,1000f);

	}
	
	
	//generate the control points for b spline,internal use
	//r1 = inner radius, r2 = outter radius, n = number of points
	void SplineRegenerator(int n = 10) {
		
		if (_spline==null) _spline = this.GetComponent<Spline>();
		
		RemoveNullNodes();
		
		_spline.autoClose = true;
		
		Vector2[] pt = new Vector2 [2*n];
		float deltaA = 360f / n ;
		
		cycleLength = 1f/n;
		
		Vector2 vi = Vector2.up * radius_inner;
		Vector2 vo = Vector2.up * radius_outter;
		vo = Quaternion.Euler(0f,0f,deltaA * 0.5f) * vo;
		
		Quaternion deltaQ = Quaternion.Euler(0f,0f,deltaA);
		
		for (int i=0;i<n;i++){
			pt[i*2]= Pos2 + vi;
			pt[i*2+1] = Pos2 + vo;
			vi = deltaQ * vi;
			vo = deltaQ * vo;
		}
		
		int curNodeCont = _spline.splineNodesArray.Count;
		if (curNodeCont < 2*n) {
			for (int i=0;i< 2 * n;i++){
				//if we exceed the no of existing nodes we need to create new node
				if (i>=curNodeCont) {
					_spline.AddSplineNode(_spline.SplineNodes[_spline.splineNodesArray.Count-1]);
					_spline.splineNodesArray[i].transform.parent = this.transform;
				}
			}
		} else if (curNodeCont > 2*n) {
			for (int i=curNodeCont-1;i>=2 * n;i--){
				_spline.RemoveSplineNode(_spline.splineNodesArray[i]);
				//DestroyImmediate(_spline.splineNodesArray[i].gameObject); dont know why it doesnt work.
			}
		}
		
		for (int i=0;i<2*n;i++){
			_spline.splineNodesArray[i].Position = pt[i];
		}
		
		RemoveNullNodes();
		
		
		
	}
	
	void RemoveNullNodes(){
		SplineNode sn1;
		foreach (Transform t in this.transform) {
			sn1 = t.GetComponent<SplineNode>();
			if (!_spline.splineNodesArray.Contains(sn1)) {
				DestroyImmediate(t.gameObject);
			}
		}
	}
	
	public float GetRadiusAtPosition(float p){
		p = Mathf.Clamp01(p);
		Vector3 po = _spline.GetPositionOnSpline(p);
		Vector2 r = Pos2 - new Vector2 (po.x,po.y);
		return r.magnitude;
	}
	
	public float GetRadiusAtPositionCycle(float p){
		p = Mathf.Clamp01(p);
		float t = p+cyclePos;
		if (t>1) t = t-cycleLength;
		Vector3 po = _spline.GetPositionOnSpline(t);
		Vector2 r = Pos2 - new Vector2 (po.x,po.y);
		return r.magnitude;
	}
	
	//point from cneter to peremeter
	public Vector2 GetDirection(float p){
		p = Mathf.Clamp01(p);
		Vector3 po = _spline.GetPositionOnSpline(p);
		Vector2 r = new Vector2 (po.x,po.y) - Pos2;
		return r;
	}
	
	public Vector2 GetPos2(float p){
		p = Mathf.Clamp01(p);
		Vector3 po = _spline.GetPositionOnSpline(p);
		return new Vector2(po.x,po.y);
	}
	
	//
	public Vector2 GetPos2Cycle(float p){
		float t = p+cyclePos;
		if (t>1) t = t-cycleLength;
		return GetPos2(t);
		
	}
#endregion

#region Gizmo
	void OnDrawGizmos(){

		DebugExtension.DrawCircle(this.transform.position,Vector3.forward,Color.gray, radius_inner);
		DebugExtension.DrawCircle(this.transform.position,Vector3.forward,Color.gray,radius_outter);
	}
#endregion
}
