using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsBuilder : MonoBehaviour
{
    public GameObject BodyModel;
    public GameObject EyesModel;
    public GameObject LegModel;
    public GameObject TailModel;
    // Each treshold serve as a modular operator
    private int eyesTresHold =10;
    private int legTresHold = 15;
    // Sliders for character generation
    public int bodyValue = 15;
    public int eyesValue = 5;
    public int legValue = 15;
    public int tailValue = 15;
    // maximum values for creator
    private int max_Value = 30;
    private GameObject [] legsr;
    private GameObject [] legsl;
    private GameObject [] eyesr;
    private GameObject [] eyesl;
    private GameObject body;
    private GameObject tail;
    public bool change = true;

    // Start is called before the first frame update
    void Start()
    {
        
        /*
        BodyModel.GetComponent<MeshController>().ScaleX = 1.0f;
        BodyModel.GetComponent<MeshController>().ScaleY = 1.0f;
        BodyModel.GetComponent<MeshController>().ScaleZ = 1.0f;
        EyesLegModel.GetComponent<MeshController>().ScaleX = 1.0f;
        EyesLegModel.GetComponent<MeshController>().ScaleY = 1.0f;
        EyesLegModel.GetComponent<MeshController>().ScaleZ = 1.0f;
        LegModel.GetComponent<MeshController>().ScaleX = 1.0f;
        LegModel.GetComponent<MeshController>().ScaleY = 1.0f;
        LegModel.GetComponent<MeshController>().ScaleZ = 1.0f;
        TailModel.GetComponent<MeshController>().ScaleX = 1.0f;
        TailModel.GetComponent<MeshController>().ScaleY = 1.0f;
        TailModel.GetComponent<MeshController>().ScaleZ = 1.0f;
        
        BodyModel.GetComponent<MeshController>().inGame = true;
        EyesLegModel.GetComponent<MeshController>().inGame = true;
        LegModel.GetComponent<MeshController>().inGame = true;
        TailModel.GetComponent<MeshCotroller>().inGame = true;
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (change){
            int legsNumber = (legValue / legTresHold) + 1;
            int eyesNumber = (eyesValue / eyesTresHold) + 1;
            legsr = new GameObject[legsNumber];
            legsl = new GameObject[legsNumber];
            eyesr = new GameObject[eyesNumber];
            eyesl = new GameObject[eyesNumber];

            body = Instantiate(BodyModel, new Vector3(0f, 0f, 0f), Quaternion.identity);
            tail = Instantiate(TailModel, new Vector3(-1f, 0f, 0f), Quaternion.identity);

            for(int i=0; i<legsNumber; i++){
                legsr[i] = Instantiate(LegModel, new Vector3(0f, i*1f, 0f), Quaternion.identity);
                legsl[i] = Instantiate(LegModel, new Vector3(0f, i*-1f, 0f), Quaternion.identity);
            }
            for(int i=0; i<eyesNumber; i++){
                eyesr[i] = Instantiate(EyesModel, new Vector3(i*0.2f, 0f, 0f), Quaternion.identity);
                eyesl[i] = Instantiate(EyesModel, new Vector3(i*-0.2f, 0f, 0f), Quaternion.identity);
            }
            change = false;
        }
    }
}
