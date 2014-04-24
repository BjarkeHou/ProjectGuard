using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
		
	private float v;
	private float h;
	private MovementController mCtrl;
	private AttackController aCtrl;
	private EquipmentController eCtrl;
	private GameController game;

	// Use this for initialization
	void Start() {
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		mCtrl = p.GetComponent<MovementController>();
		aCtrl = p.GetComponent<AttackController>();
		eCtrl = p.GetComponent<EquipmentController>();

		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		// Pause the game
		if (Input.GetKey(KeyCode.Escape)) {
			if (!game.isPaused) {
				game.PauseGame();
			}
		}
		if (!game.isPaused) {
			// Move Player
			v = Input.GetAxisRaw("Vertical");
			h = Input.GetAxisRaw("Horizontal");
			mCtrl.MoveCharacter(v, h);
			
			if (Input.GetButtonDown("Dodge")) {
				mCtrl.Dodge(v, h);
			}
					
			// Perform attack
			if (Input.GetButtonDown("Attack") && eCtrl.hasWeaponEquipped) {
				aCtrl.DeclareAttack();

			}
	
			if (Input.GetButtonDown("Parry") && eCtrl.hasWeaponEquipped) {
				aCtrl.DeclareParry();

			}
	
			// Drop weapon
			if (Input.GetButtonDown("DropWeapon")) {
				eCtrl.Drop(ItemType.Weapon);
			}

			if (Input.GetKeyDown(KeyCode.R) && !game.isInGhostMode) {
				game.isInGhostMode = true;
				game.LunaChange = true;
			}
		}
	}
}
