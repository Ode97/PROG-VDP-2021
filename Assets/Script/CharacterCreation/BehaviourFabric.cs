using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BehaviourFabric : MonoBehaviourPun
{
    private NanoBot entity;
    public GameObject[] visions;
    public GameObject[] attacks;
    public GameObject[] armours;
    public GameObject[] movements;
    public GameObject[] specials;
    public bool firstbot = true;
    // Start is called before the first frame update
    void Start()
    {
        if(!PhotonNetwork.IsConnected || photonView.IsMine){
            if(firstbot){
                entity = this.GetComponent<NanoBot>();
                GameObject atk = Instantiate(this.attacks[BotManager.atkType], this.transform);
                GameObject arm = Instantiate(this.armours[BotManager.armType], this.transform);
                GameObject mov = Instantiate(this.movements[BotManager.movType], this.transform);
                GameObject vis = Instantiate(this.visions[BotManager.visType], this.transform);
                GameObject spec;
                if (BotManager.specType != 0)
                    spec = Instantiate(this.specials[BotManager.specType], this.transform);
                firstbot = false;
                if(PhotonNetwork.IsConnected)
                    send_RPC_behaviour();
            }
        }
    }

    public void send_RPC_behaviour(){
        int[] behaviours = new int[6];
        behaviours.SetValue(GetComponent<PhotonView>().ViewID, 0);
        behaviours.SetValue(BotManager.atkType, 1);
        behaviours.SetValue(BotManager.armType, 2);
        behaviours.SetValue(BotManager.movType, 3);
        behaviours.SetValue(BotManager.visType, 4);
        behaviours.SetValue(BotManager.specType, 5);

        photonView.RPC("RPC_Behaviour", RpcTarget.Others, behaviours);
    }
    public void RPC_Behaviour(int[] b)
    {
        if(GetComponentInParent<PhotonView>().ViewID == b[0]){
            if(firstbot){
                entity = this.GetComponent<NanoBot>();
                GameObject atk = Instantiate(this.attacks[b[1]], this.transform);
                GameObject arm = Instantiate(this.armours[b[2]], this.transform);
                GameObject mov = Instantiate(this.movements[b[3]], this.transform);
                GameObject vis = Instantiate(this.visions[b[4]], this.transform);
                GameObject spec = Instantiate(this.specials[b[5]], this.transform);
                firstbot = false;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
