using UnityEngine;
using System.Collections;

public class BearBehaviour : MonoBehaviour {

	private GameController game;
	private bool isTriggered;
	public float triggerDelay;
	private float triggerTimer;

	public float holdTime;
	private float holdTimer;

	public bool isRepeatable;
	public float repeatDelay;
	private float repeatTimer;
	private bool canTrigger;

	private bool catchPlayer;
	
	void Start() {
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		
		triggerTimer = 0;
		canTrigger = true;
		
		repeatTimer = -repeatDelay;
	}
	
	void Update() {
		if (isTriggered) {
			if (holdTimer <= 0) {
				holdTimer = Time.time;
			}
			canTrigger = false;
			Behave();
		} else {
			catchPlayer = false;
		}

		if (holdTimer >= 0 && Time.time > holdTimer + holdTime) {
			Reset();
		}
	}
	
	public void Trigger() {
		if (canTrigger && Time.time > repeatTimer + repeatDelay) {
			isTriggered = true;
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player" /*&& !game.isInGhostMode*/) {
			if (catchPlayer) {
				other.gameObject.GetComponent<MovementController>().SetCanMove(false);
			} else {
				other.gameObject.GetComponent<MovementController>().SetCanMove(true);
			}
		}
	}
	
	void Behave() {
		if (triggerTimer == 0) {
			triggerTimer = Time.time;
		}
		
		if (Time.time > triggerTimer + triggerDelay) {
			catchPlayer = true;
			GetComponent<Animator>().SetBool("Trigger", true);
		}
	}
	
	void Reset() {
		GetComponent<Animator>().SetBool("Trigger", false);
		isTriggered = false;
		triggerTimer = 0;
		holdTimer = -1;
		repeatTimer = Time.time;
		
		if (isRepeatable) {
			canTrigger = true;
		}
	}

	void Catch() {
		catchPlayer = true;
	}

	void AudioPlay() {
		audio.Play();
	}
}