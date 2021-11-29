using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : Action
{
    public GameObject bullet;
    private bool alreadyShoot = false;
    private bool firstShoot = false;
    // Start is called before the first frame update
    void Start()
    {

        alreadyShoot = false;
        firstShoot = false;
        //destroy = false;
    }

    // Update is called once per frame
    public override void DoIt()
    {
        Vector2 velocity = GetComponent<NanoBot>().GetTarget().transform.position - transform.position;
        GetComponent<Rigidbody2D>().velocity =  velocity.normalized * 0;
        if(!alreadyShoot){
            alreadyShoot = true;
            StartCoroutine(CreateBullet(GetComponent<NanoBot>().GetTarget()));
        }
    }

    public void SetFirstShootBonus(){
        firstShoot = true;
    }

    private IEnumerator CreateBullet(GameObject target){
        if(target != null){
            GameObject b = Instantiate(bullet, transform.position, Quaternion.Euler(GetComponent<NanoBot>().AsVector()));
            if(firstShoot){
                b.GetComponent<Bullet>().SetDMG(GetComponent<NanoBot>().attackDamage + Constants.FIRST_ATTK_DMG);
                firstShoot = false;
            }else
                b.GetComponent<Bullet>().SetDMG(GetComponent<NanoBot>().attackDamage);

            int x = Random.Range(1, 100);
            if(x > 100 - GetComponent<NanoBot>().chanceOfCrit)
                b.GetComponent<Bullet>().SetDMG(b.GetComponent<Bullet>().GetDMG()*2);

            b.GetComponent<Bullet>().SetType(GetComponent<NanoBot>().typeOfAttk);
            if(GetComponent<NanoBot>().splashAttack){
                b.transform.localScale = new Vector3(2f, 2f, 0);
                b.GetComponent<Bullet>().SetSplashBullet();
            }else
                b.transform.localScale = new Vector3(0.3f, 0.3f, 0);
                
            Vector2 velocityBullet = target.transform.position - transform.position;
            b.GetComponent<Rigidbody2D>().velocity = velocityBullet.normalized * 20;//velocità dei proiettili
        }
        yield return new WaitForSeconds(10/GetComponent<NanoBot>().attackSpeed);
        alreadyShoot = false; 
    }
    
}
