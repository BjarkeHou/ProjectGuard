using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {

	private PlayerWillController pWill;

	private Texture2D barTex;
	private Texture2D spentTex;
	private Texture2D bgTex;
	private Texture2D borderTex;
	public Color barCol;
	public Color spentCol;
	public Color bgCol;
	public Color borderCol;

	// Use this for initialization
	void Start() {
		pWill = GetComponent<PlayerWillController>();

		barTex = new Texture2D (1, 1);
		barTex.SetPixel(0, 0, barCol);
		barTex.Apply();

		spentTex = new Texture2D (1, 1);
		spentTex.SetPixel(0, 0, spentCol);
		spentTex.Apply();

		bgTex = new Texture2D (1, 1);
		bgTex.SetPixel(0, 0, bgCol);
		bgTex.Apply();

		borderTex = new Texture2D (1, 1);
		borderTex.SetPixel(0, 0, borderCol);
		borderTex.Apply();
	}

	void OnGUI() {

		//Will Bar
		Vector2 dimension = new Vector2 (Screen.width / 3, 50);
		Vector2 anchor = new Vector2 ((Screen.width / 2) - (dimension.x / 2), Screen.height - dimension.y - 50);

		//BG
		GUI.DrawTexture(new Rect (anchor.x, anchor.y, dimension.x, dimension.y), bgTex);

		//Spent
		float width = dimension.x * Mathf.InverseLerp(0, pWill.MaxWill, pWill.CurMaxWill);

		GUI.DrawTexture(new Rect (anchor.x, anchor.y, width, dimension.y), spentTex);

		//Bar
		width = dimension.x * Mathf.InverseLerp(0, pWill.MaxWill, pWill.CurWill);
		GUI.DrawTexture(new Rect (anchor.x, anchor.y, width, dimension.y), barTex);

		//Border
		GUI.DrawTexture(new Rect (anchor.x, anchor.y, dimension.x, 1), borderTex);
		GUI.DrawTexture(new Rect (anchor.x + dimension.x, anchor.y, 1, dimension.y), borderTex);
		GUI.DrawTexture(new Rect (anchor.x + dimension.x, anchor.y + dimension.y, -dimension.x, 1), borderTex);
		GUI.DrawTexture(new Rect (anchor.x, anchor.y + dimension.y, 1, -dimension.y), borderTex);
	}
}
