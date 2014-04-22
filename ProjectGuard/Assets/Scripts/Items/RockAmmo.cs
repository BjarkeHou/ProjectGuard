using UnityEngine;
using System.Collections;

public class RockAmmo : MonoBehaviour {

	public float limit;
	private ParticleSystem part;

	// Use this for initialization
	void Start() {
		part = GetComponentInChildren<ParticleSystem>();
		part.Stop();
	}
	
	// Update is called once per frame
	void Update() {
		if (rigidbody.velocity.magnitude > limit) {
			print("emitting");
			part.Play();
		} else {
			part.Stop();
		}
	}
}
