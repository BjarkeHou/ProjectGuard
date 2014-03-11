using UnityEngine;
using System.Collections;

public class EquipmentController : MonoBehaviour
{
		public GameObject currentWeapon;
		private Transform hand;

		// Use this for initialization
		void Start ()
		{
				hand = gameObject.transform.Find("Model").Find("Hand");
		}
	
		// Update is called once per frame
		void LateUpdate ()
		{
				if (currentWeapon != null) {
						Transform weapon = currentWeapon.transform;
						weapon.position = hand.position + (weapon.position - weapon.transform.FindChild("HoldPoint").position);
						weapon.rotation = hand.rotation;
				}
		}
}
