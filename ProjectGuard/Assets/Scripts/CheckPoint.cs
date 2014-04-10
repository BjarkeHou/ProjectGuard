using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour
{

	private HealthController hCont;
	private GhostWorldController ghostCtrl;
	private ParticleSystem[] particles;

	// Use this for initialization
	void Start()
	{
		ghostCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GhostWorldController>();
		particles = transform.GetComponentsInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (ghostCtrl.deathTransition > 1)
		{
			foreach (ParticleSystem part in particles)
			{
				part.enableEmission = true;
			}
		} else
		{
			foreach (ParticleSystem part in particles)
			{
				part.enableEmission = false;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && ghostCtrl.deathTransition > 1)
		{
			hCont = other.GetComponent<HealthController>();
			hCont.adjustCurrentHealth(hCont.getMaxHealth() - hCont.getCurrentHealth());
		}
	}
}
