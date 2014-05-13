using UnityEngine;
using System.Collections;

public class MainMenuFog: MonoBehaviour {
	public GameObject fog1;
	public GameObject fog2;
	public GameObject fog3;

	public float spawnDelay;
	public float heightDiff;
	public float speed;
	public float speedDiff;

	private float timer;

	// Use this for initialization
	void Start() {
		timer = Time.time - spawnDelay;
	}
	
	// Update is called once per frame
	void Update() {
		if (Time.time > timer + spawnDelay) {
			timer = Time.time;
			GameObject fog = (GameObject)Instantiate(fog1);
			fog.transform.parent = transform;

			bool goLeft = true;
			if (Random.Range(-5, 5) >= 0) {
				goLeft = false;
			}
			if (goLeft) {
				fog.transform.localPosition = new Vector3 (-2.2f, transform.localPosition.y + Random.Range(-heightDiff, heightDiff), 0);
			} else {
				fog.transform.localPosition = new Vector3 (2.2f, transform.localPosition.y + Random.Range(-heightDiff, heightDiff), 0);
			}
			fog.GetComponent<MainMenuFog_Destroy>().goLeft = goLeft;
			fog.GetComponent<MainMenuFog_Destroy>().speed = Random.Range(speed - speedDiff, speed + speedDiff);
		}
	}
}
