using UnityEngine;
using System.Collections;

public class GhostText : MonoBehaviour {

	private GhostWorldController gWorld;
	private Material mat;
	public Texture text;

	// Use this for initialization
	void Start() {
		gWorld = GameObject.Find("GameController").GetComponent<GhostWorldController>();
		mat = GetComponent<Renderer>().material;

		mat.mainTexture = text;
		mat.color = new Color (mat.color.r, mat.color.g, mat.color.b, 0);
	}
	
	// Update is called once per frame
	void Update() {
		if (gWorld.deathTransition > 0.5f && gWorld.deathTransition < 1) {
			mat.color = new Color (mat.color.r, mat.color.g, mat.color.b, Mathf.InverseLerp(0.5f, 1, gWorld.deathTransition));
		}
	}
}
