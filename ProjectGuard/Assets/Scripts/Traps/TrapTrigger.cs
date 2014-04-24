﻿using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {

	private TrapController trapCtrl;
	private GhostWorldController ghostCtrl;
	private GameController game;

	private Transform disarm;
	private Color disarmColor;
	public float disarmTime;

	// Use this for initialization
	void Start() {
		trapCtrl = transform.parent.GetComponent<TrapController>();
		ghostCtrl = GameObject.Find("GameController").GetComponent<GhostWorldController>();
		game = GameObject.Find("GameController").GetComponent<GameController>();

		disarm = transform.Find("Disarm");
		disarmColor = new Color (0.5f, 0, 0, 1);
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other) {
		if (ghostCtrl.deathTransition <= 0) {
			if (other.tag == "Player") {
				trapCtrl.Trigger();
			}
		} else if (Input.GetKey(KeyCode.E)) {
			Disarm();
		} else {
			transform.Find("Disarm").guiTexture.enabled = false;
		}
	}

	void OnTriggerLeave(Collider other) {
		if (other.tag == "Player") {
			transform.Find("Disarm").guiTexture.enabled = false;
		}
	}

	public void Disarm() {
		disarm.guiTexture.enabled = true;
		if (disarmColor.g < 0.5f) {
			disarmColor.g += Time.deltaTime / (disarmTime / 2);
			disarm.guiTexture.color = disarmColor;
		} else if (disarmColor.r > 0) {
			disarmColor.r -= Time.deltaTime / (disarmTime / 2);
			disarm.guiTexture.color = disarmColor;
		} else {
			collider.enabled = false;
			audio.Play();
		}
	}
}
