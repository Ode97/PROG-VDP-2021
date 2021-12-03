using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int nPlayer = 1;
    private int nEnemy = 1;
    public TextMeshPro p;
    public TextMeshPro e;

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
    }
}
