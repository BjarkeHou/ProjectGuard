using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
		protected Transform holdPoint;
		protected bool equipped = false;

		// Use this for initialization
		void Start ()
		{
				
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		
		public void SetEquipped (bool _equipped)
		{
				equipped = _equipped;
				this.collider.enabled = !_equipped;
		}
}
