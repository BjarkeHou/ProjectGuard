using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
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
				Transform t = GameObject.FindGameObjectWithTag ("Player").transform;
				myPos = new Vector3 (t.position.x, t.position.y + 6, t.position.z - 2);
				this.transform.position = myPos;
		}
}
