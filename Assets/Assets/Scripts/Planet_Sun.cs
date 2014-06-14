using UnityEngine;
using System.Collections;
using antilunchbox;// in order to use that singleton crap

public class Planet_Sun : Singleton<Planet_Sun> {

	RageCircle _p;

	//Singleton Initialization
	public static Planet_Sun Instance {
		get {
			return ((Planet_Sun)mInstance);
		} set {
			mInstance = value;
		}
	}

#region Properties
	public RageCircle RCircle {
		get {
			if (_p) return _p; else {
				Debug.Log(name+": did not find planet_mono");
				return null;
			}
		}
	}

#endregion

#region Unity Monos

	void Awake (){
		_p = this.GetComponent<RageCircle>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.Equals(Planet_Player.Instance.collider2D)) {
			Debug.Log("Eklyps detected");
			WorldManager.Instance.IsEklypsing = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.Equals(Planet_Player.Instance.collider2D)) {
			Debug.Log("Eklyps exit");
			WorldManager.Instance.IsEklypsing = false;
		}
	}

#endregion

}
