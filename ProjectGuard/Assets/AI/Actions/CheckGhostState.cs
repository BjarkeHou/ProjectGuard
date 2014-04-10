using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class CheckGhostState : RAINAction
{
    private GameObject gameManager;
    private GhostWorldController ghostControl;
    public CheckGhostState()
    {
        actionName = "CheckGhostState";
    }

    public override void Start(AI ai)
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        ghostControl = gameManager.GetComponent<GhostWorldController>();
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
        bool ghostState = ghostControl.deathTransition > 0.5;

        ai.WorkingMemory.SetItem("ghostworld", ghostState);

        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}