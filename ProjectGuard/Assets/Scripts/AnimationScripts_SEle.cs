using UnityEngine;
using System.Collections;

public class AnimationScripts_SEle : AnimationScripts {

	public GameObject rock;
	private GameObject ammo;
	public GameObject rockSpot;

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

	void IniThrow() {
		isAttacking = true;
		//clear hits
		animated.GetComponent<AttackController>().targetsHit.Clear();
		//reset bools
		ResetBools();
		//can't move or rotate
		animated.GetComponent<MovementController>().SetCanMove(false);
		//no damage
		animated.GetComponent<AttackController>().doesDamage = false;

		ammo = (GameObject)Instantiate(rock) as GameObject;
		ammo.transform.parent = rockSpot.transform;
		ammo.transform.localPosition = Vector3.zero;
		ammo.transform.localRotation = Quaternion.Euler(Vector3.zero);
	}

	void ThrowRock() {
		ammo.rigidbody.isKinematic = false;
		ammo.rigidbody.useGravity = true;
		ammo.transform.parent = null;
		ammo.AddComponent<AmmoDestroy>();
		ammo.rigidbody.AddForce(transform.forward * 10, ForceMode.Impulse);
	}

	void ResetBools() {
		//don't attack again
		GetComponent<Animator>().SetBool("Attack", false);
		//can't attack
		GetComponent<Animator>().SetBool("CanAttack", false);
		//don't throw again
		GetComponent<Animator>().SetBool("Throw", false);
	}
}
