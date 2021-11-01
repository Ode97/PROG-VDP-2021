using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject wallTemplate;
    public GameObject spawnATemplate;
    public GameObject spawnBTemplate;
    public GameObject energyTemplate;
    public GameObject trapTemplate;
    public GameObject energyGeneratorTemplate;
    public GameObject neutralZoneTemplate;

    // Start is called before the first frame update
    void Start()
    {
        /*
        v - void
        a - spawn a
        b - spawn b
        t - trap
        w - wall
        e - energy
        n - neutral
        g - e. generator
        */

        int mapH = 20;
        int mapW = 40;
        // 20hx40w
        char[,] genMatrix = new char[mapH, mapW];
        for(int x=1;x<mapH-1;x++){
            for(int y=1;y<mapW-1;y++){
                genMatrix[x,y] = 'v';
            }
        }

        // Place Map Borders
        for(int i=0;i<mapH;i++){
            genMatrix[i,0] = 'w';
            genMatrix[i,mapW-1] = 'w';
        }
        for(int i=0;i<mapW;i++){
            genMatrix[0,i] = 'w';
            genMatrix[mapH-1,i] = 'w';
        }
        
        for(int x=0;x<mapH;x++){
            for(int y=0;y<mapW;y++){
                Debug.Log(genMatrix[x,y]);
            }
        }
        // Set map Seed/Type
        
        // * * * Preset Map
        // Place spawns 3x3
        setBig(10, 2,genMatrix,'a');
        setBig(10,38,genMatrix,'b');

        // Place traps
        genMatrix[10,7] = 't';
        genMatrix[10,11] = 't';

        // Place walls
        for(int y=8;y<12;y++){
            genMatrix[9,y] = 'w';
            genMatrix[31,y] = 'w';
        }

        // Place energys  4 13 15 17
        genMatrix[10,4] = 'e';
        genMatrix[10,13] = 'e';
        genMatrix[10,15] = 'e';
        genMatrix[10,17] = 'e';

        // Place neutrals 1-18 19-22
        for(int x=1;x<20;x++){
            for(int y=18;y<23;y++){
                genMatrix[x,y] = 'n';
            }
        }
        // Place Energy generators 3x3
        setBig(10,20,genMatrix,'g');

        
        // * * * Randomized Map
        // Place spawns 3x3
        // Place traps
        // Place walls
        // Place energys
        // Place neutrals
        // Place Energy generators 3x3

        // Draw map with objects

    }

    void setBig(int x, int y, char[,] map, char tag){
        for(int i=-1;i<2;i++){
            for(int j=-1;j<2;j++){
                map[x-i,y-j] = tag;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
