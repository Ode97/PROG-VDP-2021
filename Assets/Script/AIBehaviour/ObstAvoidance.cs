using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstAvoidance : Action
{
    private Rigidbody2D rb;
    private NanoBot character;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        character = GetComponent<NanoBot>();
    }
    public override void DoIt(){
        rb.velocity = character.GetTargetPos() - (Vector2)rb.transform.position;
        character.SetOrientation(NewOrientation(character.GetOrientation(), rb.velocity));
        rb.rotation = character.GetOrientation() * 180 / Mathf.PI;

    }

    public float NewOrientation(float current, Vector2 velocity){
        if(velocity.magnitude > 0)
            return Mathf.Atan2(velocity.y, velocity.x);
        else
            return current;
    }
}
