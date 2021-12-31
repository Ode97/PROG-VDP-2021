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
    public bool firstbot = true;
    // Start is called before the first frame update
    void Start()
    {
        if(!PhotonNetwork.IsConnected || photonView.IsMine){
            if(firstbot){
                entity = this.GetComponent<NanoBot>();
                GameObject atk = Instantiate(this.attacks[BotManager.atk], this.transform);
                GameObject arm = Instantiate(this.armours[BotManager.arm], this.transform);
                GameObject mov = Instantiate(this.movements[BotManager.mov], this.transform);
                GameObject vis = Instantiate(this.visions[BotManager.vis], this.transform);
                firstbot = false;
                if(PhotonNetwork.IsConnected)
                    send_RPC_behaviour();
            }
        }
    }

    public void send_RPC_behaviour(){
        int[] behaviours = new int[5];
        behaviours.SetValue(GetComponent<PhotonView>().ViewID, 0);
        behaviours.SetValue(BotManager.atk, 1);
        behaviours.SetValue(BotManager.arm, 2);
        behaviours.SetValue(BotManager.mov, 3);
        behaviours.SetValue(BotManager.vis, 4);

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
                Debug.Log(atk + " " + arm + " " + mov + " " + vis);
                firstbot = false;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
