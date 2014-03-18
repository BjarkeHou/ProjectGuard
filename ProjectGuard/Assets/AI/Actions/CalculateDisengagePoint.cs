using RAIN.Memory;
using RAIN.Navigation.Targets;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class CalculateDisengagePoint : RAINAction
{
    public CalculateDisengagePoint()
    {
        actionName = "CalculateDisengagePoint";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
        Vector3 rotaPoint = ai.WorkingMemory.GetItem<Vector3>("RotationPoint");
        Vector3 targetPos = ai.WorkingMemory.GetItem<GameObject>("detectTarget").transform.position;
        targetPos.y *= 0;
        //targetPos = targetPos
        ai.WorkingMemory.SetItem("returnTarget", targetPos+rotaPoint);
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}