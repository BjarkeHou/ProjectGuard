using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public GUISkin guiSkin;
	public Texture background;
	public Texture playButton;
	public Texture settingsButton;
	public Texture tutorialButton;
	
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
		
		if (GUI.Button(new Rect(Screen.width * playButtonX * 0.01f, Screen.height * playButtonY * 0.01f, buttonWidth, buttonHeight), "Play Game"))
		{
			print("Play Game!");
			Application.LoadLevel(1);
		}
		
		if (GUI.Button(new Rect(Screen.width * settingsButtonX * 0.01f, Screen.height * settingsButtonY * 0.01f, buttonWidth, buttonHeight), "Settings"))
		{
			print("Settings!");
		}
		
		if (GUI.Button(new Rect(Screen.width * tutorialButtonX * 0.01f, Screen.height * tutorialButtonY * 0.01f, buttonWidth, buttonHeight), "Tutorial"))
		{
			print("Tutorial!");
		}
	}
}
