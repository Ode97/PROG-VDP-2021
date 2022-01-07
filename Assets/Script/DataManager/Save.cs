using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
 
 public class Save : MonoBehaviour {
    public static void savePlayerBotFile(BotData bot){
        
        string destination = Application.persistentDataPath + "/" + PlayFabMan.username + "_playerBot.dat";
        FileStream file;

        if(File.Exists(destination)) 
            file = File.OpenWrite(destination);
        else 
            file = File.Create(destination);


        BotData data = bot;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public static BotData loadPlayerBotFile(){
        string destination = Application.persistentDataPath + "/" + PlayFabMan.username + "_playerBot.dat";
        FileStream file;

        if(File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("Save File not found");
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        BotData data = (BotData) bf.Deserialize(file);
        file.Close();
        BotManager.armType = data.bodyV;
        BotManager.atkType = data.tailV;
        BotManager.movType = data.legV;
        BotManager.visType = data.eyesV;
        BotManager.specType = data.specs;
        return data;        
    }
    public static BotData loadEnemyBotFile(int level){
        
        BotData data;
        switch(level){
            case(1):
                data = new BotData(0, 0, 0, 0, 0);
            break;
            case(2):
                data = new BotData(1, 1, 1, 1, 0);
            break;
            case(3):
                data = new BotData(0, 0, 0, 0, 0);
            break;
            case(4):
                data = new BotData(0, 0, 0, 0, 0);
            break;
            case(5):
                data = new BotData(0, 0, 0, 0, 0);
            break;
            default:
                data = new BotData(0, 0, 0, 0, 0);
            break;
        }
        return data;        
    }

    public static void DeleteFile(){
        string destination = Application.persistentDataPath + "/save.dat";

        if(File.Exists(destination)) File.Delete(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }
    }

    public static bool MainMessage(){
        string destination = Application.persistentDataPath + "/openedOnce.dat";

        if(File.Exists(destination)) {
            return true;
        }
        else {
            FileStream file;
            file = File.Create(destination);
            bool data = true;
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
            return false;
        }
    }
}