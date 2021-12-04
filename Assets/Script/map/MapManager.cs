using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public const int mapH = 21;
    public const int mapW = 41;
    public static char[,] mapMatrix = new char[mapH, mapW];
    public static char[,] playerMapMatrix;
}
