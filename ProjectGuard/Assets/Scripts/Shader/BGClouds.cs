using UnityEngine;
using System.Collections;

public class BGClouds : MonoBehaviour {

	public Vector3 offset;
	public float lerpSpeed;
	public float randTime;
	private float timer;
	private Vector3 startPos;
	private Vector3 deciredPos;

	// Use this for initialization
	void Start() {
		timer = Time.time - randTime;
		startPos = transform.localPosition;
		deciredPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update() {
		if (Time.time > timer + randTime) {
			deciredPos = new Vector3 (startPos.x + Random.Range(-offset.x, offset.x),
			                         	startPos.y + Random.Range(-offset.y, offset.y),
			                         	startPos.z + Random.Range(-offset.z, offset.z));
			timer = Time.time;
		}

		transform.localPosition = Vector3.Lerp(transform.localPosition, deciredPos, Time.deltaTime * lerpSpeed);
	}
}
