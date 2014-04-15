using UnityEngine;
using System.Collections;

public class TrapDeadlyPart : MonoBehaviour {

	private TrapController trapCtrl;
	public int damage;

	// Use this for initialization
	void Start() {
		trapCtrl = GetComponent<TrapController>();
		Transform parent = transform.parent;
		while (trapCtrl == null) {
			if (parent.GetComponent<TrapController>() == null) {
				parent = parent.parent;
			} else {
				trapCtrl = parent.GetComponent<TrapController>();
			}
		}
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision other) {
		if (other.collider.tag == "Enemy" || other.collider.tag == "Player") {
			int hitType = trapCtrl.Hit(other.collider.gameObject, damage);
			if (hitType == 1) {
				//Instantiate blood
				GameObject blood = (GameObject)Instantiate(Resources.Load("Prefabs/Blood"));
				blood.transform.parent = transform;
				blood.GetComponent<BloodDestroy>().offset = (other.contacts [0].point - transform.position).magnitude;
				
				print("HIT on " + other.collider.name);
			} else if (hitType == 0) {
				//Instantiate sparks
				GameObject spark = (GameObject)Instantiate(Resources.Load("Prefabs/Sparks"));
				spark.transform.position = other.contacts [0].point;
				spark.transform.rotation = Quaternion.LookRotation(other.contacts [0].normal);

				print("PARRY by " + other.collider.name);
			} else if (hitType == 2) {
				//in case of friendly fire
				print("Friendly Fire");
			}
		}
	}
}
