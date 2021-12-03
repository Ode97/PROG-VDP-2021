using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BotData{
    public int eyesTh;
    public int legTh;
    public int bodyV;
    public int eyesV;
    public int legV;
    public int tailV;

    public BotData(int bV, int eV, int lV, int tV, int eTh, int lTh){
        eyesTh = eTh;
        legTh = lTh;
        bodyV = bV;
        eyesV = eV;
        legV = lV;
        tailV = tV;
    }
}