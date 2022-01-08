using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsBuilder : MonoBehaviour
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
    private GameObject [] legsr = new GameObject[8];
    private GameObject [] legsl = new GameObject[8];
    private GameObject [] eyesr = new GameObject[8];
    private GameObject [] eyesl = new GameObject[8];
    private GameObject body;
    private GameObject tail;
    public Material[] legsMat;
    public Material[] bodyMat;
    public bool change = true;

    // Start is called before the first frame update
    void Start()
    {
        BotData bot = Save.loadPlayerBotFile();
        if(bot != null){
            bodyValue = bot.bodyV;
            eyesValue = bot.eyesV;
            legValue = bot.legV;
            tailValue = bot.tailV;
            specialValue = bot.specs;
        }else{
            bodyValue = 0;
            eyesValue = 0;
            legValue = 0;
            tailValue = 0;
            specialValue = 0;
        }
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
            int legsNumber = legValue + 2;
            int eyesNumber = eyesValue + 1;

            // Delete previous parts
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
            tail.transform.localPosition = new Vector3(0f, -0.1f, -0.1f);
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

            this.transform.localScale = itemScale;
            this.transform.rotation = itemRotation;
            GameObject tObject = System.Array.Find(tail.GetComponentsInChildren<Transform>(), p => p.gameObject.name == "Tail").gameObject;
            Debug.Log(tailValue);
            if(tailValue == 0) tObject.GetComponent<Renderer> ().material.color = new Color(255f/255f, 66f/255f, 66f/255f);
            else if(tailValue == 1) tObject.GetComponent<Renderer> ().material.color = new Color(255f/255f, 255f/255f, 70f/255f);
            else if(tailValue == 2) tObject.GetComponent<Renderer> ().material.color = new Color(50f/255f, 255f/255f, 50f/255f);
            System.Array.Find(body.GetComponentsInChildren<Transform>(), p => p.gameObject.name == "Body").gameObject.GetComponent<Renderer> ().material = bodyMat[bodyValue];
            for (int i=0; i<legsNumber; i++){
                System.Array.Find(legsr[i].GetComponentsInChildren<Transform>(), p => p.gameObject.name == "Leg").gameObject.GetComponent<Renderer> ().material = legsMat[bodyValue];
                System.Array.Find(legsl[i].GetComponentsInChildren<Transform>(), p => p.gameObject.name == "Leg").gameObject.GetComponent<Renderer> ().material = legsMat[bodyValue];
            }

            BotData bot = new BotData(bodyValue, eyesValue, legValue, tailValue, specialValue);
            Save.savePlayerBotFile(bot);
            change = false;
        }
    }
}
