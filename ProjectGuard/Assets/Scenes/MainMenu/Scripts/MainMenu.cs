using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public GUIStyle style;

	public GUISkin guiSkin;
	public Texture background;
	public Texture logo;
	
	public Texture[] playButton;
	public Texture[] settingsButton;
	public Texture[] exitButton;
	
	public float logoX;
	public float logoY;
	public float logoWidth;
	public float logoHeight;
	
	public float playButtonX;
	public float playButtonY;
	
	public float settingsButtonX;
	public float settingsButtonY;
	
	public float tutorialButtonX;
	public float tutorialButtonY;
	
	public float buttonWidth;
	public float buttonHeight;
	
	void OnGUI()
	{
		GUI.skin = guiSkin;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
		
		GUI.DrawTexture(new Rect(Screen.width * logoX * 0.01f, Screen.height * logoY * 0.01f, Screen.width * logoWidth * 0.01f, Screen.height * logoHeight * 0.01f), logo);
		
		if (GUI.Button(new Rect(Screen.width * playButtonX * 0.01f, Screen.height * playButtonY * 0.01f, Screen.width * buttonWidth * 0.01f, Screen.height * buttonHeight * 0.01f), "PLAY"))
		{
			print("Play Game!");
			Application.LoadLevel(1);
		}
		
		if (GUI.Button(new Rect(Screen.width * settingsButtonX * 0.01f, Screen.height * settingsButtonY * 0.01f, Screen.width * buttonWidth * 0.01f, Screen.height * buttonHeight * 0.01f), "SETTINGS"))
		{
			print("Settings!");
		}
		
		if (GUI.Button(new Rect(Screen.width * tutorialButtonX * 0.01f, Screen.height * tutorialButtonY * 0.01f, Screen.width * buttonWidth * 0.01f, Screen.height * buttonHeight * 0.01f), "EXIT"))
		{
			Application.Quit();
		}
	}
}
