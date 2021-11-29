using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{
    public bool lowEnergyUsage;
    //public bool moreEnergyPerKill;
    public bool leaveLessEnergyAfterDeath;
    public bool chanceOfCrit;
    public bool leaveBombAfterDeath; 
    public bool firstAttackDealsMoreDMG;

    void Start(){
        if(lowEnergyUsage)
            GetComponentInParent<NanoBot>().lifeLostPerSec -= 1;
        if(leaveLessEnergyAfterDeath)
            GetComponentInParent<NanoBot>().foodSpawnAfterDeath -= 1;
        if(chanceOfCrit)
            GetComponentInParent<NanoBot>().chanceOfCrit += 10;
        if(leaveBombAfterDeath)
            GetComponentInParent<NanoBot>().leaveBombAfterDeath = leaveBombAfterDeath;
        if(firstAttackDealsMoreDMG)
            GetComponentInParent<NanoBot>().firstAttackDealsMoreDMG = firstAttackDealsMoreDMG;

        Destroy(gameObject);
    }
}
