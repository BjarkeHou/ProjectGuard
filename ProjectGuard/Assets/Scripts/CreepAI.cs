using UnityEngine;
using System.Collections;

public class CreepAI : MonoBehaviour
{

		public int moveSpeed = 1;
		public int rotationSpeed = 1;
		public int minDistance = 2;

		private Transform target;
		private Transform myTransform;

		void Awake ()
		{
				myTransform = this.transform;
		}

		// Use this for initialization
		void Start ()
		{
				GameObject gameobject = GameObject.FindGameObjectWithTag ("Player");
				target = gameobject.transform;
		}
	
		// Update is called once per frame
		void Update ()
		{
				LookTowardsPlayer ();
				
				if (Vector3.Distance (target.position, myTransform.position) > (float)minDistance) {
						MoveTowardsPlayer ();
				}
		}
		
		void LookTowardsPlayer ()
		{
				myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);
				myTransform.rotation = Quaternion.Euler (new Vector3 (0, myTransform.rotation.eulerAngles.y, 0));
		}
		
		void MoveTowardsPlayer ()
		{
				myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
}
