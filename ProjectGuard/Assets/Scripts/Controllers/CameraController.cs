using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
		public float distanceAbovePlayer = 8;
		public float minDistanceAbovePlayer = 6.5f;
		public float maxDistanceAbovePlayer = 12;
		public float distanceBehindPlayer = 4;
		private Vector3 deciredPos;
		private Vector3 attackPos;
		public float lerpSpeed;
		public float scrollSpeed;
	
		private GameController game;
		// Use this for initialization
		void Start ()
		{
				game = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
				deciredPos = this.transform.position;
				this.transform.rotation = Quaternion.Euler (70, 0, 0);
		}

		// Update is called once per frame
		void FixedUpdate ()
		{
				if (!game.isPaused && !game.isInDialogMode) {
						if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
								distanceAbovePlayer += Input.GetAxis ("Mouse ScrollWheel") * scrollSpeed;
			
								if (distanceAbovePlayer < minDistanceAbovePlayer)
										distanceAbovePlayer = minDistanceAbovePlayer;
								if (distanceAbovePlayer > maxDistanceAbovePlayer)
										distanceAbovePlayer = maxDistanceAbovePlayer;
						}
						UpdatePosition ();
				}
		}
	
		void UpdatePosition ()
		{
				Transform t = GameObject.FindGameObjectWithTag ("Player").transform;
				deciredPos = new Vector3 (t.position.x, t.position.y + distanceAbovePlayer, t.position.z - distanceBehindPlayer);
				float lerpY = Mathf.Lerp (transform.position.y, deciredPos.y, Time.deltaTime * scrollSpeed);
				this.transform.position = new Vector3 (deciredPos.x, lerpY, deciredPos.z);
		}
}
