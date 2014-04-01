using UnityEngine;
using System.Collections;

public class GhostWorldController : MonoBehaviour {

	private GameObject player;
	private GhostWorld gWorld;

	public float deathTransition;
	public float deathTransitionSpeed;

	private bool fading;
	private bool prevPlayerAlive;

	// Use this for initialization
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		gWorld = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GhostWorld>();
	}
	
	// Update is called once per frame
	void Update() {
		if (player.GetComponent<HealthController>().stillAlive() != prevPlayerAlive) {
			fading = true;
			prevPlayerAlive = player.GetComponent<HealthController>().stillAlive();
		}

		if (fading) {
			if (!player.GetComponent<HealthController>().stillAlive() && deathTransition < 1) {
				deathTransition += deathTransitionSpeed * Time.deltaTime;

			} else if (player.GetComponent<HealthController>().stillAlive() && deathTransition > 0) {
				deathTransition -= deathTransitionSpeed * Time.deltaTime;
			} else {
				fading = false;
			}
		}
		gWorld.transition = deathTransition;
	}
}
