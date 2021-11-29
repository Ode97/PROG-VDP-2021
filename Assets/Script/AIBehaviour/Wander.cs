using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Action
{
    // Start is called before the first frame update
    private NanoBot character;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<NanoBot>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void DoIt(){
        rb.velocity = character.speed * character.AsVector();
        float rot = rb.rotation + RandomBinomial() * character.rotation;
        rb.rotation = Mathf.Lerp(rb.rotation, rot, 10 * Time.deltaTime);
        //rb.rotation += RandomBinomial() * character.rotation;
        /*float angle = Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);*/
        character.SetOrientation(rb.rotation * Mathf.PI / 180);
    }

    private float RandomBinomial(){
        return (Random.Range(0, 1f) - Random.Range(0, 1f));
    }
}
