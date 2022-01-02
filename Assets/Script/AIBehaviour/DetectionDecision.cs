using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionDecision : Decision
{
    DetectSignalDecision d;
    // Start is called before the first frame update
    void Start(){
        d = GetComponent<DetectSignalDecision>();
        SetTrueNode(GetComponent<IsEnemyDecision>());
        SetFalseNode(d);
    }
    public override bool TestValue()
    {
        d.SetFalseNode(GetComponent<Wander>());
        if(GetComponent<NanoBot>().HasGoal())
            return true;
        else
            return false;
    }
}
