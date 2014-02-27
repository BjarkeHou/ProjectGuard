using UnityEngine;
using System.Collections.Generic;

public class WeaponStats : MonoBehaviour {

	private enum HitTypes {HIT, REBOUND}
	private HitTypes hitType;
	public bool doesDamage;
	public float damage;
	public List<GameObject> targetsHit;
	private Animator anim;

	private Transform holdPoint;
	private Transform skinPoint;
	private Transform tipPoint;

	// Use this for initialization
	void Start () {
		doesDamage = false;
		targetsHit = new List<GameObject>();
		anim = GetComponent<Animator>();

		holdPoint = transform.FindChild("HoldPoint");
		skinPoint = transform.FindChild("SkinPoint");
		tipPoint = transform.FindChild("TipPoint");
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

		CheckCollision();
	}

	void CheckCollision() {
		RaycastHit hit;
		Ray charles = new Ray(holdPoint.position, tipPoint.position -holdPoint.position);
		float dist = (tipPoint.position -holdPoint.position).magnitude;
		if (Physics.Raycast(charles, out hit, dist)) {
			GameObject obj = hit.collider.gameObject;

			//if a target with health is hit
			if (obj.GetComponent<MonsterHealth>() != null && !targetsHit.Contains(obj) && doesDamage) {
				//Instantiate blood
				GameObject blood = (GameObject)Instantiate(Resources.Load("Prefabs/Blood"), obj.transform.position, Quaternion.LookRotation(transform.parent.position -obj.transform.position));
				Hit (obj, HitTypes.HIT);

			//if a target without health is hit
			} else if (obj.GetComponent<MonsterHealth>() == null && obj.tag != "Player" && anim.GetBool("Attack")) {
				//Instantiate sparks
				GameObject sparks = (GameObject)Instantiate(Resources.Load("Prefabs/Sparks"), hit.point, Quaternion.LookRotation(hit.normal));

				if ((hit.point -holdPoint.position).magnitude < (skinPoint.position -holdPoint.position).magnitude) {
					Hit (obj, HitTypes.REBOUND);
				}
			}
		}
	}

	/*
	void OnCollisionStay (Collision col) {
		GameObject hit = col.gameObject;
		if (hit.GetComponent<MonsterHealth>() != null && !targetsHit.Contains(hit) && doesDamage) {
			//Instantiate blood
			GameObject blood = (GameObject)Instantiate(Resources.Load("Prefabs/Blood"), hit.transform.position, Quaternion.LookRotation(transform.parent.position -hit.transform.position));
			Hit (hit, HitTypes.HIT);
		} else if (hit.GetComponent<MonsterHealth>() == null && hit.tag != "Player" && anim.GetBool("Attack")) {
			//Instantiate sparks
			GameObject sparks = (GameObject)Instantiate(Resources.Load("Prefabs/Sparks"), col.contacts[0].point, Quaternion.LookRotation(col.contacts[0].normal));
			Hit (hit, HitTypes.REBOUND);
		}
	}
	*/

	void Hit(GameObject obj, HitTypes type) {
		print (type +" on "+obj.name);

		if (type == HitTypes.HIT) {
			//withdraw health
			obj.GetComponent<MonsterHealth>().health -= damage;
			//add enemy to list of hit stuff
			targetsHit.Add(obj);

		} else if (type == HitTypes.REBOUND) {
			//change animation
			anim.SetBool("Attack", false);
			anim.SetBool("Rebound", true);
		}
	}

	//called form animations
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
