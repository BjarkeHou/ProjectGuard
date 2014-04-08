using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
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
}
