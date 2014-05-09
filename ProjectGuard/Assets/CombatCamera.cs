using UnityEngine;
using System.Collections;

public class CombatCamera : MonoBehaviour {
	private PlayerWillController pWill;

	private Vector3 deciredPos;
	private Vector3 attackPos;
	private float curWillNorm;
	public float movLerpSpeed;
	public AnimationCurve curvy;
	public float multiplier;

	private Transform lookTarget;
	private float shakeTimer;
	private float shakeDuration;
	private float shakeMagnitude;
	private float shakeSpeed;

	// Use this for initialization
	void Start() { 
		pWill = GameObject.FindWithTag("Player").GetComponent<PlayerWillController>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		
		attackPos = GameObject.FindWithTag("Player").transform.position - transform.parent.position;
		Vector3 localPoint = transform.InverseTransformPoint(transform.parent.position + (attackPos / 2));
		curWillNorm = Mathf.InverseLerp(0, pWill.MaxWill, pWill.CurWill);

		deciredPos = localPoint * (1 - curWillNorm);
		deciredPos.x = curvy.Evaluate(1 - curWillNorm) * multiplier;

		transform.localPosition = Vector3.Lerp(transform.localPosition, deciredPos, Time.deltaTime * movLerpSpeed);

		Shake();
	}

	public void ScreenShake(float duration, float magnitude, float speed) {
		shakeTimer = Time.time;
		shakeDuration = duration;
		shakeMagnitude = magnitude;
		shakeSpeed = speed;
	}

	void Shake() {
		if (Time.time < shakeTimer + shakeDuration) {
			Vector3 shakeRot = new Vector3 (Random.Range(-shakeMagnitude, shakeMagnitude), Random.Range(-shakeMagnitude, shakeMagnitude), 0);
			Quaternion targetRot = Quaternion.Euler(shakeRot * (1 - Mathf.InverseLerp(shakeTimer, shakeTimer + shakeDuration, Time.time)));
			transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime * shakeSpeed);
		}
	}
}
