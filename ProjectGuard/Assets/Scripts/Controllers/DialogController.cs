using UnityEngine;
using System.Collections;

public class DialogController : MonoBehaviour
{
	public GUISkin gui_Skin;
	public float dialogRange;
	public int dialogID;
	public bool onlyInGhostMode;

	private GameController game;
	private DialogueLoader dialog;
	private GameObject player;
	private int dialogIDCounter;
	
	private bool showDialogLabel;
	private bool isTrigger;

	// Use this for initialization
	void Start()
	{
		isTrigger = this.gameObject.tag != "Enemy";
		showDialogLabel = false;
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		dialog = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogueLoader>();
		player = GameObject.FindGameObjectWithTag("Player");
		
		dialogIDCounter = 0;
	}
	
	void OnMouseEnter()
	{
		if (!isTrigger)
		{
			if (game.isInGhostMode && !game.isPaused)
			{
				showDialogLabel = true;
			}
		}
	}
	
	void OnMouseExit()
	{
		if (!isTrigger)
		{
		
		
			if (game.isInGhostMode && !game.isPaused)
			{
				showDialogLabel = false;
			}
		}
	}
	
	void OnMouseDown()
	{
		if (!isTrigger)
		{
			if (((onlyInGhostMode && game.isInGhostMode) || 
				!onlyInGhostMode) &&
				!game.isInDialogMode && 
				!game.isPaused && 
				showDialogLabel && 
				dialogRange > Vector3.Distance(this.transform.position, player.transform.position)
				)
			{
				game.isInDialogMode = true;
				// Lerp camera here....
			}
		}
	}
	
	void OnTriggerEnter()
	{
		if (isTrigger)
		{
			if (((onlyInGhostMode && game.isInGhostMode) || 
				!onlyInGhostMode) && 
				!game.isInDialogMode && 
				!game.isPaused
				)
			{
				game.isInDialogMode = true;
				// Lerp camera here....
			}
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
		
		if (game.isInDialogMode)
		{
			GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.5f, Screen.width * 0.8f, Screen.height * 0.45f), CompileDialogString(this.gameObject.name));
			
			if (GUI.Button(new Rect(Screen.width * 0.8f, Screen.height * 0.8f, Screen.width * 0.15f, Screen.height * 0.15f), "NEXT"))
			{
				dialogIDCounter++;	
			}
		}
	}
	
	string CompileDialogString(string id)
	{
		string s = "";
		s += dialog.GetDialogueNode(id).SpeakerName + ":\n";
		s += "\"" + dialog.GetDialogueNode(id).Text + "\"\n";
		print(s);
		return s;
	}	
}
