using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {

	private TrapController trapCtrl;
	private GameController game;

	// Use this for initialization
	void Start() {
		trapCtrl = transform.parent.GetComponent<TrapController>();
		game = GameObject.Find("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			if (!trapCtrl.isTriggered && !trapCtrl.isDisarmed) {
				if (!game.isInGhostMode) {
					trapCtrl.isTriggered = true;
				} else if (Input.GetKey(KeyCode.E)) {
					trapCtrl.Disarm();
				} else {
					transform.parent.Find("Disarm").guiTexture.enabled = false;
				}
			} else {
				transform.parent.Find("Disarm").guiTexture.enabled = false;
			}
		}
	}

	void OnTriggerLeave(Collider other) {
		if (other.tag == "Player") {
			transform.parent.Find("Disarm").guiTexture.enabled = false;
		}
	}
}
