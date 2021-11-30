using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : Action
{
    private Rigidbody2D rb;
    private float speed;
    private float orientation = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = GetComponent<NanoBot>().speed;
    }

    public override void DoIt(){

        Vector2 v = GetComponent<NanoBot>().GetTargetPos() - (Vector2)transform.position;

        rb.velocity = v.normalized * GetComponent<NanoBot>().speed;
        orientation = NewOrientation(orientation, rb.velocity);
        rb.rotation = orientation * 180 / Mathf.PI; 
        /*float rot = orientation * 180 / Mathf.PI;
        rb.rotation = Mathf.Lerp(rb.rotation, rot, 1);*/

    }

    public float NewOrientation(float current, Vector2 velocity){
        if(velocity.magnitude > 0)
            return Mathf.Atan2(velocity.y, velocity.x);
        else
            return current;
    }
}
