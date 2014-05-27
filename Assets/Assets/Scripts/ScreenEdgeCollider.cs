using UnityEngine;
using System.Collections;

public class ScreenEdgeCollider : MonoBehaviour {

	EdgeCollider2D edgeCollider;
	Vector2[] pt = new Vector2[5];
	Camera c;

	void Start () {
		c = this.camera;
		edgeCollider = this.GetComponent<EdgeCollider2D>();
	
		pt[0] = c.ViewportToWorldPoint(new Vector3 (0f,0f,0f));
		pt[1] = c.ViewportToWorldPoint(new Vector3 (0f,1f,0f));
		pt[2] = c.ViewportToWorldPoint(new Vector3 (1f,1f,0f));
		pt[3] = c.ViewportToWorldPoint(new Vector3 (1f,0f,0f));
		pt[4] = c.ViewportToWorldPoint(new Vector3 (0f,0f,0f));

		edgeCollider.points = pt;

	}
	

}
