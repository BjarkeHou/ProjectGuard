using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
		
	private float v;
	private float h;
	private MovementController mCtrl;
	private AttackController aCtrl;
	private EquipmentController eCtrl;
	private PlayerSoundController playerSound;
	private GameController game;

	// Use this for initialization
	void Start()
	{
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		mCtrl = p.GetComponent<MovementController>();
		aCtrl = p.GetComponent<AttackController>();
		eCtrl = p.GetComponent<EquipmentController>();
		playerSound = p.GetComponent<PlayerSoundController>();
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		// Pause the game
		if (Input.GetKey(KeyCode.Escape))
		{
			if (!game.isPaused)
			{
				game.PauseGame();
			}
		}
		if (!game.isPaused)
		{
			// Move Player
			v = Input.GetAxisRaw("Vertical");
			h = Input.GetAxisRaw("Horizontal");
			mCtrl.MoveCharacter(v, h);
			playerSound.running(v != 0 || h != 0);
			
			if (Input.GetButtonDown("Dodge"))
			{
				mCtrl.Dodge(v, h);
				playerSound.dodge();
			}
					
			// Perform attack
			if (Input.GetButtonDown("Attack") && eCtrl.hasWeaponEquipped)
			{
				aCtrl.DeclareAttack();
				playerSound.attack();
			}
	
			if (Input.GetButtonDown("Parry") && eCtrl.hasWeaponEquipped)
			{
				aCtrl.DeclareParry();
				playerSound.parry();
			}
	
			// Drop weapon
			if (Input.GetButtonDown("DropWeapon"))
			{
				eCtrl.Drop(ItemType.Weapon);
			}
		}
	}
}
