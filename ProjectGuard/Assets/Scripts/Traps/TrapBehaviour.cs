using UnityEngine;
using System.Collections;

public class TrapBehaviour : MonoBehaviour {

	private TrapController trapCtrl;

	// Use this for initialization
	void Start() {
		trapCtrl = GetComponent<TrapController>();
		Transform parent = transform.parent;
		while (trapCtrl == null) {
			if (parent.GetComponent<TrapController>() == null) {
				parent = parent.parent;
			} else {
				trapCtrl = parent.GetComponent<TrapController>();
			}
		}
	}
	
	// Update is called once per frame
	void Update() {
		if (trapCtrl.isTriggered) {
			Behave();
		}
	}

	void Behave() {
		GetComponent<Animator>().SetBool("Trigger", true);
	}

	void Rewind() {

	}
}
