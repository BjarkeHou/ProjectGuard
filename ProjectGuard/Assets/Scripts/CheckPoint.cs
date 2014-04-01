using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	private HealthController hCont;
	private GameObject gameManager;
	private ParticleSystem[] particles;

	// Use this for initialization
	void Start() {
		gameManager = GameObject.Find("GameManager");
		particles = transform.GetComponentsInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update() {
		if (gameManager.GetComponent<GhostWorldController>().deathTransition > 1) {
			foreach (ParticleSystem part in particles) {
				part.enableEmission = true;
			}
		} else {
			foreach (ParticleSystem part in particles) {
				part.enableEmission = false;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			hCont = other.GetComponent<HealthController>();
			hCont.adjustCurrentHealth(hCont.getMaxHealth() - hCont.getCurrentHealth());
		}
	}
}
