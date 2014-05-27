using UnityEngine;
using System.Collections;

public class TestGUI : MonoBehaviour {

	bool useBloom;
	
	public Transform CirclePrefab;

	void Start () {

		Application.targetFrameRate = 60;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool _useBloom;

	void OnGUI () {
		GUILayout.BeginArea(new Rect(100,100,400,400));
		_useBloom = useBloom;
		useBloom = GUILayout.Toggle(useBloom,"Use Bloom");
		if (_useBloom != useBloom) {

		}

		if (GUILayout.Button("Add Circle",GUILayout.Width(100f))){
			Instantiate(CirclePrefab,Vector3.zero,Quaternion.identity);
		}
		GUILayout.EndArea();

	}


}
