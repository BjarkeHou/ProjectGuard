using System.Collections.Generic;
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

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 126), "Current AP");

        GUI.Label(new Rect(12, 32, 80, 20), string.Format("Engage: {0}", EngageVal.ToString()));
        GUI.Label(new Rect(12, 44, 80, 20), string.Format("Dodge: {0}", DodgeVal.ToString()));
        GUI.Label(new Rect(12, 56, 80, 20), string.Format("Attack: {0}", AttackVal.ToString()));
        GUI.Label(new Rect(12, 68, 80, 20), string.Format("Search: {0}", SearchVal.ToString()));
        GUI.Label(new Rect(12, 80, 80, 20), string.Format("StandStill: {0}", StandStillVal.ToString()));
        GUI.Label(new Rect(12, 92, 80, 20), string.Format("Wander: {0}", WanderVal.ToString()));
        GUI.Label(new Rect(12, 104, 80, 20), string.Format("Return: {0}", ReturnVal.ToString()));
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
}
