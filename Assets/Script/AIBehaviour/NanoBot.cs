using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
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
    [Range(0, 1)]
    public float fireArmor;
    [Range(0, 1)]
    public float electricArmor;
    [Range(0, 1)]
    public float acidArmor;
    [Range(0, 1)]
    public float trapArmor;
    public int attackDamage;
    public int attackSpeed;
    public Type typeOfAttk;
    public int signalSearchingTime;
    public float speed;
    public float rotation;
    public int life;
    public int lifeLostPerSec;
    private float actualLife;
    public int lifeEarnByEating;
    public int lifeLossByReproduction;
    public int lifeForReproduction;
    public int foodSpawnAfterDeath;
    private bool signalDetection = false;
    private bool noSignal = false;
    private bool pregnant = false;
    private bool inCombat = false;
    private bool timer = false;
    public bool electricTarget = false;
    private bool onFire = false;
    private int fireTimer = 1;
    private GameObject target = null;
    private Vector2 targetPos;
    private Rigidbody2D rb;
    private Signal signal;
    private List<Collider2D> colliders;
    private DecisionTreeNode decision;
    public GameObject food;
    public GameObject bomb;
    public Effects effects;
    public PhotonView view;
    public GameObject child;
    //private Vision visionUpgrade;
    //private Movment movmentUpgrade;
    //private Attack attackUpgrade;
    //private Armor armorUpgrade;
    //private Special specialUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        if(!PhotonNetwork.IsConnected){
            //GetComponent<PhotonView>().enabled = false;
            Setup();
        }else {
            view = GetComponent<PhotonView>();
            if(view.IsMine){
                Setup();
            }
        }
    }

    private void Setup(){
        actualLife = life/2;
        rb = GetComponent<Rigidbody2D>();
        decision = GetComponent<DetectionDecision>();
        signal = GetComponent<Signal>();
        pregnant = false;
        inCombat = false;
        signalDetection = false;
        target = null;
        colliders = new List<Collider2D>();
        if(firstAttackDealsMoreDMG)
            GetComponent<Combat>().SetFirstShootBonus();
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsConnected && view.IsMine)
            OnlineTask();
        else if(!PhotonNetwork.IsConnected)
            OfflineTask();
    }

    private void OnlineTask(){
        if(actualLife <= 0){
            for(int i = 0; i < foodSpawnAfterDeath; i++){
                PhotonNetwork.Instantiate(food.name, new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f), 0), Quaternion.identity);
            }
            if(leaveBombAfterDeath){
                GameObject b;
                if(PhotonNetwork.IsConnected){
                    b = PhotonNetwork.Instantiate(bomb.name, transform.position, Quaternion.identity);

                    if(gameObject.layer == Constants.PLAYER_LAYER){
                        b.GetComponent<Bomb>().SetTarget(Constants.ENEMY_LAYER);
                    }else{
                        b.GetComponent<Bomb>().SetTarget(Constants.PLAYER_LAYER);
                    }
                }
            }
            //GameManager.instance.death(gameObject.layer);
            PhotonNetwork.Destroy(gameObject);
            
        }
        
        if(actualLife >= lifeForReproduction && !pregnant){
            StartCoroutine(Reproduction());
            //GameManager.instance.newChild(gameObject.layer);
        }

        if(!timer && gameObject.activeInHierarchy)
            StartCoroutine(LostEnergyPerSec());

        if(colliders.Count == 0 && signalDetection && Vector2.Distance(rb.position,  targetPos) < 0.1){
            signalDetection = false;
            noSignal = true;
        }

        Action action = (Action)decision.MakeDecision();

        action.DoIt();
    }

    private void OfflineTask(){
        if(actualLife <= 0){
            for(int i = 0; i < foodSpawnAfterDeath; i++){
                Instantiate(food, new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f), 0), Quaternion.identity);
            }
            if(leaveBombAfterDeath){
                GameObject b;
                b = Instantiate(bomb, transform.position, Quaternion.identity);

                if(gameObject.layer == Constants.PLAYER_LAYER){
                    b.GetComponent<Bomb>().SetTarget(Constants.ENEMY_LAYER);
                }else{
                    b.GetComponent<Bomb>().SetTarget(Constants.PLAYER_LAYER);
                }
            }
            GameManager.instance.death(gameObject.layer);
            Destroy(gameObject);
        }
        
        if(actualLife >= lifeForReproduction && !pregnant){
            StartCoroutine(Reproduction());
            GameManager.instance.newChild(gameObject.layer);
        }

        if(!timer)
            StartCoroutine(LostEnergyPerSec());

        if(signalDetection && Vector2.Distance(rb.position,  targetPos) < 0.3){
            signalDetection = false;
            noSignal = true;
        }

        Action action = (Action)decision.MakeDecision();

        action.DoIt();
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
        if(!noSignal)
            signalDetection = false;
    }

    public float GetActualLife(){
        return actualLife;
    }

    public void SetChildStats(){
        actualLife = life/2;
        signalDetection = false;
        pregnant = false;
        target = null;
    }

    private IEnumerator Reproduction(){
        pregnant = true;
        yield return new WaitForSeconds(reproductionTime);
        actualLife -= lifeLossByReproduction;
        pregnant = false;
        GameObject copy;
        if(PhotonNetwork.IsConnected){
            copy = PhotonNetwork.Instantiate(child.name, new Vector3(transform.position.x + Mathf.Sign(Random.Range(-1f, 1f)) * 0.2f, transform.position.y + Mathf.Sign(Random.Range(-1f, 1f)) * 0.2f, 0) , Quaternion.Euler(0, 0,  /*Random.Range(0, 360f)*/ transform.rotation.z));
        }else{
            copy = Instantiate(child, new Vector3(transform.position.x + Mathf.Sign(Random.Range(-1f, 1f)) * 0.2f, transform.position.y + Mathf.Sign(Random.Range(-1f, 1f)) * 0.2f, 0) , Quaternion.Euler(0, 0,  Random.Range(0, 360f)));
        }

        copy.name = child.name;
        copy.GetComponent<NanoBot>().SetChildStats();
        
    }

    public void ElectricWave(float dmg){    
        electricTarget = true;
        List<Collider2D> targets = new List<Collider2D>();
        targets.AddRange(Physics2D.OverlapCircleAll(rb.position, Constants.ELECTRIC_BULLET_AREA_EFFECT));
        targets = targets.FindAll(c => c != null && c.gameObject.layer == gameObject.layer && c.gameObject != gameObject && !c.gameObject.GetComponent<NanoBot>().electricTarget);
        foreach(Collider2D c in targets){
            GameObject e;
            if(PhotonNetwork.IsConnected){
                e = Instantiate(effects.electricWaveEffect, gameObject.transform.position, Quaternion.identity, c.gameObject.transform);
                send_EffectElectricWave(view.ViewID, c.gameObject.GetComponent<NanoBot>().view.ViewID);
            }else {
                e = Instantiate(effects.electricWaveEffect, gameObject.transform.position, Quaternion.identity, c.gameObject.transform);
            }
            e.transform.localScale = new Vector3(4, 4, 4);        
            c.gameObject.GetComponent<NanoBot>().ApplyDMG(dmg * (1 - electricArmor), Type.Electric);
            c.gameObject.GetComponent<NanoBot>().ElectricWave(dmg);
        }
        StartCoroutine(electricTargetStop());
        
    }

    IEnumerator electricTargetStop(){
        yield return new WaitForSeconds(0.5f);
        electricTarget = false;
    }

    public void FireDmg(float dmg){
        if(!onFire){
            StartCoroutine(Fire(dmg));
            onFire = true;
        }else
            fireTimer = 1;

    }

    private IEnumerator Fire(float dmg){
        GameObject e;
        for(fireTimer = 1; fireTimer <= 3; fireTimer++){
            yield return new WaitForSeconds(1);
            if(PhotonNetwork.IsConnected){
                e = Instantiate(effects.FireBurstEffect, gameObject.transform.position, new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w), gameObject.transform);
                send_EffectFireBurst(view.ViewID);
            }else{
                e = Instantiate(effects.FireBurstEffect, gameObject.transform.position, new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w), gameObject.transform);
            }
            e.transform.localScale = new Vector3(2, 2, 2);
            ApplyDMG(dmg * (1 - fireArmor), Type.Fire);
        }
        onFire = false;
    }

    public void ApplyDMG(float dmg, Type type){
        if(!PhotonNetwork.IsConnected || view.IsMine){
            actualLife -= dmg;
        }
        Debug.Log(gameObject.layer + "ha ricevuto " + dmg + " danni " + type);

        Color color;
        string c;
        if(type == Type.Electric){
            color = Color.yellow;
            c = "e";
        }else if(type == Type.Acid){
            color = Color.blue;
            c = "a";
        }else if(type == Type.Fire){
            color = Color.red;
            c = "f";
        }else{
            color = Color.black;
            c = "t";
        }
        DamagePopUp.Create(transform.position, dmg, color);
        if(PhotonNetwork.IsConnected){
            send_Dmg_PopUp(transform.position.x, transform.position.y, transform.position.z, dmg, c);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        view = GetComponent<PhotonView>();
        if(!PhotonNetwork.IsConnected || (PhotonNetwork.IsConnected && view.IsMine)){
            if(collision.gameObject.layer == Constants.FOOD_LAYER){
                if(colliders != null)
                    colliders.Clear();

                actualLife += lifeEarnByEating;
                if(actualLife > life)
                    actualLife = life;

                target = null;
                collision.gameObject.SetActive(false);
                if(PhotonNetwork.IsConnected){
                    if(collision.gameObject.GetComponent<PhotonView>().IsMine)
                        PhotonNetwork.Destroy(collision.gameObject);
                    else{
                        send_RPC_destroy(collision.gameObject.GetComponent<PhotonView>().ViewID);
                    }
                }else{
                    Destroy(collision.gameObject);
                }

                if(signal != null && !signal.isSignaling()){
                    signal.enabled = true;
                    signal.radius = 3;
                    signal.SetCenter();
                
                }
            }else if(collision.gameObject.layer == Constants.TRAP_LAYER){
                ApplyDMG(Constants.TRAP_DMG * (1 - trapArmor), Type.Trap);
            }

            if(((collision.gameObject.layer == Constants.ENEMY_BULLET_LAYER && gameObject.layer == Constants.PLAYER_LAYER) || (((collision.gameObject.layer == Constants.PLAYER_BULLET_LAYER && gameObject.layer == Constants.ENEMY_LAYER))))){
                
                Debug.Log(gameObject.layer + " " + collision.gameObject.layer + " " + collision.gameObject.GetComponent<Bullet>().type + " " + collision.gameObject.GetComponent<Bullet>().atkDmg);
                float dmg;
                dmg = collision.gameObject.GetComponent<Bullet>().atkDmg;
                if(collision.gameObject.GetComponent<Bullet>().type == Type.Fire){
                    if(PhotonNetwork.IsConnected){
                        Instantiate(effects.FireEffect, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
                        send_EffectFireDmg(view.ViewID);
                    }else
                        Instantiate(effects.FireEffect, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                    ApplyDMG(dmg * (1 - fireArmor), Type.Fire);
                    FireDmg(dmg/2);
                }else if(collision.gameObject.GetComponent<Bullet>().type == Type.Acid){
                    GameObject g;
                    if(PhotonNetwork.IsConnected){
                        g = Instantiate(effects.acidEffect, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                        send_EffectAcid(view.ViewID);
                    }else{
                        g = Instantiate(effects.acidEffect, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                    }
                    g.transform.localScale = new Vector3(2, 2, 2);
                    ApplyDMG(dmg * (1 - acidArmor), Type.Acid);
                    collision.gameObject.GetComponent<Bullet>().shooted = true;
                }else if(collision.gameObject.GetComponent<Bullet>().type == Type.Electric){
                    GameObject g;
                    if(PhotonNetwork.IsConnected){
                        g = Instantiate(effects.electricEffect, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                        send_EffectElectricDmg(view.ViewID);
                    }else{
                        g = Instantiate(effects.electricEffect, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                    }
                    g.transform.localScale = new Vector3(3, 3, 3);
                    ApplyDMG(dmg * (1 - electricArmor), Type.Electric);
                    ElectricWave(dmg/3);
                }
                //if(!collision.gameObject.GetComponent<Bullet>().IsSplashBullet())
                if(collision.gameObject.GetComponent<Bullet>().type != Type.Acid)
                    if(PhotonNetwork.IsConnected){
                        if(collision.gameObject.GetComponent<PhotonView>().IsMine)
                            PhotonNetwork.Destroy(collision.gameObject);
                        else{
                            send_RPC_destroy(collision.gameObject.GetComponent<PhotonView>().ViewID);
                        }
                    }else{
                        Destroy(collision.gameObject);
                    }
            }
        }
    }

    public void send_RPC_destroy(int v){
        view.RPC("RPC_Destroy", RpcTarget.Others, v);
    }
    
    /*public void send_RPC_dmg(float d, char c){
        view.RPC("RPC_dmg", RpcTarget.AllBuffered, d, c);
    }*/
    public void send_Dmg_PopUp(float x, float y, float z, float dmg, string c){
        view.RPC("RPC_DMG_PopUp", RpcTarget.Others, x, y, z, dmg, c);
    }
    public void send_EffectFireBurst(int v){
        view.RPC("RPC_Effect_Fire_Burst", RpcTarget.Others, v);
    }
    public void send_EffectFireDmg(int v){
        view.RPC("RPC_Effect_Fire_Dmg", RpcTarget.Others, v);
    }
    public void send_EffectElectricDmg(int v){
        view.RPC("RPC_Effect_Electric_Dmg", RpcTarget.Others, v);
    }
    public void send_EffectElectricWave(int v, int t){
        view.RPC("RPC_Effect_Electric_Wave", RpcTarget.Others, v, t);
    }
    public void send_EffectAcid(int v){
        view.RPC("RPC_Effect_Acid", RpcTarget.Others, v);
    }

    [PunRPC]
    public void RPC_Nanobot(int[] b){
        GetComponentInChildren<BotFabric>().RPC_Nanobot(b);
    }
    [PunRPC]
    public void RPC_Behaviour(int[] b){
        GetComponent<BehaviourFabric>().RPC_Behaviour(b);
    }
    [PunRPC]
    public void RPC_DMG_PopUp(float x, float y, float z, float dmg, string c){
        if(view != null && !view.IsMine){
            Vector3 v = new Vector3(x, y, z);
            Color color = new Color();
            if(char.Equals(c, "a"))
                color = Color.magenta;
            else if(char.Equals(c, "e"))
                color = Color.yellow;
            else if(char.Equals(c, "f"))
                color = Color.red;
            else
                color = Color.black;

            DamagePopUp.Create(v, dmg, color);
        }
    }
    [PunRPC]
    public void RPC_Destroy(int v){
        PhotonView p = PhotonView.Find(v);
        if(p != null && p.IsMine)
            PhotonNetwork.Destroy(PhotonView.Find(v));
    }
    [PunRPC]
    public void RPC_Effect_Fire_Burst(int v){
        //PhotonView p = PhotonView.Find(v);
        if(view != null && view.ViewID == v){
            Instantiate(effects.FireBurstEffect, gameObject.transform.position, new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w), gameObject.transform);
        }
    }
    [PunRPC]
    public void RPC_Effect_Fire_Dmg(int v){
        if(view != null && view.ViewID == v){
            Instantiate(effects.FireEffect, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
        }
    }

    [PunRPC]
    public void RPC_Effect_Electric_Dmg(int v){
        if(view != null && view.ViewID == v){
            Instantiate(effects.electricEffect, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        }
    }
    [PunRPC]
    public void RPC_Effect_Electric_Wave(int v, int t){
        if(view != null && view.ViewID == v){
            GameObject g = PhotonView.Find(t).gameObject;
            Instantiate(effects.electricWaveEffect, gameObject.transform.position, Quaternion.identity, g.transform);
        }
    }
    [PunRPC]
    public void RPC_Effect_Acid(int v){
        if(view != null && view.ViewID == v){
            Instantiate(effects.acidEffect, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        }
    }
    /*[PunRPC]
    public void RPC_dmg(float d, char c){
        Type t = new Type();
        if(char.Equals(c, "f"))
            t = Type.Fire;
        else if(char.Equals(c, "e"))
            t = Type.Electric;
        else if(char.Equals(c, "a"))
            t = Type.Acid;

        ApplyDMG(d, t);
    }*/

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
