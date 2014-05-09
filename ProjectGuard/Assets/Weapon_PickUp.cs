using UnityEngine;
using System.Collections;

public class Weapon_PickUp : MonoBehaviour {

	private Weapon parent;

	void Start() {
		parent = transform.parent.GetComponent<Weapon>();
	}

	void OnMouseDown() {
		if (Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= parent.maxPickupRange) {
			parent.TryToEquip(GameObject.FindGameObjectWithTag("Player"));
		}
	}
	
	void OnMouseEnter() {
		parent.ShowStatsLabel = true;
		parent.renderer.material.shader = parent.Outlined;
	}
	
	void OnMouseExit() {
		parent.ShowStatsLabel = false;
		parent.renderer.material.shader = parent.Diffuse;
	}
}
