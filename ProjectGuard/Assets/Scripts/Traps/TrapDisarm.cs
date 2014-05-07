using UnityEngine;
using System.Collections;

public class TrapDisarm : MonoBehaviour {

	private GameController game;
	public GUISkin gui_Skin;
	private bool showTrapPrompt = false;
	private bool canBeDisarmed = false;
	public bool CanBeDisarmed { get { return canBeDisarmed; } }
	
	// Use this for initialization
	void Start() {
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update() {
		
	}
	
	void OnMouseEnter() {
		if (game.isInGhostMode && !game.isPaused && !game.isInDialogMode) {
			showTrapPrompt = true;
			canBeDisarmed = true;
		}
	}
	
	void OnMouseExit() {
		showTrapPrompt = false;
		canBeDisarmed = false;
	}
	
	void OnGUI() {
		if (showTrapPrompt && game.isInGhostMode && !game.isPaused && !game.isInDialogMode) {
			GUI.skin = gui_Skin;
			
			// Vis mouseover på trap
			GUI.Label(new Rect (Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y, 300, 200), "Press E to disarm trap");
		}
	}
}
