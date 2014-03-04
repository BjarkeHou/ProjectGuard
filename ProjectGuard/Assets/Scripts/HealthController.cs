using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{

		public int maxHealth = 100;
		public int minHealth = 0;
		private int curHealth;
	
		// Use this for initialization
		void Start ()
		{
				curHealth = maxHealth;
		}
	
		//Damage comes in minus, health comes in plus.
		//If player/creep is still alive, method returns true.
		public bool adjustCurrentHealth (int value)
		{
				curHealth += value;
		
				if (curHealth < minHealth) 
						return false;
				else 
						return true;
		}
	
		public bool stillAlive ()
		{
				if (curHealth < minHealth) 
						return false;
				else 
						return true;
		}
		
		public int getCurrentHealth ()
		{
				return curHealth;
		}
		
		public int getMaxHealth ()
		{
				return maxHealth;
		}
}
