using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackDecision : Decision
{
    // Start is called before the first frame update
    void Start(){

        SetTrueNode(GetComponent<Combat>());
        SetFalseNode(GetComponent<NearEnoughDecision>());
    
    }
    public override bool TestValue()
    {
        if(GetComponent<NanoBot>().rangedAttack){
            GetComponent<NanoBot>().InCombat(true);
            return true;
        }else
            return false;
    }
}
