using UnityEngine;
using System.Collections;

public class ScreenEdgeCollider : MonoBehaviour {

	EdgeCollider2D edgeCollider;
	Vector2[] pt = new Vector2[5];
	Camera c;

	void Start () {
		c = this.camera;
		edgeCollider = this.GetComponent<EdgeCollider2D>();
	
		pt[0] = c.ViewportToWorldPoint(new Vector3 (0f,0f,0f))-transform.position;
		pt[1] = c.ViewportToWorldPoint(new Vector3 (0f,1f,0f))-transform.position;
		pt[2] = c.ViewportToWorldPoint(new Vector3 (1f,1f,0f))-transform.position;
		pt[3] = c.ViewportToWorldPoint(new Vector3 (1f,0f,0f))-transform.position;
		pt[4] = c.ViewportToWorldPoint(new Vector3 (0f,0f,0f))-transform.position;

		edgeCollider.points = pt;

	}
	
	
	void OnDrawGizmos () {
		Vector2[] pts = new Vector2[5];
		Gizmos.color = Color.green;
		pts[0] = this.camera.ViewportToWorldPoint(new Vector3 (0f,0f,0f));
		pts[1] = this.camera.ViewportToWorldPoint(new Vector3 (0f,1f,0f));
		pts[2] = this.camera.ViewportToWorldPoint(new Vector3 (1f,1f,0f));
		pts[3] = this.camera.ViewportToWorldPoint(new Vector3 (1f,0f,0f));
		Gizmos.DrawLine (pts [0], pts [1]);
		Gizmos.DrawLine (pts [1], pts [2]);
		Gizmos.DrawLine (pts [2], pts [3]);
		Gizmos.DrawLine (pts [3], pts [0]);
	}
	

}
