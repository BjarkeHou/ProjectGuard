using UnityEngine;
using System.Collections;

public class AnimationScripts : MonoBehaviour
{

		private GameObject player;
		public bool isAttacking;

		// Use this for initialization
		void Start ()
		{
				player = GameObject.Find ("Player");
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
				player.GetComponent<AttackController> ().targetsHit.Clear ();
				//don't attack again
				GetComponent<Animator> ().SetBool ("Attack", false);
				//can't attack
				GetComponent<Animator> ().SetBool ("CanAttack", false);
				//can't move or rotate
				player.GetComponent<MovementController> ().SetCanMove (false);
				player.GetComponent<PlayerLook> ().SetPlayerCanRotate (false);
				//no damage
				player.GetComponent<AttackController> ().doesDamage = false;
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
				player.GetComponent<MovementController> ().SetCanMove (false);
				player.GetComponent<PlayerLook> ().SetPlayerCanRotate (false);
				//no damage
				player.GetComponent<AttackController> ().doesDamage = false;
		}
		void CanAttack ()
		{
				GetComponent<Animator> ().SetBool ("CanAttack", true);
				isAttacking = false;
		}
		void CanNotAttack ()
		{
				GetComponent<Animator> ().SetBool ("CanAttack", false);
		}
		void Damage ()
		{
				player.GetComponent<AttackController> ().doesDamage = true;
		}
		void NoDamage ()
		{
				player.GetComponent<AttackController> ().doesDamage = false;
		}
		void CanMove ()
		{
				if (!isAttacking) {
						player.GetComponent<MovementController> ().SetCanMove (true);
				}
		}
		void CanNotMove ()
		{
				player.GetComponent<MovementController> ().SetCanMove (false);
		}
		void CanRotate ()
		{
				player.GetComponent<PlayerLook> ().SetPlayerCanRotate (true);
		}
}
