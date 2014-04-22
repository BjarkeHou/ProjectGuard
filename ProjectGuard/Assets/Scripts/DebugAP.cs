using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using System.Collections;

public class DebugAP : MonoBehaviour {
    private double EngageVal;
    private double DodgeVal;
    private double AttackVal;
    private double SearchVal;
    private double StandStillVal;
    private double WanderVal;
    private double ReturnVal;
    private string CurrentParameters;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 200, 126), "Current AP");

        GUI.Label(new Rect(12, 32, 180, 20), string.Format("Engage: {0}", Math.Round(EngageVal,4)));
        GUI.Label(new Rect(12, 44, 180, 20), string.Format("Dodge: {0}", Math.Round(DodgeVal,4)));
        GUI.Label(new Rect(12, 56, 180, 20), string.Format("Attack: {0}", Math.Round(AttackVal,4)));
        GUI.Label(new Rect(12, 68, 180, 20), string.Format("Search: {0}", Math.Round(SearchVal,4)));
        GUI.Label(new Rect(12, 80, 180, 20), string.Format("StandStill: {0}", Math.Round(StandStillVal,4)));
        GUI.Label(new Rect(12, 92, 180, 20), string.Format("Wander: {0}", Math.Round(WanderVal,4)));
        GUI.Label(new Rect(12, 104, 180, 20), string.Format("Return: {0}", Math.Round(ReturnVal, 4)));

        GUI.Label(new Rect(800,12,150,200), CurrentParameters);
    }

    public void SetAPVals(Dictionary<ActionType,double> apList)
    {
        //Debug.LogWarning(apList[ActionType.Engage]);

        EngageVal = apList[ActionType.Engage];
        DodgeVal = apList[ActionType.Dodge];
        AttackVal = apList[ActionType.Attack];
        SearchVal = apList[ActionType.Search];
        StandStillVal = apList[ActionType.StandStill];
        WanderVal = apList[ActionType.Wander];
        ReturnVal = apList[ActionType.Return];
    }

    public void SetCurrentParameters(string parameterString)
    {
        CurrentParameters = parameterString;
    }
}
