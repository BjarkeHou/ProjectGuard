using UnityEngine;
using System.Collections.Generic;

public class WeaponStats : MonoBehaviour {

	public bool doesDamage;
	public float damage;
	public List<GameObject> targetsHit;
	private Animator anim;

	// Use this for initialization
	void Start () {
		doesDamage = false;
		targetsHit = new List<GameObject>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			anim.SetBool("Attack", true);
		}
		if (anim.GetNextAnimatorStateInfo(0).IsName("Idle")) {
			anim.SetBool("Attack", false);
			anim.SetBool("Rebound", false);
		}
	}

	void OnCollisionEnter (Collision col) {
		GameObject hit = col.collider.gameObject;
		if (hit.GetComponent<MonsterHealth>() != null && !targetsHit.Contains(hit) && doesDamage) {
			GameObject blood = (GameObject)Instantiate(Resources.Load("Prefabs/Blood"), hit.transform.position, Quaternion.LookRotation(transform.parent.position -hit.transform.position));
			print ("HIT!: " +hit.name);
			hit.GetComponent<MonsterHealth>().health -= damage;
			targetsHit.Add(hit);
		} else if (hit.GetComponent<MonsterHealth>() == null && hit.tag != "Player" && anim.GetBool("Attack")) {
			GameObject sparks = (GameObject)Instantiate(Resources.Load("Prefabs/Sparks"), col.contacts[0].point, Quaternion.LookRotation(col.contacts[0].normal));
			print ("REBOUND!");
			anim.SetBool("Attack", false);
			anim.SetBool("Rebound", true);
			NoDamage();
		}
	}

	//called form animation
	void Damage () {
		doesDamage = true;
	}
	void NoDamage () {
		doesDamage = false;
	}

	//called from animation
	void ClearHits () {
		targetsHit.Clear();
	}
}
