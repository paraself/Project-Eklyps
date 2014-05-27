using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public RageCircle c;

	// Use this for initialization
	void Start () {
		c = new RageCircle ();
		c.Radius = 15f;
		c.Width = 3f;
		c.RefreshMesh(true,true,true);
		//LeanTween.value(c.gameObject,changeRadius,10f,200f,3f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
		c.TweenRadius (100f,200f,3f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void changeRadius(float val){
		c.Radius = val;
		c.RefreshMesh();
	}
}
