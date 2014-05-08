using UnityEngine;
using System.Collections;

public class Weapon_Hand : MonoBehaviour {

	private AttackController atkCtrl;
	private GameController game;
	public float damage;

	// Use this for initialization
	void Start() {
		Transform parent = transform;
		while (atkCtrl == null) {
			parent = parent.parent;
			atkCtrl = parent.GetComponent<AttackController>();
		}
		game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		//if an enemy is hit
		if (atkCtrl.doesDamage && !game.isInGhostMode && obj.layer != LayerMask.NameToLayer("DeadEnemies")) {
			if ((obj.tag == "Enemy" || obj.tag == "Player")) {
				int hitType = atkCtrl.Hit(obj, damage);
				if (hitType == 1) {
					//Instantiate blood
					GameObject blood = (GameObject)Instantiate(Resources.Load("Prefabs/Blood"));
					blood.GetComponent<BloodDestroy>().origin = transform.position;
					blood.transform.parent = transform;

					print("HIT on " + obj.name);
				} else if (hitType == 0) {
					print("PARRY by " + obj.name);
					//Instantiate sparks
					GameObject spark = (GameObject)Instantiate(Resources.Load("Prefabs/Sparks"));
					spark.transform.position = transform.position;
				} else if (hitType == 2) {
					//print("Friendly Fire");
				}
				
				//if an object is hit
			} else {
				/*
				//Instantiate sparks
				GameObject spark = (GameObject)Instantiate(Resources.Load("Prefabs/Sparks"));
				spark.transform.position = other.contacts[0].point;
				spark.transform.rotation = Quaternion.LookRotation(other.contacts[0].normal);
				*/
			}
		}
	}
}
