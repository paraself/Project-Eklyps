using UnityEngine;
using System.Collections;

public class CameraManager {
	
	static Camera _mainCamera;
	
	public static Camera MainCamera {
		get {
			if (_mainCamera == null ) {
				_mainCamera = Camera.main;
				return _mainCamera;
			} else return _mainCamera;
		}
	}
	
	public static LTDescr TweenBackgroundColor(Color c1, Color c2,float t){
		if (t<=0) return null;
		else {
			//_tempColor = new Vector3(c2.r,c2.r,c2.b);
			return LeanTween.value(
				CameraManager.MainCamera.gameObject,
				_tweenBackgroundColor,
				new Vector3(c1.r,c1.g,c1.b),
				new Vector3(c2.r,c2.g,c2.b),
				t);
		}
	}
	
	static void _tweenBackgroundColor (Vector3 x){
		CameraManager.MainCamera.backgroundColor = new Color(x.x,x.y,x.z);
	}
}
