using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NanoBot : MonoBehaviour
{
    private float orientation;
    public Transform playerSpawn;
    public float avoidDistance;
    public float lookahead;
    public int reproductionTime;
    public int attackDamage;
    public int attackSpeed;
    public int signalSearchingTime;
    public int speed;
    public int rotation;
    public int life;
    private int actualLife;
    public int lifeEarnByEating;

    public int lifeLossByReproduction;
    public int lifeForReproduction;
    private bool signalDetection = false;
    private bool pregnant = false;
    private bool hasGoal = false;
    private bool inCombat = false;
    private GameObject target = null;
    private Vector2 targetPos;
    private Rigidbody2D rb;
    private Signal signal;
    private List<Collider2D> colliders;
    private DecisionTreeNode decision;
    // Start is called before the first frame update
    void Start()
    {
        actualLife = life;
        rb = GetComponent<Rigidbody2D>();
        decision = GetComponent<DetectionDecision>();
        signal = GetComponent<Signal>();
        pregnant = false;
        hasGoal = false;
        inCombat = false;
        target = null;
        colliders = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(actualLife >= lifeForReproduction && !pregnant){
            StartCoroutine(Reproduction());
        }
        Action action = (Action)decision.MakeDecision();

        action.DoIt();

        //Debug.Log(action);
    }

    public Vector2 AsVector(){
        return new Vector2(Mathf.Cos(orientation), Mathf.Sin(orientation));
    }

    public bool HasGoal(){
        colliders.AddRange(Physics2D.OverlapCircleAll(rb.position, lookahead)); 
        colliders.FindAll(c => c != null && c.gameObject.layer != gameObject.layer); //= System.Array.Find(colliders.ToArray(), c => c != null && c.gameObject.layer != gameObject.layer);
        if(colliders != null){
            return true;
        }else{
            target = null;
            targetPos = Vector2.zero;
            return false;
        }
    }

    public bool IsInCombat(){
        return inCombat;
    }

    public void InCombat(bool c){
        inCombat = c;
    }

    public GameObject GetTarget(){
        return target;
    }

    public float GetOrientation(){
        return orientation;
    }

    public void SetOrientation(float o){
        orientation = o;
    }

    public void SetTarget(GameObject t){
        target = t;
    }

    public Vector2 GetTargetPos(){
        return targetPos;
    }

    public void SetTargetPos(Vector2 v){
        targetPos = v;
    }

    public void DetectSignal(){
        if(!signalDetection){
            signalDetection = true;
            StartCoroutine(NoSignal());
        }
    }

    public List<Collider2D> GetColliders(){
        return colliders;
    }

    public bool GetSignalDetection(){
        return signalDetection;
    }

    private IEnumerator NoSignal(){
        yield return new WaitForSeconds(signalSearchingTime);
        signalDetection = false;
    }

    public int GetActualLife(){
        return actualLife;
    }

    public void SetChildStats(){
        actualLife = life;
        signalDetection = false;
        pregnant = false;
        hasGoal = false;
        target = null;
    }

    private IEnumerator Reproduction(){
        pregnant = true;
        yield return new WaitForSeconds(reproductionTime);
        actualLife -= lifeLossByReproduction;
        pregnant = false;
        GameObject copy = Instantiate(gameObject, new Vector3(playerSpawn.position.x, playerSpawn.position.y, 0) , Quaternion.Euler(0, 0,  Random.Range(0, 360f)));
        copy.GetComponent<NanoBot>().SetChildStats();
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == Constants.FOOD_LAYER){
            colliders.Clear();
            actualLife += lifeEarnByEating;
            target = null;
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);

            if(!signal.isSignaling()){
                signal.enabled = true;
                signal.radius = 3;
                signal.SetCenter(transform.position);
            
            }
        }

        if(((collision.gameObject.layer == Constants.ENEMY_BULLET_LAYER && gameObject.layer == Constants.PLAYER_LAYER) || (((collision.gameObject.layer == Constants.PLAYER_BULLET_LAYER && gameObject.layer == Constants.ENEMY_LAYER))))){
                gameObject.GetComponent<NanoBot>().actualLife -= attackDamage;
                Destroy(collision.gameObject);
                if(actualLife <= 0)
                    Destroy(gameObject);
            }
    }

    

    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rb.position, 
                rb.position + rb.velocity.normalized*lookahead);
    }

}
