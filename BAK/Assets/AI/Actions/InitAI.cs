using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class InitAI : RAINAction
{
    public InitAI()
    {
        actionName = "InitAI";
    }

    public override void Start(AI ai)
    {
        
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
        GameObject self = ai.WorkingMemory.GetItem<GameObject>("self");

        ai.WorkingMemory.SetItem("initPosition",self.transform.position);

        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}