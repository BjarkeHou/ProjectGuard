using UnityEngine;
using System.Collections;

public class SparkDestroy : MonoBehaviour {

	private float timer;

	// Use this for initialization
	void Start () {
		timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.GetComponent<ParticleSystem>().particleCount == 0 && Time.time > timer +0.1f) {
			Destroy(gameObject);
		}
	}
}
