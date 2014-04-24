using UnityEngine;
using System.Collections;

public class DialogController : MonoBehaviour
{
	public GUISkin gui_Skin;

	private GameController game;
	
	private bool showDialogLabel;

	// Use this for initialization
	void Start()
	{
		showDialogLabel = false;
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	void OnMouseEnter()
	{
		if (game.isInGhostMode && !game.isPaused)
		{
			showDialogLabel = true;
		}
	}
	
	void OnMouseExit()
	{
		if (game.isInGhostMode && !game.isPaused)
		{
			showDialogLabel = false;
		}
	}
	
	void OnGUI()
	{
		if (game.isInGhostMode && !game.isPaused && showDialogLabel)
		{
			GUI.skin = gui_Skin;
			GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y, 300, 200), "Click to speak with " + this.gameObject.name);
		}
	}
}
