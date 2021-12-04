using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int nPlayer = 1;
    private int nEnemy = 1;
    public TextMeshPro p;
    public TextMeshPro e;
    public TextMeshPro c;
    public TextMeshPro result;
    public float timeRemaining = 10;
    private bool timerIsRunning = true;

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
        p.transform.GetComponent<TextMeshPro>().color = Color.white;
        e.transform.GetComponent<TextMeshPro>().color = Color.red;
        p.transform.GetComponent<TextMeshPro>().SetText(nPlayer.ToString());
        e.transform.GetComponent<TextMeshPro>().SetText(nEnemy.ToString());
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
                GetComponent<Signal>().enabled = true;
                GetComponent<Signal>().FinalSignal();
            }
        }
    }

    void DisplayTime(float timeToDisplay) {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        c.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
    


    public void newChild(int layer){
        if(layer == Constants.PLAYER_LAYER)
            nPlayer++;
        else
            nEnemy++;

        p.transform.GetComponent<TextMeshPro>().SetText(nPlayer.ToString());
        e.transform.GetComponent<TextMeshPro>().SetText(nEnemy.ToString());
    }

    public void death(int layer){
        if(layer == Constants.PLAYER_LAYER)
            nPlayer--;
        else
            nEnemy--;

        p.transform.GetComponent<TextMeshPro>().SetText(nPlayer.ToString());
        e.transform.GetComponent<TextMeshPro>().SetText(nEnemy.ToString());

        if(nPlayer == 0){
            
            result.transform.GetComponent<TextMeshPro>().SetText("You Lose");
        }else if(nEnemy == 0){
            GetComponentInChildren<Button>().enabled = true;
            result.transform.GetComponent<TextMeshPro>().SetText("You Win");
        }
    }
}
