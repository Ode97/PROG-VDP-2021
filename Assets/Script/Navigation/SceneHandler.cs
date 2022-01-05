using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public static void MainMenu() {  
        SceneManager.LoadScene("MainMenu");  
    } 
    public static void LabScene() {  
        SceneManager.LoadScene("LabScene");  
    }  
    public static void StoryScene() {
        SceneManager.LoadScene("MapEditor");  
    }
    public static void LevelSelector() {
        SceneManager.LoadScene("LevelSelector");  
    }
    public static void GameScene() {
        SceneManager.LoadScene("BattleScene");  
    }
    public static void GameScene(int id) {
        SceneManager.LoadScene("Level"+id+"Scene");  
    }
    public static void LoseScreen(){
        SceneManager.LoadScene("EndLoseGame");  
    }
    public static void WinScreen(){
        SceneManager.LoadScene("EndWinGame");  
    }
    public static void LobbyScreen(){
        SceneManager.LoadScene("Lobby");  
    }
    public static void LoadingScreen(){
        SceneManager.LoadScene("Loading");  
    }
    public static void BattleTestScreen(){
        SceneManager.LoadScene("BattleTest");  
    }
}