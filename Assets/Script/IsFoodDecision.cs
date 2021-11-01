using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFoodDecision : Decision
{
    // Start is called before the first frame update
    void Start(){
        SetTrueNode(GetComponent<Seek>());
        SetFalseNode(GetComponent<InFrontOfWallDecision>());
    }
    public override bool TestValue()
    { 
        Collider2D col = GetComponent<NanoBot>().GetColliders().Find(c => c != null && c.gameObject.layer == Constants.FOOD_LAYER);
        if(col != null){
            GetComponent<NanoBot>().SetTargetPos(col.transform.position);
            return true;
        }else
            return false;
    }
}
