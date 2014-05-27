using UnityEngine;
using System.Collections;


public class SimpleController_2D : MonoBehaviour {

	public float maxVerticalForce;
	public float maxHorizontalForce;
	float verticalForce;
	float horizontalForce;
	Rigidbody2D rb;
	
	void Start(){
		rb = this.rigidbody2D;
		float a = Random.Range(-10000f,10000f);
		float b = Random.Range(-10000f,10000f);
		rb.AddForce(new Vector2 (a,b));
		//Application.targetFrameRate = 60;
	}
	
	void Update() {
		
		verticalForce = Input.GetAxis("Vertical") * maxVerticalForce * Time.deltaTime;
		horizontalForce = Input.GetAxis("Horizontal") * maxHorizontalForce * Time.deltaTime;
		
	}
	
	void FixedUpdate(){
	
		Vector2 v1 = new Vector2(horizontalForce,verticalForce);
		rb.AddForce(v1);
		//rb.velocity = v1;

	}
		
		
}