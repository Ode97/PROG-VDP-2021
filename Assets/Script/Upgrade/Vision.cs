using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [Range(0, 360)]
    public int FOVBonus;
    public float lookaheadBonus;

    void Start(){
        GetComponentInParent<NanoBot>().viewAngle += FOVBonus;
        GetComponentInParent<NanoBot>().lookahead += lookaheadBonus;

        Destroy(gameObject);
    }
}
