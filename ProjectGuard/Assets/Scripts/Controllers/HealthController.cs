using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	public const int maxHealth = 100;
	public const int minHealth = 0;
	public int curHealth;
	
	// Use this for initialization
	void Start() {
		curHealth = maxHealth;
	}
 
	void Update() {
		if (!stillAlive() && tag != "Player") {
			Destroy(gameObject);
		}
	}

	
	//Damage comes in minus, health comes in plus.
	//If player/creep is still alive, method returns true.
	public bool adjustCurrentHealth(int value) {
		curHealth += value;
		
		if (curHealth < minHealth) 
			return false;
		else 
			return true;
	}
	
	public bool stillAlive() {
		if (curHealth <= minHealth) 
			return false;
		else 
			return true;
	}
		
	public int getCurrentHealth() {
		return curHealth;
	}
		
	public int getMaxHealth() {
		return maxHealth;
	}
}
