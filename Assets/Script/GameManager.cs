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
    public Text p;
    public Text e;
    public Text tc;
    public float timeRemaining = 10;
    private bool timerIsRunning = true;
    private int mousein = -1;
    public Camera mainCamera;
    public int pLayer;
    public static bool gameEnd = false;
    public GameObject endMessage;

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
        endMessage.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = false;
        /*p.transform.GetComponent<TextMeshPro>().color = Color.white;
        e.transform.GetComponent<TextMeshPro>().color = Color.red;
        p.transform.GetComponent<TextMeshPro>().SetText(nPlayer.ToString());
        e.transform.GetComponent<TextMeshPro>().SetText(nEnemy.ToString());*/
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
                GetComponent<Signal>().enabled = true;
                GetComponent<Signal>().FinalSignal();
            }
        }
        signalByPlayer();
    }

    private void signalByPlayer(){
        //if (Input.GetMouseButtonDown(0))
        //    mousein = 0;
        
        if (Input.GetMouseButtonUp(0))
            mousein = 0;
        
        if (mousein == 0) {
            Vector2 mouseMapPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<Signal>().enabled = true;
            GetComponent<Signal>().radius = 3;
            GetComponent<LineRenderer>().startColor = Color.white;
            GetComponent<LineRenderer>().endColor = Color.white;
            if(!PhotonNetwork.IsConnected || PhotonNetwork.IsMasterClient)
                pLayer = Constants.PLAYER_LAYER;
            else
                pLayer = Constants.ENEMY_LAYER;

            GetComponent<Signal>().SetCenter(mouseMapPosition, pLayer);
            mousein = -1;
        }
    }

    void DisplayTime(float timeToDisplay) {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

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
}
