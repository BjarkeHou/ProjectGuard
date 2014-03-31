using UnityEngine;
using System.Collections;

public class GhostWorldController : MonoBehaviour {

	private GameObject player;
	private GhostWorld gWorld;

	private float deathTransition;
	public float DeathTransition { get { return deathTransition; } }
	public float deathTransitionSpeed;

	// Use this for initialization
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		gWorld = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GhostWorld>();
	}
	
	// Update is called once per frame
	void Update() {
		if (!player.GetComponent<HealthController>().stillAlive() && deathTransition < 1) {
			deathTransition += deathTransitionSpeed * Time.deltaTime;
			gWorld.transition = deathTransition;

		} else if (player.GetComponent<HealthController>().stillAlive() && deathTransition > 0) {
			deathTransition -= deathTransitionSpeed * Time.deltaTime;
			gWorld.transition = deathTransition;
		}
	}
}
