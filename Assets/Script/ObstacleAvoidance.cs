using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public float speed = 5;
    public float avoidDistance;
    public float lookahead;
    public LayerMask obstacleLayer;
    private Vector2 target;
    private Nanobot character;
    private Rigidbody2D rb;
    private KinematicSeekBehaviour seekBehaviour;
    private KinematicWander wander;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seekBehaviour = GetComponent<KinematicSeekBehaviour>();
        wander = GetComponent<KinematicWander>();
        character = GetComponent<Nanobot>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!GetComponentInChildren<DetectingObjects>().busy){
            RaycastHit2D ray = Physics2D.Raycast(rb.position, rb.velocity.normalized, rb.velocity.normalized.magnitude * lookahead, obstacleLayer);
            if(ray.collider != null){
                target = ray.point + ray.normal * avoidDistance;
                rb.velocity = target - (Vector2)rb.transform.position;
                seekBehaviour.GetSteering();
            }else{
                rb.velocity = speed * character.AsVector();
                wander.GetSteering();
            }
        }
    }

    public KinematicSeekBehaviour GetSeekBehaviour(){
        return seekBehaviour;
    }

    public KinematicWander GetWanderBehaviour(){
        return wander;
    }

    public float GetOrientation(){
        return character.orientation;
    }

    public void SetOrientation(float o){
        character.orientation = o;
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rb.position, 
                rb.position + rb.velocity.normalized*lookahead);
    }
}
