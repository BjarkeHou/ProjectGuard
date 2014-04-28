 using UnityEngine;
using System.Collections;

public class GhostWorldController : MonoBehaviour {
	private GhostWorld gWorld;
	private GameController game;
	private PlayerSoundController playerAudio;
	private bool screenIsInGhostMode = false;

	public float deathTransition;
	public float deathTransitionSpeed;
	
	

	private bool fading = false;
	private bool shaderSet = false;

	// Use this for initialization
	void Start() {
		gWorld = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GhostWorld>();
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		playerAudio = GameObject.FindWithTag("Player").GetComponent<PlayerSoundController>();
	}
	
	// Update is called once per frame
	void Update() {
		playerAudio.LivingAmbSource.volume = 1 - deathTransition;
		playerAudio.GhostAmbSource.volume = Mathf.Clamp(deathTransition, 0, Mathf.InverseLerp(-game.timeToReviveInGhostMode / 2, game.timeToReviveInGhostMode, game.timeToReviveInGhostMode - game.timeLeftToReviveFromGhostMode));

		if (playerAudio.LivingAmbSource.volume < 0 && playerAudio.LivingAmbSource.isPlaying) {
			playerAudio.LivingAmbSource.Stop();
		} else if (playerAudio.LivingAmbSource.volume > 0 && !playerAudio.LivingAmbSource.isPlaying) {
			playerAudio.LivingAmbSource.Play();
		}
		if (playerAudio.GhostAmbSource.volume < 0 && playerAudio.GhostAmbSource.isPlaying) {
			playerAudio.GhostAmbSource.Stop();
		} else if (playerAudio.GhostAmbSource.volume > 0 && !playerAudio.GhostAmbSource.isPlaying) {
			playerAudio.GhostAmbSource.Play();
		}


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
			shaderSet = false;
			TransitionLOL();
		} else if (!shaderSet) {
			ResetShaders();
			shaderSet = true;
		}
		gWorld.transition = deathTransition;
	}
	
	void TransitionLOL() {
		float normalModelAlphaValue = Mathf.InverseLerp(0f, 0.5f, deathTransition);
		float liamModelAlphaValue = Mathf.InverseLerp(0.5f, 1f, deathTransition);
		
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies) {
			enemy.transform.FindChild("GhostModel").gameObject.SetActive(true);
			enemy.transform.FindChild("Model").gameObject.SetActive(true);

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
			enemy.transform.Find("GhostModel").GetComponent<Animator>().speed = liamModelAlphaValue;
			renderes = enemy.transform.FindChild("GhostModel").GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderes) {
				r.material.shader = Shader.Find("Transparent/Diffuse");
				Color c = r.material.color;
				c.a = liamModelAlphaValue;
				r.material.color = c;
			}
		}
	}

	void ResetShaders() {
		shaderSet = true;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		Renderer[] renderes;
		foreach (GameObject enemy in enemies) {
			if (!screenIsInGhostMode) {
				enemy.transform.FindChild("GhostModel").gameObject.SetActive(false);
				//set live model
				renderes = enemy.transform.FindChild("Model").GetComponentsInChildren<Renderer>();
				foreach (Renderer r in renderes) {
					if (r.tag != "IgnoreGhostTrans") {
						r.material.shader = Shader.Find("Custom/Self-Illumin/Bumped Specular");
					}
				}
			} else {
				enemy.transform.FindChild("Model").gameObject.SetActive(false);
				//set ghost model
				renderes = enemy.transform.FindChild("GhostModel").GetComponentsInChildren<Renderer>();
				foreach (Renderer r in renderes) {
					if (r.tag != "IgnoreGhostTrans") {
						r.material.shader = Shader.Find("Custom/Self-Illumin/Bumped Specular");
					}
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
}