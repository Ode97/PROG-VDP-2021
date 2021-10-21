using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour {   

    public float ThetaScale = 0.01f;
    public float radius = 3f;
    private int Size;
    public LineRenderer LineDrawer;
    private float Theta = 0f;
    private Rigidbody2D rb;
    private Vector2 Center;
    private List<Collider2D> copy = new List<Collider2D>();
    private bool alreadySignaling = false;

    void Start() {
        radius = 3f;
        rb = GetComponent<Rigidbody2D>();
        LineDrawer.enabled = false;
        this.enabled = false;
        alreadySignaling = false;
    }

    void Update() {
        if(radius < 20){
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
                    if(c != GetComponent<Collider2D>() && nanoBot.layer == gameObject.layer && !nanoBot.GetComponent<Nanobot>().HasGoal() && !copy.Contains(c)){
                        copy.Add(c);
                        Vector2 v = Center - nanoBot.GetComponent<Rigidbody2D>().position;
                        nanoBot.GetComponent<Rigidbody2D>().velocity = v.normalized * 5;
                        nanoBot.GetComponent<Nanobot>().orientation = nanoBot.GetComponent<KinematicSeekBehaviour>().NewOrientation(nanoBot.GetComponent<Nanobot>().orientation, nanoBot.GetComponent<Rigidbody2D>().velocity);
                        nanoBot.GetComponent<Rigidbody2D>().rotation = nanoBot.GetComponent<Nanobot>().orientation * 180 / Mathf.PI; 
                        nanoBot.GetComponent<Nanobot>().SetGoal(true);
                        StartCoroutine(searchingTime(nanoBot));
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

    private IEnumerator searchingTime(GameObject n){
        yield return new WaitForSeconds(3);
        if(n != null)
            n.GetComponent<Nanobot>().SetGoal(false);
    }

    public void SetCenter(Vector2 center){
        Center = center;
        LineDrawer = GetComponent<LineRenderer>();
        LineDrawer.enabled = true;
    }
}