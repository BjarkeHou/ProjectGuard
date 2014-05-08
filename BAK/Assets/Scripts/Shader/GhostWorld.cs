using UnityEngine;
using System.Collections;

public class GhostWorld : MonoBehaviour {

	private GameController game;

	public Material ghostWorld;
	public float deathTimer;
	public float transition;
	public float blueshift;
	public float waveCount;
	public float waveSize;
	public float overlayAlpha;

	// Use this for initialization
	void Start() {
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		ghostWorld.SetFloat("_Transition", Mathf.Clamp(transition, 0, 1));
		ghostWorld.SetFloat("_DeathTimer", deathTimer);
		ghostWorld.SetFloat("_BlueShift", blueshift);
		ghostWorld.SetFloat("_WaveCount", waveCount);
		ghostWorld.SetFloat("_WaveSize", waveSize);
		ghostWorld.SetFloat("_OverlayAlpha", overlayAlpha);
	}
	
	// Update is called once per frame
	void Update() {
		if (game.isInGhostMode) {
			deathTimer = 1 - Mathf.InverseLerp(0, game.timeToReviveInGhostMode, game.timeLeftToReviveFromGhostMode);
			overlayAlpha = 1.5f - Mathf.InverseLerp(-game.timeToReviveInGhostMode, game.timeToReviveInGhostMode, game.timeLeftToReviveFromGhostMode);
		}

		ghostWorld.SetFloat("_Transition", Mathf.Clamp(transition, 0, 1));
		ghostWorld.SetFloat("_DeathTimer", deathTimer);
		ghostWorld.SetFloat("_BlueShift", blueshift);
		ghostWorld.SetFloat("_WaveCount", waveCount);
		ghostWorld.SetFloat("_WaveSize", waveSize);
		ghostWorld.SetFloat("_OverlayAlpha", overlayAlpha);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		Graphics.Blit(src, dest, ghostWorld);
	}
}
