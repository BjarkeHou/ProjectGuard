using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
		

		private float v;
		private float h;
		private MovementController mCtrl;
		private AttackController aCtrl;
		private EquipmentController eCtrl;

		// Use this for initialization
		void Start ()
		{
				GameObject p = GameObject.FindGameObjectWithTag ("Player");
				mCtrl = p.GetComponent<MovementController> ();
				aCtrl = p.GetComponent<AttackController> ();
				eCtrl = p.GetComponent<EquipmentController> ();
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
				
				// Drop weapon
				if (Input.GetKey (KeyCode.G)) {
						eCtrl.Drop (ItemType.Weapon);
				}
		}
}
