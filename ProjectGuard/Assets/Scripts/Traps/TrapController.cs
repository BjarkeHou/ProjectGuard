using UnityEngine;

public class TrapController : MonoBehaviour {

	private SpearBehaviour[] spearBehav;
	private BearBehaviour[] bearBehav;

	// Use this for initialization
	void Start() {
		spearBehav = transform.GetComponentsInChildren<SpearBehaviour>();
		bearBehav = transform.GetComponentsInChildren<BearBehaviour>();
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
		foreach (BearBehaviour bear in bearBehav) {
			bear.Trigger();
		}
	}


}
