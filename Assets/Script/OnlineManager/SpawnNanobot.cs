using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; 
using Photon.Realtime;

public class SpawnNanobot : MonoBehaviourPunCallbacks
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public Text onlinePlayer;
    public Button launch;
    public Button back;
    private bool ok = false;
    private bool yetLoaded = false;
    private bool alreadyInstantiate = false;
    public int p = 0;
    private bool second = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        launch.onClick.AddListener(send_Ok);
        back.onClick.AddListener(DestroySpawn);
        PhotonNetwork.AutomaticallySyncScene = true;
        if(PhotonNetwork.IsMasterClient)
            onlinePlayer.text = "player: 1/2";
        else{
            second = true;
            onlinePlayer.text = "player: 2/2";
            launch.gameObject.SetActive(true);
        }
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
                    StartCoroutine(wait(player1Prefab, "Player 1"));
                    //GameObject g = PhotonNetwork.Instantiate(player1Prefab.name, new Vector3(BotManager.spawnX, BotManager.spawnY, 0), Quaternion.identity);
                    alreadyInstantiate = true;
                    //g.name = "Player 1";
                    PhotonNetwork.Destroy(gameObject);
                }else{
                    StartCoroutine(wait(player2Prefab, "Player 2"));
                    //GameObject o = PhotonNetwork.Instantiate(player2Prefab.name, new Vector3(BotManager.spawnX + 20, BotManager.spawnY, 0), new Quaternion(0,0,180,0));
                    alreadyInstantiate = true;
                    //o.name = "Player 2";
                    //PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    }
    public IEnumerator wait(GameObject a, string s){
        yield return new WaitForSeconds(0.2f);
        GameObject g = PhotonNetwork.Instantiate(a.name, new Vector3(BotManager.spawnX, BotManager.spawnY, 0), Quaternion.identity);
        g.name = s;
    }
    public IEnumerator wait2(GameObject a, string s){
        yield return new WaitForSeconds(0.2f);
        GameObject o = PhotonNetwork.Instantiate(player2Prefab.name, new Vector3(BotManager.spawnX + 20, BotManager.spawnY, 0), new Quaternion(0,0,180,0));
        o.name = s;
    }

    public void send_Ok(){
        GetComponent<PhotonView>().RPC("Ok_RPC", RpcTarget.Others);
        launch.gameObject.SetActive(false);
        if(PhotonNetwork.IsMasterClient){
            for(int x = 0; x < MapManager.playerMapMatrix.GetUpperBound(0); x++)
                for(int y = 0; y < MapManager.playerMapMatrix.GetUpperBound(1)+1; y++)
                    MapManager.mapMatrix[x, y] = MapManager.playerMapMatrix[x, y];
        }
        onlinePlayer.text = "player: 2/2\nwait the\nother player";

        ok = true;
        p++;
    }
    [PunRPC]
    public void Ok_RPC(){
        p++;
        onlinePlayer.text = "player: 2/2\nother player\nis ready";
    }
    [PunRPC]
    public void Receive_map(string s, int x, int y){
        char c = new char();
        c = s[0];    
        MapManager.mapMatrix[x+20, y] = c;
    }

    public override void OnPlayerEnteredRoom(Player otherPlayer){
        launch.gameObject.SetActive(true);
        onlinePlayer.text = "player: 2/2";
    }

    public override void OnPlayerLeftRoom(Player otherPlayer){
        if(PhotonNetwork.IsMasterClient){
            launch.gameObject.SetActive(false);
            for(int x = 0; x < 41; x++)
                for(int y = 0; y < 21; y++)
                    MapManager.mapMatrix[x, y] = 'v';

            onlinePlayer.text = "player: 1/2";
        }

        if(second){
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.Disconnect();
            DestroySpawn();
            SceneHandler.MainMenu();
        }
    }

    public void DestroySpawn(){
        PhotonNetwork.Destroy(gameObject);
    }
}
