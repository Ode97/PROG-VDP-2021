using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicWander : MonoBehaviour
{
    public Nanobot character;
    public float rotation;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Nanobot>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void GetSteering(){

        //rb.velocity = speed * character.AsVector();

        rb.rotation += RandomBinomial() * rotation;

        character.orientation = rb.rotation * Mathf.PI / 180;

        //character.position = rb.position;
    }

    private float RandomBinomial(){
        return (Random.Range(0, 1f) - Random.Range(0, 1f));
    }

}
