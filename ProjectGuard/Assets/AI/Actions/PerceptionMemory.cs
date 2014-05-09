using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class PerceptionMemory : RAINAction
{
    public const float timerMax = 10.0f;
    private float missingTimer = timerMax;

    private Vector3 lastKnownPosition;

    public PerceptionMemory()
    {
        actionName = "PerceptionMemory";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
        GameObject player = ai.WorkingMemory.GetItem<GameObject>("detectTarget");
        if (player == null) //player not found
        {
            missingTimer -= Time.deltaTime;
        }
        else //player found
        {
            if(missingTimer != timerMax) missingTimer = timerMax;
            lastKnownPosition = player.transform.position;

        }
        ai.WorkingMemory.SetItem("detectTimerOut", missingTimer <= 0);
        ai.WorkingMemory.SetItem("lastKnownPos", lastKnownPosition);

        //check whether the player is in dialogue
        GameController GCont = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        ai.WorkingMemory.SetItem("dialogue",GCont.isInDialogMode);

        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}