using UnityEngine;
using System.Collections.Generic;

public class Weapon : Item
{
	
		public AttackController playerAtkCont;

		private Transform skinPoint;
		private Transform tipPoint;
		public float damage;

//		private GameObject hand;

		// Use this for initialization
		void Start ()
		{
				//transform.parent = GameObject.Find("Player").transform;
				//transform.parent.gameObject.GetComponent<AttackController>();
				
				holdPoint = this.transform.FindChild ("HoldPoint");
				skinPoint = transform.FindChild ("SkinPoint");
				tipPoint = transform.FindChild ("TipPoint");
//				hand = playerAtkCont.gameObject.transform.FindChild ("PlayerModel").FindChild ("Hand").gameObject;
//				if (hand == null) {
//						Debug.LogError ("Sword found no Hand");
//				}
		}
		
		void OnMouseDown ()
		{
				if (!equipped) {
						SetEquipped (true);
						this.tag = "ActiveWeapon";
						Transform hand = GameObject.FindGameObjectWithTag ("Hand").transform;
						this.transform.position = new Vector3 (0, 0, 0);
						this.transform.rotation = new Quaternion (0, 0, 0, 0);
						this.transform.parent = hand;
						this.transform.position = hand.position + (this.transform.position - holdPoint.position);
						this.transform.rotation = hand.rotation;
				}
		}

		void LateUpdate ()
		{
				//Check for collisions
				if (equipped) {
						CheckCollision ();
				}
		}

		void CheckCollision ()
		{
				RaycastHit hit;
				Ray charles = new Ray (holdPoint.position, tipPoint.position - holdPoint.position);
				float dist = (tipPoint.position - holdPoint.position).magnitude;
				if (Physics.Raycast (charles, out hit, dist) && playerAtkCont.doesDamage) {
						GameObject obj = hit.collider.gameObject;
						//if an enemy is hit
//						if (obj.tag != "ActiveWeapon") {
						if (obj.tag == "Enemy") {
								playerAtkCont.Hit (obj, damage);

								//if an object is hit
						} else {
								//Instantiate sparks
								GameObject sparks = (GameObject)Instantiate (Resources.Load ("Prefabs/Sparks"), hit.point, Quaternion.LookRotation (hit.normal));

								//If collision is within skin depth
								if ((hit.point - holdPoint.position).magnitude < (skinPoint.position - holdPoint.position).magnitude) {
										playerAtkCont.Rebound ();
								}
						}
//						}
				}
		}
}
