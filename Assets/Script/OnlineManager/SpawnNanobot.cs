using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; 

public class SpawnNanobot : MonoBehaviourPun
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public Button launch;
    public Button back;
    private bool ok = false;
    private bool yetLoaded = false;
    private bool alreadyInstantiate = false;
    public int p = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        launch.onClick.AddListener(send_Ok);
        back.onClick.AddListener(DestroySpawn);
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
            if(PhotonNetwork.LevelLoadingProgress == 1 && !alreadyInstantiate){
                if(PhotonNetwork.IsMasterClient){
                    GameObject g = PhotonNetwork.Instantiate(player1Prefab.name, new Vector3(3, 10, 0), Quaternion.identity);
                    alreadyInstantiate = true;
                    g.name = "Player 1";
                    PhotonNetwork.Destroy(gameObject);
                }else{
                    GameObject o = PhotonNetwork.Instantiate(player2Prefab.name, new Vector3(37, 10, 0), new Quaternion(0,0,180,0));
                    alreadyInstantiate = true;
                    o.name = "Player 2";
                    //PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    }

    public void send_Ok(){
        GetComponent<PhotonView>().RPC("Ok_RPC", RpcTarget.Others);
        ok = true;
        launch.enabled = false;
        if(PhotonNetwork.IsMasterClient){
            for(int x = 0; x < MapManager.playerMapMatrix.GetUpperBound(0); x++)
                for(int y = 0; y < MapManager.playerMapMatrix.GetUpperBound(1)+1; y++)
                    MapManager.mapMatrix[x, y] = MapManager.playerMapMatrix[x, y];
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
        MapManager.mapMatrix[x+20, y] = c;
    }

    public void DestroySpawn(){
        PhotonNetwork.Destroy(gameObject);
    }
}
