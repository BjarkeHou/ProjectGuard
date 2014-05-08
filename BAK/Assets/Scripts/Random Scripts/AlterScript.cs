using UnityEngine;
using System.Collections;

public class AlterScript : MonoBehaviour
{
	
	private GameController game;
	
	// Use this for initialization
	void Start()
	{
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (!game.isInGhostMode)
			Application.LoadLevel("MainMenu");
	}
}
