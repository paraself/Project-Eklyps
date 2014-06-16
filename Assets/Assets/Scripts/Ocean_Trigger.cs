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
	void Update () {
		if (targets.Count >0 ) {
			for (int i=targets.Count-2;i>=0;i--){
				Vector3 ds = targets[i].transform.position - sp.Pos3;
				if (ds.sqrMagnitude >= sqrRange) {
					targets.RemoveAt(i);
					continue;
				}
				float force = targets[i].velocity.sqrMagnitude/1000f;
				sp.ApplyForce(force);
				targets[i].AddForceAtPosition(-sp.Di*force*10f,sp.Pos2);
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D c){
		//print("Trigger");
		if (!targets.Contains(c.rigidbody2D)) targets.Add(c.rigidbody2D);
	}
		
		
	
	
	void OnTriggerExit2D(Collider2D c){
		if (targets.Contains(c.rigidbody2D)) targets.Remove(c.rigidbody2D);
	}
	
	
	
	#endregion
	
}
