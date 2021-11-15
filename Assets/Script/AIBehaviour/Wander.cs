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
        rb.rotation += RandomBinomial() * character.rotation;
        character.SetOrientation(rb.rotation * Mathf.PI / 180);
    }

    private float RandomBinomial(){
        return (Random.Range(0, 1f) - Random.Range(0, 1f));
    }
}
