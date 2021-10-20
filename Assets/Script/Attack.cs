using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public LayerMask target;
    public GameObject bullet;
    private bool alreadyShoot = false;
    // Start is called before the first frame update
    void Start()
    {

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
        yield return new WaitForSeconds(6);
        GameObject b = Instantiate(bullet, transform.position, Quaternion.Euler(GetComponent<Nanobot>().AsVector()));
        Vector2 velocity = -target.transform.position + transform.position;
        Debug.Log(velocity);
        b.GetComponent<Rigidbody2D>().velocity = velocity.normalized * 0.1f;
        alreadyShoot = false; 
    }

    private void OnCollisionEnter2D(Collision2D collision2D){
        if((collision2D.gameObject.layer == 10 && gameObject.layer == 3) || (((collision2D.gameObject.layer == 11 && gameObject.layer == 9)))){
            gameObject.GetComponent<Nanobot>().life -= 10;
            Destroy(collision2D.gameObject);
            if(gameObject.GetComponent<Nanobot>().life <= 0)
                Destroy(gameObject);
        }
    }
}
