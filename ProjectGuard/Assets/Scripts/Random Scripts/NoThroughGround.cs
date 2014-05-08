using UnityEngine;
using System.Collections;

public class NoThroughGround : MonoBehaviour
{

	private float y;

	// Use this for initialization
	void Start()
	{
		y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (transform.position.y < y)
		{
			transform.position = new Vector3(transform.position.x, y, transform.position.z);
		} else if (transform.position.y > y + 0.1f)
		{
			transform.position = new Vector3(transform.position.x, y, transform.position.z);
		}
	}
}
