﻿using UnityEngine;

public class TrapController : MonoBehaviour {
	
	private SpearBehaviour[] spearBehav;

	// Use this for initialization
	void Start() {
		spearBehav = transform.GetComponentsInChildren<SpearBehaviour>();

	}

	public int Hit(GameObject obj, float damage) {
		if (obj.tag != gameObject.tag) {
			//check if the character is parrying
			if (obj.GetComponent<HealthController>().IsParrying) {
				return 0;
			} else {
				//withdraw health
				obj.GetComponent<HealthController>().adjustCurrentHealth(-(int)damage);
				return 1;
			}
		} else {
			return 2;
		}
	}

	public void Trigger() {
		foreach (SpearBehaviour spear in spearBehav) {
			spear.Trigger();
		}
	}


}
