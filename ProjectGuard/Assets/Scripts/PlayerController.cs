using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float movementSpeed = 2;
	private float v;
	private float h;
	private Vector3 moveDirection;
	private bool playerCanMove;
    public AttackController atkCtrl;


	// Use this for initialization
	void Start ()
	{
		playerCanMove = true;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetMouseButton(0))
		{
			atkCtrl.DeclareAttack();
		}
		if (playerCanMove) {
					MoveCharacter ();
			}
				
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
		
		
			this.transform.position += (moveDirection * Time.deltaTime * movementSpeed);
	}
		
	public void SetCanMove (bool value)
	{
			playerCanMove = value;
	}
}
