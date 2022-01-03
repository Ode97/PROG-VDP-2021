using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Combat : Action
{
    public GameObject bullet;
    public bool alreadyShoot = false;
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
            GameObject b;
            if(PhotonNetwork.IsConnected){
                b = PhotonNetwork.Instantiate(bullet.name, transform.position, Quaternion.Euler(GetComponent<NanoBot>().AsVector()));
                b.GetComponent<Bullet>().view = this.GetComponent<NanoBot>().view.ViewID;
            }else{
                b = Instantiate(bullet, transform.position, Quaternion.Euler(GetComponent<NanoBot>().AsVector()));
            }
            b.GetComponent<Bullet>().type = GetComponent<NanoBot>().typeOfAttk;
            if(firstShoot){
                b.GetComponent<Bullet>().SetDMG(GetComponent<NanoBot>().attackDamage + Constants.FIRST_ATTK_DMG);
                firstShoot = false;
            }else
                b.GetComponent<Bullet>().SetDMG(GetComponent<NanoBot>().attackDamage);

            int x = Random.Range(1, 100);
            if(x > 100 - GetComponent<NanoBot>().chanceOfCrit)
                b.GetComponent<Bullet>().SetDMG(b.GetComponent<Bullet>().GetDMG()*2);

            //b.GetComponent<Bullet>().SetType(GetComponent<NanoBot>().typeOfAttk);
            if(GetComponent<NanoBot>().splashAttack){
                b.transform.localScale = new Vector3(10f, 10f, 1);
                b.GetComponent<Bullet>().SetSplashBullet();
            }else
                b.transform.localScale = new Vector3(8f, 8f, 1);
                
            Vector2 velocityBullet = target.transform.position - transform.position;
            b.GetComponent<Rigidbody2D>().velocity = velocityBullet.normalized * Constants.BULLET_VELOCITY;
        }
        yield return new WaitForSeconds((float)10/GetComponent<NanoBot>().attackSpeed);
        alreadyShoot = false; 
        
    }
    
}
