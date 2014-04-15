using UnityEngine;
using System.Collections.Generic;

public class AttackController : MonoBehaviour
{

	public GameObject playerModel;

	public bool doesDamage;
	public List<GameObject> targetsHit;
	private Animator anim;
	private bool m_inAnAttack = false;
	private GameController game;
	
	public float rangeForAttack;

	// Use this for initialization
	void Start()
	{
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		doesDamage = false;
		targetsHit = new List<GameObject>();
		anim = playerModel.GetComponent<Animator>();
	}

	void Update()
	{
		//TEMP SOLUTION
		ThereIsSpaceForNormalAttack();
	}

	public void DeclareAttack()
	{
		if (!game.isInGhostMode)
		{
			inAnAttack = true;
			//if (ThereIsSpaceForNormalAttack()) {
			anim.SetBool("Attack", true);
			//} else {
			//	anim.SetBool("Stab", true);
			//}
		}
	}

	public void DeclareParry()
	{
		
		anim.SetBool("Parry", true);
	}

	//called from the equiped weapon
	public int Hit(GameObject obj, float damage)
	{
		if (!targetsHit.Contains(obj))
		{
			if (obj.tag != gameObject.tag)
			{ 
				//add enemy to list of hit stuff
				targetsHit.Add(obj);
				print(obj.GetComponent<HealthController>().IsParrying);
				//check if the character is parrying
				if (obj.GetComponent<HealthController>().IsParrying)
				{
					Rebound();
					return 0;
				} else
				{
					//withdraw health
					obj.GetComponent<HealthController>().adjustCurrentHealth(-(int)damage);
					return 1;
				}
			} else
			{
				return 2;
			}
		} else
		{
			return -1;
		}
	}
	//called from the equiped weapon
	public void Rebound()
	{
		//change animation
		anim.SetBool("Rebound", true);
	}
	
	public bool inAnAttack
	{
		get { return m_inAnAttack;}
		set { m_inAnAttack = value;}
	}
	
	private void ThereIsSpaceForNormalAttack()
	{
		//bool returnValue = true;
		
		RaycastHit hit;
		
		if (Physics.Raycast(this.transform.position, this.transform.right, out hit, rangeForAttack))
		{
			if (hit.transform.gameObject.layer == LayerMask.NameToLayer("LightMap"))
			{
				//returnValue = false;

				//TEMP SOLUTION
				anim.SetBool("Stab", true);
			}
		} else
		{
			anim.SetBool("Stab", false);
		}
	}
}
