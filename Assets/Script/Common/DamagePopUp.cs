using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private static float disappearTimerMax = 1;
    private static int sortingOrder = 1;
    public Color textColor;
    private Vector3 moveVector;
    public static DamagePopUp Create(Vector3 pos, float damageAmount, Color color){
        Transform dmgPopUpTransform = Instantiate(GameAssets.i.dmgPopUp, pos, Quaternion.identity);
        DamagePopUp damagePopUp = dmgPopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damageAmount, color);
        return damagePopUp;
    }
    void Awake(){
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(float damageAmount, Color color){
        textMesh.SetText(damageAmount.ToString());
        textMesh.color = color;
        disappearTimer = disappearTimerMax;
        moveVector = new Vector3(0.7f,1, -3) * 10;
        sortingOrder ++;
        textMesh.sortingOrder = sortingOrder;

    }

    private void Update(){
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if(disappearTimer > disappearTimerMax * 0.5){
            float increaseScale = 0.5f;
            transform.localScale += Vector3.one * increaseScale * Time.deltaTime;
        }else{
            float increaseScale = 0.5f;
            transform.localScale -= Vector3.one * increaseScale * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0){
            float disappearSpeed = 0.5f;
            textColor.a -= disappearSpeed*Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0){
                Destroy(gameObject);
            }
        }
    }
}
