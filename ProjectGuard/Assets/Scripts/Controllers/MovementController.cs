﻿using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
		private CharacterController charCont;
		private PlayerLook pLook;
		protected Animator anim;
		private PlayerSoundController playerSound;

		public float normalMovementSpeed = 6;
		public float attackingMovementSpeed = 0.5f;
		public float dodgeSpeed;
		public float dodgeDelay;
		public float dodgeDistance;
		public AnimationCurve curve;
	
		private float movementSpeed;

		protected Vector3 moveDirection;
		public bool playerCanMove = true;
		protected bool playerCanDodge = true;
		private float progress;
	
		protected float dodgeDelayTimer = 0;
		private float dodgedDistance = 0;

		// Use this for initialization
		protected void Start ()
		{
				playerSound = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerSoundController> ();
				charCont = GetComponent<CharacterController> ();
				pLook = this.GetComponent<PlayerLook> ();
				anim = transform.Find ("Model").GetComponent<Animator> ();
				movementSpeed = normalMovementSpeed;
		}

		public void MoveCharacter (float v, float h)
		{
				if (playerCanMove) {	
						moveDirection = Vector3.zero;
		
						if (v < 0) 
								moveDirection += Vector3.back;
						else if (v > 0)
								moveDirection += Vector3.forward;
		
						if (h < 0)
								moveDirection += Vector3.left;
						else if (h > 0)
								moveDirection += Vector3.right;

						playerSound.running (v != 0 || h != 0);
						Vector3 localDir = pLook.transform.InverseTransformDirection (moveDirection);
						anim.SetFloat ("RunForward", localDir.z);
						anim.SetFloat ("RunSideways", localDir.x);

						charCont.Move (moveDirection * Time.deltaTime * movementSpeed);
				}
		}

		public virtual void Dodge (float v, float h)
		{

				transform.Find ("Model").GetComponent<Animator> ().SetBool ("Attack", false);

				// Test if player is trying to dodge
				if (playerCanDodge && Time.time > dodgeDelayTimer) {

						if (GetComponent<PlayerWillController> () != null) {
								GetComponent<PlayerWillController> ().Dodge ();
						}

						Vector3 sPoint = this.transform.position;
						Vector3 dodgeDirection = moveDirection.normalized;
						float dodgeDist = dodgeDistance;
						string animState = "Dodge_Forward";

						//if no movement is detected, do a backstep
						if (moveDirection == Vector3.zero) {
								animState = "Dodge_Backwards";
								dodgeDirection = transform.TransformDirection (Vector3.back);
						}
						anim.SetBool (animState, true);
						SetCanMove (false);
						SetCanDodge (false);
						StartCoroutine (PerformDodge (sPoint, dodgeDirection, dodgeDist));
				}
	    
		}

		protected IEnumerator PerformDodge (Vector3 startPoint, Vector3 dodgeDir, float dodgeDist)
		{

				if (moveDirection != Vector3.zero) {
						pLook.LockPlayerOnDirection (dodgeDir);
				}
				dodgeDelayTimer = Time.time + dodgeDelay;
				playerSound.dodge ();

				while (progress < 1.0f) {
						progress = Mathf.InverseLerp (0, dodgeDist, dodgedDistance);
						dodgedDistance += curve.Evaluate (progress) * dodgeSpeed * Time.deltaTime;
						Vector3 desiredPos = startPoint + dodgeDir.normalized * dodgedDistance;
						charCont.Move (desiredPos - transform.position);
						yield return null;
				}

				progress = 0.0f;
				dodgedDistance = 0;
				pLook.playerCanRotate = true;
				SetCanDodge (true);
				SetCanMove (true);
				anim.SetBool ("CanAttack", true);

				Debug.Log ("Dodge performed!!");

		}

		public void AttackStep ()
		{
				StartCoroutine (PerformAttackStep (transform.TransformDirection (Vector3.forward)));
		}

		private IEnumerator PerformAttackStep (Vector3 attackDir)
		{
				float timer = Time.time;
				float aStepSpeed = 0.5f;
				while (Time.time < timer + 0.35f) {
						charCont.Move (attackDir * Time.deltaTime * aStepSpeed);
						yield return null;
				}
		}

		public void SetCanMove (bool value)
		{
				playerCanMove = value;
			
				if (value) {
						movementSpeed = normalMovementSpeed;
				} else if (!value) {
						movementSpeed = attackingMovementSpeed;
				}
		}
	
		public void SetCanDodge (bool value)
		{
				playerCanDodge = value;
		}

		public virtual bool IsLunaBlasted ()
		{
				return false;
		}
}

