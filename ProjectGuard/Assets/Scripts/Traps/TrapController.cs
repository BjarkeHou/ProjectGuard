using UnityEngine;

public class TrapController : MonoBehaviour
{
	
	public GUISkin gui_Skin;
	
	private SpearBehaviour[] spearBehav;
	private BearBehaviour[] bearBehav;
	
	private GameController game;
	private bool showTrapPrompt = false;
	private bool m_canBeDisarmed = false;
	
	public bool canBeDisarmed
	{
		get{ return m_canBeDisarmed; }
		set { m_canBeDisarmed = value; }
	}

	// Use this for initialization
	void Start()
	{
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		spearBehav = transform.GetComponentsInChildren<SpearBehaviour>();
		bearBehav = transform.GetComponentsInChildren<BearBehaviour>();
	}

	public int Hit(GameObject obj, float damage)
	{
		if (obj.tag != gameObject.tag)
		{
			//check if the character is parrying
			if (obj.GetComponent<HealthController>().IsParrying)
			{
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
	}
	
	void OnMouseEnter()
	{
		if (game.isInGhostMode && !game.isPaused && !game.isInDialogMode)
		{
			showTrapPrompt = true;
			canBeDisarmed = true;
		}
	}
	
	void OnMouseExit()
	{
		showTrapPrompt = false;
		canBeDisarmed = false;
	}
	
	void OnGUI()
	{
		if (showTrapPrompt && game.isInGhostMode && !game.isPaused && !game.isInDialogMode)
		{
			GUI.skin = gui_Skin;
			
			// Vis mouseover på trap
			GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y, 300, 200), "Press E to disarm trap");
		}
	}

	public void Trigger()
	{
		foreach (SpearBehaviour spear in spearBehav)
		{
			spear.Trigger();
		}
		foreach (BearBehaviour bear in bearBehav)
		{
			bear.Trigger();
		}
	}


}
