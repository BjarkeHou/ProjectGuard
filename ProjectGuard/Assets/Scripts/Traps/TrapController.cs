using UnityEngine;
using System.Collections.Generic;

public class TrapController : MonoBehaviour {

	private GameController game;
	private bool inGhostWorld;

	private TrapBehaviour trapBehav;
	public bool isRepeatable;

	public List<GameObject> targetsHit;
	public bool isTriggered;

	private Transform disarm;
	public bool isDisarmed;
	private Color disarmColor;
	public float disarmTime;


	// Use this for initialization
	void Start() {
		game = GameObject.Find("GameController").GetComponent<GameController>();
		inGhostWorld = game.isInGhostMode;
		trapBehav = transform.GetComponentInChildren<TrapBehaviour>();
		targetsHit = new List<GameObject> ();
		disarm = transform.Find ("Disarm");
		disarmColor = new Color (0.5f, 0, 0, 1);
	}

	void Update() {
		//if we transition from ghost world to being alive
		if (isTriggered) {
			trapBehav.Behave();
		}
		if (game.isInGhostMode != inGhostWorld) {
			if (isRepeatable) {
				disarmColor = new Color (0.5f, 0, 0, 1);
				isTriggered = false;
				targetsHit.Clear();
				trapBehav.Rewind();
				inGhostWorld = game.isInGhostMode;
			}
		}
	}

	public int Hit(GameObject obj, float damage) {
		if (!targetsHit.Contains(obj)) {
			if (obj.tag != gameObject.tag) { 
				//add enemy to list of hit stuff
				targetsHit.Add(obj);
				print(obj.GetComponent<HealthController>().IsParrying);
				//check if the character is parrying
				if (obj.GetComponent<HealthController>().IsParrying) {
					return 0;
				} else {
					//withdraw health
					obj.GetComponent<HealthController>().adjustCurrentHealth(-(int)damage);
					return 1;
				}
			} else {
				return 2;
			}
		} else {
			return -1;
		}
	}

	public void Disarm() {
		disarm.guiTexture.enabled = true;
		if (disarmColor.g < 0.5f) {
			disarmColor.g += Time.deltaTime / (disarmTime/2);
			disarm.guiTexture.color = disarmColor;
		} else if (disarmColor.r > 0) {
			disarmColor.r -= Time.deltaTime / (disarmTime/2);
			disarm.guiTexture.color = disarmColor;
		} else {
			isDisarmed = true;
			audio.Play();
		}
	}
}
