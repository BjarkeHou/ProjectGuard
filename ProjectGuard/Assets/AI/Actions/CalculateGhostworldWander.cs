using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;
using RAIN.Utility;

[RAINAction]
public class CalculateGhostworldWander : RAINAction
{
    public const int SCALE = 2;

    public CalculateGhostworldWander()
    {
        actionName = "CalculateGhostworldWander";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {

        float degree = Random.Range(0f, 2 * Mathf.PI);

        Vector3 vec = new Vector3(Mathf.Cos(degree), 0, Mathf.Sin(degree)) * SCALE;

        Vector3 init = ai.WorkingMemory.GetItem<Vector3>("initPosition");

        ai.WorkingMemory.SetItem("moveTarget", (init+vec));

        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}