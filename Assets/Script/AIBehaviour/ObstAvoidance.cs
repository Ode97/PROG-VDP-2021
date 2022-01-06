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
        Vector2 t = character.GetTargetPos();
        Vector2 pos = (Vector2)rb.transform.position;
        rb.velocity = t - pos;
        character.SetOrientation(NewOrientation(character.GetOrientation(), rb.velocity));
        rb.rotation = character.GetOrientation() * 180 / Mathf.PI;
        if(rb.velocity.magnitude == 0){
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + 2, transform.rotation.w); 
        }
        /*float rot = character.GetOrientation() * 180 / Mathf.PI;
        rb.rotation = Mathf.Lerp(rb.rotation, rot, 1);*/

    }

    public float NewOrientation(float current, Vector2 velocity){
        if(velocity.magnitude > 0)
            return Mathf.Atan2(velocity.y, velocity.x);
        else
            return current;
    }
}
