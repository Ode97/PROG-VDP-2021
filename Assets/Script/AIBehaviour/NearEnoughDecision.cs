using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearEnoughDecision : Decision
{
    // Start is called before the first frame update
    void Start(){

        SetTrueNode(GetComponent<Combat>());
        SetFalseNode(GetComponent<Seek>());
    
    }
    public override bool TestValue()
    {
        if(Vector2.Distance(GetComponent<NanoBot>().GetTarget().transform.position, transform.position) < 1){
            GetComponent<NanoBot>().InCombat(true);
            return true;
        }else
            return false;
    }
}