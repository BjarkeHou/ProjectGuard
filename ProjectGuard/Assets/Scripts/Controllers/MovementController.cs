using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
	private CharacterController charCont;
	private PlayerLook pLook;

	public float normalMovementSpeed = 6;
	public float attackingMovementSpeed = 0.5f;
	public float dodgeSpeed;
	public float dodgeDelay;
	public float dodgeDistance;
	public AnimationCurve curve;
	public float lerpSpeed;
	
	private float movementSpeed;

	private Vector3 moveDirection;
	public bool playerCanMove = true;
	private bool playerCanDodge = true;
	private float progress;
	
	private float dodgeDelayTimer = 0;
	private float dodgedDistance = 0;

	// Use this for initialization
	protected void Start() {
		charCont = GetComponent<CharacterController>();
		pLook = this.GetComponent<PlayerLook>();
		movementSpeed = normalMovementSpeed;
	}

	public void MoveCharacter(float v, float h) {
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
		
			charCont.Move(moveDirection * Time.deltaTime * movementSpeed);
		}
	}

	public void Dodge(float v, float h) {
		moveDirection = Vector3.zero;

		if (v < 0) 
			moveDirection += Vector3.back;
		else if (v > 0)
			moveDirection += Vector3.forward;
		
		if (h < 0)
			moveDirection += Vector3.left;
		else if (h > 0)
			moveDirection += Vector3.right;

		transform.Find("Model").GetComponent<Animator>().SetBool("Attack", false);

		// Test if player is trying to dodge
		if (playerCanDodge && Time.time > dodgeDelayTimer) {
			if (GetComponent<PlayerWillController>() != null) {
				GetComponent<PlayerWillController>().Dodge();
			}

			Vector3 sPoint = this.transform.position;
			Vector3 dodgeDirection = moveDirection.normalized;
			float dodgeDist = dodgeDistance;
			
			//if no movement is detected, do a backstep
			if (moveDirection == Vector3.zero) {
				dodgeDirection = transform.TransformDirection(Vector3.back);
				dodgeDist = dodgeDistance * 0.75f;
			}
			StartCoroutine(PerformDodge(sPoint, dodgeDirection, dodgeDist));
		}
	}

	private IEnumerator PerformDodge(Vector3 startPoint, Vector3 dodgeDir, float dodgeDist) {
		if (moveDirection != Vector3.zero) {
			pLook.LockPlayerOnDirection(dodgeDir);
		}
		dodgeDelayTimer = Time.time + dodgeDelay;
		SetCanDodge(false);

		while (progress < 1.0f) {
			progress = Mathf.InverseLerp(0, dodgeDist, dodgedDistance);
			dodgedDistance += curve.Evaluate(progress) * dodgeSpeed * Time.deltaTime;	
			Vector3 desiredPos = startPoint + dodgeDir.normalized * dodgedDistance;
			charCont.Move(desiredPos - transform.position);
			if (!pLook.playerCanRotate && progress > 0.8f)
				pLook.playerCanRotate = true;
			yield return null;
		}
		
		progress = 0.0f;
		dodgedDistance = 0;
		pLook.playerCanRotate = true;
		SetCanDodge(true);
		
		Debug.Log("Dodge performed!!");
	}

	public void SetCanMove(bool value) {
		playerCanMove = value;
			
		if (value) {
			movementSpeed = normalMovementSpeed;
		} else if (!value) {
			movementSpeed = attackingMovementSpeed;
		}
	}
	
	public void SetCanDodge(bool value) {
		playerCanDodge = value;
	}
}
