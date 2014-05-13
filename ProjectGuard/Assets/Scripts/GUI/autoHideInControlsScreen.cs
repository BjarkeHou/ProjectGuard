using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class autoHideInControlsScreen : MonoBehaviour
{

	private dfButton _button;
	private GUIController gui;

	// Called by Unity just before any of the Update methods is called the first time.
	public void Start()
	{
		// Obtain a reference to the dfButton instance attached to this object
		this._button = GetComponent<dfButton>();
		
		gui = GameObject.Find("UI Root").GetComponent<GUIController>();
	}

	void Update()
	{
		if (gui.showControlScreen)
		{
			_button.IsInteractive = false;
		} else
		{
			_button.IsInteractive = true;
		}
	}
}
