using UnityEngine;
using System.Collections;

public class PlayerLook : MonoBehaviour
{
	public GameObject camObj;
	private Vector3 mousePos;
	private float cameraDistance;
	private Vector3 worldPos;
	public float rotationSpeed;
	
	private bool playerCanRotate;

	void Start ()
	{
		playerCanRotate = true;
		UpdateCameraDistance ();
	}

	void FixedUpdate ()
	{ 
		if (playerCanRotate) {
			mousePos = Input.mousePosition;
			worldPos = camObj.GetComponent<Camera>().ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, cameraDistance));
		}
		//Lerp towards decired rotation
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(worldPos -transform.position), Time.deltaTime *rotationSpeed);
		//Freeze x and z rotation
		transform.rotation = Quaternion.Euler (new Vector3 (0, transform.rotation.eulerAngles.y, 0));
	}	
	
	public void UpdateCameraDistance ()
	{
		cameraDistance = camObj.transform.position.y - transform.position.y;
	}
	
	public void SetPlayerCanRotate (bool value)
	{
		playerCanRotate = value;
	}
}
