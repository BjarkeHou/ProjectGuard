using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour
{

	private PlayerWillController pWill;
	private HealthController hCtrl;
	private GameController game;

	public GUISkin guiSkin;

	private Texture2D barTex;
	private Texture2D spentTex;
	private Texture2D damageTex;
	private Texture2D bgTex;
	private Texture2D borderTex;
	public Color barCol;
	public Color spentCol;
	public Color damageCol;
	public Color bgCol;
	public Color borderCol;

	public float PauseMenuButtonHeight;
	public float PauseMenuButtonWidth;
	public float ButtonsX;
	public float PlayButtonY;
	public float SettingsButtonY;
	public float CreditsButtonY;

	private int prevHealth;
	private float barAlpha;

	// Use this for initialization
	void Start()
	{
		pWill = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWillController>();
		hCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		SetupWillBar();

		prevHealth = hCtrl.getCurrentHealth();
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
					UpdateTimerBar();
				} else
				{
					UpdateWillBar();
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
	
	void SetupWillBar()
	{
		barTex = new Texture2D(1, 1);
		barTex.SetPixel(0, 0, barCol);
		barTex.Apply();
		
		spentTex = new Texture2D(1, 1);
		spentTex.SetPixel(0, 0, spentCol);
		spentTex.Apply();

		damageTex = new Texture2D(1, 1);
		damageTex.SetPixel(0, 0, damageCol);
		damageTex.Apply();
		
		bgTex = new Texture2D(1, 1);
		bgTex.SetPixel(0, 0, bgCol);
		bgTex.Apply();
		
		borderTex = new Texture2D(1, 1);
		borderTex.SetPixel(0, 0, borderCol);
		borderTex.Apply();
	}
	void UpdateWillBar()
	{
		//Will Bar
		Vector2 dimension = new Vector2(Screen.width / 3, 50);
		Vector2 anchor = new Vector2((Screen.width / 2) - (dimension.x / 2), Screen.height - dimension.y - 50);
		
		//BG
		GUI.DrawTexture(new Rect(anchor.x, anchor.y, dimension.x, dimension.y), bgTex);
		
		//Spent
		float width = dimension.x * Mathf.InverseLerp(0, pWill.MaxWill, pWill.CurMaxWill);
		GUI.DrawTexture(new Rect(anchor.x, anchor.y, width, dimension.y), spentTex);
		
		//Bar
		width = dimension.x * Mathf.InverseLerp(0, pWill.MaxWill, pWill.CurWill);
		GUI.DrawTexture(new Rect(anchor.x, anchor.y, width, dimension.y), barTex);

		//Damage taken
		if (hCtrl.getCurrentHealth() != prevHealth)
		{
			if (hCtrl.getCurrentHealth() < prevHealth)
			{
				barAlpha = 1;
			}
			prevHealth = hCtrl.getCurrentHealth();
		}

		if (barAlpha > 0)
		{

			//set color (alpha)
			Color guiCol = GUI.color;
			GUI.color = new Color(1, 1, 1, barAlpha);

			width = dimension.x * Mathf.InverseLerp(0, pWill.MaxWill, Mathf.Abs(hCtrl.LastDamageTaken));
			GUI.DrawTexture(new Rect(anchor.x, anchor.y, width, dimension.y), damageTex);

			//reduce alpha
			barAlpha -= Time.deltaTime;

			GUI.color = guiCol;
		}
		
		//Border
		GUI.DrawTexture(new Rect(anchor.x, anchor.y, dimension.x, 1), borderTex);
		GUI.DrawTexture(new Rect(anchor.x + dimension.x, anchor.y, 1, dimension.y), borderTex);
		GUI.DrawTexture(new Rect(anchor.x + dimension.x, anchor.y + dimension.y, -dimension.x, 1), borderTex);
		GUI.DrawTexture(new Rect(anchor.x, anchor.y + dimension.y, 1, -dimension.y), borderTex);
	}
	
	void UpdateTimerBar()
	{
		//TimerBar Bar
		Vector2 dimension = new Vector2(Screen.width / 3, 50);
		Vector2 anchor = new Vector2((Screen.width / 2) - (dimension.x / 2), Screen.height - dimension.y - 50);
		
		//BG
		GUI.DrawTexture(new Rect(anchor.x, anchor.y, dimension.x, dimension.y), bgTex);
		
		//Spent
		float width = dimension.x * Mathf.InverseLerp(0, game.timeToReviveInGhostMode, game.timeLeftToReviveFromGhostMode);
		GUI.DrawTexture(new Rect(anchor.x, anchor.y, width, dimension.y), borderTex);
	}

}
