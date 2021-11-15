using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    private IEnumerator DestroyBullet(){
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
