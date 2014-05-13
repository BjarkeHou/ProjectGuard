using UnityEngine;
using System.Collections;

public class ClickToSwitchScene : MonoBehaviour
{
	public string levelToLoad;
	public float delay = 10;
	
	private bool clicked = false;

	public Texture background;
	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !clicked)
		{
			Invoke("GoAway", delay);
			clicked = true;
		}
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
		if (clicked)
		{
			GUI.Label(new Rect(Screen.width * .4f, Screen.height * .8f, Screen.width * .2f, Screen.height * .1f), "Loading...");
		}
	}
	
	void GoAway()
	{
		Application.LoadLevel(levelToLoad);
	}
}
