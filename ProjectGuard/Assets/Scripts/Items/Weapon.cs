using UnityEngine;
using System.Collections.Generic;

public class Weapon : Item
{
		private AttackController atkCtrl;
		private GameController game;
	
		private Transform skinPoint;
		private Transform tipPoint;
		public float damage;
		public string weaponName;
		public string age;
		public string quality;
	
		public float attackWillCost;
	
		public GUISkin gui_Skin;
		private bool showStatsLabel = false;
	
		private Shader diffuse;
		private Shader outlined;

		// Use this for initialization
		void Start ()
		{
				game = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();

				ResizeBoxCollider ();
				if (transform.parent != null) {
						if (transform.parent.tag == "Player" || transform.parent.tag == "Enemy") {
								TryToEquip (transform.parent.gameObject);
						}
				}
				holdPoint = transform.FindChild ("HoldPoint");
				skinPoint = transform.FindChild ("SkinPoint");
				tipPoint = transform.FindChild ("TipPoint");
		
				diffuse = Shader.Find ("Diffuse");
				outlined = Shader.Find ("Toon/Basic Outline");
		}
	
		void ResizeBoxCollider ()
		{
				BoxCollider b = collider as BoxCollider;
				Vector3 pre = b.size;
				b.size = new Vector3 (pre.x, pre.y * 1.5f, pre.z * 3.5f);
		}
	
		void OnMouseDown ()
		{
				if (Vector3.Distance (this.transform.position, GameObject.FindGameObjectWithTag ("Player").transform.position) <= maxPickupRange) {
						TryToEquip (GameObject.FindGameObjectWithTag ("Player"));
				}
		}
	
		void OnMouseEnter ()
		{
				showStatsLabel = true;
				this.renderer.material.shader = outlined;
		}
	
		void OnMouseExit ()
		{
				showStatsLabel = false;
				this.renderer.material.shader = diffuse;
		}
	
		void OnGUI ()
		{
				if (showStatsLabel && !equipped) {
						GUI.skin = gui_Skin;
						GUI.Label (new Rect (Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y, 200, 200), "Name: " + weaponName + "\nAge: " + age + "\nDamage: " + damage);
				}
		}
	
		void TryToEquip (GameObject wielder)
		{
				if (!equipped) {
						equipped = true;
						atkCtrl = wielder.GetComponent<AttackController> ();
						wielder.GetComponent<EquipmentController> ().Equip (this.gameObject);
				}
		}
	
		public void Dropped ()
		{
				if (equipped) {
						equipped = false;
						this.transform.parent = null;
				}
		}
	
		void LateUpdate ()
		{
				//Update position and check for collisions
				if (equipped) {
						CheckHit ();
				}
		}

		void CheckHit ()
		{
				RaycastHit hit;
				Ray charles = new Ray (holdPoint.position, tipPoint.position - holdPoint.position);
				float dist = (tipPoint.position - holdPoint.position).magnitude;
		
				if (Physics.Raycast (charles, out hit, dist) &&
						atkCtrl.doesDamage &&
						game.GetComponent<GhostWorldController> ().deathTransition <= 0 &&
						hit.collider.gameObject.layer != LayerMask.NameToLayer ("DeadEnemies")) {

						GameObject obj = hit.collider.gameObject;
						//if an enemy is hit
						if ((obj.tag == "Enemy" || obj.tag == "Player")) {
								int hitType = atkCtrl.Hit (obj, damage);
								if (hitType == 1) {
										//Instantiate blood
										GameObject blood = (GameObject)Instantiate (Resources.Load ("Prefabs/Blood"));
										blood.transform.parent = transform;
										blood.GetComponent<BloodDestroy> ().origin = hit.point;
					
										print ("HIT on " + obj.name);
								} else if (hitType == 0) {
										//Instantiate sparks
										GameObject spark = (GameObject)Instantiate (Resources.Load ("Prefabs/Sparks"));
										spark.transform.position = hit.point;
										spark.transform.rotation = Quaternion.LookRotation (hit.normal);
					
										print ("PARRY by " + obj.name);
								} else if (hitType == 2) {
										//in case of friendly fire
										print ("Friendly Fire");
								}
				
								//if an object is hit
						} else {
								//Instantiate sparks
								GameObject spark = (GameObject)Instantiate (Resources.Load ("Prefabs/Sparks"));
								spark.transform.position = hit.point;
								spark.transform.rotation = Quaternion.LookRotation (hit.normal);
				
								//If collision is within skin depth
								if ((hit.point - holdPoint.position).magnitude < (skinPoint.position - holdPoint.position).magnitude) {
										print ("REBOUND on " + obj.name);
										atkCtrl.Rebound ();
								}
						}
				}
		}

		//HACK
		//in case hand hits before blade
		void OnTriggerEnter (Collider other)
		{
				GameObject obj = other.gameObject;
				//if an enemy is hit
				if (atkCtrl.doesDamage && !game.isInGhostMode && obj.layer != LayerMask.NameToLayer ("DeadEnemies")) {
						if ((obj.tag == "Enemy" || obj.tag == "Player")) {
								int hitType = atkCtrl.Hit (obj, damage);
								if (hitType == 1) {
										//Instantiate blood
										GameObject blood = (GameObject)Instantiate (Resources.Load ("Prefabs/Blood"));
										blood.GetComponent<BloodDestroy> ().origin = transform.position;
										blood.transform.parent = transform;
					
										print ("HIT on " + obj.name);
								} else if (hitType == 0) {
										print ("PARRY by " + obj.name);
										//Instantiate sparks
										GameObject spark = (GameObject)Instantiate (Resources.Load ("Prefabs/Sparks"));
										spark.transform.position = transform.position;
								} else if (hitType == 2) {
										//print("Friendly Fire");
								}
				
								//if an object is hit

						} else {
								//Instantiate sparks
								GameObject spark = (GameObject)Instantiate (Resources.Load ("Prefabs/Sparks"));
								spark.transform.position = transform.position;
						}
				}
		}
}
