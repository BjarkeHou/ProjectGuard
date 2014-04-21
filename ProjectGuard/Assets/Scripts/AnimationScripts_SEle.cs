using UnityEngine;
using System.Collections;

public class AnimationScripts_SEle : AnimationScripts {

	public GameObject rock;
	private GameObject ammo;
	public GameObject rockSpot;

	//called form animations
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
	}
}
