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
            GetComponentInParent<NanoBot>().lifeLostPerSec -= 0;
        else if(leaveLessEnergyAfterDeath)
            GetComponentInParent<NanoBot>().foodSpawnAfterDeath -= 0;
        else if(chanceOfCrit)
            GetComponentInParent<NanoBot>().chanceOfCrit += 0;
        else if(leaveBombAfterDeath)
            GetComponentInParent<NanoBot>().leaveBombAfterDeath = leaveBombAfterDeath;
        else if(firstAttackDealsMoreDMG)
            GetComponentInParent<NanoBot>().firstAttackDealsMoreDMG = firstAttackDealsMoreDMG;

    }
}
