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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.layer != transform.parent.gameObject.layer && collision.gameObject.layer != 6){
            
            busy = true;

            Vector2 target = (Vector2)collision.gameObject.transform.position - rb.position;

            if(target.magnitude > 2 || collision.gameObject.layer == 7){
                movment.speed = 5;
                rb.velocity = target.normalized * movment.speed;
                movment.GetSeekBehaviour().GetSteering();
            }else
                movment.speed = 0;

            if(collision.gameObject.layer == GetComponentInParent<Attack>().target)
                GetComponentInParent<Attack>().Shoot(collision.gameObject);
            
            GetComponentInParent<Nanobot>().SetGoal(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        busy = false;
        movment.SetOrientation(movment.GetSeekBehaviour().NewOrientation(movment.GetOrientation(), rb.velocity));
    }
}
