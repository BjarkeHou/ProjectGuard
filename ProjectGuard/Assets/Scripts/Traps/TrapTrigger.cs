using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {

	private TrapController trapCtrl;
	private TrapDisarm disarm;
	private GhostWorldController ghostCtrl;
	private GameObject player;
	
	private bool isDisarmed = false;

	public float disarmTime;
	public float disarmRange;

	public AudioClip trapTrigger;

	// Use this for initialization
	void Start() {
		trapCtrl = transform.parent.gameObject.GetComponent<TrapController>();
		ghostCtrl = GameObject.Find("GameController").GetComponent<GhostWorldController>();

		player = GameObject.FindGameObjectWithTag("Player");

		disarm = transform.Find("Disarm").GetComponent<TrapDisarm>();
	}
	
	void Update() {
		if (Vector3.Distance(this.transform.position, player.transform.position) < disarmRange) {
//			print(trapCtrl.canBeDisarmed);
//			print(Input.GetKey(KeyCode.E));
//			print(isDisarmed);
//			print(Vector3.Distance(this.transform.position, player.transform.position));
		}
		if (disarm.CanBeDisarmed && Input.GetKey(KeyCode.E) && !isDisarmed && Vector3.Distance(this.transform.position, player.transform.position) < disarmRange) {
			print("wee");
			Disarm();
		}
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other) {
		if (ghostCtrl.deathTransition <= 0) {
			if (other.tag == "Player") {
				trapCtrl.Trigger();
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && ghostCtrl.deathTransition <= 0) {
			audio.clip = trapTrigger;
			if (!audio.isPlaying) {
				audio.Play();
			}
		}
	}

	void OnTriggerLeave(Collider other) {
		if (other.tag == "Player") {
			transform.Find("Disarm").guiTexture.enabled = false;
		}
	}

	public void Disarm() {
		disarm.showTrapPrompt = false;
		isDisarmed = true;
		collider.enabled = false;
		print("Trap disarmed");
		
		// Make particles here
		GameObject disarmSmoke = (GameObject)Instantiate(Resources.Load("Prefabs/DisarmSmoke")) as GameObject;
		disarmSmoke.transform.position = transform.position;
		Destroy(gameObject);
	}
}
