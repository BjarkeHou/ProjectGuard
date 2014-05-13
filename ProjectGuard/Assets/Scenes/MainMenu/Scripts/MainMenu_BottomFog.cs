using UnityEngine;
using System.Collections;

public class MainMenu_BottomFog : MonoBehaviour {

	public float speed;
	private bool hasSpawned;

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
		transform.position = new Vector3 (transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
		if (transform.localPosition.x > 0 && !hasSpawned) {
			hasSpawned = true;
			GameObject bottomFog = (GameObject)Instantiate(gameObject);
			bottomFog.transform.parent = transform.parent;
			bottomFog.transform.localPosition = new Vector3 (-5, transform.localPosition.y, transform.localPosition.z);
			//bottomFog.AddComponent<MainMenu_BottomFog>();
		}

		if (transform.localPosition.x > 5.5f) {
			Destroy(gameObject);
		}
	}
}
