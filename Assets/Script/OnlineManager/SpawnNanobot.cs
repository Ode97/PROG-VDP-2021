using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; 

public class SpawnNanobot : MonoBehaviourPun
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public Button button;
    private bool ok = false;
    private bool yetLoaded = false;
    public int p = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        button.onClick.AddListener(send_Ok);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(p == 2 && ok){
            if(PhotonNetwork.IsMasterClient && !yetLoaded){
                yetLoaded = true;
                PhotonNetwork.LoadLevel(9);
            }

            if(PhotonNetwork.LevelLoadingProgress == 1){
                if(PhotonNetwork.IsMasterClient){
                    GameObject g = PhotonNetwork.Instantiate(player1Prefab.name, new Vector3(3, 10, 0), Quaternion.identity);
                    g.name = "Player 1";
                    //Destroy(gameObject);
                    gameObject.SetActive(false);
                }else{
                    GameObject o = PhotonNetwork.Instantiate(player2Prefab.name, new Vector3(37, 10, 0), new Quaternion(0,0,180,0));
                    o.name = "Player 2";
                    gameObject.SetActive(false);
                    //Destroy(gameObject);
                }
            }
        }
    }

    public void send_Ok(){
        GetComponent<PhotonView>().RPC("Ok_RPC", RpcTarget.Others);
        ok = true;
        button.enabled = false;
        if(PhotonNetwork.IsMasterClient){
            for(int x = 0; x < MapManager.playerMapMatrix.GetUpperBound(0)+1; x++)
                for(int y = 0; y < MapManager.playerMapMatrix.GetUpperBound(1)+1; y++)
                    MapManager.mapMatrix[y, x] = MapManager.playerMapMatrix[y, x];
        }

        p++;
    }
    [PunRPC]
    public void Ok_RPC(){
        p++;
    }
    [PunRPC]
    public void Receive_map(string s, int x, int y){
        char c = new char();
        c = s[0];    
        MapManager.mapMatrix[y, x+20] = c;
    }

}
