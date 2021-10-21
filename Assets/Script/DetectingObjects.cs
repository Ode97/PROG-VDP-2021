using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingObjects : MonoBehaviour
{
    public bool busy = false;
    private ObstacleAvoidance movment;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        movment = GetComponentInParent<ObstacleAvoidance>();
        rb = GetComponentInParent<Rigidbody2D>();
        busy = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.layer != transform.parent.gameObject.layer && collision.gameObject.layer != 6 && collision.gameObject.layer != 10 && collision.gameObject.layer != 11){
            
            busy = true;

            Vector2 target = (Vector2)collision.gameObject.transform.position - rb.position;

            rb.velocity = target.normalized * movment.speed;
                
            if(collision.gameObject.layer == GetComponentInParent<Attack>().target) {
                movment.speed = 0;
                GetComponentInParent<Attack>().Shoot(collision.gameObject);
                GetComponentInParent<Nanobot>().SetGoal(true);
            }
            
            movment.GetSeekBehaviour().GetSteering();
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        busy = false;
        if(collision.gameObject.layer == GetComponentInParent<Attack>().target){
            movment.speed = 5;
            rb.velocity = movment.speed*rb.velocity;
            movment.SetOrientation(movment.GetSeekBehaviour().NewOrientation(movment.GetOrientation(), rb.velocity));
            GetComponentInParent<Nanobot>().SetGoal(false);
        }
    }
}
