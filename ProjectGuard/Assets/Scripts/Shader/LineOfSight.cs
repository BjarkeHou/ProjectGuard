using UnityEngine;
using System.Collections;

public class LineOfSight : MonoBehaviour {

	private RenderTexture lightMap;
	private Camera lightCam; 
	public Material matLos;
	public GameObject player;
	public float sightRange;
	private Light sightLight;
	private Light[] lights;

	void Start() {
		lightMap = new RenderTexture (Screen.width, Screen.height, 24);
		lightMap.name = "lightMap";

		//setup the second camera
		lightCam = transform.Find("Camera").camera;
		lightCam.targetTexture = lightMap;
		lightCam.cullingMask = (1 << LayerMask.NameToLayer("LightMap"));

		//instantiate LoS light on the player
		sightLight = player.AddComponent<Light>();
		sightLight.color = new Color (0, 1, 0);
		sightLight.range = sightRange;
		sightLight.intensity = 8;
		sightLight.cullingMask = (1 << LayerMask.NameToLayer("LightMap"));
		sightLight.shadows = LightShadows.Hard;

		matLos.SetTexture("_LightTex", lightMap);
	}

	void Update() { 
		//Get a list of all the lights
		lights = FindObjectsOfType(typeof(Light)) as Light[];

		//disable all lights except the LoS light
		foreach (Light light in lights) {
			light.enabled = false;
		}
		sightLight.enabled = true;

		//render the light info
		lightCam.Render();

		//re-enable all lights except the LoS light
		foreach (Light light in lights) {
			light.enabled = true;
		}
		sightLight.enabled = false;
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		Graphics.Blit(src, dest, matLos);
	}
}