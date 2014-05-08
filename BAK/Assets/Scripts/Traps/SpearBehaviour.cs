using UnityEngine;
using System.Collections;

public class SpearBehaviour : MonoBehaviour {
	
	private TrapDeadlyPart deadlyPart;
	private bool isTriggered;
	public float triggerDelay;
	private float triggerTimer;

	public bool isRepeatable;
	public float repeatDelay;
	private float repeatTimer;
	private bool canTrigger;

	void Start() {
		deadlyPart = GetComponentInChildren<TrapDeadlyPart>();

		triggerTimer = 0;
		canTrigger = true;

		repeatTimer = -repeatDelay;
	}

	void Update() {
		if (isTriggered) {
			canTrigger = false;
			Behave();
		}
	}

	public void Trigger() {
		if (canTrigger && Time.time > repeatTimer + repeatDelay) {
			isTriggered = true;
		}
	}

	void Behave() {
		if (triggerTimer == 0) {
			triggerTimer = Time.time;
		}

		if (Time.time > triggerTimer + triggerDelay) {
			GetComponent<Animator>().SetBool("Trigger", true);
		}
	}

	void Reset() {
		GetComponent<Animator>().SetBool("Trigger", false);
		isTriggered = false;
		triggerTimer = 0;
		repeatTimer = Time.time;

		if (isRepeatable) {
			deadlyPart.targetsHit.Clear();
			canTrigger = true;
		}
	}

	void AudioPlay() {
		audio.Play();
	}
}