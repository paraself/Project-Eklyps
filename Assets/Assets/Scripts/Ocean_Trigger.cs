using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ocean_Trigger : MonoBehaviour {
	
	Ocean_Spring spring;
	public Spring sp;//will be feed in with Ocean_Springs
	bool isInTrigger = false;
	[SerializeField]
	List<Rigidbody2D> targets = new List<Rigidbody2D> ();
	float sqrRange;
	
	#region Unity Monos
	void Awake(){
		
	}
	
	void Start () {
		sqrRange = this.GetComponent<CircleCollider2D>().radius;
		sqrRange *= sqrRange;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (targets.Count >0 ) {
			for (int i=0;i<targets.Count;i++){
				float force = targets[i].velocity.sqrMagnitude * 0.001f;
				sp.ApplyForce(force);
				targets[i].AddForce(-sp.Di*force);
				DebugExtension.DebugPoint(sp.Pos3,Color.red,20f,0.5f,false);
				
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D c){
		//print("Trigger");
		Rigidbody2D b = c.transform.rigidbody2D;
		if (!targets.Contains(b)) targets.Add(b);
	}
		
		
	
	
	void OnCollisionExit2D(Collision2D c){
		Rigidbody2D b = c.transform.rigidbody2D;
		if (targets.Contains(b)) targets.Remove(b);
	}
	
	
	
	#endregion
	
}
