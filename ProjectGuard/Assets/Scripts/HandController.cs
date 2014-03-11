using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (GameObject.FindGameObjectWithTag ("ActiveWeapon") != null) {
						Transform weapon = GameObject.FindGameObjectWithTag ("ActiveWeapon").transform;	
						weapon.position = this.transform.position + (weapon.position - weapon.GetChild (0).position);
						weapon.rotation = this.transform.rotation;
				}
		}
}
