using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeekBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;
    private float orientation;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = GetComponent<ObstacleAvoidance>().speed;
        orientation = GetComponent<Nanobot>().orientation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetSteering(){

        orientation = NewOrientation(orientation, rb.velocity);
        rb.rotation = orientation * 180 / Mathf.PI; 

    }

    public float NewOrientation(float current, Vector2 velocity){
        if(velocity.magnitude > 0)
            return Mathf.Atan2(velocity.y, velocity.x);
        else
            return current;
    }
}
