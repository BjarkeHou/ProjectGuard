using UnityEngine;
using System.Collections;

public class victoryKill : MonoBehaviour
{

	private HealthController hCtrl;
	private bool invoked = false;

	// Use this for initialization
	void Start()
	{
		hCtrl = GetComponent<HealthController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!hCtrl.stillAlive() && !invoked)
		{
			invoked = true;
			Invoke("ChangeLevel", 3);
		}
	}
	
	void ChangeLevel()
	{
		Application.LoadLevel("End");
	}
}
