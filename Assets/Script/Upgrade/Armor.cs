using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArmorLevel {None,Low, Mid, High}

public class Armor : MonoBehaviour
{
    public Type type1;
    public ArmorLevel level1;
    public Type type2;
    public ArmorLevel level2;
    public bool generalArmor;

    void Start(){

        if(generalArmor){
            GetComponentInParent<NanoBot>().fireArmor = Constants.HIGH_PROTECTION;
            GetComponentInParent<NanoBot>().acidArmor = Constants.HIGH_PROTECTION;
            GetComponentInParent<NanoBot>().electricArmor = Constants.HIGH_PROTECTION;
            GetComponentInParent<NanoBot>().trapArmor = Constants.HIGH_PROTECTION;
        }else{

            if(level1 != ArmorLevel.None){
                if(level1 == ArmorLevel.Mid)
                    if(type1 == Type.Electric)
                        GetComponentInParent<NanoBot>().electricArmor = Constants.MID_PROTECTION;
                    else if(type1 == Type.Acid)
                        GetComponentInParent<NanoBot>().acidArmor = Constants.MID_PROTECTION;
                    else if(type1 == Type.Fire)
                        GetComponentInParent<NanoBot>().fireArmor = Constants.MID_PROTECTION;
                    else if(type1 == Type.Trap)
                        GetComponentInParent<NanoBot>().trapArmor = Constants.MID_PROTECTION;
                else if(level1 == ArmorLevel.Low)
                    if(type1 == Type.Electric)
                        GetComponentInParent<NanoBot>().electricArmor = Constants.LOW_PROTECTION;
                    else if(type1 == Type.Acid)
                        GetComponentInParent<NanoBot>().acidArmor = Constants.LOW_PROTECTION;
                    else if(type1 == Type.Fire)
                        GetComponentInParent<NanoBot>().fireArmor = Constants.LOW_PROTECTION;
                    else if(type1 == Type.Trap)
                        GetComponentInParent<NanoBot>().trapArmor = Constants.LOW_PROTECTION;
                else if(level1 == ArmorLevel.High)
                    if(type1 == Type.Electric)
                        GetComponentInParent<NanoBot>().electricArmor = Constants.HIGH_PROTECTION;
                    else if(type1 == Type.Acid)
                        GetComponentInParent<NanoBot>().acidArmor = Constants.HIGH_PROTECTION;
                    else if(type1 == Type.Fire)
                        GetComponentInParent<NanoBot>().fireArmor = Constants.HIGH_PROTECTION;
                    else if(type1 == Type.Trap)
                        GetComponentInParent<NanoBot>().trapArmor = Constants.HIGH_PROTECTION;
            }

            if(level2 != ArmorLevel.None){
                if(level2 == ArmorLevel.Mid)
                    if(type2 == Type.Electric)
                        GetComponentInParent<NanoBot>().electricArmor = Constants.MID_PROTECTION;
                    else if(type2 == Type.Acid)
                        GetComponentInParent<NanoBot>().acidArmor = Constants.MID_PROTECTION;
                    else if(type2 == Type.Fire)
                        GetComponentInParent<NanoBot>().fireArmor = Constants.MID_PROTECTION;
                    else if(type2 == Type.Trap)
                        GetComponentInParent<NanoBot>().trapArmor = Constants.MID_PROTECTION;
                else if(level2 == ArmorLevel.Low)
                    if(type2 == Type.Electric)
                        GetComponentInParent<NanoBot>().electricArmor = Constants.LOW_PROTECTION;
                    else if(type2 == Type.Acid)
                        GetComponentInParent<NanoBot>().acidArmor = Constants.LOW_PROTECTION;
                    else if(type2 == Type.Fire)
                        GetComponentInParent<NanoBot>().fireArmor = Constants.LOW_PROTECTION;
                    else if(type2 == Type.Trap)
                        GetComponentInParent<NanoBot>().trapArmor = Constants.LOW_PROTECTION;
                else if(level2 == ArmorLevel.High)
                    if(type2 == Type.Electric)
                        GetComponentInParent<NanoBot>().electricArmor = Constants.HIGH_PROTECTION;
                    else if(type2 == Type.Acid)
                        GetComponentInParent<NanoBot>().acidArmor = Constants.HIGH_PROTECTION;
                    else if(type2 == Type.Fire)
                        GetComponentInParent<NanoBot>().fireArmor = Constants.HIGH_PROTECTION;
                    else if(type2 == Type.Trap)
                        GetComponentInParent<NanoBot>().trapArmor = Constants.HIGH_PROTECTION;
            }
        }
        Destroy(gameObject);
    }
}
