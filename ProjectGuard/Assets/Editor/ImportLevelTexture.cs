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
		if (GameObject.Find ("GeneratedLevel") != null) {
			Undo.DestroyObjectImmediate(GameObject.Find ("GeneratedLevel"));
		}
		new GameObject("GeneratedLevel");
		if (GameObject.Find ("Assets") == null) {
			new GameObject("Assets");
		}
	}

	static void BuildLevel(Color[] pix) {
		for (int y = 0; y < inputTexture.width; y ++) {
			for (int x = 0; x < inputTexture.height; x ++) {
				int num = inputTexture.height * y + x;

				//Build walls
				if (pix [num] == Color.blue) {
					wallType = DetermineType(x, y);
					wallRot = DetermineRotation(x, y);

					PlaceWall(x, y, wallType, wallRot);
				}

				//place lights
				if (pix [num] == Color.green) {
					PlaceLight(x, y);
				}
			}
		}
		//Place floor
		PlaceFloor();
	}

	static void PlaceWall(int xPos, int zPos, WallTypes type, int rot) {
		Quaternion rotation = Quaternion.Euler(new Vector3 (0, 90 * rot, 0));
		string prefab = "Empty";
		if (type == WallTypes.WALL) {
			prefab = "Wall_" + Random.Range(1, 4);
		} else if (type == WallTypes.CORNER_INNER) {
			prefab = "Wall_CornerInner";
		} else if (type == WallTypes.CORNER_OUTER) {
			prefab = "Wall_CornerOuter";
		} else {
			prefab = "ErrorCube";
		}

		GameObject wall = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/Environment/" + prefab) as GameObject);
		Undo.RegisterCreatedObjectUndo(wall, "Undo Level insert");
		if (GameObject.Find ("GeneratedLevel") != null) {
			wall.transform.parent = GameObject.Find ("GeneratedLevel").transform;
		}
		wall.transform.position = new Vector3(xPos, 0, zPos);
		wall.transform.rotation = rotation;
	}

	static void PlaceFloor() {
		for (int y = 0; y <= Mathf.Ceil(inputTexture.height/50); y ++) {
			for (int x = 0; x <= Mathf.Ceil(inputTexture.width/50); x++) {
				GameObject floor = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/Environment/Floor") as GameObject);
				Undo.RegisterCreatedObjectUndo(floor, "Undo Level insert");
				if (GameObject.Find ("GeneratedLevel") != null) {
					floor.transform.parent = GameObject.Find ("GeneratedLevel").transform;
				}
				floor.transform.position = new Vector3(12.5f + x * 50, -0.5f,12.5f + y * 50);
			}
		}
	}

	static void PlaceLight(int xPos, int zPos) {
		Color[] tempBlock = inputTexture.GetPixels(xPos - 1, zPos - 1, 3, 3);
		Quaternion rotation = Quaternion.identity;
		Vector2 offset = new Vector2(0, 0);

		if (tempBlock[1] == Color.blue) {
			rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
			if (tempBlock[0] == Color.black && tempBlock[2] != Color.black) {
				offset = new Vector3(0.37f, 0);
			} else if (tempBlock[0] != Color.black && tempBlock[2] == Color.black) {
				offset = new Vector3(-0.37f, 0);
			}
		} else if (tempBlock[3] == Color.blue) {
			rotation = Quaternion.Euler(new Vector3(-90, 90, 0));
			if (tempBlock[0] == Color.black && tempBlock[6] != Color.black) {
				offset = new Vector3(0, 0.37f);
			} else if (tempBlock[0] != Color.black && tempBlock[6] == Color.black) {
				offset = new Vector3(0, -0.37f);
			}
		} else if (tempBlock[5] == Color.blue) {
			rotation = Quaternion.Euler(new Vector3(-90, -90, 0));
			if (tempBlock[2] == Color.black && tempBlock[8] != Color.black) {
				offset = new Vector3(0, 0.37f);
			} else if (tempBlock[2] != Color.black && tempBlock[8] == Color.black) {
				offset = new Vector3(0, -0.37f);
			}
		} else if (tempBlock[7] == Color.blue) {
			rotation = Quaternion.Euler(new Vector3(-90, 180, 0));
			if (tempBlock[6] == Color.black && tempBlock[8] != Color.black) {
				offset = new Vector3(0.37f, 0);
			} else if (tempBlock[6] != Color.black && tempBlock[8] == Color.black) {
				offset = new Vector3(-0.37f, 0);
			}
		}

		GameObject torch = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/Environment/Torch_WithMount") as GameObject);
		Undo.RegisterCreatedObjectUndo(torch, "Undo Level insert");
		torch.transform.parent = GameObject.Find ("GeneratedLevel").transform;
		torch.transform.position = new Vector3(xPos + offset.x, 0, zPos + offset.y);
		torch.transform.rotation = rotation;
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
				if (tempBlock [3] != Color.white) {
					return 0;
				} else {
					return 2;
				}
			} else {
				if (tempBlock [1] != Color.white) {
					return 3;
				} else {
					return 1;
				} 
			}
		
		case WallTypes.CORNER_INNER:
			if (tempBlock [0] != Color.white) {
				return 0;
			} else if (tempBlock [2] != Color.white) {
				return 3;
			} else if (tempBlock [6] != Color.white) {
				return 1;
			} else if (tempBlock [8] != Color.white) {
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
