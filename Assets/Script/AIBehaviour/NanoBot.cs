using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NanoBot : MonoBehaviour
{
    private float orientation;
    [Range(0,360)]
    public float viewAngle;
    public float avoidDistance;
    public float lookahead;
    public int reproductionTime;
    public bool rangedAttack;
    public bool splashAttack;
    public bool leaveBombAfterDeath;
    public bool firstAttackDealsMoreDMG;
    public float chanceOfCrit;
    public int attackDamage;
    public int attackSpeed;
    public int signalSearchingTime;
    public float speed;
    public float rotation;
    public int life;
    public int lifeLostPerSec;
    private int actualLife;
    public int lifeEarnByEating;
    public int lifeLossByReproduction;
    public int lifeForReproduction;
    public int foodSpawnAfterDeath;
    private bool signalDetection = false;
    private bool pregnant = false;
    private bool hasGoal = false;
    private bool inCombat = false;
    private bool timer = false;
    private GameObject target = null;
    private Vector2 targetPos;
    private Rigidbody2D rb;
    private Signal signal;
    private List<Collider2D> colliders;
    private DecisionTreeNode decision;
    public GameObject food;
    private Vision visionUpgrade;
    private Movment movmentUpgrade;
    private Attack attackUpgrade;
    private Armor armorUpgrade;
    private Special specialUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        actualLife = life/2;
        rb = GetComponent<Rigidbody2D>();
        decision = GetComponent<DetectionDecision>();
        signal = GetComponent<Signal>();
        pregnant = false;
        hasGoal = false;
        inCombat = false;
        signalDetection = false;
        target = null;
        colliders = new List<Collider2D>();

        visionUpgrade = GetComponentInChildren<Vision>();
        movmentUpgrade = GetComponentInChildren<Movment>();
        attackUpgrade = GetComponentInChildren<Attack>();
        armorUpgrade = GetComponentInChildren<Armor>();
        specialUpgrade = GetComponentInChildren<Special>();
    }

    // Update is called once per frame
    void Update()
    {
        if(actualLife <= 0){
            for(int i = 0; i < foodSpawnAfterDeath; i++)
                Instantiate(food, new Vector3(transform.position.x + Mathf.Sign(Random.Range(-1f, 1f)) * 0.5f, transform.position.y + Mathf.Sign(Random.Range(-1f, 1f)) * 0.5f, 0), Quaternion.identity);
    
            Destroy(gameObject);
        }

        if(actualLife >= lifeForReproduction && !pregnant){
            StartCoroutine(Reproduction());
        }


        if(!timer)
            StartCoroutine(LostEnergyPerSec());

        if(signalDetection && Vector2.Distance(rb.position,  targetPos) < 0.5)
            signalDetection = false;

        Action action = (Action)decision.MakeDecision();

        action.DoIt();

        //Debug.Log(action);
    }

    private IEnumerator LostEnergyPerSec(){
        timer = true;
        yield return new WaitForSeconds(1);
        timer = false;
        actualLife -= lifeLostPerSec;
    }

    public Vector2 AsVector(){
        return new Vector2(Mathf.Cos(orientation), Mathf.Sin(orientation));
    }

    public Vector2 DirFromAngle(float angle){
        angle += transform.eulerAngles.z;
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    public bool HasGoal(){
        colliders.Clear();
        colliders.AddRange(Physics2D.OverlapCircleAll(rb.position, lookahead)); 
        colliders = colliders.FindAll(c => c != null && c.gameObject.layer != gameObject.layer && (Vector2.Angle(transform.right, (c.transform.position - transform.position).normalized) < viewAngle/2 || c.gameObject.layer == Constants.OBSTACLE_LAYER));
        if(colliders.Count >= 1){
            return true;
        }else{
            //target = null;
            //targetPos = Vector2.zero;
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
        actualLife = life/2;
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
        GameObject copy = Instantiate(gameObject, new Vector3(transform.position.x + Mathf.Sign(Random.Range(-1f, 1f)) * 0.5f, transform.position.y + Mathf.Sign(Random.Range(-1f, 1f)) * 0.5f, 0) , Quaternion.Euler(0, 0,  Random.Range(0, 360f)));
        copy.GetComponent<NanoBot>().SetChildStats();
        //Debug.Log(actualLife + " " + copy.GetComponent<NanoBot>().GetActualLife());
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == Constants.FOOD_LAYER){
            colliders.Clear();
            actualLife += lifeEarnByEating;
            if(actualLife > life)
                actualLife = life;

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
                actualLife -= collision.gameObject.GetComponent<Bullet>().atkDmg;
                //Debug.Log(gameObject.name + " " + actualLife);
                if(!collision.gameObject.GetComponent<Bullet>().IsSplashBullet())
                    Destroy(collision.gameObject);
            }
    }

    

    /*void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rb.position, 
                rb.position + rb.velocity.normalized*lookahead);

        Vector2 viewA = DirFromAngle(-viewAngle/2);
        Vector2 viewB = DirFromAngle(viewAngle/2);

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + viewA * lookahead);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + viewB * lookahead);
    }*/

}
