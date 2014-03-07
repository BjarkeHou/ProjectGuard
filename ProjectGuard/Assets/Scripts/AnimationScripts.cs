using UnityEngine;
using System.Collections;

public class AnimationScripts : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//called form animations
	void IniAttack () {
		//clear hits
		player.GetComponent<AttackController>().targetsHit.Clear();
		//don't attack again
		GetComponent<Animator>().SetBool("Attack", false);
		//can't attack
		GetComponent<Animator>().SetBool("CanAttack", false);
		//can't move or rotate
		player.GetComponent<PlayerController>().SetCanMove(false);
		GameObject.Find("Main Camera").GetComponent<PlayerLook>().SetPlayerCanRotate(false);
		//no damage
		player.GetComponent<AttackController>().doesDamage = false;
	}
	void IniRebound () {
		//don't rebound again
		GetComponent<Animator>().SetBool("Rebound", false);
		//don't attack again
		GetComponent<Animator>().SetBool("Attack", false);
		//can't attack
		GetComponent<Animator>().SetBool("CanAttack", false);
		//can't move or rotate
		player.GetComponent<PlayerController>().SetCanMove(false);
		GameObject.Find("Main Camera").GetComponent<PlayerLook>().SetPlayerCanRotate(false);
		//no damage
		player.GetComponent<AttackController>().doesDamage = false;
	}
	void CanAttack () {
		GetComponent<Animator>().SetBool("CanAttack", true);
	}
	void CanNotAttack () {
		GetComponent<Animator>().SetBool("CanAttack", false);
	}
	void Damage () {
		player.GetComponent<AttackController>().doesDamage = true;
	}
	void NoDamage () {
		player.GetComponent<AttackController>().doesDamage = false;
	}
	void CanMove () {
		player.GetComponent<PlayerController>().SetCanMove(true);
		GameObject.Find("Main Camera").GetComponent<PlayerLook>().SetPlayerCanRotate(true);
	}
	void CanNotMove () {
		player.GetComponent<PlayerController>().SetCanMove(false);
		GameObject.Find("Main Camera").GetComponent<PlayerLook>().SetPlayerCanRotate(false);
	}
}
