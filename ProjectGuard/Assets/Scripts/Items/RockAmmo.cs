using UnityEngine;
using System.Collections;

public class RockAmmo : MonoBehaviour {

	public int damage;
	public float limit;
	private ParticleSystem part;
	private GameObject thrower;
	public GameObject Thrower { set { thrower = value; } }

	// Use this for initialization
	void Start() {
		part = GetComponentInChildren<ParticleSystem>();
		part.Stop();
	}
	
	// Update is called once per frame
	void Update() {
		if (rigidbody.velocity.magnitude > limit) {
			part.Play();
		} else {
			part.Stop();
		}
	}

	void OnCollisionEnter(Collision other) {
		GameObject obj = other.collider.gameObject;
		//if an enemy is hit
		if (obj.tag == "Enemy" || obj.tag == "Player") {
			int hitType = thrower.GetComponent<AttackController>().Hit(obj, damage);
			if (hitType == 1) {
				//Instantiate blood
				GameObject blood = (GameObject)Instantiate(Resources.Load("Prefabs/Blood"));
				blood.transform.parent = transform;
				blood.GetComponent<BloodDestroy>().origin = other.contacts[0].point;
				
				print("HIT on " + obj.name);
			} else if (hitType == 0) {
				//print("PARRY by " + obj.name);
			} else if (hitType == 2) {
				//print("Friendly Fire");
			}
			
			//if an object is hit
		} else {
			/*
			//Instantiate sparks
			GameObject spark = (GameObject)Instantiate(Resources.Load("Prefabs/Sparks"));
			spark.transform.position = other.contacts[0].point;
			spark.transform.rotation = Quaternion.LookRotation(other.contacts[0].normal);
			*/
		}
	}
}
