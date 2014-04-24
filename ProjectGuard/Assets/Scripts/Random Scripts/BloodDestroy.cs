using UnityEngine;
using System.Collections;

public class BloodDestroy : MonoBehaviour {

	private float startTimer;
	private float particles;
	public float bleedOutSpeed;
	public Vector3 origin;
	
	// Use this for initialization
	void Start() {
		transform.position = origin;
		startTimer = Time.time;
		particles = transform.GetComponent<ParticleSystem>().maxParticles;
	}
	
	// Update is called once per frame
	void Update() {
		particles -= Time.deltaTime * bleedOutSpeed;
		transform.GetComponent<ParticleSystem>().maxParticles = Mathf.CeilToInt(particles);

		if (transform.GetComponent<ParticleSystem>().particleCount <= 0 && Time.time > startTimer + 0.1f) {
			Destroy(gameObject);
		}
	}
}
