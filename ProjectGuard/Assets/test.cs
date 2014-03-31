using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	public Camera cam;

	
	// Update is called once per frame
	void OnPreRender() {
		cam.Render();
	}
}
