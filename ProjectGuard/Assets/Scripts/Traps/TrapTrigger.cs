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
		if (!trapCtrl.isTriggered && !trapCtrl.isDisarmed) {
			print("in trigger");
			if (!game.isInGhostMode) {
				trapCtrl.isTriggered = true;
			} else if (Input.GetKey(KeyCode.E)) {
				print("disarmed!");
				trapCtrl.isDisarmed = true;
			}
		}
	}
}
