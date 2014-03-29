using UnityEngine;
using System.Collections;

public class GhostWorld : MonoBehaviour {

	public Material ghostWorld;
	public float transition;
	public float blueshift;
	public float waveCount;
	public float waveSize;

	// Use this for initialization
	void Start() {
		ghostWorld.SetFloat("_Transition", Mathf.Clamp(transition, 0, 1));
		ghostWorld.SetFloat("_BlueShift", blueshift);
		ghostWorld.SetFloat("_WaveCount", waveCount);
		ghostWorld.SetFloat("_WaveSize", waveSize);
	}
	
	// Update is called once per frame
	void Update() {
		ghostWorld.SetFloat("_Transition", Mathf.Clamp(transition, 0, 1));
		ghostWorld.SetFloat("_BlueShift", blueshift);
		ghostWorld.SetFloat("_WaveCount", waveCount);
		ghostWorld.SetFloat("_WaveSize", waveSize);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		Graphics.Blit(src, dest, ghostWorld);
	}
}
