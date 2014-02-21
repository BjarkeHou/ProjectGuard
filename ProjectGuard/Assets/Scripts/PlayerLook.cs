using UnityEngine;
using System.Collections;

public class PlayerLook : MonoBehaviour
{
		public GameObject player;
		private Vector3 mousePos;
		private int cameraDistance;
		private Vector3 worldPos;
	
		void Start ()
		{
				cameraDistance = (int)(camera.transform.position.y - player.transform.position.y);
		}
	
		void FixedUpdate ()
		{ 
				mousePos = Input.mousePosition;
				worldPos = camera.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, cameraDistance));
			
				player.transform.LookAt (worldPos);
				player.transform.rotation = Quaternion.Euler (new Vector3 (0, player.transform.rotation.eulerAngles.y, 0));
		}	
}
