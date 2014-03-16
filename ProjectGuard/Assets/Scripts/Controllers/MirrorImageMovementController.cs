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

	    new void Start()
        {
            base.Start();
            ai = gameObject.GetComponentInChildren<AIRig>();
            if (ai == null) Debug.Log("AIRig not found in " + this);
        }

	    void Update()
	    {
	        bool canMove = playerCanMove;

            ai.AI.WorkingMemory.SetItem("canMove", canMove);

        }
	}
}
