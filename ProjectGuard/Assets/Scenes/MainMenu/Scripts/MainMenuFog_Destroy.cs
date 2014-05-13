using UnityEngine;
using System.Collections;

public class MainMenuFog_Destroy : MonoBehaviour {

	public bool goLeft;
	public float speed;

	// Use this for initialization
	void Start() {
		if (!goLeft) {
			speed *= -1;
		}
	}
	
	// Update is called once per frame
	void Update() {
		transform.position = new Vector3 (transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);

		if (transform.localPosition.x < -2.5f) {
			Destroy(gameObject);
		} else if (transform.localPosition.x > 2.5f) {
			Destroy(gameObject);
		}
	}
}
