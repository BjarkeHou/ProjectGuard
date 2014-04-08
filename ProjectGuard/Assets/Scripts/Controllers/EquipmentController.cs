using UnityEngine;
using System.Collections;

public enum ItemType
{
	Weapon,
	Helm
}
;

public class EquipmentController : MonoBehaviour
{
	public GameObject currentWeapon;
	private GameObject currentHelm;
	public Transform hand;
	private Transform holdPoint;
	public bool m_hasWeaponEquipped = false;
	private bool m_hasHelmEquipped = false;

	void Start() {
		if (hand == null) {
			hand = transform.Find ("Model").Find("Hand");
		}
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		/*if (hasWeaponEquipped) {
			currentWeapon.transform.position = hand.position + (currentWeapon.transform.position - holdPoint.position);
			currentWeapon.transform.rotation = hand.rotation;
			currentWeapon.transform.localRotation = Quaternion.Euler(-280.5f, 2.5f, -15f);
		}*/
	}
		
	public void Equip (GameObject weapon)
	{
		currentWeapon = weapon;
		currentWeapon.rigidbody.isKinematic = true;
		currentWeapon.rigidbody.useGravity = false;
		currentWeapon.transform.parent = hand;
		holdPoint = weapon.transform.Find ("HoldPoint");


		//currentWeapon.transform.rotation = Quaternion.Euler(Vector3.zero);
		currentWeapon.transform.localRotation = Quaternion.Euler(-100.5f, 2.5f, -15f);
		//currentWeapon.transform.position = Vector3.zero;
		currentWeapon.transform.position = hand.position + (currentWeapon.transform.position - holdPoint.position);
		
		Invoke ("InvokedWeaponEquipFlag", 0.1f);
	}
	
	private void InvokedWeaponEquipFlag ()
	{
		hasWeaponEquipped = true;
	}
		
	public void Drop (ItemType type)
	{
		// If there is a weapon equipped, and player is not in the middle of an attack
		if (type == ItemType.Weapon && hasWeaponEquipped &&
			!this.GetComponent<AttackController> ().inAnAttack) {
		    		
			currentWeapon.GetComponent<Weapon> ().Dropped ();
			currentWeapon.rigidbody.isKinematic = false;
			currentWeapon.rigidbody.useGravity = true;
			hasWeaponEquipped = false;
			currentWeapon = null;
		    		
		}
				// If there is a helm equipped
				else if (type == ItemType.Helm && hasHelmEquipped) {
		}
	}
		
	public bool hasWeaponEquipped {
		get { return m_hasWeaponEquipped; }
		set { m_hasWeaponEquipped = value; }
	}
		
	public bool hasHelmEquipped {
		get { return m_hasHelmEquipped;}
		set { m_hasHelmEquipped = value;}
	}
}
