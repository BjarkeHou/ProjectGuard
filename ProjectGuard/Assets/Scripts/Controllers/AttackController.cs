﻿using UnityEngine;
using System.Collections.Generic;

public class AttackController : MonoBehaviour {
	
	private enum HitTypes {
		HIT,
		REBOUND
	}
	private HitTypes hitType;
	public GameObject playerModel;

	public bool doesDamage;
	public List<GameObject> targetsHit;
	private Animator anim;
	private bool m_inAnAttack = false;

	// Use this for initialization
	void Start() {
		doesDamage = false;
		targetsHit = new List<GameObject> ();
		anim = playerModel.GetComponent<Animator>();
	}

	public void DeclareAttack() {
		anim.SetBool("Attack", true);
		inAnAttack = true;
	}

	//called from the equiped weapon
	public void Hit(GameObject obj, float damage) {
		if (!targetsHit.Contains(obj) && obj.tag != gameObject.tag) { 
			//withdraw health
			obj.GetComponent<HealthController>().adjustCurrentHealth(-(int)damage);
			//add enemy to list of hit stuff
			targetsHit.Add(obj);
		}
	}
	//called from the equiped weapon
	public void Rebound() {
		//change animation
		anim.SetBool("Rebound", true);
	}
	
	public bool inAnAttack {
		get { return m_inAnAttack;}
		set { m_inAnAttack = value;}
	}
}
