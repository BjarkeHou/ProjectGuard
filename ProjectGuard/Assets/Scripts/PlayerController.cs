using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

		public float normalMovementSpeed = 6;
		public float attackingMovementSpeed = 0.5f;
		public float dodgeTime;
		public float dodgeSpeed;
		public float dodgeDelay;
		public float dodgeDistance;
		public AnimationCurve curve;
		
		private float movementSpeed;
		private float v;
		private float h;
		private Vector3 moveDirection;
		private bool playerCanMove = true;
		
		private float dodgeDelayCounter = 0;
		private bool inDodgeMove = false;
		
		public AttackController atkCtrl;


		// Use this for initialization
		void Start ()
		{
				movementSpeed = normalMovementSpeed;
		}
	
		// Update is called once per frame
		void Update ()
		{

				if (Input.GetMouseButtonDown (0)) {
						atkCtrl.DeclareAttack ();
				}
				
				MoveCharacter ();
		}
		
		void MoveCharacter ()
		{
				moveDirection = Vector3.zero;
				v = Input.GetAxisRaw ("Vertical");
				h = Input.GetAxisRaw ("Horizontal");
		
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
				if (dodgeDelayCounter == -1 && Input.GetKey (KeyCode.LeftShift) && playerCanMove) {
						StartCoroutine (PerformDodge ());
//						StartCoroutine (MoveFromTo (this.transform.position, moveDirection * dodgeSpeed, dodgeTime));
				} else if (!inDodgeMove) {
						this.transform.position += (moveDirection * Time.deltaTime * movementSpeed);
				}
		}
		
		private IEnumerator PerformDodge ()
		{
				inDodgeMove = true;
				Vector3 startPoint = this.transform.position;
				Vector3 endPoint = moveDirection.normalized * dodgeDistance;
				float progress = Mathf.InverseLerp (0, dodgeDistance, (startPoint - this.transform.position).magnitude);
				dodgeDelayCounter = dodgeDelay;
		
				while (progress < 1.0f) {
						this.transform.position += moveDirection.normalized * curve.Evaluate (progress) * Time.deltaTime;
			
						yield return null;
				}
		
		
		
				Debug.Log ("Dodge performed!!");
				
		}
		
		
		private IEnumerator MoveFromTo (Vector3 from, Vector3 to, float time)
		{
				if (!inDodgeMove) {
						inDodgeMove = true;
						float t = 0.0f;
						while (t < 1.0f) {
								t += Time.smoothDeltaTime / time;
								this.transform.position = Vector3.Lerp (from, from + to, t);
								yield return null;
						}
				
						inDodgeMove = false;
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
}
