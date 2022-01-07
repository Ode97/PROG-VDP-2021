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
    // Sliders for character generation
    public int bodyValue = 0;
    public int eyesValue = 0;
    public int legValue = 0;
    public int tailValue = 0;
    public int specialValue = 0;
    public GameObject creature;
    private GameObject [] legsr;
    private GameObject [] legsl;
    private GameObject [] eyesr;
    private GameObject [] eyesl;
    private GameObject body;
    private GameObject tail;
    public Material[] legsMat;
    public Material[] bodyMat;
    public bool isPlayer = true;

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
            BotData bot;
            if(isPlayer) bot = Save.loadPlayerBotFile();
            else bot = Save.loadEnemyBotFile(SceneNavigation.level);
            if (bot!= null) {
                eyesValue = bot.eyesV;
                legValue = bot.legV;
                tailValue = bot.tailV;
                bodyValue = bot.bodyV;
                if(PhotonNetwork.IsConnected)
                    send_RPC_nanobot();
            }

            Vector3 itemScale = creature.transform.localScale;
            Quaternion itemRotation = creature.transform.rotation;
            Debug.Log(isPlayer+ ", rot:"+itemRotation);
            creature.transform.localScale = new Vector3(1,1,1);
            creature.transform.rotation = Quaternion.Euler(0, 0, 0);
            int legsNumber = legValue + 2;
            int eyesNumber = eyesValue + 1;

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

            GameObject tObject = System.Array.Find(tail.GetComponentsInChildren<Transform>(), p => p.gameObject.name == "Tail").gameObject;
            if(tailValue == 0) tObject.GetComponent<Renderer> ().material.color = new Color(255f/255f, 66f/255f, 66f/255f);
            else if(tailValue == 1) tObject.GetComponent<Renderer> ().material.color = new Color(255f/255f, 255f/255f, 70f/255f);
            else if(tailValue == 2) tObject.GetComponent<Renderer> ().material.color = new Color(50f/255f, 255f/255f, 50f/255f);
            System.Array.Find(body.GetComponentsInChildren<Transform>(), p => p.gameObject.name == "Body").gameObject.GetComponent<Renderer> ().material = bodyMat[bodyValue];
            for (int i=0; i<legsNumber; i++){
                System.Array.Find(legsr[i].GetComponentsInChildren<Transform>(), p => p.gameObject.name == "Leg").gameObject.GetComponent<Renderer> ().material = legsMat[bodyValue];
                System.Array.Find(legsl[i].GetComponentsInChildren<Transform>(), p => p.gameObject.name == "Leg").gameObject.GetComponent<Renderer> ().material = legsMat[bodyValue];
            }
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
                eyesValue = b[1];
                legValue = b[2];
                tailValue = b[3];
                bodyValue = b[4];
            }

            Vector3 itemScale = creature.transform.localScale;
            Quaternion itemRotation = creature.transform.rotation;
            creature.transform.localScale = new Vector3(1,1,1);
            creature.transform.rotation = Quaternion.Euler(0, 0, 0);
            
            int legsNumber = legValue + 2;
            int eyesNumber = eyesValue + 1;

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
