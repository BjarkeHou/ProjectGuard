using UnityEngine;
using System.Collections;

public class lololol : MonoBehaviour
{


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
			GoAway();
		}
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
	}
	
	void GoAway()
	{
		Application.LoadLevel("Boss");
	}
}
