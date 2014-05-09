using UnityEngine;
using System.Collections;

public class ClickToSwitchScene : MonoBehaviour
{
	public string levelToLoad;
	public float delay = 10;

	public Texture background;
	// Use this for initialization
	void Start()
	{
		Invoke("GoAway", 60);
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Invoke("GoAway", delay);
		}
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
	}
	
	void GoAway()
	{
		Application.LoadLevel(levelToLoad);
	}
}
