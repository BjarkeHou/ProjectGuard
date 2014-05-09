using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class InMeleeRange : RAINAction
{
    public InMeleeRange()
    {
        actionName = "InMeleeRange";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
        GameObject self = ai.WorkingMemory.GetItem<GameObject>("self");
        GameObject target = ai.WorkingMemory.GetItem<GameObject>("detectTarget");

        var dist = Vector3.Distance(self.transform.position, target.transform.position);

        //Debug.Log(dist);

        ai.WorkingMemory.SetItem("InMeleeRange", (dist < 2.5));

        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}