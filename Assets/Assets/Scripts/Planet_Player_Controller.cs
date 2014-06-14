using UnityEngine;
using System.Collections;

/// <summary>
/// C_ easy joystick template.
/// </summary>
public class Planet_Player_Controller : MonoBehaviour {
	
	public float maxForce = 800f;
	
	Vector2 force;
	
	Rigidbody2D rb;
	bool isMoving = false;
	Vector2 joystickValue;
	
	void Start(){
		rb = this.rigidbody2D;
		
	}

#region Unity Monobehaviours
	void Update() {
		
		if (isMoving) {
			force = joystickValue * maxForce * Time.deltaTime;
			
		}
		
	}
	
	
	void FixedUpdate(){
		if (isMoving) {
			
			rb.AddForce(force);
			//Debug.Log(force);
		}
		
	}
#endregion
		
#region EasyTouch Events Subscribe
	void OnEnable(){
		//EasyJoystick.On_JoystickTouchStart += On_JoystickTouchStart;
		EasyJoystick.On_JoystickMoveStart += On_JoystickMoveStart;
		EasyJoystick.On_JoystickMove += On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
		EasyJoystick.On_JoystickTouchUp += On_JoystickTouchUp;
		EasyJoystick.On_JoystickTap += On_JoystickTap;
		//EasyJoystick.On_JoystickDoubleTap += On_JoystickDoubleTap;
	}
	
	void OnDisable(){
		//EasyJoystick.On_JoystickTouchStart -= On_JoystickTouchStart;
		EasyJoystick.On_JoystickMoveStart -= On_JoystickMoveStart;
		EasyJoystick.On_JoystickMove -= On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
		EasyJoystick.On_JoystickTouchUp -= On_JoystickTouchUp;
		EasyJoystick.On_JoystickTap -= On_JoystickTap;
		//EasyJoystick.On_JoystickDoubleTap -= On_JoystickDoubleTap;
	}
	
	void OnDestroy(){
		//EasyJoystick.On_JoystickTouchStart -= On_JoystickTouchStart;
		EasyJoystick.On_JoystickMoveStart -= On_JoystickMoveStart;
		EasyJoystick.On_JoystickMove -= On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
		EasyJoystick.On_JoystickTouchUp -= On_JoystickTouchUp;
		EasyJoystick.On_JoystickTap -= On_JoystickTap;
		//EasyJoystick.On_JoystickDoubleTap -= On_JoystickDoubleTap;
	}
#endregion 

#region EasyTouch Event Methods
	
	//void On_JoystickDoubleTap (MovingJoystick move){Debug.Log("On_JoystickDoubleTap");}
	
	void On_JoystickTap (MovingJoystick move){Debug.Log("On_JoystickTap");}
	
	void On_JoystickTouchUp (MovingJoystick move){Debug.Log("On_JoystickTouchUp");}
	
	void On_JoystickMoveStart (MovingJoystick move){
		Debug.Log("On_JoystickMoveStart");
		isMoving = true;
	}
	
	void On_JoystickMoveEnd (MovingJoystick move){
		Debug.Log("On_JoystickMoveEnd");
		isMoving = false;
	}

	//void On_JoystickTouchStart (MovingJoystick move){Debug.Log("On_JoystickTouchStart");}
	
	void On_JoystickMove(MovingJoystick move){
		//Debug.Log("On_JoystickMove");
		joystickValue = move.joystickValue;
	}
#endregion
	
	
}