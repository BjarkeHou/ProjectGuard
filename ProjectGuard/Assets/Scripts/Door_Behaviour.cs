using UnityEngine;
using System.Collections;

public class Door_Behaviour : MonoBehaviour {
	 
	private bool canOpen;

	void LateUpdate() {
		canOpen = true;
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			if (Input.GetKeyDown(KeyCode.E) && canOpen) { 
				canOpen = false;
				transform.parent.GetComponent<Animator>().SetBool("Open", !transform.parent.GetComponent<Animator>().GetBool("Open"));
			}
		}
	}
}
