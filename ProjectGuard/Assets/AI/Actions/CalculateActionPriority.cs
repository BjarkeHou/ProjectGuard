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
        Dictionary<ActionType, int> actionVector = Enum.GetValues(typeof (ActionType)).Cast<ActionType>().ToDictionary(at => at, at => 0);

        //actionVector = DecisionParameters(ai, actionVector);

        ActionType highestRatedAction = ActionType.StandStill;
        int highestRating = 0;
        foreach (KeyValuePair<ActionType, int> atVal in actionVector)
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

    protected virtual Dictionary<ActionType, int> DecisionParameters(AI ai, Dictionary<ActionType, int> actionVector)
    {
        // Player:
        // - moving
        // - standing still
        // - attacking
        // - Cannot detect

        // AI:
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