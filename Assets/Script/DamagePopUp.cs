using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer = 1;
    public Color textColor;
    // Start is called before the first frame update
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
    }

    private void Update(){
        float moveYspeed = 10f;
        transform.position += new Vector3(0, moveYspeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0){
            float disappearSpeed = 0.5f;
            textColor.a -= disappearSpeed*Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
                Destroy(gameObject);
        }
    }
}
