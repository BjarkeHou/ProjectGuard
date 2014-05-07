using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour
{

	private PlayerWillController pWill;
	private HealthController hCtrl;
	private GameController game;
	private Camera mainCam;

	public GUISkin guiSkin;

	private Texture2D barTex;
	private Texture2D spentTex;
	private Texture2D damageTex;
	private Texture2D bgTex;
	private Texture2D borderTex;
	public Texture2D corpsePointerTex;

	public float PauseMenuButtonHeight;
	public float PauseMenuButtonWidth;
	public float ButtonsX;
	public float PlayButtonY;
	public float SettingsButtonY;
	public float CreditsButtonY;

	private int prevHealth;
	private float barAlpha;

	public GameObject rezzSpot;
	public GameObject RezzSpot { set { rezzSpot = value; } }

	// Use this for initialization
	void Start()
	{
		pWill = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWillController>();
		hCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		mainCam = GameObject.FindWithTag("MainCamera").camera;

		prevHealth = hCtrl.getCurrentHealth();

		rezzSpot = GameObject.FindWithTag("Player");
	}

	void OnGUI()
	{
		GUI.skin = guiSkin;
		if (game.isPaused)
		{
			ShowPauseMenu();
		} else
		{
			if (!game.isInDialogMode)
			{
				if (game.isInGhostMode)
				{
					UpdateCorpsePointer();
				}
			}
		}
	}
	
	void ShowPauseMenu()
	{
		GUI.Label(new Rect(Screen.width * .20f, Screen.height * .1f, Screen.width * .6f, Screen.height * .8f), "");
		
		if (GUI.Button(new Rect(Screen.width * ButtonsX * .01f, Screen.height * SettingsButtonY * .01f, Screen.width * PauseMenuButtonWidth * .01f, Screen.height * PauseMenuButtonHeight * 0.01f), "Settings"))
		{
			print("Settings");
		}
		
		if (GUI.Button(new Rect(Screen.width * ButtonsX * .01f, Screen.height * CreditsButtonY * .01f, Screen.width * PauseMenuButtonWidth * .01f, Screen.height * PauseMenuButtonHeight * 0.01f), "Credits"))
		{
			print("Credits");
		}
		
		if (GUI.Button(new Rect(Screen.width * ButtonsX * .01f, Screen.height * PlayButtonY * .01f, Screen.width * PauseMenuButtonWidth * .01f, Screen.height * PauseMenuButtonHeight * 0.01f), "Resume"))
		{
			game.UnPauseGame();
		}
	}

	void UpdateCorpsePointer()
	{
		Vector3 corpsePos;
		if (rezzSpot == null)
		{
			corpsePos = mainCam.WorldToScreenPoint(GameObject.FindWithTag("Player").transform.position);
		} else
		{
			corpsePos = mainCam.WorldToScreenPoint(rezzSpot.transform.position);
		}

		if (corpsePos.z < 0 || //is out of screen
			corpsePos.x < 0 || corpsePos.x > Screen.width ||
			corpsePos.y < 0 || corpsePos.y > Screen.height)
		{

			if (corpsePos.z < 0)
			{
				corpsePos *= -1;
			}
		
			Vector3 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
			corpsePos -= screenCenter;

			float angle = Mathf.Atan2(corpsePos.y, corpsePos.x);
			angle -= 90 * Mathf.Deg2Rad;

			float cos = Mathf.Cos(angle);
			float sin = -Mathf.Sin(angle);

			corpsePos = screenCenter + new Vector3(sin * 150, cos * 150, 0);

			//y = mx+b
			float m = cos / sin;

			//ability to add padding to marker
			Vector3 screenBounds = screenCenter;

			//check up and down first
			if (cos > 0)
			{
				corpsePos = new Vector3(screenBounds.y / m, screenBounds.y, 0);
			} else
			{
				corpsePos = new Vector3(-screenBounds.y / m, -screenBounds.y, 0);
			}

			//if out of bounds, get point on appropriate side
			if (corpsePos.x > screenBounds.x)
			{//out of bounds! must be on the right
				corpsePos = new Vector3(screenBounds.x, screenBounds.x * m, 0);
			} else if (corpsePos.x < -screenBounds.x)
			{//out of bounds left
				corpsePos = new Vector3(-screenBounds.x, -screenBounds.x * m, 0);
			} //else in bounds

			//remove coordinate translation
			corpsePos += screenCenter;

			Vector2 anchor = corpsePos;
			Vector2 dimension = new Vector2(256, 256);
		
			GUI.DrawTexture(new Rect(anchor.x - (dimension.x / 2), (Screen.height - anchor.y) - (dimension.y / 2), dimension.x, dimension.y), corpsePointerTex);
		}
	}

}
