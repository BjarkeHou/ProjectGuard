using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
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
		
	private float dodgeDelayCounter = 0;
	private bool inDodgeMove = false;

	// Use this for initialization
    protected void Start ()
	{
			movementSpeed = normalMovementSpeed;
	}

	public void MoveCharacter (float v, float h, bool shift)
	{
			if (inDodgeMove)
					return;
				
			moveDirection = Vector3.zero;
			;
		
			if (v < 0) 
					moveDirection += Vector3.back;
			else if (v > 0)
					moveDirection += Vector3.forward;
		
			if (h < 0)
					moveDirection += Vector3.left;
			else if (h > 0)
					moveDirection += Vector3.right;
		
			// Update dodge delay timer
			if (dodgeDelayCounter > 0) {
					dodgeDelayCounter -= Time.deltaTime * 1000;
			}
			if (dodgeDelayCounter <= 0) {
					dodgeDelayCounter = -1;
			}
				
			// Test if player is trying to dodge
			if (playerCanDodge && shift && playerCanMove && !inDodgeMove && dodgeDelayCounter == -1 && moveDirection != Vector3.zero) {
					Vector3 sPoint = this.transform.position;
					Vector3 ePoint = moveDirection.normalized * dodgeDistance;
					StartCoroutine (PerformDodge (sPoint, ePoint));
			} else if (!inDodgeMove) {
					this.transform.position += (moveDirection * Time.deltaTime * movementSpeed);
			}
	}
		
	private IEnumerator PerformDodge (Vector3 startPoint, Vector3 endPoint)
	{
			inDodgeMove = true;

			dodgeDelayCounter = dodgeDelay;
		
			while (progress < 1.0f) {
					progress = Mathf.InverseLerp (0, dodgeDistance, (startPoint - this.transform.position).magnitude);
					Vector3 desiredPos = this.transform.position + moveDirection.normalized * curve.Evaluate (progress) * Time.deltaTime * dodgeSpeed;
					this.transform.position = Vector3.Lerp (this.transform.position, desiredPos, Time.deltaTime * lerpSpeed); 
					yield return null;
			}
				
			progress = 0.0f;
			inDodgeMove = false;
				
			Debug.Log ("Dodge performed!!");
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
}
