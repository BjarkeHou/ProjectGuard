using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
		

		private float v;
		private float h;
		private MovementController mCtrl;
		private AttackController aCtrl;

		// Use this for initialization
		void Start ()
		{
				GameObject p = GameObject.FindGameObjectWithTag ("Player");
				mCtrl = p.GetComponent<MovementController> ();
				aCtrl = p.GetComponent<AttackController> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				// Move Player
				v = Input.GetAxisRaw ("Vertical");
				h = Input.GetAxisRaw ("Horizontal");
				mCtrl.MoveCharacter (v, h, Input.GetKey (KeyCode.LeftShift));
				
				// Perform attack
				if (Input.GetMouseButtonDown (0)) {
						aCtrl.DeclareAttack ();
				}
		}
}
