using UnityEngine;
using System.Collections;

public class DeathSmoke : MonoBehaviour {

	private float timer;

	// Use this for initialization
	void Start() {
		timer = Time.time;
	}
	
	// Update is called once per frame
	void Update() {
		if (Time.time > timer + 0.2f) {
			GetComponent<ParticleSystem>().Stop();
			if (Time.time > timer + 0.2f + GetComponent<ParticleSystem>().startLifetime) {
				Destroy(gameObject);
			}
		}
	}
}
