using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Signal : MonoBehaviour {   

    public int maxRadius;
    public float ThetaScale = 0.01f;
    public float radius = 3f;
    private int Size;
    public LineRenderer LineDrawer;
    private float Theta = 0f;
    private Rigidbody2D rb;
    private Vector3 Center;
    private List<Collider2D> copy = new List<Collider2D>();
    private bool alreadySignaling = false;
    private GradientColorKey[] colorKey = new GradientColorKey[2];
    private GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
    private bool final = false;
    private bool player = false;
    public int playerLayer = Constants.PLAYER_LAYER;

    void Start() {
        radius = 3f;
        rb = GetComponent<Rigidbody2D>();
        LineDrawer.enabled = false;
        this.enabled = false;
        alreadySignaling = false;
        if(!PhotonNetwork.IsConnected)
            playerLayer = Constants.PLAYER_LAYER;
    }

    void Update() {
        if(radius < maxRadius){

            alreadySignaling = true;
            Theta = 0f;
            Size = (int)((1f / ThetaScale) + 1f);
            LineDrawer.SetVertexCount(Size);
            for (int i = 0; i < Size; i++) {
                Theta += (2.0f * Mathf.PI * ThetaScale);
                float x = (radius * Mathf.Cos(Theta)) + Center.x;
                float y = (radius * Mathf.Sin(Theta)) + Center.y;
                LineDrawer.SetPosition(i, new Vector3(x, y, 0));
            }

            List<Collider2D> colliders = new List<Collider2D>();
            colliders.AddRange(Physics2D.OverlapCircleAll(Center, radius));
            Vector2 p = new Vector2();
            if(!final && !player){
                colliders = colliders.FindAll(c => c != null && c.gameObject.layer == gameObject.layer && c.gameObject != gameObject);
                p = rb.position; 
            }else if(player){
                colliders = colliders.FindAll(c => c != null && c.gameObject.layer == playerLayer);
                p = Center;
            }else{
                colliders = colliders.FindAll(c => c != null && (c.gameObject.layer == Constants.PLAYER_LAYER ||c.gameObject.layer == Constants.ENEMY_LAYER));
                p = new Vector2(20, 10);
            }

            if(colliders != null ){
                foreach (Collider2D c in colliders)
                {
                    GameObject nanoBot = c.gameObject;
                    if(!copy.Contains(c)){
                        copy.Add(c);
                        nanoBot.GetComponent<NanoBot>().SetTargetPos(p);
                        nanoBot.GetComponent<NanoBot>().DetectSignal();
                        
                    }
                }
            }

            radius += 0.1f;
        }else{
            if(final)
                final = false;
            if(player)
                player = false;

            LineDrawer.enabled = false;
            alreadySignaling = false;
            copy.Clear();
        }
    }

    public bool isSignaling(){
        return alreadySignaling;
    }

    public void SetCenter(){
        Center = new Vector2(GetComponent<NanoBot>().GetTargetPos().x, GetComponent<NanoBot>().GetTargetPos().y);
        LineDrawer = GetComponent<LineRenderer>();
        LineDrawer.enabled = true;
    }

    public void SetCenter(Vector2 pos, int l){
        Center = new Vector2(pos.x, pos.y);
        player = true;
        radius = 3;
        Debug.Log(final + " " + player);
        playerLayer = l;
        LineDrawer = GetComponent<LineRenderer>();
        LineDrawer.enabled = true;
    }

    public void FinalSignal(){
        Center = new Vector2(20, 10);
        radius = 3;
        LineDrawer = GetComponent<LineRenderer>();
        LineDrawer.enabled = true;
        final = true;
    }
}