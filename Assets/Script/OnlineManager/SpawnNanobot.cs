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
    private int t = 0;
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
            if(!yetLoaded){
                yetLoaded = true;
                StartCoroutine(waitScene());
            }
            Debug.Log(t);
            if(PhotonNetwork.IsMasterClient && t == 2 && !alreadyInstantiate){
                GameObject m = GameObject.Find("Map");
                Debug.Log(m);
                //m.GetComponent<MapGeneratorOnline>().enabled = true;
                //GameObject g = PhotonNetwork.Instantiate(player1Prefab.name, new Vector3(BotManager.spawnX, BotManager.spawnY, 0), Quaternion.identity);
                sendSpawn();
                //g.name = "Player 1";
                PhotonNetwork.Destroy(gameObject);
            }/*else if(!PhotonNetwork.IsMasterClient && t == 2 && !alreadyInstantiate){
                GameObject o = PhotonNetwork.Instantiate(player2Prefab.name, new Vector3(BotManager.spawnX + 20, BotManager.spawnY, 0), new Quaternion(0,0,180,0));
                alreadyInstantiate = true;
                o.name = "Player 2";
            }*/
                //LoadNewScene(newSceneName); // custom method to load the new scene by name
                /*while(PhotonNetwork.LevelLoadingProgress == 1)
                {
                    yield return null;
                }*/
            //}
            /*f(PhotonNetwork.LevelLoadingProgress == 1 && !alreadyInstantiate){
                if(PhotonNetwork.IsMasterClient){
                    GameObject g = PhotonNetwork.Instantiate(player1Prefab.name, new Vector3(BotManager.spawnX, BotManager.spawnY, 0), Quaternion.identity);
                    alreadyInstantiate = true;
                    g.name = "Player 1";
                    PhotonNetwork.Destroy(gameObject);
                }else{
                    GameObject o = PhotonNetwork.Instantiate(player2Prefab.name, new Vector3(BotManager.spawnX + 20, BotManager.spawnY, 0), new Quaternion(0,0,180,0));
                    alreadyInstantiate = true;
                    o.name = "Player 2";
                    //PhotonNetwork.Destroy(gameObject);
                }
            }*/
        }
    }
    public IEnumerator waitScene(){
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.IsMessageQueueRunning = false;
            PhotonNetwork.LoadLevel(9);
            Debug.Log("g");
        }
        while(PhotonNetwork.LevelLoadingProgress < 1)
        {
            Debug.Log(PhotonNetwork.LevelLoadingProgress);
            yield return null;
        }
        PhotonNetwork.IsMessageQueueRunning = true;
        //yield return new WaitForSeconds(0.5f);
        t++;
        send_Ready();
    }

    public void sendSpawn(){
        GetComponent<PhotonView>().RPC("Spawn_RPC", RpcTarget.All);
    }

    public void send_Ready(){
        GetComponent<PhotonView>().RPC("Ready_RPC", RpcTarget.Others);
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
    public void Spawn_RPC(){
        GameObject g;
        if(PhotonNetwork.IsMasterClient){
            g = PhotonNetwork.Instantiate(player1Prefab.name, new Vector3(BotManager.spawnX, BotManager.spawnY, 0), Quaternion.identity);
            g.name = "Player 1";
        }else{
            g = PhotonNetwork.Instantiate(player2Prefab.name, new Vector3(BotManager.spawnX + 20, BotManager.spawnY, 0), new Quaternion(0,0,180,0));
            g.name = "Player 2";
        }
    }
    [PunRPC]
    public void Ready_RPC(){
        t++;
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
