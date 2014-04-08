using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
		
	private float v;
	private float h;
	private MovementController mCtrl;
	private AttackController aCtrl;
	private EquipmentController eCtrl;
	private GameController gCtrl;

	// Use this for initialization
	void Start()
	{
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		mCtrl = p.GetComponent<MovementController>();
		aCtrl = p.GetComponent<AttackController>();
		eCtrl = p.GetComponent<EquipmentController>();
		gCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		// Pause the game
		if (Input.GetKey(KeyCode.Escape))
		{
			if (!gCtrl.isPaused)
			{
				gCtrl.PauseGame();
			}
		}
	
		if (!gCtrl.isPaused)
		{
			// Move Player
			v = Input.GetAxisRaw("Vertical");
			h = Input.GetAxisRaw("Horizontal");
			mCtrl.MoveCharacter(v, h);
			if (Input.GetButtonDown("Dodge"))
			{
				mCtrl.Dodge(v, h);
			}
					
			// Perform attack
			if (Input.GetButtonDown("Attack") && eCtrl.hasWeaponEquipped)
			{
				aCtrl.DeclareAttack();
			}
	
			if (Input.GetButtonDown("Parry") && eCtrl.hasWeaponEquipped)
			{
				aCtrl.DeclareParry();
			}
	
			// Drop weapon
			if (Input.GetButtonDown("DropWeapon"))
			{
				eCtrl.Drop(ItemType.Weapon);
			}
		}
	}
}
