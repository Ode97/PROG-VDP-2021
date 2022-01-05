using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SceneNavigation : MonoBehaviourPunCallbacks
{
    public Button button;
    public string sceneName = "";
    public static int level;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(changeScene);
    }
    void changeScene(){
        if (sceneName=="lab") SceneHandler.LabScene();
        if (sceneName=="story") {
            level = 0;
            SceneHandler.LevelSelector();
        }
        if (sceneName=="Level1") {
            level = 1;
            SceneHandler.StoryScene();
        }
        if (sceneName=="Level2") {
            level = 2;
            SceneHandler.StoryScene();
        }
        if (sceneName=="Level3") {
            level = 3;
            SceneHandler.StoryScene();
        }
        if (sceneName=="Level4") {
            level = 4;
            SceneHandler.StoryScene();
        }
        if (sceneName=="Level5") {
            level = 5;
            SceneHandler.StoryScene();
        }
        if (sceneName=="battle") SceneHandler.GameScene();
        if (sceneName=="main"){
            if(PhotonNetwork.IsConnected){
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LeaveLobby();
                PhotonNetwork.Disconnect();
            }else
                SceneHandler.MainMenu();
        }
        if (sceneName=="battletest") SceneHandler.BattleTestScreen();
        if (sceneName=="load") SceneHandler.LoadingScreen();
    }

    
public override void OnLeftRoom()
{
    SceneHandler.MainMenu();
 
    base.OnLeftRoom();
}
}
