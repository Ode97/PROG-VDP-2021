using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionDecision : Decision
{
    
    // Start is called before the first frame update
    void Start(){
        SetTrueNode(GetComponent<IsEnemyDecision>());
        SetFalseNode(GetComponent<DetectSignalDecision>());
    }
    public override bool TestValue()
    {
        if(GetComponent<NanoBot>().HasGoal())
            return true;
        else
            return false;
    }
}
