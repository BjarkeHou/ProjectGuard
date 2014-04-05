using UnityEditor;
using UnityEngine;
using System.Collections;

public class ImportLevelTexture {

	static Texture2D inputTexture;
	enum WallTypes {
		WALL,
		CORNER_INNER,
		CORNER_OUTER,
		ERROR}
	;
	static WallTypes wallType;
	static int wallRot;


	[MenuItem ("Assets/Import as Level Texture")]
	static void GenerateLevelFromTexture() {
		ClearLevel();
		inputTexture = null;
		inputTexture = Selection.activeObject as Texture2D;
		if (inputTexture == null) { 
			Debug.Log("Selected object is not a texture");
			return;
		}
		BuildLevel(inputTexture.GetPixels());
	}

	static void ClearLevel() {
		object[] obj = GameObject.FindSceneObjectsOfType(typeof(GameObject));
		foreach (object o in obj) {
			GameObject g = (GameObject)o;
			string tempName = g.name;
			if (tempName.IndexOf("_") != -1) {
				if (tempName.Substring(0, tempName.IndexOf("_")) == "Wall") {
					Undo.DestroyObjectImmediate(g);
				}
			} else {
				Debug.Log(tempName);
			}
		}
	}

	static void BuildLevel(Color[] pix) {
		for (int y = 0; y < inputTexture.width; y ++) {
			for (int x = 0; x < inputTexture.height; x ++) {

				int num = inputTexture.height * y + x;
				if (pix [num] == new Color (0, 0, 1)) {
					wallType = DetermineType(x, y);
					//Debug.Log(wallType);
					wallRot = DetermineRotation(x, y);
					//Debug.Log(wallRot);
					PlaceWall(x, y, wallType, wallRot);
				}
			}
		}
	}

	static void PlaceWall(int xPos, int zPos, WallTypes type, int rot) {
		Quaternion rotation = Quaternion.Euler(new Vector3 (-90, 90 * rot, 0));
		string prefab = "Empty";
		if (type == WallTypes.WALL) {
			prefab = "Wall_" + Random.Range(1, 3) + "_Temp";
		} else if (type == WallTypes.CORNER_INNER) {
			prefab = "Wall_CornerInner_Temp";
		} else if (type == WallTypes.CORNER_OUTER) {
			prefab = "Wall_CornerOuter_Temp";
		}

		Object wall = Object.Instantiate(Resources.Load("Prefabs/Environment/" + prefab), new Vector3 (xPos, 0, zPos), rotation);
		Undo.RegisterCreatedObjectUndo(wall, "Undo Level insert");
	}

	static WallTypes DetermineType(int xPos, int yPos) {
		Color[] tempBlock = inputTexture.GetPixels(xPos - 1, yPos - 1, 3, 3);

		int value = 0;
		for (int i = 0; i < tempBlock.Length; i ++) {
			if (tempBlock [i] == Color.black || tempBlock [i] == Color.blue) {
				value += i;
			}
		}

		if (value == 12) {
			return WallTypes.WALL;

		} else {
			if (value == 8) {
				if (tempBlock [0] == Color.white) {
					return WallTypes.CORNER_OUTER;
				} else {
					return WallTypes.CORNER_INNER;
				}
			} else if (value == 10) {
				if (tempBlock [2] == Color.white) {
					return WallTypes.CORNER_OUTER;
				} else {
					return WallTypes.CORNER_INNER;
				}
			} else if (value == 14) {
				if (tempBlock [6] == Color.white) {
					return WallTypes.CORNER_OUTER;
				} else {
					return WallTypes.CORNER_INNER;
				}
			} else if (value == 16) {
				if (tempBlock [8] == Color.white) {
					return WallTypes.CORNER_OUTER;
				} else {
					return WallTypes.CORNER_INNER;
				}
			}
			Debug.Log("Error!");
			return WallTypes.ERROR;
		}
	}

	static int DetermineRotation(int xPos, int yPos) {
		Color[] tempBlock = inputTexture.GetPixels(xPos - 1, yPos - 1, 3, 3);

		switch (wallType) {
		case WallTypes.WALL:
			if (tempBlock [1] == Color.black) {
				if (tempBlock [3] == Color.red) {
					return 0;
				} else {
					return 2;
				}
			} else {
				if (tempBlock [1] == Color.red) {
					return 3;
				} else {
					return 1;
				} 
			}
		
		case WallTypes.CORNER_INNER:
			if (tempBlock [0] == Color.red) {
				return 0;
			} else if (tempBlock [2] == Color.red) {
				return 3;
			} else if (tempBlock [6] == Color.red) {
				return 1;
			} else if (tempBlock [8] == Color.red) {
				return 2;
			}
			break;
		
		case WallTypes.CORNER_OUTER:
			if (tempBlock [0] == Color.white) {
				return 0;
			} else if (tempBlock [2] == Color.white) {
				return 3;
			} else if (tempBlock [6] == Color.white) {
				return 1;
			} else if (tempBlock [8] == Color.white) {
				return 2;
			}
			break;
		}
		return -1;
	}
}
