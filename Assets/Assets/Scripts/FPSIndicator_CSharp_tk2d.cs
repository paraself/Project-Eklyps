using UnityEngine;
using System.Collections;

public class FPSIndicator_CSharp_tk2d : MonoBehaviour {

	float updateInterval = 0.5f;
	float accum;
	float frames;
	float timeleft;
	
	tk2dTextMesh text;

	void Start () {
		
		text = this.GetComponent<tk2dTextMesh>();
		if (!text) {
			print ("FramesPerSecond needs a 2dtk text mesh component!");
			this.enabled = false;
			return;
		} else {
			text.MakePixelPerfect();
			text.anchor = TextAnchor.UpperLeft;
			
		}
		timeleft = updateInterval; 
	
	}
	
	// Update is called once per frame
	void Update () {
	
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		// Interval ended - update GUI text and start new interval
		if( timeleft <= 0.0f )
		{
			// display two fractional digits (f2 format)
			text.text = "fps:" + (accum/frames).ToString("f2");
			timeleft = updateInterval;
			accum = 0.0f;
			frames = 0f;
			text.Commit();
		}
	
	}
}
