using UnityEngine;
using System.Collections;

public class AnimationScripts : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//called form animations
	void Damage () {
		player.GetComponent<AttackController>().doesDamage = true;
	}
	void NoDamage () {
		player.GetComponent<AttackController>().doesDamage = false;
	}
	void CanMove () {
		player.GetComponent<PlayerController>().SetCanMove(true);
		GameObject.Find("Main Camera").GetComponent<PlayerLook>().SetPlayerCanRotate(true);
	}
	void CanNotMove () {
		player.GetComponent<PlayerController>().SetCanMove(false);
		GameObject.Find("Main Camera").GetComponent<PlayerLook>().SetPlayerCanRotate(false);
	}
	void ClearHits () {
		player.GetComponent<AttackController>().targetsHit.Clear();
	}
}
