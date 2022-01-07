using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public bool clockwise = true; 
    public int speed = 30;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(clockwise) this.transform.Rotate(Vector3.forward * (-1 * speed) * Time.deltaTime);
        else this.transform.Rotate(Vector3.forward *speed* Time.deltaTime);
    }
}
