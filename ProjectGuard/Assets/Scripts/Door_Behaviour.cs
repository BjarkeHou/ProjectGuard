using UnityEngine;
using System.Collections;

public class Door_Behaviour : MonoBehaviour {

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			if (Input.GetKeyDown(KeyCode.E)) {
				transform.parent.GetComponent<Animator>().SetBool("Open", !transform.parent.GetComponent<Animator>().GetBool("Open"));
			}
		}
	}
}
