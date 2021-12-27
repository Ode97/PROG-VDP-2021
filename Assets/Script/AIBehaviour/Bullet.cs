using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour//, IPunObservable
{
    public Type type;
    public float atkDmg;
    public bool shooted = false;
    private bool splashBullet = false;
    public int view;
    // Start is called before the first frame update
    void Start()
    {
        //atkDmg = GetComponentInParent<NanoBot>().attackDamage;
        send_RPC_dmg(atkDmg, GetComponent<PhotonView>().ViewID);
        StartCoroutine(DestroyBullet());
    }

    void Update(){
        if(type == Type.Acid && shooted){
            float x;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            x = Mathf.Lerp(transform.localScale.x, 56, 10 * Time.deltaTime);
            transform.localScale = new Vector3(x, x, x);
        }
    }

    public void SetDMG(float dmg){
        atkDmg = dmg;
    }

    public float GetDMG(){
        return atkDmg;
    }

    public void SetType(Type t){
        type = t;
    }

    private IEnumerator DestroyBullet(){
        yield return new WaitForSeconds(3);
        if(PhotonNetwork.IsConnected)
            send_RPC_destroy(view);
        else
            Destroy(gameObject);
    }

    public void send_RPC_destroy(int v){
        PhotonView p = PhotonView.Find(v);
        if(p != null)
            p.GetComponent<PhotonView>().RPC("RPC_Destroy", RpcTarget.AllBuffered, v);
    }
    public void send_RPC_dmg(float d, int v){
        PhotonView p = PhotonView.Find(v);
        if(p != null)
            p.GetComponent<PhotonView>().RPC("RPC_Dmg", RpcTarget.Others, d, v);
    }

    [PunRPC]
    public void RPC_Dmg(float d, int v){
        if(v == GetComponent<PhotonView>().ViewID)
            atkDmg = d;
    }

    public void SetSplashBullet(){
        splashBullet = true;
    }

    public bool IsSplashBullet(){
        return splashBullet;
    }

    /*public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info){
        //Debug.Log("a");
        if(stream.IsWriting){
            stream.SendNext(type);
            stream.SendNext(atkDmg);
        }else{
            type = (Type)stream.ReceiveNext();
            atkDmg = (float)stream.ReceiveNext();
        }
    }*/
}
