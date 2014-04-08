using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;

[RAINAction]
public class CalculateActionPriority : RAINAction
{


    public CalculateActionPriority()
    {
        actionName = "CalculateActionPriority";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
        Dictionary<ActionType, double> actionVector = Enum.GetValues(typeof (ActionType)).Cast<ActionType>().ToDictionary(at => at, at => 1.0);

        //actionVector = DecisionParameters(ai, actionVector);

        ActionType highestRatedAction = ActionType.StandStill;
        double highestRating = 1;
        foreach (KeyValuePair<ActionType, double> atVal in actionVector)
        {
            if (atVal.Value > highestRating)
            {
                highestRating = atVal.Value;
                highestRatedAction = atVal.Key;
            }
        }


        ai.WorkingMemory.SetItem("ActionPriority", highestRatedAction);

        return ActionResult.SUCCESS;
    }

    protected virtual Dictionary<ActionType, double> DecisionParameters(AI ai, Dictionary<ActionType, double> actionVector)
    {
        // Player:
        // - moving
        // - standing still
        // - attacking
        // - Cannot detect

        // AI:
        // - can see player
        var player = ai.WorkingMemory.GetItem<GameObject>("DetectTarget");
        if (player == null)
        {
            actionVector[ActionType.Search] *= 3;
            actionVector[ActionType.StandStill] *= 1.5;
            actionVector[ActionType.Attack] *= 0;
            actionVector[ActionType.Engage] *= 0;
            actionVector[ActionType.Dodge] *= 0;
        }
        else
        {
            actionVector[ActionType.Engage] *= 2;
            actionVector[ActionType.Attack] *= 1.1;
            actionVector[ActionType.Search] *= 0;
            actionVector[ActionType.StandStill] *= 0.8;
            actionVector[ActionType.Dodge] *= 1;
        }
        // - distance from player
        // - current health
        // - Sword rebounded last swing
        // - taking damage
        // - Luna-blasted
        throw new NotImplementedException();
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}

public enum ActionType {Engage = 0, Dodge = 1, Attack = 2, Search = 3, StandStill = 4}