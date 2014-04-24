using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public AudioClip activateSound;
	private AudioSource activateSource;
	private HealthController hCont;
	private GhostWorldController ghostCtrl;
	private ParticleSystem[] particles;

	// Use this for initialization
	void Start() {
		activateSource = gameObject.AddComponent<AudioSource>();
		activateSource.clip = activateSound;
		activateSource.playOnAwake = false;
		ghostCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GhostWorldController>();
		particles = transform.GetComponentsInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update() {
		if (tag == "ActiveCheckpoint") {
			if (ghostCtrl.deathTransition >= 1) {
				foreach (ParticleSystem part in particles) {
					if (part.gameObject.name == "GhostParticles") {
						part.enableEmission = true;
					} else {
						part.enableEmission = false;
					}
				}
			} else {
				foreach (ParticleSystem part in particles) {
					if (part.gameObject.name == "GhostParticles") {
						part.enableEmission = false;
					} else {
						part.enableEmission = true;
					}
				}
			}
		} else {
			foreach (ParticleSystem part in particles) {
				part.enableEmission = false;
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player" && ghostCtrl.deathTransition <= 0) {
			if (tag != "ActiveCheckpoint") {
				if (GameObject.FindGameObjectWithTag("ActiveCheckpoint") != null) {
					GameObject.FindGameObjectWithTag("ActiveCheckpoint").tag = "Untagged";
				}
				tag = "ActiveCheckpoint";
				activateSource.Play();
			}
			hCont = other.GetComponent<HealthController>();
			hCont.adjustCurrentHealth(Mathf.CeilToInt(Time.deltaTime));
		}
	}
}
