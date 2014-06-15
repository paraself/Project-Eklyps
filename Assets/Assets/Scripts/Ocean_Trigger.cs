using UnityEngine;
using System.Collections;

public class Ocean_Trigger : MonoBehaviour {
	
	Ocean_Spring spring;
	public Spring sp;//will be feed in with Ocean_Springs
	bool isInTrigger = false;
	
	#region Unity Monos
	void Awake(){
		
	}
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay2D(Collider2D c){
		print("Trigger");
		/*
		float force = 100000000f/(c.transform.position - this.transform.position).sqrMagnitude;
		sp.ApplyForce(force);
		c.rigidbody2D.AddForceAtPosition(-sp.Di * force,sp.Pos2);
		*/
		float force = c.rigidbody2D.velocity.sqrMagnitude/1000f;
		if (sp.isDebug) Debug.Log("Force = "+force);
		
		sp.ApplyForce(force);
		c.rigidbody2D.AddForceAtPosition(-sp.Di*force*10f,sp.Pos2);
		//sp.ApplyForceVerlet(force);
	}
	
	
	
	#endregion
	
}
