using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class SpawnNanobot : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2){
            if(PhotonNetwork.IsMasterClient){
                GameObject g = PhotonNetwork.Instantiate(player1Prefab.name, new Vector3(3, 10, 0), Quaternion.identity);
                g.name = "Player 1";
                Destroy(gameObject);
            }else{
                GameObject o = PhotonNetwork.Instantiate(player2Prefab.name, new Vector3(37, 10, 0), new Quaternion(0,0,180,0));
                o.name = "Player 2";
                Destroy(gameObject);
            }
        }
    }
}
