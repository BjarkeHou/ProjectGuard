using UnityEditor;
using UnityEngine;
using System.Collections;

public class ImportLevelTexture {

	static Texture2D inputTexture = Selection.activeObject as Texture2D;
	enum WallTypes {WALL, CORNER_INNER, CORNER_OUTER, ERROR};
	static WallTypes wallType;


	[MenuItem ("Assets/Import as Level Texture")]
	static void GenerateLevelFromTexture() {
		if (inputTexture == null) { 
			Debug.Log("Selected object is not a texture");
			return;
		}
		
		Color[] texturePixels = inputTexture.GetPixels();
		int[] wallPlacement = ReadPixels(texturePixels);
	}

	static int[] ReadPixels(Color[] pix) {
		int[] temp = new int[pix.Length];

		for (int y = 0; y < inputTexture.width; y += 3) {
			for (int x = 0; x < inputTexture.height; x += 3) {

				int num = inputTexture.height * y + x;
				if (pix[num] == new Color(0, 0, 1)) {
					wallType = DetermineType(x, y);
					Debug.Log(wallType);
					temp[num] = DetermineRotation(x, y);
					Debug.Log(temp[num]);
				} else {
					temp[num] = 0;
				}
			}
		}

		return temp;
	}

	static WallTypes DetermineType(int xPos, int yPos) {
		Color[] tempBlock = inputTexture.GetPixels(xPos - 1, yPos - 1, 3, 3);

		//is it a wall?
		int value = 0;
		for (int i = 0; i < tempBlock.Length; i ++) {
			if (tempBlock[i] == Color.black || tempBlock[i] == Color.blue) {
				value += i;
			}
		}

		if (value == 12) {
			return WallTypes.WALL;

		} else {
			if (value == 8) {
				if (tempBlock[0] == Color.white) {
					return WallTypes.CORNER_OUTER;
				} else {
					return WallTypes.CORNER_INNER;
				}
			} else if (value == 10) {
				if (tempBlock[2] == Color.white) {
					return WallTypes.CORNER_OUTER;
				} else {
					return WallTypes.CORNER_INNER;
				}
			} else if (value == 14) {
				if (tempBlock[6] == Color.white) {
					return WallTypes.CORNER_OUTER;
				} else {
					return WallTypes.CORNER_INNER;
				}
			} else if (value == 16) {
				if (tempBlock[8] == Color.white) {
					return WallTypes.CORNER_OUTER;
				} else {
					return WallTypes.CORNER_INNER;
				}
			}
			Debug.Log("Error!");
			return WallTypes.ERROR;
		}
	}

	static int DetermineRotation (int xPos, int yPos) {
		Color[] tempBlock = inputTexture.GetPixels(xPos - 1, yPos - 1, 3, 3);

		switch (wallType) {
		case WallTypes.WALL:
			Debug.Log("bleh");
			return 1;
		
		case WallTypes.CORNER_INNER:
			Debug.Log("bleh");
			return 1;
		
		case WallTypes.CORNER_OUTER:
			Debug.Log("bleh");
			return 1;
		}

		return -1;
	}
}
