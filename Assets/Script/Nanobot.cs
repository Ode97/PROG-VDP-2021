using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nanobot : MonoBehaviour
{
    public float orientation;
    private bool hasGoal = false;
    private GameObject coneOfSight;
    public int energy = 0;
    public int life = 100;
    private Signal signal;

    void Start(){
        signal = GetComponent<Signal>();
    }

    public Vector2 AsVector(){
        return new Vector2(Mathf.Cos(orientation), Mathf.Sin(orientation));
    }

    private IEnumerator Reproduction(){
        yield return new WaitForSeconds(5f);
        GameObject copy = Instantiate(gameObject, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), 0) , Quaternion.Euler(0, 0,  Random.Range(0, 360f)));
        copy.GetComponent<Signal>().LineDrawer.enabled = false;
        copy.GetComponent<Signal>().enabled = false;
        copy.GetComponent<Nanobot>().hasGoal = false;
    }

    void OnCollisionEnter2D(Collision2D collision2D){

        if(collision2D.gameObject.layer == 7 && collision2D.otherCollider.gameObject.layer != 8){

            if(hasGoal)
                hasGoal = false;
            
            energy += 1;
            if(energy == 3){
                StartCoroutine(Reproduction());
                energy = 0;
            }
            Destroy(collision2D.gameObject);
            signal.enabled = true;
            
            if(!signal.isSignaling()){
                signal.radius = 3;
                signal.SetCenter(transform.position);
            }
        }
    }

    public Signal GetSignal(){
        return signal;
    }

    public bool HasGoal(){
        return hasGoal;
    }

    public void SetGoal(bool goal){
        hasGoal = goal;
    }

    private void ConeOfSight(){
        float dotProduct = Vector3.Dot(GetComponent<Rigidbody2D>().velocity.normalized, transform.forward);
        if (dotProduct > 0.1f) 
        {
                
        }
    }
}
