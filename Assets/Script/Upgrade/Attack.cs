using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type {Electric, Acid, Fire, Trap}

public class Attack : MonoBehaviour
{
    public Type typeOfAttack;
    public bool ranged;
    public int attkSpeedBonus;
    public int dmgBonus;
    public bool splash;

    void Start(){
        GetComponentInParent<Combat>().bullet.GetComponent<Bullet>().type = typeOfAttack;
        GetComponentInParent<NanoBot>().attackDamage += dmgBonus;
        GetComponentInParent<NanoBot>().attackSpeed += attkSpeedBonus;
        GetComponentInParent<NanoBot>().rangedAttack = ranged;
        GetComponentInParent<NanoBot>().splashAttack = splash;
    }
}
