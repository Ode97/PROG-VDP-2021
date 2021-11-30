using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour {   

    public int maxRadius;
    public float ThetaScale = 0.01f;
    public float radius = 3f;
    private int Size;
    public LineRenderer LineDrawer;
    private float Theta = 0f;
    private Rigidbody2D rb;
    private Vector2 Center;
    private List<Collider2D> copy = new List<Collider2D>();
    private bool alreadySignaling = false;
    private GradientColorKey[] colorKey = new GradientColorKey[2];
    private GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];

    void Start() {
        radius = 3f;
        rb = GetComponent<Rigidbody2D>();
        LineDrawer.enabled = false;
        this.enabled = false;
        alreadySignaling = false;

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

            Collider2D[] colliders = Physics2D.OverlapCircleAll(Center, radius);


            if(colliders != null ){
                foreach (Collider2D c in colliders)
                {
                    GameObject nanoBot = c.gameObject;
                    if(nanoBot != null && nanoBot.layer == gameObject.layer && !copy.Contains(c) && !nanoBot.GetComponent<NanoBot>().IsInCombat()){
                        copy.Add(c);
                        nanoBot.GetComponent<NanoBot>().SetTargetPos(rb.position);
                        nanoBot.GetComponent<NanoBot>().DetectSignal();
                        
                    }
                }
            }

            radius += 0.2f;
        }else{
            LineDrawer.enabled = false;
            alreadySignaling = false;
            copy.Clear();
        }
    }

    public bool isSignaling(){
        return alreadySignaling;
    }

    public void SetCenter(){
        Center = new Vector2(0, 0);
        LineDrawer = GetComponent<LineRenderer>();
        LineDrawer.enabled = true;
    }
}