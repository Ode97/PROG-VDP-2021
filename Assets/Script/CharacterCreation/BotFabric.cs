using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class BotFabric : MonoBehaviourPun
{
    public GameObject BodyModel;
    public GameObject EyesModel;
    public GameObject LegModel;
    public GameObject TailModel;
    // Each treshold serve as a modular operator
    public int eyesTresHold = 10;
    public int legTresHold = 15;
    // Sliders for character generation
    public int bodyValue = 15;
    public int eyesValue = 20;
    public int legValue = 15;
    public int tailValue = 15;
    public GameObject creature;
    private GameObject [] legsr;
    private GameObject [] legsl;
    private GameObject [] eyesr;
    private GameObject [] eyesl;
    private GameObject body;
    private GameObject tail;

    // Start is called before the first frame update
    void Start()
    {
        
        /*
        Do the mesh stuff later on
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

        // Read file P1Bot to get all the numeric "Value"s
        if(!PhotonNetwork.IsConnected || photonView.IsMine){
            BotData bot = Save.loadPlayerBotFile();
            if (bot!= null) {
                eyesTresHold = bot.eyesTh;
                legTresHold = bot.legTh;
                eyesValue = bot.eyesV;
                legValue = bot.legV;
                tailValue = bot.tailV;
                bodyValue = bot.bodyV;
                if(PhotonNetwork.IsConnected)
                    send_RPC_nanobot();
            }

            Vector3 itemScale = creature.transform.localScale;
            Quaternion itemRotation = creature.transform.rotation;
            creature.transform.localScale = new Vector3(1,1,1);
            creature.transform.rotation = Quaternion.Euler(0, 0, 0);
            
            int legsNumber = (legValue / legTresHold) + 2;
            int eyesNumber = (eyesValue / eyesTresHold) + 1;

            legsr = new GameObject[legsNumber];
            legsl = new GameObject[legsNumber];
            eyesr = new GameObject[eyesNumber];
            eyesl = new GameObject[eyesNumber];

            body = Instantiate(BodyModel);
            body.transform.SetParent(creature.transform);
            body.transform.localPosition = new Vector3(0f, 0f, 0f);
            body.transform.rotation = Quaternion.identity;
            tail = Instantiate(TailModel); 
            tail.transform.SetParent(creature.transform);
            tail.transform.localPosition = new Vector3(0f, -0.1f, -0.1f);
            tail.transform.rotation = Quaternion.identity;

            for(int i=0; i<legsNumber; i++) {
                legsr[i] = Instantiate(LegModel);
                legsl[i] = Instantiate(LegModel);
                legsr[i].transform.SetParent(creature.transform);
                legsl[i].transform.SetParent(creature.transform);

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
                eyesr[i].transform.SetParent(creature.transform);
                eyesl[i].transform.SetParent(creature.transform);
                eyesr[i].transform.localPosition = new Vector3((1 + i)* 0.04f, 0.25f-(i*0.03f), -0.1f);
                eyesl[i].transform.localPosition = new Vector3((1 + i)*-0.04f, 0.25f-(i*0.03f), -0.1f);
                eyesr[i].transform.rotation = Quaternion.identity;
                eyesl[i].transform.rotation = Quaternion.identity;
            }
            if(legsNumber == 2) {
                legsr[1].transform.localPosition = new Vector3( 0.12f, 0.12f, 0.12f);
                legsl[1].transform.localPosition = new Vector3(-0.12f,-0.12f, 0.12f);
                legsr[1].transform.rotation = Quaternion.Euler(0f, 0f, 210f);
                legsl[1].transform.rotation = Quaternion.Euler(0f, 0f, 30f);
            }
            if(eyesNumber == 3) {
                eyesr[2].transform.localPosition = new Vector3(0.11f,0.24f,-0.1f);
                eyesl[2].transform.localPosition = new Vector3(-0.11f,0.24f,-0.1f);
            }
            else if(eyesNumber == 4) {
                eyesr[2].transform.localPosition = new Vector3(0.11f,0.24f,-0.1f);
                eyesl[2].transform.localPosition = new Vector3(-0.11f,0.24f,-0.1f);
                eyesr[3].transform.localPosition = new Vector3(0.1f,0.24f,-0.05f);
                eyesl[3].transform.localPosition = new Vector3(-0.1f,0.24f,-0.05f);
            }

            creature.transform.localScale = itemScale;
            creature.transform.rotation = itemRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void send_RPC_nanobot(){
        BotData bot = Save.loadPlayerBotFile();
        int[] parts = new int[7];
        parts.SetValue(GetComponentInParent<PhotonView>().ViewID, 0);
        parts.SetValue(bot.eyesTh, 1);
        parts.SetValue(bot.legTh, 2);
        parts.SetValue(bot.eyesV, 3);
        parts.SetValue(bot.legV, 4);
        parts.SetValue(bot.tailV, 5);
        parts.SetValue(bot.bodyV, 6);

        photonView.RPC("RPC_Nanobot", RpcTarget.Others, parts);
    }

    public void RPC_Nanobot(int[] b)
    {
        if(GetComponentInParent<PhotonView>().ViewID == b[0]){
            if (b != null) {
                eyesTresHold = b[1];
                legTresHold = b[2];
                eyesValue = b[3];
                legValue = b[4];
                tailValue = b[5];
                bodyValue = b[6];
            }

            Vector3 itemScale = creature.transform.localScale;
            Quaternion itemRotation = creature.transform.rotation;
            creature.transform.localScale = new Vector3(1,1,1);
            creature.transform.rotation = Quaternion.Euler(0, 0, 0);
            
            int legsNumber = (legValue / legTresHold) + 2;
            int eyesNumber = (eyesValue / eyesTresHold) + 1;

            legsr = new GameObject[legsNumber];
            legsl = new GameObject[legsNumber];
            eyesr = new GameObject[eyesNumber];
            eyesl = new GameObject[eyesNumber];

            body = Instantiate(BodyModel);
            body.transform.SetParent(creature.transform);
            body.transform.localPosition = new Vector3(0f, 0f, 0f);
            body.transform.rotation = Quaternion.identity;
            tail = Instantiate(TailModel); 
            tail.transform.SetParent(creature.transform);
            tail.transform.localPosition = new Vector3(0f, -0.25f, -0.16f);
            tail.transform.rotation = Quaternion.identity;

            for(int i=0; i<legsNumber; i++) {
                legsr[i] = Instantiate(LegModel);
                legsl[i] = Instantiate(LegModel);
                legsr[i].transform.SetParent(creature.transform);
                legsl[i].transform.SetParent(creature.transform);

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
                eyesr[i].transform.SetParent(creature.transform);
                eyesl[i].transform.SetParent(creature.transform);
                eyesr[i].transform.localPosition = new Vector3((1 + i)* 0.04f, 0.25f-(i*0.03f), -0.1f);
                eyesl[i].transform.localPosition = new Vector3((1 + i)*-0.04f, 0.25f-(i*0.03f), -0.1f);
                eyesr[i].transform.rotation = Quaternion.identity;
                eyesl[i].transform.rotation = Quaternion.identity;
            }
            if(legsNumber == 2) {
                legsr[1].transform.localPosition = new Vector3( 0.12f, 0.12f, 0.12f);
                legsl[1].transform.localPosition = new Vector3(-0.12f,-0.12f, 0.12f);
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

            creature.transform.localScale = itemScale;
            creature.transform.rotation = itemRotation;
        }
    }
}
