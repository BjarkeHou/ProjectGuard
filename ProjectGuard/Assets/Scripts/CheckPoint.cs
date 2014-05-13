using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour
{

		public float healSpeed;

		public AudioClip activateSound;
		private AudioSource activateSource;
		private HealthController hCont;
		private GhostWorldController ghostCtrl;
		private ParticleSystem[] particles;
		private Light[] lights;

		// Use this for initialization
		void Start ()
		{
				activateSource = gameObject.AddComponent<AudioSource> ();
				activateSource.clip = activateSound;
				activateSource.playOnAwake = false;
				ghostCtrl = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GhostWorldController> ();
				particles = transform.GetComponentsInChildren<ParticleSystem> ();
				lights = transform.GetComponentsInChildren<Light> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (tag == "ActiveCheckpoint") {
						foreach (Light light in lights) { 
								light.enabled = true;
						}

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
								foreach (Light light in lights) { 
										light.enabled = false;
								}
						}
				}
		}

		void OnTriggerStay (Collider other)
		{
				if (other.tag == "Player" && ghostCtrl.deathTransition <= 0) {
						if (tag != "ActiveCheckpoint") {
								if (GameObject.FindGameObjectWithTag ("ActiveCheckpoint") != null) {
										GameObject.FindGameObjectWithTag ("ActiveCheckpoint").tag = "Untagged";
								}
								tag = "ActiveCheckpoint";
								activateSource.Play ();
						}
						hCont = other.GetComponent<HealthController> ();
						hCont.adjustCurrentHealth (Time.deltaTime * healSpeed);
				}
		}
}
