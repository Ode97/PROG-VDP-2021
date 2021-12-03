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
    public const int max_Value = 30;
    private GameObject [] legsr = new GameObject[8];
    private GameObject [] legsl = new GameObject[8];
    private GameObject [] eyesr = new GameObject[8];
    private GameObject [] eyesl = new GameObject[8];
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
            Vector3 itemScale = this.transform.localScale;
            Quaternion itemRotation = this.transform.rotation;
            this.transform.localScale = new Vector3(1,1,1);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (legValue > max_Value) legValue = max_Value;
            if (eyesValue > max_Value) eyesValue = max_Value;
            if (tailValue > max_Value) tailValue = max_Value;
            if (bodyValue > max_Value) bodyValue = max_Value;
            int legsNumber = (legValue / legTresHold) + 2;
            int eyesNumber = (eyesValue / eyesTresHold) + 1;
            Destroy(body);
            Destroy(tail);
            for(int i=0; i<legsr.Length; i++){
                Destroy(legsr[i]);
                Destroy(legsl[i]);
            }
            for(int i=0; i<eyesr.Length; i++){
                Destroy(eyesr[i]);
                Destroy(eyesl[i]);
            }
            legsr = new GameObject[legsNumber];
            legsl = new GameObject[legsNumber];
            eyesr = new GameObject[eyesNumber];
            eyesl = new GameObject[eyesNumber];

            body = Instantiate(BodyModel);
            body.transform.SetParent(this.transform);
            body.transform.localPosition = new Vector3(0f, 0f, 0f);
            body.transform.rotation = Quaternion.identity;
            tail = Instantiate(TailModel); 
            tail.transform.SetParent(this.transform);
            tail.transform.localPosition = new Vector3(0f, -0.25f, -0.16f);
            tail.transform.rotation = Quaternion.identity;

            for(int i=0; i<legsNumber; i++) {
                legsr[i] = Instantiate(LegModel);
                legsl[i] = Instantiate(LegModel);
                legsr[i].transform.SetParent(this.transform);
                legsl[i].transform.SetParent(this.transform);

                if(legsNumber!=4){
                    legsr[i].transform.localPosition = new Vector3(0.12f, (i-1)* 0.12f, 0.12f);
                    legsl[i].transform.localPosition = new Vector3(-0.12f, (i-1)*-0.12f, 0.12f);
                    legsr[i].transform.rotation = Quaternion.Euler(0f, 0f, (-30f + i*30f) + 180f);
                    legsl[i].transform.rotation = Quaternion.Euler(0f, 0f, (-30f + i*30f));
                }
                else {
                    legsr[i].transform.localPosition = new Vector3( 0.12f, (i-1.5f)* 0.10f, 0.12f);
                    legsl[i].transform.localPosition = new Vector3(-0.12f, (i-1.5f)* 0.10f, 0.12f);
                    legsr[i].transform.rotation = Quaternion.Euler(0f, 0f, (-35f + i*20f) + 180f);
                    legsl[i].transform.rotation = Quaternion.Euler(0f, 0f, (35f + i*-20f));
                }
            }
            for(int i=0; i<eyesNumber; i++){
                eyesr[i] = Instantiate(EyesModel);
                eyesl[i] = Instantiate(EyesModel);
                eyesr[i].transform.SetParent(this.transform);
                eyesl[i].transform.SetParent(this.transform);
                eyesr[i].transform.localPosition = new Vector3((1 + i)* 0.04f, 0.25f-(i*0.03f), -0.1f);
                eyesl[i].transform.localPosition = new Vector3((1 + i)*-0.04f, 0.25f-(i*0.03f), -0.1f);
                eyesr[i].transform.rotation = Quaternion.identity;
                eyesl[i].transform.rotation = Quaternion.identity;
            }
            if(legsNumber == 2) {
                legsr[1].transform.position = new Vector3( 0.12f, 0.12f, 0.12f);
                legsl[1].transform.position = new Vector3(-0.12f,-0.12f, 0.12f);
                legsr[1].transform.rotation = Quaternion.Euler(0f, 0f, 210f);
                legsl[1].transform.rotation = Quaternion.Euler(0f, 0f, 30f);
            }
            if(eyesNumber == 3) {
                eyesr[2].transform.localPosition = new Vector3(0.12f,0.19f,-0.07f);
                eyesl[2].transform.localPosition = new Vector3(-0.12f,0.19f,-0.07f);
            }
            else if(eyesNumber == 4) {
                eyesr[2].transform.localPosition = new Vector3(0.12f,0.19f,-0.07f);
                eyesl[2].transform.localPosition = new Vector3(-0.12f,0.19f,-0.07f);
                eyesr[3].transform.localPosition = new Vector3(0.1f,0.24f,-0.05f);
                eyesl[3].transform.localPosition = new Vector3(-0.1f,0.24f,-0.05f);
            }

            this.transform.localScale = itemScale;
            this.transform.rotation = itemRotation;
            BotData bot = new BotData(bodyValue, eyesValue, legValue, tailValue, eyesTresHold, legTresHold);
            Save.savePlayerBotFile(bot);
            change = false;
        }
    }
}
