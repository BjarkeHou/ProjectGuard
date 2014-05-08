using UnityEngine;
using System.Collections;

public class DialogController : MonoBehaviour
{
	public float dialogRange = 2;
	public bool onlyInGhostMode = true;
	public bool isRepeatable = false;

	private GameController game;
	private GUIController gui;
	private DialogueLoader dialogLoader;
	private GameObject player;
	private int dialogIDCounter;
	
	private bool isTrigger;
	private bool isTriggered;
	
	private DialogueNode[] dialog;

	// Use this for initialization
	void Start()
	{
		if (this.gameObject.name != "Mirror Image")
		{
			gui = GameObject.Find("UI Root").GetComponent<GUIController>();
			isTrigger = this.gameObject.tag != "Enemy";
			if (!isTrigger)
				player = GameObject.FindGameObjectWithTag("Player");
		
			dialogIDCounter = 0;
			game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
			dialogLoader = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogueLoader>();

			dialog = dialogLoader.GetDialogueNode(this.gameObject.name);
		}
	}
	
	void OnMouseEnter()
	{
		if (!isTrigger && this.gameObject.name != "Mirror Image")
		{
			if (game.isInGhostMode && !game.isPaused && !game.isInDialogMode)
			{
				gui.showDialogPrompt = true;
				gui.dialogPromptText = "Click to speak with " + dialog [dialogIDCounter].SpeakerName;
			}
		}
	}
	
	void OnMouseExit()
	{
		if (!isTrigger && this.gameObject.name != "Mirror Image")
		{
		
		
			gui.showDialogPrompt = false;
			gui.dialogPromptText = "";
			
		}
	}
	
	void OnMouseDown()
	{
		if (!isTrigger && this.gameObject.name != "Mirror Image")
		{
			
			isTriggered = true;

			if (((onlyInGhostMode && game.isInGhostMode) || 
				!onlyInGhostMode) &&
				!game.isInDialogMode && 
				!game.isPaused && 
				dialogRange > Vector3.Distance(this.transform.position, player.transform.position)
				)
			{
				gui.dialogCtrl = this;
				game.isInDialogMode = true;
				// Lerp camera here....
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			
			isTriggered = true;
			if (((onlyInGhostMode && game.isInGhostMode) || 
				!onlyInGhostMode) && 
				!game.isInDialogMode && 
				!game.isPaused
				)
			{
				gui.dialogCtrl = this;
				game.isInDialogMode = true;
				// Lerp camera here....
			}
		}
	}
	
	void Update()
	{
		if (this.gameObject.name != "Mirror Image")
		{
		
//			// Vis mouseover på at ham kan man snakke med
//			if (game.isInGhostMode && !game.isPaused && !game.isInDialogMode)
//			{
//				gui.showDialogPrompt = true;
//				gui.dialogPromptText = "Click to speak with " + dialog [dialogIDCounter].SpeakerName;
//			} else
//			{
//				gui.showDialogPrompt = false;
//				gui.dialogPromptText = "";
//			}
		
			if (game.isInDialogMode && isTriggered)
			{
				gui.showDialogPrompt = false;
			
				gui.dialogText = CompileDialogString();
			
				gui.dialogButtonText = dialogIDCounter < dialog.Length - 1 ? "NEXT" : "FINISH";
			}
		}
	}
	
	public void dialogButtonClicked()
	{
		print("Hent vand!");
		dialogIDCounter++;	
		if (dialogIDCounter == dialog.Length)
		{
			gui.dialogCtrl = null;
			isTriggered = false;
			game.isInDialogMode = false;
			dialogIDCounter = 0;
			if (!isRepeatable)
			{
				if (isTrigger)
					Destroy(this.gameObject);
				else
					Destroy(this);
			}	
		}
	}
	
	string CompileDialogString()
	{
		string s = "";
		s += dialog [dialogIDCounter].SpeakerName + ":\n";
		s += "\"" + dialog [dialogIDCounter].Text + "\"\n";
		return s;
	}	
}
