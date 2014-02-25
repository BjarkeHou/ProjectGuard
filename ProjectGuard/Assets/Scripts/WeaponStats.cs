using UnityEngine;
using System.Collections.Generic;

public class WeaponStats : MonoBehaviour {

	public bool doesDamage;
	public float damage;
	public List<GameObject> targetsHit;

	// Use this for initialization
	void Start () {
		doesDamage = false;
		targetsHit = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.GetComponent<MonsterHealth>() != null && !targetsHit.Contains(other.gameObject) && doesDamage) {
			print ("HIT!: " +other.name);
			other.GetComponent<MonsterHealth>().health -= damage;
			targetsHit.Add(other.gameObject);
		}
	}

	//called form animation
	void Attacking () {
		doesDamage = true;
	}

	void StopAttacking () {
		doesDamage = false;
	}

	//called from animation
	void ClearHits () {
		targetsHit.Clear();
	}
}
