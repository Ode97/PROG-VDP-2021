using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEnemyDecision : Decision
{
    private int targetLayer;
    void Start(){

        if(gameObject.layer == Constants.PLAYER_LAYER)
            targetLayer = Constants.ENEMY_LAYER;
        else
        {
            targetLayer = Constants.PLAYER_LAYER;
        }

        SetTrueNode(GetComponent<RangedAttackDecision>());
        SetFalseNode(GetComponent<IsFoodDecision>());
    }
    public override bool TestValue()
    {
        Collider2D col = GetComponent<NanoBot>().GetColliders().Find(c => c != null && c.gameObject.layer == targetLayer);

        if(col != null){
            GetComponent<NanoBot>().SetTarget(col.gameObject);
            GetComponent<NanoBot>().SetTargetPos(col.gameObject.transform.position);
            if(!GetComponent<Signal>().isSignaling()){
                GetComponent<Signal>().enabled = true;
                GetComponent<Signal>().radius = 3;
                GetComponent<Signal>().SetCenter();
            }

            return true;
        }else{
            GetComponent<NanoBot>().InCombat(false);
            return false;
        }
    }
}
