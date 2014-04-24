using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{

	public const int maxHealth = 100;
	public const int minHealth = 0;
	public int curHealth;

	private int lastDamageTaken;
	public int LastDamageTaken { get { return lastDamageTaken; } }
	
	private GameController game;
	
	private bool isParrying;
	public bool IsParrying
	{ 
		get { return isParrying; }
		set { isParrying = value; } 
	}
	
	// Use this for initialization
	void Start()
	{
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		//curHealth = maxHealth;
	}
 
	void Update()
	{
		if (!stillAlive() && tag != "Player")
		{
			Destroy(gameObject);
		}
	}

	//Damage comes in minus, health comes in plus.
	//If player/creep is still alive, method returns true.
	public bool adjustCurrentHealth(int value)
	{
		if (value < 0) {
			lastDamageTaken = value;
		}

		curHealth += value;
		
		if (curHealth <= minHealth)
		{	
			if (tag == "Player")
				game.isInGhostMode = true;
			return false;
		} else
		{
			if (tag == "Player")
				game.isInGhostMode = false;
			return true;
		}
	}
	
	public bool stillAlive()
	{
		if (curHealth <= minHealth) 
			return false;
		else 
			return true;
	}
		
	public int getCurrentHealth()
	{
		return curHealth;
	}
		
	public int getMaxHealth()
	{
		return maxHealth;
	}
}
