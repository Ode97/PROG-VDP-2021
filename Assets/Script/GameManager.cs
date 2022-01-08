using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    private int nPlayer = 1;
    private int nEnemy = 1;
    public GameObject explosion;
    public Text p;
    public Text e;
    public Text tc;
    public float timeRemaining = 10;
    private bool timerIsRunning = true;
    private float signalTimeRemaining = 5;
    private float destructionTimeRemaining = 5;
    private int mousein = -1;
    public Camera mainCamera;
    public int pLayer;
    public static bool gameEnd = false;
    public GameObject endMessage;
    public Signal active;
    public Signal final;
    public Button signalButton;
    public Button explosionButton;
    private bool s = false;
    private bool d = false;
    private bool a = false;
    private bool b = false;

    // Start is called before the first frame update
    public static GameManager instance=null;

    void Awake()
    {
        if (instance==null)
            instance=this;
        else if (instance !=this)
            Destroy (gameObject);

    }

    void Start(){
        gameEnd = false;
        s = false;
        d = false;
        a = false;
        b = false;
        endMessage.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = false;
        signalButton.onClick.AddListener(EnableSignal);
        explosionButton.onClick.AddListener(EnableDestruction);
    }

    void Update()
    {
       if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                DisplayTime(timeRemaining);
                timerIsRunning = false;
                GetComponent<LineRenderer>().startColor = Color.yellow;
                GetComponent<LineRenderer>().endColor = Color.yellow;
                final.enabled = true;
                final.FinalSignal();
            }
        }

        if (signalTimeRemaining > 0){
            signalTimeRemaining -= Time.deltaTime;
            signalButton.GetComponentInChildren<Text>().text = ((int)signalTimeRemaining + 1).ToString();
        }else{
            signalButton.GetComponentInChildren<Text>().text = "Signal";
            if (Input.GetMouseButtonUp(0) && !a){
                a = true;
            }
            if(s){
                if(a)
                    signalByPlayer();
            }
        }

        if (destructionTimeRemaining > 0){
            destructionTimeRemaining -= Time.deltaTime;
            explosionButton.GetComponentInChildren<Text>().text = ((int)destructionTimeRemaining + 1).ToString();
        }else{
            explosionButton.GetComponentInChildren<Text>().text = "Wall\nDestruction";
            if (Input.GetMouseButtonUp(0) && !b){
                b = true;
            }
            if(d){
                if(b)
                    destructionByPlayer();
            }
        }
    }

    private void EnableSignal(){
        s = true;
        d = false;
        b = false;
        explosionButton.interactable = true;
        signalButton.interactable = false;
    }

    private void EnableDestruction(){
        d = true;
        s = false;
        a = false;
        signalButton.interactable = true;
        explosionButton.interactable = false;
    }

    private void signalByPlayer(){
        
        if (Input.GetMouseButtonDown(0)){
            mousein = 0;
        }

        Debug.Log(mousein);
        if (mousein == 0) {
            Ray hit = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;           
            GetComponent<LineRenderer>().startColor = Color.white;
            GetComponent<LineRenderer>().endColor = Color.white;
            if(!PhotonNetwork.IsConnected || PhotonNetwork.IsMasterClient){
                pLayer = Constants.PLAYER_LAYER;
            }else{
                pLayer = Constants.ENEMY_LAYER;
            }
        
            if(Physics.Raycast(hit, out hitData, 1000)){
                active.enabled = true;
                active.SetCenter(new Vector2(hitData.point.x, hitData.point.y - 5), pLayer);
            }
            mousein = -1;
            signalTimeRemaining = 5;
        }
    }

    private void destructionByPlayer(){
        //if (Input.GetMouseButtonDown(0))
        //    mousein = 0;
        if (Input.GetMouseButtonDown(0)){
            mousein = 0;
        }
        
        if (mousein == 0) {
            //RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera)), Vector2.zero, 50, 1 << Constants.OBSTACLE_LAYER);
            Ray hit = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            //Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition));
            //Vector2 mouseMapPosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);
            if(Physics.Raycast(hit, out hitData, 1000)){
                if(hitData.collider.gameObject.layer == Constants.OBSTACLE_LAYER){
                    if(PhotonNetwork.IsConnected){
                        if(PhotonNetwork.IsMasterClient){
                            PhotonNetwork.Destroy(hitData.transform.parent.gameObject);
                        }else{
                            send_Explosion_RPC(hitData.transform.parent.GetComponent<PhotonView>().ViewID);
                        }
                    }else{
                        Destroy(hitData.transform.parent.gameObject);
                    }
                    Instantiate(explosion, hitData.transform.parent.transform.position, Quaternion.identity);
                }
            }
            mousein = -1;
            destructionTimeRemaining = 5;
        }
    }

    void DisplayTime(float timeToDisplay) {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt((timeToDisplay + 1) % 60);

        // c.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        tc.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    


    public void newChild(int layer){
        if(layer == Constants.PLAYER_LAYER)
            nPlayer++;
        else
            nEnemy++;

        p.text = nPlayer.ToString();
        e.text = nEnemy.ToString();
    }

    public void death(int layer){
        if(layer == Constants.PLAYER_LAYER)
            nPlayer--;
        else
            nEnemy--;

        p.text = nPlayer.ToString();
        e.text = nEnemy.ToString();

        if(nPlayer == 0 && !gameEnd){
            gameEnd = true;
            StartCoroutine(playerLose());
        }else if(nEnemy == 0 && !gameEnd){
            gameEnd = true;
            StartCoroutine(enemyLose());
        }
    }

    IEnumerator playerLose() {
        StoryController.pause();
        endMessage.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        StoryController.unpause();
        if(PhotonNetwork.IsConnected){
            if(PhotonNetwork.IsMasterClient){
                SceneHandler.LoseScreen();
            }else{
                SceneHandler.WinScreen();
            }
        }else
            SceneHandler.LoseScreen();
    }
    IEnumerator enemyLose() {
        StoryController.pause();
        endMessage.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        StoryController.unpause();
        if(PhotonNetwork.IsConnected){
            if(PhotonNetwork.IsMasterClient)
                SceneHandler.WinScreen();
            else
                SceneHandler.LoseScreen();
        }else
            SceneHandler.WinScreen();
    }

    public void send_Explosion_RPC(int view){
        GetComponent<PhotonView>().RPC("RPC_Explosion", RpcTarget.Others, view);
    }
    [PunRPC]
    public void RPC_Explosion(int view){
        PhotonView pv = PhotonView.Find(view);
        PhotonNetwork.Destroy(pv);
        Instantiate(explosion, pv.gameObject.transform.position, Quaternion.identity);
    }
}
