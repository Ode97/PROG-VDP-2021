using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // if(Save.MainMessage()) {
        if(StoryController.labMessage){
            Destroy(this.gameObject);
        }
        StoryController.labMessage = true;
    }
}
