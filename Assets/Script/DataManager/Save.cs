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
}