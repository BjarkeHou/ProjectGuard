using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;
using Random = UnityEngine.Random;

[RAINAction]
public class DisengagePoint : RAINAction
{
    public const int SCALE = 2;

    public DisengagePoint()
    {
        actionName = "DisengagePoint";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
        GameObject self = ai.WorkingMemory.GetItem<GameObject>("self");
        GameObject target = ai.WorkingMemory.GetItem<GameObject>("detectTarget");
        Vector3 vecToPlayer = target.transform.position - self.transform.position;
        vecToPlayer.Normalize();
        vecToPlayer.y *= 0;
        vecToPlayer *= -1;

        //float degree = Random.Range(0f, 2*Mathf.PI);

        //Vector3 vec = new Vector3(Mathf.Cos(degree), 0, Mathf.Sin(degree))*SCALE;

        ai.WorkingMemory.SetItem("moveTarget", self.transform.position+(vecToPlayer*SCALE));

        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}