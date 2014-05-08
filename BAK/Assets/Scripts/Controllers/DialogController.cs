using UnityEngine;
using System.Collections;

public class DialogController : MonoBehaviour
{
	public GUISkin gui_Skin;
	public float dialogRange = 2;
	public bool onlyInGhostMode = true;
	public bool isRepeatable = false;

	private GameController game;
	private DialogueLoader dialogLoader;
	private GameObject player;
	private int dialogIDCounter;
	
	private bool showDialogLabel;
	private bool isTrigger;
	private bool isTriggered;
	
	private DialogueNode[] dialog;

	// Use this for initialization
	void Start()
	{
		if (this.gameObject.name != "Mirror Image")
		{
			isTrigger = this.gameObject.tag != "Enemy";
			if (!isTrigger)
				player = GameObject.FindGameObjectWithTag("Player");
		
			showDialogLabel = false;
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
			if (game.isInGhostMode && !game.isPaused)
			{
				showDialogLabel = true;
			}
		}
	}
	
	void OnMouseExit()
	{
		if (!isTrigger && this.gameObject.name != "Mirror Image")
		{
		
		
			if (game.isInGhostMode && !game.isPaused)
			{
				showDialogLabel = false;
			}
		}
	}
	
	void OnMouseDown()
	{
		if (!isTrigger && this.gameObject.name != "Mirror Image")
		{
			isTriggered = true;
			Debug.Log(player);
			print(Vector3.Distance(this.transform.position, player.transform.position)); // null?
			

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
				game.isInDialogMode = true;
				// Lerp camera here....
			}
		}
	}
	
	void OnGUI()
	{
		if (this.gameObject.name != "Mirror Image")
		{
			GUI.skin = gui_Skin;
		
			// Vis mouseover på at ham kan man snakke med
			if (game.isInGhostMode && !game.isPaused && showDialogLabel && !game.isInDialogMode)
			{
				GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y, 300, 200), "Click to speak with " + this.gameObject.name);
			}
		
			if (game.isInDialogMode && isTriggered)
			{
				GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.5f, Screen.width * 0.8f, Screen.height * 0.45f), CompileDialogString());
			
				string s = dialogIDCounter < dialog.Length - 1 ? "NEXT" : "FINISH";
				if (GUI.Button(new Rect(Screen.width * 0.8f, Screen.height * 0.8f, Screen.width * 0.15f, Screen.height * 0.15f), s))
				{
					dialogIDCounter++;	
					if (dialogIDCounter == dialog.Length)
					{
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
			}
		}
	}
	
	string CompileDialogString()
	{
		string s = "";
		s += "\n" + dialog [dialogIDCounter].SpeakerName + ":\n";
		s += "\"" + dialog [dialogIDCounter].Text + "\"\n";
		return s;
	}	
}
