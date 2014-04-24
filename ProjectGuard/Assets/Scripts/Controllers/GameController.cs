using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public float timeToReviveInGhostMode;
	
	private bool m_isPaused = false;
	private bool m_isInGhostMode = false;
	private bool m_isInDialogMode = false;
	private float m_timeLeftToReviveFromGhostMode;
	private bool lunaChange = false;
	public bool LunaChange { set { lunaChange = value; } }

	public Texture2D deathScreen;
	private float alpha;
	private bool fadeIn;
	public float fadeInSpeed;
	public float screenTime;
	private float timer;
	public float fadeOutSpeed;
	private AudioSource audioS;
	
	public bool isPaused {
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
		set { 
			m_isInGhostMode = value;
			m_timeLeftToReviveFromGhostMode = timeToReviveInGhostMode;
		}
	}
	
	public float timeLeftToReviveFromGhostMode {
		get{ return m_timeLeftToReviveFromGhostMode;}
		set{ m_timeLeftToReviveFromGhostMode = value; }
	}

	void Start() {
		fadeIn = true;
		alpha = -1;
		timer = 0;
	}
	
	void Update() {
		//game over timer
		if (isInGhostMode) {
			float factor = 1;
			if (GameObject.FindWithTag("Player").GetComponent<CharacterController>().velocity == Vector3.zero) {
				factor = 0.2f;
			}
			m_timeLeftToReviveFromGhostMode -= Time.deltaTime * factor;
			if (m_timeLeftToReviveFromGhostMode < 0) {
				print("GAME FUCKING OVER!");
				Time.timeScale = 0;
			}
		}
	}

	//death screen
	void OnGUI() {
		if (isInGhostMode && alpha == -1) {
			alpha = 0;							//if alpha is -1 it means that we are in the living world
		}
		if (alpha >= 0) {
			if (!lunaChange) {
				GUI.color = new Color (1, 1, 1, alpha);
				GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), deathScreen);
				GUI.color = new Color (1, 1, 1, 1);

				if (fadeIn) {
					//lock player
					GameObject.FindWithTag("Player").GetComponent<MovementController>().SetCanMove(false);

					//fade screen in
					alpha += fadeInSpeed * Time.deltaTime;
					if (alpha >= 1) {
						Vector3 deathSpot = GameObject.FindWithTag("Player").transform.position;
						//move player to checkPoint
						if (GameObject.FindWithTag("ActiveCheckpoint") != null) {
							GameObject.FindWithTag("Player").transform.position = GameObject.FindWithTag("ActiveCheckpoint").transform.Find("Spawn").position;
						} else {
							print("GAME FUCKING OVER MAN!");
						}

						//wait for screen to finish showing
						if (timer <= 0) {
							//create respawn point where the player died
							GameObject respawnPoint = (GameObject)Instantiate(Resources.Load("Prefabs/DeathSpot")) as GameObject;
							respawnPoint.transform.position = new Vector3 (deathSpot.x, -0.3811884f, deathSpot.z);

							timer = Time.time;
						}
						if (Time.time > timer + screenTime) {
							fadeIn = false;
							timer = -1;
						}
					}

					m_timeLeftToReviveFromGhostMode = timeToReviveInGhostMode;
				} else if (alpha > 0) {
					//fade out
					GameObject.FindWithTag("Player").GetComponent<MovementController>().SetCanMove(true);
					alpha -= fadeOutSpeed * Time.deltaTime;
				}
			} else {
				GameObject respawnPoint = (GameObject)Instantiate(Resources.Load("Prefabs/DeathSpot")) as GameObject;
				Transform playerSpot = GameObject.FindWithTag("Player").transform;
				respawnPoint.transform.position = new Vector3 (playerSpot.position.x, -0.3811884f, playerSpot.position.z);
				alpha = -0.1f;
			}

		} else if (!isInGhostMode && alpha != -1) {
			lunaChange = false;
			fadeIn = true;
			timer = -1;
			alpha = -1;
		}
	}

	public void PauseGame() {
		isPaused = true;
		Time.timeScale = 0;
	}
	
	public void UnPauseGame() {
		isPaused = false;
		Time.timeScale = 1;
	}
}
