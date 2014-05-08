using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class DecideAction : RAINAction
{
    public DecideAction()
    {
        actionName = "DecideAction";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
        //Random rand = new Random();

        int num = Random.Range(1, 5);

        ai.WorkingMemory.SetItem("ghostAction", num);

        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}