using UnityEngine;
using System.Collections;

public class BlurIterationGUI : MonoBehaviour {

	GlowEffect ge;
	tk2dTextMesh text;
	
	void Start () {
		ge = Camera.main.GetComponent<GlowEffect>();
		text = this.GetComponent<tk2dTextMesh>();
		text.text = "Blur Iteration: "+ge.blurIterations.ToString();
		Debug.Log(text.text);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnBlurBtnPlus(){
		ge.blurIterations += 1;
		text.text = "Blur Iteration: "+ge.blurIterations;
		
	}
	
	public void OnBlurBtnMinus(){
		ge.blurIterations -= 1;
		text.text = "Blur Iteration: "+ge.blurIterations;
	}
}
