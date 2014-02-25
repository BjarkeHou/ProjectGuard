﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
		public int maxHealth = 100;
		private int curHealth;
		
		private float healthBarLength;

		// Use this for initialization
		void Start ()
		{
				curHealth = maxHealth;
				healthBarLength = Screen.width / 2;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void AddjustCurrentHealth (int adjustment)
		{
				curHealth += adjustment;
				if (curHealth < 0) {
						curHealth = 0;
				}
				if (curHealth > maxHealth) {
						curHealth = maxHealth;
				}
				if (maxHealth < 0) {
						maxHealth = 1;
				}
                
				healthBarLength = (Screen.width / 2) * (curHealth / (float)maxHealth);
		}

		void OnGUI ()
		{
//				GUI.backgroundColor = Color.red;
				GUI.color = Color.red;
				GUI.Box (new Rect (10, 10, healthBarLength, 20), curHealth + " / " + maxHealth);
				
		}
}
