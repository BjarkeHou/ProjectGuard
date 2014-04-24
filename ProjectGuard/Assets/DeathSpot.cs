using UnityEngine;
using System.Collections;

public class DeathSpot : MonoBehaviour {

	private GameController game;
	private HealthController hCtrl;
	public float reviveSpeed;

	// Use this for initialization
	void Start() {
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	void Update() {
		if (!game.isInGhostMode) {
			Destroy(gameObject);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player" && game.isInGhostMode) {
			hCtrl = other.GetComponent<HealthController>();
			if (game.timeLeftToReviveFromGhostMode <= game.timeToReviveInGhostMode) {
				game.timeLeftToReviveFromGhostMode += reviveSpeed * Time.deltaTime;
			} else {
				hCtrl.adjustCurrentHealth(hCtrl.getMaxHealth() - hCtrl.getCurrentHealth());
			}
		}
	}
}
