using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourFabric : MonoBehaviour
{
    private NanoBot entity;
    public GameObject[] visions;
    public GameObject[] attacks;
    public GameObject[] armours;
    public GameObject[] movements;
    public bool firstbot = true;
    // Start is called before the first frame update
    void Start()
    {
        if(firstbot){
            entity = this.GetComponent<NanoBot>();
            GameObject atk = Instantiate(this.attacks[BotManager.atk], this.transform);
            GameObject arm = Instantiate(this.armours[BotManager.arm], this.transform);
            GameObject mov = Instantiate(this.movements[BotManager.mov], this.transform);
            GameObject vis = Instantiate(this.visions[BotManager.vis], this.transform);
            firstbot = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
