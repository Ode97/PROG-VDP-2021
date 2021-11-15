using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFrontOfWallDecision : Decision
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();

        SetTrueNode(GetComponent<ObstAvoidance>());
        SetFalseNode(GetComponent<Wander>());
    
    }
    public override bool TestValue()
    {
        RaycastHit2D ray = Physics2D.Raycast(rb.position, rb.velocity.normalized, rb.velocity.normalized.magnitude * GetComponent<NanoBot>().lookahead, 1 << Constants.OBSTACLE_LAYER);
        if(ray.collider != null){
            GetComponent<NanoBot>().SetTargetPos(ray.point + ray.normal * GetComponent<NanoBot>().avoidDistance);
            return true;
        }else
            return false;
    }
}
