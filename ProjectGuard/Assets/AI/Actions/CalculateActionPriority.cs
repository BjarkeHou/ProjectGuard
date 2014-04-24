using System;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;
using Random = UnityEngine.Random;

[RAINAction]
public class CalculateActionPriority : RAINAction
{
    private int lastHealth;
    private HealthController aiHealthControl;
    private GameObject self;
    private GhostWorldController ghostController;
    private DebugAP gui;
    private Random rand;
    protected float selfAtkRng;

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
        var eCont = self.GetComponent<EquipmentController>();
        if(eCont != null) selfAtkRng = eCont.GetWeaponRange();
        //rand = new Random();
    }

    public override ActionResult Execute(AI ai)
    {
        Dictionary<ActionType, double> actionVector = Enum.GetValues(typeof (ActionType)).Cast<ActionType>().ToDictionary(at => at, at => 1.0);


        actionVector = DecisionParameters(ai, actionVector);

        gui.SetAPVals(actionVector);

        double max = actionVector.Values.Max();
        List<KeyValuePair<ActionType, double>> highest = actionVector.Where(e => Math.Abs(e.Value - max) < 0.001).ToList();
        if (!highest.Any())
        {
            throw new ArgumentOutOfRangeException();
        }
        if (highest.Count() == 1)
        {
            ai.WorkingMemory.SetItem("ActionPriority", (int)highest[0].Key);
        }
        else
        {
            int rVal = Random.Range(0, highest.Count);
            ai.WorkingMemory.SetItem("ActionPriority", (int)highest[rVal].Key);

        }
        
        return ActionResult.SUCCESS;
    }

    protected virtual Dictionary<ActionType, double> DecisionParameters(AI ai, Dictionary<ActionType, double> actionVector)
    {
        GameObject player = ai.WorkingMemory.GetItem<GameObject>("detectTarget");

        StringBuilder sitB = new StringBuilder();

        //Debug.LogWarning("ActionPriority Calculated");
        
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

            if (distToPlayer <= selfAtkRng && distToPlayer > playerAtkRng)
            {
                InSelfAttackRange(actionVector);
                sitB.Append("In Range of Self Attack\n");
            }
            else if (distToPlayer > selfAtkRng && distToPlayer <= playerAtkRng)
            {
                InPlayerAttackRange(actionVector);
                sitB.Append("In Range of Player Attack \n");
            }
            else if (distToPlayer <= selfAtkRng && distToPlayer <= playerAtkRng)
            {
                InBothAttackRange(actionVector);
                sitB.Append("In Range of Both Attack \n");
            }
            else
            {
                OutsideBothAttackRange(actionVector);
                sitB.Append("Outside Attack Ranges \n");
            }
        }
        // - cant see player
        else
        {
            CannotSeePlayer(actionVector);
            sitB.Append("Cannot See Player \n");
        }


        #region AI situation

        // - current self health
        float AIHealthPerc = ((float)aiHealthControl.getCurrentHealth())/((float)aiHealthControl.getMaxHealth());
        Debug.Log(string.Format("AI Health: {0}",AIHealthPerc));
        if (AIHealthPerc >= 0.70f)
        {
            HighHealth(actionVector);
            sitB.Append("High Health\n");
        }
        else if (AIHealthPerc > 0.30f)
        {
            MediumHealth(actionVector);
            sitB.Append("Medium Health\n");
        }
        else
        {
            LowHealth(actionVector);
            sitB.Append("Low Health\n");
        }

        // - is at origin postion
        Vector3 initPos = ai.WorkingMemory.GetItem<Vector3>("initPosition");
        float dist = Vector3.Distance(self.transform.position, initPos);

        if (dist < 2.5)
        {
            AtOrigin(actionVector);
            sitB.Append("At Origin Position\n");
        }

        // - Sword rebounded last swing
        //TODO sword rebound

        // - took damage
        if (lastHealth != aiHealthControl.getCurrentHealth())
        {
            TookDamage(actionVector);
            sitB.Append("Taking Damage\n");
        }

        // - In Ghost World
        if (ghostController.deathTransition > 0.5)
        {
            InGhostWorld(actionVector);
            sitB.Append("Ghost World\n");
        }

        // - Luna-blasted
        if (self.GetComponent<MovementController>().IsLunaBlasted())
        {
            LunaBlasted(actionVector);
            sitB.Append("Luna Blasted\n");
        }
        #endregion

        //Debug.LogWarning(actionVector[ActionType.Engage]);


        lastHealth = aiHealthControl.getCurrentHealth();

        gui.SetCurrentParameters(sitB.ToString());

        /*foreach (ActionType at in actionVector.Keys.ToList())
        {
            Debug.Log(actionVector[at]);
        }*/
        //Debug.LogWarning(actionVector[ActionType.Engage]);

        //throw new NotImplementedException();
        return actionVector;
    }

    protected virtual void AtOrigin(Dictionary<ActionType, double> actionVector)
    {
        actionVector[ActionType.Engage] *= 1.5;
        actionVector[ActionType.Dodge] *= 1;
        actionVector[ActionType.Attack] *= 1;
        actionVector[ActionType.Search] *= 1.5;
        actionVector[ActionType.StandStill] /= 1.5;
        actionVector[ActionType.Wander] *= 4;
        actionVector[ActionType.Return] *= 0;
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