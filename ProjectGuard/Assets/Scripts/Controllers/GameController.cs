﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	
	public float timeToReviveInGhostMode;
	
	private bool m_isPaused = false;
	public bool m_isInGhostMode = false;
	private float m_timeLeftToReviveFromGhostMode;
	
	public bool isPaused
	{
		get { return m_isPaused; }
		set { m_isPaused = value; }
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
		if (isInGhostMode)
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