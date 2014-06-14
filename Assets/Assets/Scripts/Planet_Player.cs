using UnityEngine;
using System.Collections;
using antilunchbox;// in order to use that singleton crap

public class Planet_Player : Singleton<Planet_Player> {

	RageCircle _p;

	//Singleton Initialization
	public static Planet_Player Instance {
		get {
			return ((Planet_Player)mInstance);
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
#endregion
}
