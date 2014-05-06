using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour
{

	private TrapController trapCtrl;
	private GhostWorldController ghostCtrl;
	private GameObject player;
	
	private bool isDisarmed = false;
	
	public float disarmTime;
	public float disarmRange;

	public AudioClip trapTrigger;
	public AudioClip trapDisarm;

	// Use this for initialization
	void Start()
	{
		trapCtrl = transform.parent.gameObject.GetComponent<TrapController>();
		ghostCtrl = GameObject.Find("GameController").GetComponent<GhostWorldController>();

		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update()
	{
		if (Vector3.Distance(this.transform.position, player.transform.position) < disarmRange)
		{
//			print(trapCtrl.canBeDisarmed);
//			print(Input.GetKey(KeyCode.E));
//			print(isDisarmed);
//			print(Vector3.Distance(this.transform.position, player.transform.position));
		}
		if (trapCtrl.canBeDisarmed && Input.GetKey(KeyCode.E) && !isDisarmed && Vector3.Distance(this.transform.position, player.transform.position) < disarmRange)
		{
			Disarm();
		}
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other)
	{
		if (ghostCtrl.deathTransition <= 0)
		{
			if (other.tag == "Player")
			{
				trapCtrl.Trigger();
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && ghostCtrl.deathTransition <= 0)
		{
			audio.clip = trapTrigger;
			if (!audio.isPlaying)
			{
				audio.Play();
			}
		}
	}

	void OnTriggerLeave(Collider other)
	{
		if (other.tag == "Player")
		{
			transform.Find("Disarm").guiTexture.enabled = false;
		}
	}

	public void Disarm()
	{
		if (trapCtrl.canBeDisarmed)
		{	
			isDisarmed = true;
			collider.enabled = false;
			audio.clip = trapDisarm;
			audio.Play();
			print("Trap disarmed");
			
			// Make particles here
			GameObject deathSmoke = (GameObject)Instantiate(Resources.Load("Prefabs/DeathSmoke")) as GameObject;
			deathSmoke.GetComponent<AudioSource>().enabled = false;
			deathSmoke.transform.position = transform.position;
			
			trapCtrl.GetComponent<BoxCollider>().enabled = false;
	
			Destroy(gameObject);
		}
	}
}
