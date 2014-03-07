using UnityEngine;
using System.Collections.Generic;

public class AttackController : MonoBehaviour {
	
	private enum HitTypes {HIT, REBOUND}
	private HitTypes hitType;
	public GameObject playerModel;

	public bool doesDamage;
	public List<GameObject> targetsHit;
	private Animator anim;

	// Use this for initialization
	void Start () {
		doesDamage = false;
		targetsHit = new List<GameObject>();
		anim = playerModel.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			anim.SetBool("Attack", true);

		}
	}

	//called from the equiped weapon
	public void Hit(GameObject obj, float damage) {
		if (!targetsHit.Contains(obj)) {
			print ("Hit on " +obj.name);
			//Instantiate blood
			GameObject blood = (GameObject)Instantiate(Resources.Load("Prefabs/Blood"), obj.transform.position, Quaternion.LookRotation(transform.position -obj.transform.position));

			//withdraw health
			obj.GetComponent<HealthController>().adjustCurrentHealth(-(int)damage);
			//add enemy to list of hit stuff
			targetsHit.Add(obj);
		}
	}
	//called from the equiped weapon
	public void Rebound() {
		print ("Rebound!");
		//change animation
		anim.SetBool("Rebound", true);
	}
}
