using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialScene : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeScene());
    }

    // Update is called once per frame
    private IEnumerator ChangeScene(){
        yield return new WaitForSeconds(3);
        if (sceneName== "login") SceneHandler.LoginScreen();
        if (sceneName== "polimi") SceneHandler.PolimiScreen();
    
    }
}
