using UnityEngine;
using System.Collections;

public class PlayerWillController : MonoBehaviour
{

		private HealthController hCtrl;

		public float handAttackAdj;
		public float dodgeAdj;
		public float parryAdj;
		public float minAtkSp;
		public float maxAtkSp;
		public float regenCD;
		public float regenSpeed;

		private float depleteTimer; //a hack-fix; animation scripts are called twice in a blend tree, which resulted in double will depletion
		private float regenTimer;
		private float maxWill;
		private float curMaxWill;
		private float curWill;

		public float MaxWill { get { return maxWill; } }
		public float CurMaxWill { get { return curMaxWill; } }
		public float CurWill { get { return curWill; } }

		private int prevHealth;

		// Use this for initialization
		void Start ()
		{
				hCtrl = GetComponent<HealthController> ();

				regenTimer = Time.time;
				maxWill = hCtrl.getMaxHealth ();
				curMaxWill = hCtrl.getCurrentHealth ();
				curWill = maxWill;

				prevHealth = hCtrl.getCurrentHealth ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				//if there is a change in health, check if the player has gone below the current will
				if (hCtrl.getCurrentHealth () != prevHealth) {
						if (hCtrl.getCurrentHealth () < prevHealth && curWill + hCtrl.LastDamageTaken < 0) {
								hCtrl.adjustCurrentHealth (-hCtrl.getMaxHealth ());
						} else {
								//curWill += hCtrl.LastDamageTaken;
						}
						prevHealth = hCtrl.getCurrentHealth ();
				}

				if (Time.time > regenTimer + regenCD && curWill < curMaxWill) {
						curWill += regenSpeed * Time.deltaTime;
				}

				//adjuct current max will
				curMaxWill = hCtrl.getCurrentHealth ();

				//regen current will
				if (curWill > curMaxWill) {
						curWill = curMaxWill;
				} else if (curWill < 0) {
						curWill = 0;
				}

				//adjust cd timer
				regenCD = 0.5f * Mathf.InverseLerp (0, maxWill, curWill);

				//anim.SetFloat("curWill", 1 - Mathf.InverseLerp(0, maxWill, curWill));
		}

		public void Attack ()
		{
				if (Time.time > depleteTimer) {
						depleteTimer = Time.time + 0.1f;
						if (GetComponentInChildren<Weapon> ()) {
								curWill += GetComponentInChildren<Weapon> ().attackWillCost; 
						} else {
								curWill += handAttackAdj;
						}
						regenTimer = Time.time;
				}
		}

		public void Parry ()
		{
				if (Time.time > depleteTimer) {
						depleteTimer = Time.time + 0.1f;
						curWill += parryAdj;
						regenTimer = Time.time;
				}
		}

		public void Dodge ()
		{
				if (Time.time > depleteTimer) {
						depleteTimer = Time.time + 0.1f;
						curWill += dodgeAdj;
						regenTimer = Time.time;
				}
		}
}
