using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
		public float distanceAbovePlayer = 8;
		public float minDistanceAbovePlayer = 6.5f;
		public float maxDistanceAbovePlayer = 12;
		public float distanceBehindPlayer = 4;

		private Vector3 myPos;
		// Use this for initialization
		void Start ()
		{
				myPos = this.transform.position;
				this.transform.rotation = Quaternion.Euler (70, 0, 0);
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
						distanceAbovePlayer += Input.GetAxis ("Mouse ScrollWheel");
						
						if (distanceAbovePlayer < minDistanceAbovePlayer)
								distanceAbovePlayer = minDistanceAbovePlayer;
						if (distanceAbovePlayer > maxDistanceAbovePlayer)
								distanceAbovePlayer = maxDistanceAbovePlayer;
								
						GetComponent<PlayerLook> ().UpdateCameraDistance ();
				}
				
				UpdatePosition ();
		}
		
		void UpdatePosition ()
		{
				Transform t = GameObject.FindGameObjectWithTag ("Player").transform;
				myPos = new Vector3 (t.position.x, t.position.y + distanceAbovePlayer, t.position.z - distanceBehindPlayer);
				this.transform.position = myPos;
				this.transform.LookAt (t.position);
		}
}
