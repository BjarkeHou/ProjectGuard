using UnityEngine;
using System.Collections;

public class PlayerAspect : MonoBehaviour
{
    //// - moving
    //// - standing still
    //// - attacking
    //// - Player Health
    //private HealthController playerHealth;
    //private MovementController playerMove;
    private AttackController playerAttack;
    private Vector3 playerLastPos;
    private Vector3 playerPreviousPos;
    private bool playerMoved;

    // Use this for initialization
	void Start ()
	{
	    //playerMove = this.GetComponent<MovementController>();
	    playerAttack = this.GetComponent<AttackController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    playerPreviousPos = playerLastPos;
	    playerLastPos = this.transform.position;
	    playerMoved = (playerLastPos != playerPreviousPos);
	}

    public bool IsPlayerMoving()
    {
        return playerMoved;
    }

    public bool IsPlayerAttacking()
    {
        return playerAttack.inAnAttack;
    }
}
