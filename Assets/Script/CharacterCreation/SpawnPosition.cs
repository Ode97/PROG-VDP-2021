using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!BotManager.spawned){
            this.transform.position = new Vector2(BotManager.spawnX, BotManager.spawnY);
            BotManager.spawned = true;
        }
    }
}
