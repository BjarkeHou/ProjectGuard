using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class disarmTrapLabel : MonoBehaviour
{

	private dfLabel _label;
	
	private float _x;
	private float _y;

	// Called by Unity just before any of the Update methods is called the first time.
	public void Start()
	{
		// Obtain a reference to the dfLabel instance attached to this object
		this._label = GetComponent<dfLabel>();
	}
	
	void Update()
	{
		Vector3 v = new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 0);
		this._label.RelativePosition = v;
		this._label.BringToFront();
	}


}
