using UnityEngine;
using System.Collections;

public class AnimationScripts_SEle : AnimationScripts {
	
	public GameObject rock;
	private Vector3 attackSpot;
	private GameObject ammo;
	public GameObject rockSpot;

	//called form animations
    protected override void IniAttack() {
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
	}

	void SpawnRock() {
		if (rockSpot.transform.Find("RockAmme") == null) {
			ammo = (GameObject)Instantiate(rock) as GameObject;
		} else {
			ammo = rockSpot.transform.Find("RockAmmo").gameObject;
		}
		ammo.GetComponent<RockAmmo>().Thrower = gameObject.transform.parent.gameObject;
		ammo.transform.parent = rockSpot.transform;
		ammo.transform.localPosition = Vector3.zero;
		ammo.transform.localRotation = Quaternion.Euler(Vector3.zero);
	}

	void SelectTarget() {
		attackSpot = GameObject.Find("Player").transform.position;
	}

	void ThrowRock() {
		ammo.rigidbody.isKinematic = false;
		ammo.rigidbody.useGravity = true;
		ammo.transform.parent = null;
		ammo.AddComponent<AmmoDestroy>();
		Vector3 dir = attackSpot - rockSpot.transform.position;
		ammo.rigidbody.AddForce(dir.normalized * 10, ForceMode.Impulse);
	}

    protected override void ResetBools() {
		//don't attack again
		GetComponent<Animator>().SetBool("Attack", false);
		//can't attack
		GetComponent<Animator>().SetBool("CanAttack", false);
		//don't throw again
		GetComponent<Animator>().SetBool("Throw", false);
	}
}
