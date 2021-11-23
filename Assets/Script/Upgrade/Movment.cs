using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public float speedBonus;
    public float precisionBonus;

    void Start(){
        GetComponentInParent<NanoBot>().speed += speedBonus;
        GetComponentInParent<NanoBot>().rotation -= precisionBonus;

        Destroy(gameObject);
    }
}
