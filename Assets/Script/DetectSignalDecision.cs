using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSignalDecision : Decision
{
    // Start is called before the first frame update    
    void Start(){
        SetTrueNode(GetComponent<Seek>());
        SetFalseNode(GetComponent<Wander>());
    }
    public override bool TestValue()
    {
        if(GetComponent<NanoBot>().GetSignalDetection())
            return true;
        else
            return false;
    }
}
