using UnityEngine;
using System.Collections.Generic;

public class TrapController : MonoBehaviour {

	public List<GameObject> targetsHit;
	public bool isTriggered;
	public bool isDisarmed;

	// Use this for initialization
	void Start() {
		targetsHit = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public int Hit(GameObject obj, float damage) {
		if (!targetsHit.Contains(obj)) {
			if (obj.tag != gameObject.tag) { 
				//add enemy to list of hit stuff
				targetsHit.Add(obj);
				print(obj.GetComponent<HealthController>().IsParrying);
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
		} else {
			return -1;
		}
	}
}
