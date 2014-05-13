using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public Texture background;
	public AudioClip theme;

	void Start()
	{
		this.audio.clip = theme;
		this.audio.Play();
	}

	public void PlayClicked()
	{
		print("Play Game!");
		Application.LoadLevel("alphaScreen");
	}
	
	public void CreditsClicked()
	{
		Application.LoadLevel("End");
	}
	
	public void ExitClicked()
	{
		Application.Quit();
	}
	
//	void OnGUI()
//	{
//		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
//	}
}
