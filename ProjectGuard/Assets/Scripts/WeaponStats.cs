using UnityEngine;
using System.Collections.Generic;

public class WeaponStats : MonoBehaviour {
	
	private AttackController playerAtkCont;

	private Transform holdPoint;
	private Transform skinPoint;
	private Transform tipPoint;
	public float damage;



	// Use this for initialization
	void Start () {
		transform.parent = GameObject.Find("Player").transform;
		playerAtkCont = transform.parent.gameObject.GetComponent<AttackController>();

		holdPoint = transform.FindChild("HoldPoint");
		skinPoint = transform.FindChild("SkinPoint");
		tipPoint = transform.FindChild("TipPoint");
	}

	void LateUpdate () {
		//Check for collisions
		CheckCollision();

		//Update position in hand
		transform.position = transform.parent.Find ("PlayerModel/Hand").position +(transform.position -holdPoint.position);
		transform.rotation = transform.parent.Find ("PlayerModel/Hand").rotation;
	}

	void CheckCollision() {
		RaycastHit hit;
		Ray charles = new Ray(holdPoint.position, tipPoint.position -holdPoint.position);
		float dist = (tipPoint.position -holdPoint.position).magnitude;
		if (Physics.Raycast(charles, out hit, dist) && playerAtkCont.doesDamage) {
			GameObject obj = hit.collider.gameObject;
			//if an enemy is hit
			if (obj.tag == "Enemy") {
				playerAtkCont.Hit(obj, damage);

			//if an object is hit
			} else {
				//Instantiate sparks
				GameObject sparks = (GameObject)Instantiate(Resources.Load("Prefabs/Sparks"), hit.point, Quaternion.LookRotation(hit.normal));

				//If collision is within skin depth
				if ((hit.point -holdPoint.position).magnitude < (skinPoint.position -holdPoint.position).magnitude) {
					playerAtkCont.Rebound();
				}
			}
		}
	}
}
