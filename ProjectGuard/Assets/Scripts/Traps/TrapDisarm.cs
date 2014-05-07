using UnityEngine;
using System.Collections;

public class TrapDisarm : MonoBehaviour
{

	private GameController game;
	public GUISkin gui_Skin;
	public bool showTrapPrompt = false;
	private bool canBeDisarmed = false;
	public bool CanBeDisarmed { get { return canBeDisarmed; } }
	
	// Use this for initialization
	void Start()
	{
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!game.isInGhostMode || game.isPaused || game.isInDialogMode)
			showTrapPrompt = canBeDisarmed = false;
	}
	
	void OnMouseEnter()
	{
		if (game.isInGhostMode && !game.isPaused && !game.isInDialogMode)
		{
			showTrapPrompt = true;
			canBeDisarmed = true;
		}
	}
	
	void OnMouseExit()
	{
		showTrapPrompt = false;
		canBeDisarmed = false;
	}
}
