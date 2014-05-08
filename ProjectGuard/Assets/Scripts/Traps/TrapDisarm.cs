using UnityEngine;
using System.Collections;

public class TrapDisarm : MonoBehaviour
{

	private GameController game;
	private GUIController gui;
	private bool canBeDisarmed = false;
	public bool CanBeDisarmed { get { return canBeDisarmed; } }
	
	// Use this for initialization
	void Start()
	{
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		gui = GameObject.Find("UI Root").GetComponent<GUIController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!game.isInGhostMode || game.isPaused || game.isInDialogMode)
			gui.showDisarmTrapPrompt = canBeDisarmed = false;
	}
	
	void OnMouseEnter()
	{
		if (game.isInGhostMode && !game.isPaused && !game.isInDialogMode)
		{
			gui.showDisarmTrapPrompt = true;
			canBeDisarmed = true;
		}
	}
	
	void OnMouseExit()
	{
		gui.showDisarmTrapPrompt = false;
		canBeDisarmed = false;
	}
}
