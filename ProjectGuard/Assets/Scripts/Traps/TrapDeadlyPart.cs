using UnityEngine;
using System.Collections.Generic;

public class TrapDeadlyPart : MonoBehaviour
{

		private TrapController trapCtrl;
		public List<GameObject> targetsHit;
		public int damage;

		// Use this for initialization
		void Start ()
		{
				trapCtrl = GetComponent<TrapController> ();
				targetsHit = new List<GameObject> ();

				Transform parent = transform.parent;
				while (trapCtrl == null) {
						trapCtrl = parent.GetComponent<TrapController> ();
						parent = parent.parent;
				}
		}
	
		// Update is called once per frame
		void OnCollisionEnter (Collision other)
		{
				if (!targetsHit.Contains (other.gameObject) && other.collider.gameObject.layer != LayerMask.NameToLayer ("DeadEnemies")) {
						if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")) {
								targetsHit.Add (other.gameObject);
								int hitType = trapCtrl.Hit (other.collider.gameObject, damage);
								if (hitType == 1) {
										//Instantiate blood
										GameObject blood = (GameObject)Instantiate (Resources.Load ("Prefabs/Blood"));
										blood.transform.parent = transform;
										blood.GetComponent<BloodDestroy> ().origin = other.contacts [0].point;
					
										print ("HIT on " + other.collider.name);
								} else if (hitType == 0) {
										//Instantiate sparks
										GameObject spark = (GameObject)Instantiate (Resources.Load ("Prefabs/Sparks"));
										spark.transform.position = other.contacts [0].point;
										spark.transform.rotation = Quaternion.LookRotation (other.contacts [0].normal);

										print ("PARRY by " + other.collider.name);
								} else if (hitType == 2) {
										//in case of friendly fire
										print ("Friendly Fire");
								}
						}
				}
		}
}
