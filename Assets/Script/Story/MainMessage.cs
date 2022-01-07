using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // if(Save.MainMessage()) {
        if(StoryController.mainMessage){
            Destroy(this.gameObject);
        }
        StoryController.mainMessage = true;
    }
}
