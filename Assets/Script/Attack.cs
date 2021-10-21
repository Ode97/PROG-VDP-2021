using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public LayerMask target;
    public GameObject bullet;
    private bool alreadyShoot = false;
    private bool destroy = false;
    // Start is called before the first frame update
    void Start()
    {

        alreadyShoot = false;
        destroy = false;

        if(gameObject.layer == 3)
            target = 9;

        else
            target = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(GameObject target){
        if(!alreadyShoot){
            alreadyShoot = true;
            StartCoroutine(CreateBullet(target));
        }
    }

    private IEnumerator CreateBullet(GameObject target){
        yield return new WaitForSeconds(1);
        alreadyShoot = false; 
        if(!destroy && target != null){
            GameObject b = Instantiate(bullet, transform.position, Quaternion.Euler(GetComponent<Nanobot>().AsVector()));
            b.transform.localScale = new Vector3(0.3f, 0.3f, 0);
            Vector2 velocity = target.transform.position - transform.position;
            b.GetComponent<Rigidbody2D>().velocity = velocity.normalized * 3;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D){
        if(((collision2D.gameObject.layer == 10 && gameObject.layer == 3) || (((collision2D.gameObject.layer == 11 && gameObject.layer == 9)))) && collision2D.otherCollider.gameObject.layer != 8){
            gameObject.GetComponent<Nanobot>().life -= 20;
            Destroy(collision2D.gameObject);
            if(gameObject.GetComponent<Nanobot>().life <= 0){
                destroy = true;
            }
        }
    }
}
