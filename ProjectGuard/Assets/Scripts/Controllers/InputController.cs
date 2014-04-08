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
			if (gCtrl.isPaused)
			{
				gCtrl.UnPauseGame();
			} else if (!gCtrl.isPaused)
			{
				gCtrl.PauseGame();
			}
		}
	
		// Move Player
		v = Input.GetAxisRaw("Vertical");
		h = Input.GetAxisRaw("Horizontal");
		mCtrl.MoveCharacter(v, h);
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			mCtrl.Dodge(v, h);
		}
				
		// Perform attack
		if (Input.GetMouseButtonDown(0) && eCtrl.hasWeaponEquipped)
		{
			aCtrl.DeclareAttack();
		}

		if (Input.GetMouseButtonDown(1) && eCtrl.hasWeaponEquipped)
		{
			aCtrl.DeclareParry();
		}

		// Drop weapon
		if (Input.GetKey(KeyCode.G))
		{
			eCtrl.Drop(ItemType.Weapon);
		}
		

	}
}
