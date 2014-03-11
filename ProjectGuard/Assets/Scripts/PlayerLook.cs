using UnityEngine;
using System.Collections;

public class PlayerLook : MonoBehaviour
{
	public GameObject camObj;
	private Vector2 mousePos;
	private float cameraDistance;
	private Vector3 worldPos;
	public float rotationSpeed;
	
	private bool playerCanRotate;

	void Start ()
	{
		playerCanRotate = true;
	}

	void FixedUpdate ()
	{
		//convert mouseposition to relatives from 0 to 1
		mousePos = new Vector2(Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x), Mathf.InverseLerp(0, Screen.height, Input.mousePosition.y));

		if (playerCanRotate) {
			//raycast trough viewspace and into the world
			Ray mousePoint = camObj.GetComponent<Camera>().ViewportPointToRay (new Vector3 (mousePos.x, mousePos.y, 0));
			RaycastHit hit;
			Physics.Raycast(mousePoint, out hit);
			worldPos = hit.point;
		}
		//Lerp towards decired rotation
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(worldPos -transform.position), Time.deltaTime *rotationSpeed);
		//Freeze x and z rotation
		transform.rotation = Quaternion.Euler (new Vector3 (0, transform.rotation.eulerAngles.y, 0));
	}
	
	public void SetPlayerCanRotate (bool value)
	{
		playerCanRotate = value;
	}
}
