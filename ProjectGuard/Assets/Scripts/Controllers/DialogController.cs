using UnityEngine;
using System.Collections;

public class DialogController : MonoBehaviour
{
	public GUISkin gui_Skin;
	public float dialogRange;

	private GameController game;
	private GameObject player;
	
	private bool showDialogLabel;

	// Use this for initialization
	void Start()
	{
		showDialogLabel = false;
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		player = GameObject.FindGameObjectWithTag("Player");
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
	
	void OnMouseDown()
	{
		if (game.isInGhostMode && !game.isInDialogMode && !game.isPaused && showDialogLabel && dialogRange > Vector3.Distance(this.transform.position, player.transform.position))
		{
			game.isInDialogMode = true;
			// Lerp camera here....
			
			
		}
	}
	
	void OnGUI()
	{
		GUI.skin = gui_Skin;
		
		// Vis mouseover på at ham kan man snakke med
		if (game.isInGhostMode && !game.isPaused && showDialogLabel && !game.isInDialogMode)
		{
			GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y, 300, 200), "Click to speak with " + this.gameObject.name);
		}
		
		if (game.isInDialogMode && game.isInGhostMode && !game.isPaused)
		{
			
		}
	}
}
