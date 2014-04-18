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
    private int lastHealth;
    private HealthController aiHealthControl;
    private GameObject self;
    private GhostWorldController ghostController;
    private DebugAP gui;

    public CalculateActionPriority()
    {
        actionName = "CalculateActionPriority";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
        self = ai.WorkingMemory.GetItem<GameObject>("self");
        aiHealthControl = self.GetComponent<HealthController>();
        ghostController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GhostWorldController>();
        lastHealth = aiHealthControl.getCurrentHealth();
        gui = self.GetComponent<DebugAP>();
    }

    public override ActionResult Execute(AI ai)
    {
        Dictionary<ActionType, double> actionVector = Enum.GetValues(typeof (ActionType)).Cast<ActionType>().ToDictionary(at => at, at => 1.0);


        actionVector = DecisionParameters(ai, actionVector);

        gui.SetAPVals(actionVector);

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

        ai.WorkingMemory.SetItem("ActionPriority", (int)highestRatedAction);

        return ActionResult.SUCCESS;
    }

    protected virtual Dictionary<ActionType, double> DecisionParameters(AI ai, Dictionary<ActionType, double> actionVector)
    {
        GameObject player = ai.WorkingMemory.GetItem<GameObject>("detectTarget");

        Debug.LogWarning("ActionPriority Calculated");
        
        if (player != null)
        {
            // Player Dependant Parameters:
            //Player Situation
            // - moving
            //TODO PA Moving
            // - standing still
            //TODO PA Standing Still
            // - attacking
            //TODO PA Attacking
            // - Player Health
            //TODO PA Player Health
            // - Cannot detect
            //TODO PA Cannot Detect

            // - distance from player
            float distToPlayer = Vector3.Distance(self.transform.position, player.transform.position);
            float playerAtkRng = player.GetComponent<EquipmentController>().GetWeaponRange();
            float selfAtkRng = self.GetComponent<EquipmentController>().GetWeaponRange();

            if (distToPlayer <= selfAtkRng && distToPlayer > playerAtkRng)
            {
                InSelfAttackRange(actionVector);
            }
            else if (distToPlayer > selfAtkRng && distToPlayer <= playerAtkRng)
            {
                InPlayerAttackRange(actionVector);
            }
            else if (distToPlayer <= selfAtkRng && distToPlayer <= playerAtkRng)
            {
                InBothAttackRange(actionVector);
            }
            else
            {
                OutsideBothAttackRange(actionVector);
            }
        }
        // - cant see player
        else
        {
            CannotSeePlayer(actionVector);
        }
        // AI situation:

        

        // - current self health
        float AIHealthPerc = ((float)aiHealthControl.getCurrentHealth())/((float)aiHealthControl.getMaxHealth());
        if (AIHealthPerc >= 0.75)
        {
            HighHealth(actionVector);
        }
        else if (AIHealthPerc < 0.25)
        {
            MediumHealth(actionVector);
        }
        else
        {
            LowHealth(actionVector);
        }

        // - is at origin postion
        //TODO at origin

        // - Sword rebounded last swing
        //TODO sword rebound

        // - took damage
        if (lastHealth != aiHealthControl.getCurrentHealth())
        {
            TookDamage(actionVector);
        }

        // - In Ghost World
        if (ghostController.deathTransition > 0.5)
        {
            InGhostWorld(actionVector);
        }

        // - Luna-blasted
        if (self.GetComponent<MovementController>().IsLunaBlasted())
        {
            LunaBlasted(actionVector);
        }
        //Debug.LogWarning(actionVector[ActionType.Engage]);


        lastHealth = aiHealthControl.getCurrentHealth();

        //foreach (ActionType at in actionVector.Keys.ToList())
        //{
        //    actionVector[at] = Math.Log(actionVector[at], 2);
        //}
        //Debug.LogWarning(actionVector[ActionType.Engage]);

        //throw new NotImplementedException();
        return actionVector;
    }

    protected virtual void InGhostWorld(Dictionary<ActionType, double> actionVector)
    {
        actionVector[ActionType.Engage] *= 0;
        actionVector[ActionType.Dodge] *= 0;
        actionVector[ActionType.Attack] *= 0;
        actionVector[ActionType.Search] *= 0;
        actionVector[ActionType.StandStill] *= 1;
        actionVector[ActionType.Wander] *= 16;
        actionVector[ActionType.Return] *= 32;
    }

    protected virtual void LunaBlasted(Dictionary<ActionType, double> actionVector)
    {
        actionVector[ActionType.Engage] *= 0;
        actionVector[ActionType.Dodge] *= 0;
        actionVector[ActionType.Attack] /= 8;
        actionVector[ActionType.Search] *= 0;
        actionVector[ActionType.StandStill] *= 4;
        actionVector[ActionType.Wander] *= 32;
        actionVector[ActionType.Return] *= 0;
    }

    protected virtual void TookDamage(Dictionary<ActionType, double> actionVector)
    {
        actionVector[ActionType.Engage] /= 1.5;
        actionVector[ActionType.Dodge] *= 8;
        actionVector[ActionType.Attack] *= 1;
        actionVector[ActionType.Search] *= 1;
        actionVector[ActionType.StandStill] /= 8;
        actionVector[ActionType.Wander] /= 8;
        actionVector[ActionType.Return] *= 1;
    }

    protected virtual void OutsideBothAttackRange(Dictionary<ActionType, double> actionVector)
    {
        actionVector[ActionType.Engage] *= 4;
        actionVector[ActionType.Dodge] /= 2;
        actionVector[ActionType.Attack] /= 8;
        actionVector[ActionType.Search] *= 0;
        actionVector[ActionType.StandStill] /= 2;
        actionVector[ActionType.Wander] /= 8;
        actionVector[ActionType.Return] *= 1;
    }

    protected virtual void InBothAttackRange(Dictionary<ActionType, double> actionVector)
    {
        actionVector[ActionType.Engage] /= 8;
        actionVector[ActionType.Dodge] *= 1.5;
        actionVector[ActionType.Attack] *= 2;
        actionVector[ActionType.Search] *= 0;
        actionVector[ActionType.StandStill] /= 1.5;
        actionVector[ActionType.Wander] /= 8;
        actionVector[ActionType.Return] /= 2;
    }

    protected virtual void InPlayerAttackRange(Dictionary<ActionType, double> actionVector)
    {
        actionVector[ActionType.Engage] *= 2;
        actionVector[ActionType.Dodge] *= 4;
        actionVector[ActionType.Attack] /= 8;
        actionVector[ActionType.Search] *= 0;
        actionVector[ActionType.StandStill] /= 4;
        actionVector[ActionType.Wander] /= 8;
        actionVector[ActionType.Return] /= 2;
    }

    protected virtual void InSelfAttackRange(Dictionary<ActionType, double> actionVector)
    {
        actionVector[ActionType.Engage] /= 8;
        actionVector[ActionType.Dodge] /= 4;
        actionVector[ActionType.Attack] *= 4;
        actionVector[ActionType.Search] *= 0;
        actionVector[ActionType.StandStill] *= 1.5;
        actionVector[ActionType.Wander] /= 8;
        actionVector[ActionType.Return] /= 8;
    }

    protected virtual void LowHealth(Dictionary<ActionType, double> actionVector)
    {
        actionVector[ActionType.Engage] /= 1.5;
        actionVector[ActionType.Dodge] *= 2;
        actionVector[ActionType.Attack] *= 1.5;
        actionVector[ActionType.Search] *= 1;
        actionVector[ActionType.StandStill] *= 1;
        actionVector[ActionType.Wander] *= 1;
        actionVector[ActionType.Return] *= 1;
    }

    protected virtual void MediumHealth(Dictionary<ActionType, double> actionVector)
    {
        actionVector[ActionType.Engage] *= 1.5;
        actionVector[ActionType.Dodge] *= 1;
        actionVector[ActionType.Attack] *= 2;
        actionVector[ActionType.Search] *= 1;
        actionVector[ActionType.StandStill] /= 1.5;
        actionVector[ActionType.Wander] *= 1;
        actionVector[ActionType.Return] /= 1.5;
    }

    protected virtual void HighHealth(Dictionary<ActionType, double> actionVector)
    {
        actionVector[ActionType.Engage] *= 2;
        actionVector[ActionType.Dodge] /= 1.5;
        actionVector[ActionType.Attack] *= 4;
        actionVector[ActionType.Search] *= 1.5;
        actionVector[ActionType.StandStill] /= 2;
        actionVector[ActionType.Wander] *= 1;
        actionVector[ActionType.Return] /= 2;
    }

    protected virtual void CannotSeePlayer(Dictionary<ActionType, double> actionVector)
    {
        actionVector[ActionType.Engage] *= 0;
        actionVector[ActionType.Dodge] *= 0;
        actionVector[ActionType.Attack] *= 0;
        actionVector[ActionType.Search] *= 4;
        actionVector[ActionType.StandStill] *= 2;
        actionVector[ActionType.Wander] *= 1.5;
        actionVector[ActionType.Return] *= 2;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}

public enum ActionType {Engage = 0, Dodge = 1, Attack = 2, Search = 3, StandStill = 4, Wander = 5, Return = 6}