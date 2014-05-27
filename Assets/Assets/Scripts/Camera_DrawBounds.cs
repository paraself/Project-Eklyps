using UnityEngine;
using System.Collections;

public class Camera_DrawBounds : MonoBehaviour {



	Vector2[] pt = new Vector2[5];
	void OnDrawGizmos () {
		Gizmos.color = Color.green;
		pt[0] = this.camera.ViewportToWorldPoint(new Vector3 (0f,0f,0f));
		pt[1] = this.camera.ViewportToWorldPoint(new Vector3 (0f,1f,0f));
		pt[2] = this.camera.ViewportToWorldPoint(new Vector3 (1f,1f,0f));
		pt[3] = this.camera.ViewportToWorldPoint(new Vector3 (1f,0f,0f));
		Gizmos.DrawLine (pt [0], pt [1]);
		Gizmos.DrawLine (pt [1], pt [2]);
		Gizmos.DrawLine (pt [2], pt [3]);
		Gizmos.DrawLine (pt [3], pt [0]);
	}
}
