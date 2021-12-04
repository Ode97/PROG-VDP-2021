using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourFabric : MonoBehaviour
{
    private NanoBot entity;
    // Start is called before the first frame update
    void Start()
    {
        entity = this.GetComponent<NanoBot>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
