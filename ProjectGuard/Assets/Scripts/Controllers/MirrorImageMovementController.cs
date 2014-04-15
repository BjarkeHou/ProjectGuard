using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RAIN.Core;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
	class MirrorImageMovementController : MovementController
	{
	    private AIRig ai;
	    public bool LunaBlasted = false;
	    public float LunaBlastTimer;
        public const float LunaBlastTimerMax = 5f;

	    new void Start()
        {
            base.Start();
	        LunaBlastTimer = 0f;
            ai = gameObject.GetComponentInChildren<AIRig>();
            if (ai == null) Debug.Log("AIRig not found in " + this);
        }

	    void Update()
	    {
	        bool canMove = playerCanMove;

            ai.AI.WorkingMemory.SetItem("canMove", canMove);

            //Luna Blast
	        if (LunaBlasted)
	        {
	            LunaBlastTimer -= Time.deltaTime;
	            if (LunaBlastTimer <= 0)
	            {
	                LunaBlastTimer = 0;
	                LunaBlasted = false;
	            }
	        }
            

        }

	    public void HitWithLunaBlast()
	    {
	        LunaBlastTimer = LunaBlastTimerMax;
	        LunaBlasted = true;
	    }

	    public override bool IsLunaBlasted()
	    {
	        return LunaBlasted;
	    }
	}
}
