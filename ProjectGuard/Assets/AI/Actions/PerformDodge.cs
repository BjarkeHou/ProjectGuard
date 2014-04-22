using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class PerformDodge : RAINAction
{
    private MovementController mCon;

    public PerformDodge()
    {
        actionName = "PerformDodge";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
        /*if (mCon == null)
        {
            mCon = ai.WorkingMemory.GetItem<GameObject>("self").GetComponent<MovementController>();
        }*/


        //mCon.Dodge(0,0); //TODO: check whether dodge direction is worth implementing
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}