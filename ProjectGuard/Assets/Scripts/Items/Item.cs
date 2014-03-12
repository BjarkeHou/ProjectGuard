using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
		public float maxPickupRange;
		protected Transform holdPoint;
		protected bool m_equipped = false;
		
		public bool equipped {
				get { return m_equipped;}
				set { 
						m_equipped = value;
						this.collider.enabled = !value;
				}
		}
}
