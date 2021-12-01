using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Type type;
    public float atkDmg;
    public bool shooted = false;
    private bool splashBullet = false;
    // Start is called before the first frame update
    void Start()
    {
        //atkDmg = GetComponentInParent<NanoBot>().attackDamage;
        StartCoroutine(DestroyBullet());
    }

    void Update(){
        if(type == Type.Acid && shooted){
            float x;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            x = Mathf.Lerp(transform.localScale.x, 56, 10 * Time.deltaTime);
            transform.localScale = new Vector3(x, x, x);
        }
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
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    public void SetSplashBullet(){
        splashBullet = true;
    }

    public bool IsSplashBullet(){
        return splashBullet;
    }
}
