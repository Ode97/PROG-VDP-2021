using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision : DecisionTreeNode
{
    private DecisionTreeNode trueNode;
    private DecisionTreeNode falseNode;

    public abstract bool TestValue();

    private DecisionTreeNode GetBranch(){
        if(TestValue()){
            /*if(gameObject.layer == 3)
                Debug.Log(trueNode);*/
            return trueNode;
        }else{
            /*if(gameObject.layer == 3)
                Debug.Log(falseNode);*/
            return falseNode;
        }
    }
    public override DecisionTreeNode MakeDecision()
    {
        //DecisionTreeNode branch = GetBranch();

        return GetBranch().MakeDecision();
    }

    public void SetTrueNode(DecisionTreeNode node){
        trueNode = node;
    }

    public void SetFalseNode(DecisionTreeNode node){
        falseNode = node;
    }

}
