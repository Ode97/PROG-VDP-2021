using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BotData{
    public int bodyV;
    public int eyesV;
    public int legV;
    public int tailV;
    public int specs;

    public BotData(int bV, int eV, int lV, int tV, int sV){
        bodyV = bV;
        eyesV = eV;
        legV = lV;
        tailV = tV;
        specs = sV;
    }
}