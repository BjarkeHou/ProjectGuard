using UnityEngine;
using System.Collections;

public class DialogController : MonoBehaviour
{
	public float dialogRange = 2;
	public bool onlyInGhostMode = true;
	public bool isRepeatable = false;

	public string speakerName;

	private GameController game;
	private GUIController gui;
	private DialogueLoader dialogLoader;
	private GameObject player;
	private int dialogIDCounter;
	
	private bool isTrigger;
	private bool isTriggered;
	
	private AudioSource dialogSpeaker;
	
	private DialogueNode[] dialog;

	// Use this for initialization
	void Start()
	{
		dialogSpeaker = GameObject.Find("Voices").GetComponent<AudioSource>();
		gui = GameObject.Find("UI Root").GetComponent<GUIController>();
		isTrigger = this.gameObject.tag != "Enemy";
		if (!isTrigger)
			player = GameObject.FindGameObjectWithTag("Player");
	
		dialogIDCounter = 0;
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		dialogLoader = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogueLoader>();

		if (speakerName.Length > 0)
		{
			print("specialSpeaker = " + speakerName);
			dialog = dialogLoader.GetDialogueNode(speakerName);
		} else
		{
			print("Random");
			int randomNum = Random.Range(1, 6);
			dialog = dialogLoader.GetDialogueNode("RandomSpirit" + randomNum);
		}
	}
	
	void OnMouseEnter()
	{
		if (!isTrigger)
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
		if (!isTrigger)
		{
			gui.showDialogPrompt = false;
			gui.dialogPromptText = "";
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
				dialogRange > Vector3.Distance(this.transform.position, player.transform.position)
				)
			{
				
				isTriggered = true;
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
			if (((onlyInGhostMode && game.isInGhostMode) || 
				!onlyInGhostMode) && 
				!game.isInDialogMode && 
				!game.isPaused
				)
			{
				
				isTriggered = true;
				gui.dialogCtrl = this;
				game.isInDialogMode = true;
				// Lerp camera here....
			}
		}
	}
	
	void Update()
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

			if (Resources.Load("Dialogs/Audio/" + dialog [dialogIDCounter].AudioName) != null)
			{
				if (dialogSpeaker.clip != Resources.Load("Dialogs/Audio/" + dialog [dialogIDCounter].AudioName) as AudioClip)
				{
					dialogSpeaker.clip = Resources.Load("Dialogs/Audio/" + dialog [dialogIDCounter].AudioName) as AudioClip;
					dialogSpeaker.Play();
				}
			}
			
			gui.showDialogPrompt = false;
		
			gui.dialogText = CompileDialogString();
			
			
			gui.dialogButtonText = dialogIDCounter < dialog.Length - 1 ? "NEXT" : "FINISH";
		}
	}
	
	public void dialogButtonClicked()
	{
		dialogIDCounter++;	
		if (dialogIDCounter == dialog.Length)
		{
			gui.dialogCtrl = null;
			isTriggered = false;
			game.isInDialogMode = false;
			dialogIDCounter = 0;
			dialogSpeaker.Stop();
			dialogSpeaker.clip = null;
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
