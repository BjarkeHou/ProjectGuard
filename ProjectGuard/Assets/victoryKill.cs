using UnityEngine;
using System.Collections;

public class victoryKill : MonoBehaviour {

	private HealthController hCtrl;

	// Use this for initialization
	void Start() {
		hCtrl = GetComponent<HealthController>();
	}
	
	// Update is called once per frame
	void Update() {
		if (!hCtrl.stillAlive()) {
			Application.LoadLevel("End");
		}
	}
}
