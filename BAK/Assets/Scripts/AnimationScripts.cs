using UnityEngine;
using System.Collections;

public class AnimationScripts : MonoBehaviour {

	private Animator anim;
	private PlayerWillController pWillCtrl;
	private GameObject parryEffect;

	protected GameObject animated;
	public bool isAttacking;
	protected PlayerLook pLook;

	// Use this for initialization
	void Start() {
		anim = GetComponent<Animator>();
		pWillCtrl = GameObject.FindWithTag("Player").GetComponent<PlayerWillController>();

		animated = this.transform.parent.gameObject;
		pLook = animated.GetComponent<PlayerLook>();
	}
	
	// Update is called once per frame
	void Update() {

	}

	//called form animations
	void IniAttack() {
		if (animated.tag == "Player") {
			anim.SetFloat("curWill", 1 - Mathf.InverseLerp(0, pWillCtrl.MaxWill, pWillCtrl.CurWill));
			//print("curWill = " + (1 - Mathf.InverseLerp(0, pWillCtrl.MaxWill, pWillCtrl.CurWill)));
		}
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
		if (animated.tag == "Player") {
			anim.SetFloat("curWill", 1);
			//print("curWill = 1");
		}

		animated.GetComponent<AttackController>().doesDamage = true;

		if (transform.GetComponentInChildren<Weapon>() != null) { 
			transform.GetComponentInChildren<Weapon>().gameObject.audio.Play();
		}
	}
	void NoDamage() {
		if (animated.tag == "Player") {
			anim.SetFloat("curWill", 1 - Mathf.InverseLerp(0, pWillCtrl.MaxWill, pWillCtrl.CurWill));
			//print("curWill = " + (1 - Mathf.InverseLerp(0, pWillCtrl.MaxWill, pWillCtrl.CurWill)));
		}
		animated.GetComponent<AttackController>().doesDamage = false;
	}
	void Parry() {
		if (parryEffect == null) {
			parryEffect = (GameObject)Instantiate(Resources.Load("Prefabs/Parry_Effect")) as GameObject;
			parryEffect.transform.parent = animated.transform;
			parryEffect.transform.localPosition = Vector3.zero;
			parryEffect.transform.localRotation = Quaternion.Euler(Vector3.zero);
		}
		animated.GetComponent<HealthController>().IsParrying = true;
	}
	void NoParry() {
		if (parryEffect != null) {
			Destroy(parryEffect);
		}
		animated.GetComponent<HealthController>().IsParrying = false;
	}
	void CanRotate() {
		if (pLook != null)
			pLook.playerCanRotate = true;
	}
	void CanAttack() {
		animated.GetComponent<MovementController>().SetCanDodge(true);
		anim.SetBool("CanAttack", true);
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
		anim.SetBool("Attack", false);
		//don't stab again
		anim.SetBool("Stab", false);
		//don't parry again
		anim.SetBool("Parry", false);
		//don't rebound again
		anim.SetBool("Rebound", false);
		//don't dodge again
		anim.SetBool("Dodge_Forward", false);
		anim.SetBool("Dodge_Backwards", false);
		//can't attack
		anim.SetBool("CanAttack", false);
	}
}
