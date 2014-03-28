﻿using UnityEngine;
using System.Collections.Generic;

public class Weapon : Item
{
	private AttackController playerAtkCont;
	
	private Transform skinPoint;
	private Transform tipPoint;
	public float damage;
	public string name;
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
			GUI.Label (new Rect (Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y, 200, 200), "Name: " + name + "\nAge: " + age + "\nDamage: " + damage);
		}
	}
	
	void TryToEquip (GameObject wielder)
	{
		if (!equipped) {
			equipped = true;
			playerAtkCont = wielder.GetComponent<AttackController> ();
			wielder.GetComponent<EquipmentController> ().Equip (this.gameObject);
			this.transform.parent = wielder.transform;
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

		if (Physics.Raycast (charles, out hit, dist) && playerAtkCont.doesDamage) {
			GameObject obj = hit.collider.gameObject;
			//if an enemy is hit
			if (obj.tag == "Enemy" || obj.tag == "Player") {
				//Instantiate blood
				GameObject blood = (GameObject)Instantiate (Resources.Load ("Prefabs/Blood"));
				blood.transform.parent = transform;
				blood.GetComponent<BloodDestroy> ().offset = (hit.point - transform.position).magnitude;

				print ("HIT on " + obj.name);
				playerAtkCont.Hit (obj, damage);

				//if an object is hit
			} else {
				//Instantiate sparks
				GameObject spark = (GameObject)Instantiate (Resources.Load ("Prefabs/Sparks"), hit.point, Quaternion.LookRotation (hit.normal));

				//If collision is within skin depth
				if ((hit.point - holdPoint.position).magnitude < (skinPoint.position - holdPoint.position).magnitude) {
					print ("REBOUND on " + obj.name);
					if (obj.name == "Floor") {
						UnityEditor.EditorApplication.isPaused = true;
					}
					playerAtkCont.Rebound ();
				}
			}
		}
	}
}
