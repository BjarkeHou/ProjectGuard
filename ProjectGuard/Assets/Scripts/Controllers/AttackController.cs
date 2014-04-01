using UnityEngine;
using System.Collections.Generic;

public class AttackController : MonoBehaviour {
	
	private enum HitTypes {
		HIT,
		REBOUND
	}
	private HitTypes hitType;
	public GameObject playerModel;

	public bool doesDamage;
	public List<GameObject> targetsHit;
	private Animator anim;
	private bool m_inAnAttack = false;
	
	public float rangeForAttack;

	// Use this for initialization
	void Start() {
		doesDamage = false;
		targetsHit = new List<GameObject> ();
		anim = playerModel.GetComponent<Animator>();
	}

	void Update() {
		//TEMP SOLUTION
		ThereIsSpaceForNormalAttack();
	}

	public void DeclareAttack() {
		inAnAttack = true;
		//if (ThereIsSpaceForNormalAttack()) {
		anim.SetBool("Attack", true);
		//} else {
		//	anim.SetBool("Stab", true);
		//}
	}

	//called from the equiped weapon
	public void Hit(GameObject obj, float damage) {
		if (!targetsHit.Contains(obj) && obj.tag != gameObject.tag) { 
			//withdraw health
			obj.GetComponent<HealthController>().adjustCurrentHealth(-(int)damage);
			//add enemy to list of hit stuff
			targetsHit.Add(obj);
		}
	}
	//called from the equiped weapon
	public void Rebound() {
		//change animation
		anim.SetBool("Rebound", true);
	}
	
	public bool inAnAttack {
		get { return m_inAnAttack;}
		set { m_inAnAttack = value;}
	}
	
	private void ThereIsSpaceForNormalAttack() {
		bool returnValue = true;
		
		RaycastHit hit;
		
		if (Physics.Raycast(this.transform.position, this.transform.right, out hit, rangeForAttack)) {
			if (hit.transform.gameObject.layer == LayerMask.NameToLayer("LightMap")) {
				returnValue = false;

				//TEMP SOLUTION
				anim.SetBool("Stab", true);
			}
		}
		//return returnValue;
	}
}
