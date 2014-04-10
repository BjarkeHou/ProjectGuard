using UnityEngine;
using System.Collections;

public class GhostWorldController : MonoBehaviour
{

	private GameObject player;
	private GhostWorld gWorld;
	private GameController game;
	private bool screenIsInGhostMode = false;

	public float deathTransition;
	public float deathTransitionSpeed;
	
	

	private bool fading = false;

	// Use this for initialization
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		gWorld = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GhostWorld>();
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (game.isInGhostMode != screenIsInGhostMode)
		{
			fading = true;
			screenIsInGhostMode = game.isInGhostMode;
		}
		

		if (fading)
		{
			if (!player.GetComponent<HealthController>().stillAlive() && deathTransition < 1)
			{
				deathTransition += deathTransitionSpeed * Time.deltaTime;

			} else if (player.GetComponent<HealthController>().stillAlive() && deathTransition > 0)
			{
				deathTransition -= deathTransitionSpeed * Time.deltaTime;
			} else
			{
				fading = false;
			}
		}
		gWorld.transition = deathTransition;
	}
}
