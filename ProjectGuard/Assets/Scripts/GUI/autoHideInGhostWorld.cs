using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class autoHideInGhostWorld : MonoBehaviour
{

	private dfProgressBar _progressBar;
	private GameController game;

	// Called by Unity just before any of the Update methods is called the first time.
	public void Start()
	{
		// Obtain a reference to the dfProgressBar instance attached to this object
		this._progressBar = GetComponent<dfProgressBar>();
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	void Update()
	{
		this._progressBar.IsVisible = !game.isInGhostMode;
	}
}
