using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : DecisionTreeNode{
    public abstract void DoIt();

    public override DecisionTreeNode MakeDecision(){
        Debug.Log(this);
        return this;
    }
}
