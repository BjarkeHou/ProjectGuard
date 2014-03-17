using UnityEngine;
using System.Collections;

public class AnimationScripts : MonoBehaviour
{

	private GameObject animated;
	public bool isAttacking;
    private PlayerLook pLook;

	// Use this for initialization
	void Start ()
	{
		animated = this.transform.parent.gameObject;
	    pLook = animated.GetComponent<PlayerLook>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	//called form animations
	void IniAttack ()
	{
		isAttacking = true;
		//clear hits
		animated.GetComponent<AttackController> ().targetsHit.Clear ();
		//don't attack again
		GetComponent<Animator> ().SetBool ("Attack", false);
		//can't attack
		GetComponent<Animator> ().SetBool ("CanAttack", false);
		//can't move or rotate
		animated.GetComponent<MovementController> ().SetCanMove (false);
	    if (pLook != null) pLook.playerCanRotate = false;
		//no damage
		animated.GetComponent<AttackController> ().doesDamage = false;
	}
	void IniRebound ()
	{
		isAttacking = false;
		//don't rebound again
		GetComponent<Animator> ().SetBool ("Rebound", false);
		//don't attack again
		GetComponent<Animator> ().SetBool ("Attack", false);
		//can't attack
		GetComponent<Animator> ().SetBool ("CanAttack", false);
		//can't move or rotate
		animated.GetComponent<MovementController> ().SetCanMove (false);
        if (pLook != null) pLook.playerCanRotate = false;
		//no damage
		animated.GetComponent<AttackController> ().doesDamage = false;
	}
	void CanAttack ()
	{
		GetComponent<Animator> ().SetBool ("CanAttack", true);
		isAttacking = false;
		animated.GetComponent<AttackController> ().inAnAttack = false;
	}
	void CanNotAttack ()
	{
		GetComponent<Animator> ().SetBool ("CanAttack", false);
	}
	void Damage ()
	{
		animated.GetComponent<AttackController> ().doesDamage = true;
	}
	void NoDamage ()
	{
		animated.GetComponent<AttackController> ().doesDamage = false;
	}
	void CanMove ()
	{
		if (!isAttacking) {
			animated.GetComponent<MovementController> ().SetCanMove (true);
		}
	}
	void CanNotMove ()
	{
		animated.GetComponent<MovementController> ().SetCanMove (false);
	}
	void CanRotate ()
	{
        if (pLook != null) pLook.playerCanRotate = true;
	}
}
