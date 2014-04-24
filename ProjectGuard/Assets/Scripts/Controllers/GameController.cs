using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	
	public float timeToReviveInGhostMode;
	
	private bool m_isPaused = false;
	private bool m_isInGhostMode = false;
	private bool m_isInDialogMode = false;
	private float m_timeLeftToReviveFromGhostMode;
	
	public bool isPaused
	{
		get { return m_isPaused; }
		set { m_isPaused = value; }
	}
	
	public bool isInDialogMode
	{
		get { return m_isInDialogMode; }
		set { m_isInDialogMode = value; }
	}
	
	public bool isInGhostMode
	{
		get { return m_isInGhostMode;}
		set
		{ 
			m_isInGhostMode = value;
			m_timeLeftToReviveFromGhostMode = timeToReviveInGhostMode;
		}
	}
	
	public float timeLeftToReviveFromGhostMode
	{
		get{ return m_timeLeftToReviveFromGhostMode;}
	}
	
	void Update()
	{
		if (isInGhostMode && !isInDialogMode)
		{
			m_timeLeftToReviveFromGhostMode -= Time.deltaTime;
			if (m_timeLeftToReviveFromGhostMode < 0)
			{
				print("GAME FUCKING OVER!");
				Time.timeScale = 0;
			}
		}
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
