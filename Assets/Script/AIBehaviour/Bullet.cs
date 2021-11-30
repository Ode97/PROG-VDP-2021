using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Type type;
    public float atkDmg;
    private bool splashBullet = false;
    // Start is called before the first frame update
    void Start()
    {
        //atkDmg = GetComponentInParent<NanoBot>().attackDamage;
        StartCoroutine(DestroyBullet());
    }

    public void SetDMG(float dmg){
        atkDmg = dmg;
    }

    public float GetDMG(){
        return atkDmg;
    }

    public void SetType(Type t){
        type = t;
    }

    private IEnumerator DestroyBullet(){
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public void SetSplashBullet(){
        splashBullet = true;
    }

    public bool IsSplashBullet(){
        return splashBullet;
    }
}
