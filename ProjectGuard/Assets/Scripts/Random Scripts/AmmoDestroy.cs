using UnityEngine;
using System.Collections;

public class AmmoDestroy : MonoBehaviour {

	private float timer;

	// Use this for initialization
	void Start () {
		timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.rigidbody.velocity.x > 0.1f) {
			timer = Time.time;
		}
		if (gameObject.rigidbody.velocity.y > 0.1f) {
			timer = Time.time;
		}
		if (gameObject.rigidbody.velocity.z > 0.1f) {
			timer = Time.time;
		}

		if (Time.time > timer + 1.5f) {
			Destroy (gameObject);
		}
	}
}
