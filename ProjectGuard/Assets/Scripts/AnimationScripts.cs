using UnityEngine;
using System.Collections;

public class AnimationScripts : MonoBehaviour {

	protected GameObject animated;
	public bool isAttacking;
	protected PlayerLook pLook;

	// Use this for initialization
	void Start() {
		animated = this.transform.parent.gameObject;
		pLook = animated.GetComponent<PlayerLook>();
	}
	
	// Update is called once per frame
	void Update() {

	}

	//called form animations
	void IniAttack() {
		isAttacking = true;
		//clear hits
		animated.GetComponent<AttackController>().targetsHit.Clear();
		//reset bools
		ResetBools();
		//can't move or rotate
		animated.GetComponent<MovementController>().SetCanMove(false);
		animated.GetComponent<MovementController>().SetCanDodge(false);
		if (pLook != null)
			pLook.playerCanRotate = false;
		//no damage
		animated.GetComponent<AttackController>().doesDamage = false;

		//take a step forward
		animated.GetComponent<MovementController>().AttackStep();

		//deplete will if that script exists
		if (animated.GetComponent<PlayerWillController>() != null) {
			animated.GetComponent<PlayerWillController>().Attack();
		}
	}
	void IniRebound() {
		isAttacking = false;
		ResetBools();
		//can't move or rotate
		animated.GetComponent<MovementController>().SetCanMove(false);
		if (pLook != null)
			pLook.playerCanRotate = false;
		//no damage
		animated.GetComponent<AttackController>().doesDamage = false;
	}
	void IniParry() {
		isAttacking = false;
		//reset bools
		ResetBools();
		//can't move or rotate
		animated.GetComponent<MovementController>().SetCanMove(false);
		animated.GetComponent<MovementController>().SetCanDodge(false);
		if (pLook != null)
			pLook.playerCanRotate = false;
		//no damage
		animated.GetComponent<AttackController>().doesDamage = false;
		//deplete will if that script exists
		if (animated.GetComponent<PlayerWillController>() != null) {
			animated.GetComponent<PlayerWillController>().Parry();
		}
	}
	void IniDodge() {
		isAttacking = false;
		//reset bools
		ResetBools();

		//can't move or rotate
		animated.GetComponent<MovementController>().SetCanMove(false);
		animated.GetComponent<MovementController>().SetCanDodge(false);
		if (pLook != null)
			pLook.playerCanRotate = false;
		//no damage
		animated.GetComponent<AttackController>().doesDamage = false;
	}
	void Damage() {
		animated.GetComponent<AttackController>().doesDamage = true;

		if (transform.GetComponentInChildren<Weapon>() != null) { 
			transform.GetComponentInChildren<Weapon>().gameObject.audio.Play();
		}
	}
	void NoDamage() {
		animated.GetComponent<AttackController>().doesDamage = false;
	}
	void Parry() {
		animated.GetComponent<HealthController>().IsParrying = true;
	}
	void NoParry() {
		animated.GetComponent<HealthController>().IsParrying = false;
	}
	void CanRotate() {
		if (pLook != null)
			pLook.playerCanRotate = true;
	}
	void CanAttack() {
		animated.GetComponent<MovementController>().SetCanDodge(true);
		GetComponent<Animator>().SetBool("CanAttack", true);
		animated.GetComponent<AttackController>().inAnAttack = false;
		isAttacking = false;
	}
	void CanMove() {
		if (!isAttacking) {
			animated.GetComponent<MovementController>().SetCanMove(true);
		}
	}

	void ResetBools() {
		//don't attack again
		GetComponent<Animator>().SetBool("Attack", false);
		//don't stab again
		GetComponent<Animator>().SetBool("Stab", false);
		//don't parry again
		GetComponent<Animator>().SetBool("Parry", false);
		//don't rebound again
		GetComponent<Animator>().SetBool("Rebound", false);
		//don't dodge again
		GetComponent<Animator>().SetBool("Dodge_Forward", false);
		GetComponent<Animator>().SetBool("Dodge_Backwards", false);
		//can't attack
		GetComponent<Animator>().SetBool("CanAttack", false);
	}
}
