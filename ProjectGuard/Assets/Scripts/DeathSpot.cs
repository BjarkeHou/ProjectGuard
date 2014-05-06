using UnityEngine;
using System.Collections;

public class DeathSpot : MonoBehaviour {

	private GameController game;
	private HealthController hCtrl;
	public float reviveSpeed;
	private bool spawn;

	// Use this for initialization
	void Start() {
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	void Update() {
		if (!game.isInGhostMode) {
			Destroy(gameObject);
		}

		if (spawn && game.isInGhostMode) {
			hCtrl = GameObject.FindWithTag("Player").GetComponent<HealthController>();
			if (game.timeLeftToReviveFromGhostMode <= game.timeToReviveInGhostMode) {
				GameObject.FindWithTag("Player").GetComponent<MovementController>().SetCanMove(false);
				game.timeLeftToReviveFromGhostMode += reviveSpeed * Time.deltaTime;
			} else {
				hCtrl.adjustCurrentHealth(hCtrl.getMaxHealth() - hCtrl.getCurrentHealth());
				GameObject.FindWithTag("Player").GetComponent<MovementController>().SetCanMove(true);
			}
		} else if (game.isInGhostMode) {
			spawn = false;
		}
	}

	void OnTriggerStay(Collider other) {
		if (Input.GetKeyDown(KeyCode.R)) {
			spawn = true;

		}
	}
}
