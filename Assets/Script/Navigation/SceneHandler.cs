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
    public static void GameScene() {
        SceneManager.LoadScene("BattleScene");  
    }
}