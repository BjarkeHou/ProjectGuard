using UnityEngine;
using System.Collections;

public class GhostWorldController : MonoBehaviour {
	private GhostWorld gWorld;
	private GameController game;
	private bool screenIsInGhostMode = false;

	public float deathTransition;
	public float deathTransitionSpeed;
	
	

	private bool fading = false;

	// Use this for initialization
	void Start() {
		gWorld = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GhostWorld>();
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update() {
		if (game.isInGhostMode != screenIsInGhostMode) {
			fading = true;
			screenIsInGhostMode = game.isInGhostMode;
		}
		

		if (fading) {
			if (game.isInGhostMode && deathTransition < 1) {
				deathTransition += deathTransitionSpeed * Time.deltaTime;
//				TransitionToGhostMode();
			} else if (!game.isInGhostMode && deathTransition > 0) {
				deathTransition -= deathTransitionSpeed * Time.deltaTime;
//				TransitionToNormalMode();
			} else {
				fading = false;
			}
			TransitionLOL();
		}
		gWorld.transition = deathTransition;
	}
	
	void TransitionLOL() {
		float normalModelAlphaValue = Mathf.InverseLerp(0f, 0.5f, deathTransition);
		float liamModelAlphaValue = Mathf.InverseLerp(0.5f, 1f, deathTransition);
		
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies) {
			enemy.transform.FindChild("LiamModel").gameObject.SetActive(true);

			//set live model
			enemy.transform.Find("Model").GetComponent<Animator>().speed = 1 - normalModelAlphaValue;
			Renderer[] renderes = enemy.transform.FindChild("Model").GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderes) {
				r.material.shader = Shader.Find("Transparent/Diffuse");
				Color c = r.material.color;
				c.a = 1 - normalModelAlphaValue;
				r.material.color = c;
			}

			//set ghost model
			enemy.transform.Find("LiamModel").GetComponent<Animator>().speed = liamModelAlphaValue;
			renderes = enemy.transform.FindChild("LiamModel").GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderes) {	
				r.material.shader = Shader.Find("Transparent/Diffuse");
				Color c = r.material.color;
				c.a = liamModelAlphaValue;
				r.material.color = c;
			}
		}
	}
	
	/*void TransitionToGhostMode()
	{
		deathTransition += deathTransitionSpeed * Time.deltaTime;
		
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies)
		{
			Renderer[] renderes = enemy.transform.FindChild("Model").GetComponentsInChildren<Renderer>();
			
			foreach (Renderer r in renderes)
			{
				if (r.material.color == null)
					break;
					
				r.material.shader = Shader.Find("Transparent/Diffuse");
				Color c = r.material.color;
				c.a = 1 - Mathf.InverseLerp(0, 0.5, deathTransition);
				r.material.color = c;
			}
			
			enemy.transform.FindChild("LiamModel").gameObject.SetActive(true);
			renderes = enemy.transform.FindChild("LiamModel").GetComponentsInChildren<Renderer>();
			
			foreach (Renderer r in renderes)
			{
				if (r.material.color == null)
					break;
					
				r.material.shader = Shader.Find("Transparent/Diffuse");
				Color c = r.material.color;
				c.a = Mathf.InverseLerp(0, 0.5, deathTransition);
				r.material.color = c;
			}
			
			if (deathTransition >= 1)
			{
				enemy.transform.FindChild("Model").gameObject.SetActive(false);
				print("deathTransition = " + deathTransition);
			}
		}
	}
	
	void TransitionToNormalMode()
	{
		deathTransition -= deathTransitionSpeed * Time.deltaTime;
		
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies)
		{
			if (deathTransition > .5f)
			{
				enemy.transform.FindChild("Model").gameObject.SetActive(true);
				Renderer[] renderes = enemy.transform.FindChild("Model").GetComponentsInChildren<Renderer>();
			
				foreach (Renderer r in renderes)
				{
					if (r.material.color == null)
						break;
				
					r.material.shader = Shader.Find("Diffuse");
					Color c = r.material.color;
					c.a = Mathf.InverseLerp(0.5, 1, deathTransition);
					r.material.color = c;
				}
			} else if (deathTransition < .5f)
			{
				Renderer[] renderes = enemy.transform.FindChild("LiamModel").GetComponentsInChildren<Renderer>();
			
				foreach (Renderer r in renderes)
				{
					if (r.material.color == null)
						break;
				
					r.material.shader = Shader.Find("Transparent/Diffuse");
					Color c = r.material.color;
					c.a = 1 - Mathf.InverseLerp(0, 0.5, deathTransition);
					r.material.color = c;
				}
				
				if (deathTransition <= 0)
				{
					enemy.transform.FindChild("LiamModel").gameObject.SetActive(false);
					print("deathTransition = " + deathTransition);
				}
			}
			
		}
	}*/
}
