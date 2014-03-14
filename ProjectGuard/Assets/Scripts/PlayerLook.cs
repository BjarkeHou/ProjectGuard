using UnityEngine;
using System.Collections;

public class PlayerLook : MonoBehaviour
{
	public GameObject camObj;
	private Vector2 mousePos;
	private float cameraDistance;
	private Vector3 worldPos;
	private Vector3 lockedLookDirection;
	private bool rotationLocked = false;
	public float rotationSpeed;
	
	private bool m_playerCanRotate;

	void Start ()
	{
		playerCanRotate = true;
	}

	void FixedUpdate ()
	{
		RotatePlayer ();
	}
	
	private void RotatePlayer ()
	{
		if (playerCanRotate) {
		
			//convert mouseposition to relatives from 0 to 1
			mousePos = new Vector2 (Mathf.InverseLerp (0, Screen.width, Input.mousePosition.x), Mathf.InverseLerp (0, Screen.height, Input.mousePosition.y));
			
			//raycast trough viewspace and into the world
			Ray mousePoint = camObj.GetComponent<Camera> ().ViewportPointToRay (new Vector3 (mousePos.x, mousePos.y, 0));
			RaycastHit hit;
			Physics.Raycast (mousePoint, out hit);
			worldPos = hit.point;
			lockedLookDirection = worldPos - transform.position;
		}
	
		//Lerp towards decired rotation
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (lockedLookDirection), Time.deltaTime * rotationSpeed);
		//Freeze x and z rotation
		transform.rotation = Quaternion.Euler (new Vector3 (0, transform.rotation.eulerAngles.y, 0));
	}
	
	public bool playerCanRotate {
		get { return m_playerCanRotate;}
		set { m_playerCanRotate = value;}
	}
	
	public void LockPlayerOnDirection (Vector3 _direction)
	{
		playerCanRotate = false;
		lockedLookDirection = _direction.normalized;
	}
}












