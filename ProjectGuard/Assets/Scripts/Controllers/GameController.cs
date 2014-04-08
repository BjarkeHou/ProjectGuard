using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GUISkin guiSkin;
	public float PauseMenuButtonHeight;
	private bool m_isPaused = false;
	
	public bool isPaused
	{
		get { return m_isPaused; }
		set { m_isPaused = value; }
	}

	public void PauseGame()
	{
		isPaused = true;
		Time.timeScale = 0;
	}
	
	public void UnPauseGame()
	{
		isPaused = false;
		Time.timeScale = 1;
	}

	void OnGUI()
	{
		if (isPaused)
		{
			GUI.skin = guiSkin;
			GUI.Label(new Rect(Screen.width * .4f, Screen.height * .25f, Screen.width * .2f, Screen.height * .5f), "");
			
			if (GUI.Button(new Rect(Screen.width * .42f, Screen.height * .4f, Screen.width * .16f, Screen.height * PauseMenuButtonHeight), "Options"))
			{
				UnPauseGame();
			}
			
			if (GUI.Button(new Rect(Screen.width * .42f, Screen.height * .5f, Screen.width * .16f, Screen.height * PauseMenuButtonHeight), "Credits"))
			{
				UnPauseGame();
			}
			
			if (GUI.Button(new Rect(Screen.width * .42f, Screen.height * .6f, Screen.width * .16f, Screen.height * PauseMenuButtonHeight), "Resume"))
			{
				UnPauseGame();
			}
		}
	}
}
