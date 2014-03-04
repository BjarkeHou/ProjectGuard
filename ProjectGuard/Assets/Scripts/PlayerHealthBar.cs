using UnityEngine;
using System.Collections;

public class PlayerHealthBar : MonoBehaviour
{
		private float healthBarLength;

		// Use this for initialization
		void Start ()
		{
				healthBarLength = Screen.width / 2;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnGUI ()
		{
//				GUI.backgroundColor = Color.red;
				int curHealth = this.GetComponent<HealthController> ().getCurrentHealth ();
				int maxHealth = this.GetComponent<HealthController> ().getMaxHealth ();
				GUI.color = Color.red;
				GUI.Box (new Rect (10, 10, healthBarLength, 20), curHealth + " / " + maxHealth);
				
		}
}
