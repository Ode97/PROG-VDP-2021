using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type {None,Electric, Acid, Fire, Trap}

public class Attack : MonoBehaviour
{
    public Type typeOfAttack;
    public GameObject bullet;
    public bool ranged;
    [Range(1, 100)]
    public int attkSpeedBonus;
    public int dmgBonus;
    public bool splash;

    void Start(){
        if(GetComponentInParent<NanoBot>().gameObject.layer == Constants.PLAYER_LAYER)
            bullet.layer = Constants.PLAYER_BULLET_LAYER;
        else
            bullet.layer = Constants.ENEMY_BULLET_LAYER;
            
        GetComponentInParent<Combat>().bullet = bullet;
        GetComponentInParent<NanoBot>().attackDamage += dmgBonus;
        GetComponentInParent<NanoBot>().attackSpeed += attkSpeedBonus;
        GetComponentInParent<NanoBot>().rangedAttack = ranged;
        GetComponentInParent<NanoBot>().splashAttack = splash;
    
        Destroy(gameObject);
    }
}
