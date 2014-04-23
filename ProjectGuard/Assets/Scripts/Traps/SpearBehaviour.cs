using UnityEngine;
using System.Collections;

public class SpearBehaviour : MonoBehaviour {
	
	private TrapDeadlyPart deadlyPart;
	private bool isTriggered;
	public float triggerDelay;
	private float timer;

	public bool isRepeatable;
	private bool canTrigger;

	void Start() {
		deadlyPart = GetComponentInChildren<TrapDeadlyPart>();

		timer = 0;
		canTrigger = true;
	}

	void Update() {
		if (isTriggered) {
			canTrigger = false;
			Behave();
		}
	}

	public void Trigger () {
		if (canTrigger) {
			isTriggered = true;
		}
	}

	void Behave() {
		if (timer == 0) {
			timer = Time.time;
		}

		if (Time.time > timer + triggerDelay) {
			GetComponent<Animator>().SetBool("Trigger", true);
		}
	}

	void Reset() {
		GetComponent<Animator>().SetBool("Trigger", false);
		isTriggered = false;
		timer = 0;

		if (isRepeatable) {
			deadlyPart.targetsHit.Clear();
			canTrigger = true;
		}
	}
}