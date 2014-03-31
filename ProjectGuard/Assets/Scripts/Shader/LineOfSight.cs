using UnityEngine;
using System.Collections;

public class LineOfSight : MonoBehaviour {

	private RenderTexture lightMap;
	private GameObject lightCam; 
	public Material matLos;
	public GameObject player;
	public float sightRange;
	private Light sightLight;
	private Light[] lights;

	void Start() {
		lightMap = new RenderTexture (Screen.width, Screen.height, 24);
		lightMap.name = "lightMap";

		//setup the second camera
		lightCam = new GameObject ();
		lightCam.name = name + "_LightMapClone";
		lightCam.AddComponent<Camera>();
		lightCam.camera.enabled = false;
		lightCam.camera.targetTexture = lightMap;
		lightCam.camera.cullingMask = (1 << LayerMask.NameToLayer("LightMap"));

		//instantiate LoS light on the player
		sightLight = player.AddComponent<Light>();
		sightLight.color = new Color (0, 1, 0);
		sightLight.range = sightRange;
		sightLight.intensity = 8;
		sightLight.cullingMask = (1 << LayerMask.NameToLayer("LightMap"));
		sightLight.shadows = LightShadows.Hard;

		matLos.SetTexture("_LightTex", lightMap);
	}

	void OnPreRender() {
		//Get a list of lights
		lights = FindObjectsOfType(typeof(Light)) as Light[];
		
		//disable all lights except the LoS light
		foreach (Light light in lights) {
			light.enabled = false;
		}
		sightLight.enabled = true;
		
		//move the second camera and render the line of sight info
		lightCam.transform.position = transform.position;
		lightCam.transform.rotation = transform.rotation;
		lightCam.camera.Render();

		//enable all lights and disable the LoS light
		foreach (Light light in lights) {
			light.enabled = true;
		}
		sightLight.enabled = false;
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		Graphics.Blit(src, dest, matLos);
	}
}