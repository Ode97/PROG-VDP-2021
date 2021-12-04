using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneNavigation : MonoBehaviour
{
    public Button button;
    public string sceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(changeScene);
    }
    void changeScene(){
        if (sceneName=="lab") SceneHandler.LabScene();
        if (sceneName=="story") SceneHandler.StoryScene();
        if (sceneName=="battle") SceneHandler.GameScene();
        if (sceneName=="main") SceneHandler.MainMenu();
    }
}
