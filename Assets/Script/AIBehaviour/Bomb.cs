using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private LayerMask target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explode());
    }

    public void SetTarget(LayerMask layer){
        target = layer;
    }

    // Update is called once per frame
    private IEnumerator Explode(){
        yield return new WaitForSeconds(Constants.BOMB_TIMING);
        List<Collider2D> colliders = new List<Collider2D>();
        colliders.AddRange(Physics2D.OverlapCircleAll(gameObject.transform.position, Constants.BOMB_DISTANCE_EXPLOSION, target));
        colliders = colliders.FindAll(c => c != null && c.gameObject.layer != gameObject.layer && (c.gameObject.layer == Constants.ENEMY_LAYER || c.gameObject.layer == Constants.PLAYER_LAYER));

        if(colliders.Count >= 1)
            foreach (var collider2D in colliders)
            {
                collider2D.gameObject.GetComponent<NanoBot>().ApplyDMG(Constants.BOMB_DMG_EXPLOSION);
            }


        Destroy(gameObject);
    }
}
