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
        int mapH = 21;
        int mapW = 41;

        // 20hx40w
        char[,] genMatrix = new char[mapH, mapW];

        if (SceneNavigation.level == 0) genMap1(mapH, mapW, genMatrix);
        if (SceneNavigation.level == 1) genMap1(mapH, mapW, genMatrix);
        if (SceneNavigation.level == 2) genMap2(mapH, mapW, genMatrix);
        if (SceneNavigation.level == 3) genMap3(mapH, mapW, genMatrix);
        if (SceneNavigation.level == 4) genMap4(mapH, mapW, genMatrix);
        if (SceneNavigation.level == 5) genMap5(mapH, mapW, genMatrix);
        
        // Draw player map with objects

        char [,] playerMap = MapManager.playerMapMatrix; 
        int rh = playerMap.GetUpperBound(0)+1;
        int rw = playerMap.GetUpperBound(1)+1;
        for(int i=1; i<rh-1;i++){
            for(int j=0; j<rw-1;j++){
                genMatrix[i,j] = playerMap[j,i];
            }
        }
        instantiateMap(genMatrix);
    }

    void genMap1(int mapH, int mapW, char[,] genMatrix) {
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
        // Set map Seed/Type
        // * * * Preset Map
        // Place spawns 3x3

        // Place traps
        for(int y=1;y<4;y++){
            genMatrix[y,30] = 't';
        }

        for(int y=9;y<12;y++){
            genMatrix[y,30] = 't';
        }

        for(int y=17;y<20;y++){
            genMatrix[y,30] = 't';
        }

        // Place walls
        for(int y=1;y<4;y++){
            genMatrix[y,31] = 'w';
        }

        for(int y=9;y<12;y++){
            genMatrix[y,31] = 'w';
        }

        for(int y=17;y<20;y++){
            genMatrix[y,31] = 'w';
        }

        // Place energys  4 13 15 17
        for(int x = 33; x > 31; x--){
            for(int y=1;y<4;y++){
                genMatrix[y,x] = 'e';
            }
            for(int y=9;y<12;y++){
                genMatrix[y,x] = 'e';
            }
            for(int y=17;y<20;y++){
                genMatrix[y,x] = 'e';
            }
        }

        for(int y=5;y<8;y++){
                genMatrix[y,30] = 'e';
        }
        for(int y=9;y<12;y++){
                genMatrix[y,28] = 'e';
        }
        for(int y=13;y<16;y++){
                genMatrix[y,30] = 'e';
        }
        
        for(int x = 36; x > 33; x--){
                genMatrix[10,x] = 'e';
        }
    }
    void genMap2(int mapH, int mapW, char[,] genMatrix){
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
        // Set map Seed/Type
        // * * * Preset Map
        // Place spawns 3x3

        // Place traps
        for(int y=1;y<4;y++){
            genMatrix[y,30] = 't';
        }

        for(int y=9;y<12;y++){
            genMatrix[y,30] = 't';
        }

        for(int y=17;y<20;y++){
            genMatrix[y,30] = 't';
        }

        // Place walls
        for(int y=1;y<4;y++){
            genMatrix[y,31] = 'w';
        }

        for(int y=9;y<12;y++){
            genMatrix[y,31] = 'w';
        }

        for(int y=17;y<20;y++){
            genMatrix[y,31] = 'w';
        }

        // Place energys  4 13 15 17
        for(int x = 33; x > 31; x--){
            for(int y=1;y<4;y++){
                genMatrix[y,x] = 'e';
            }
            for(int y=9;y<12;y++){
                genMatrix[y,x] = 'e';
            }
            for(int y=17;y<20;y++){
                genMatrix[y,x] = 'e';
            }
        }

        for(int y=5;y<8;y++){
                genMatrix[y,30] = 'e';
        }
        for(int y=9;y<12;y++){
                genMatrix[y,28] = 'e';
        }
        for(int y=13;y<16;y++){
                genMatrix[y,30] = 'e';
        }
        
        for(int x = 36; x > 33; x--){
                genMatrix[10,x] = 'e';
        }
    }
    void genMap3(int mapH, int mapW, char[,] genMatrix){
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
        // Set map Seed/Type
        // * * * Preset Map
        // Place spawns 3x3

        // Place traps
        for(int y=8;y<12;y++){
            genMatrix[y,21] = 't';
            genMatrix[y,22] = 't';
            genMatrix[y,23] = 't';
        }

        // Place walls
        for(int x=21;x<25;x++){
            genMatrix[7,x] = 'w';
            genMatrix[12,x] = 'w';
        }
        for(int y=7;y<13;y++){
            genMatrix[y,24] = 'w';
        }

        // Place energys  4 13 15 17
        for(int x = 21; x < 36; x++){
            genMatrix[16,x] = 'e';
            genMatrix[17,x] = 'e';
        }
    }
    void genMap4(int mapH, int mapW, char[,] genMatrix){
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
        // Set map Seed/Type
        // * * * Preset Map
        // Place spawns 3x3

        // Place traps
        for(int y=16;y<20;y++){
            genMatrix[y,27] = 't';
        }

        for(int y=16;y<19;y++){
            genMatrix[y,28] = 't';
        }

        for(int y=16;y<18;y++){
            genMatrix[y,29] = 't';
        }

        genMatrix[16,30] = 't';

        // Place walls
        genMatrix[19,28] = 'w';
        genMatrix[18,29] = 'w';
        genMatrix[17,30] = 'w';
        genMatrix[16,31] = 'w';
        genMatrix[15,32] = 'w';
        genMatrix[14,33] = 'w';
        genMatrix[13,34] = 'w';
        genMatrix[12,35] = 'w';
        genMatrix[11,36] = 'w';
        genMatrix[10,37] = 'w';
        genMatrix[9,38] = 'w';
        genMatrix[8,39] = 'w';

        

        // Place energys  4 13 15 17
        for(int x = 34; x < 38; x++){
            for(int y=14;y<20;y++){
                genMatrix[y,x] = 'e';
            } 
        }

        for(int y=15;y<18;y++){
            genMatrix[y,38] = 'e';
            genMatrix[y,39] = 'e';
        }
    
    }
    void genMap5(int mapH, int mapW, char[,] genMatrix){
        genMap1(mapH, mapW, genMatrix);
    }

    void instantiateMap(char[,] map){
        for(int i=0; i<map.GetUpperBound(0)+1;i++){
            for(int j=0; j<map.GetUpperBound(1)+1;j++){
                GameObject tile;
                switch(map[i,j]){
                    case 'a':
                        tile = Instantiate(spawnATemplate, new Vector2(j, i), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    case 'b':
                        tile = Instantiate(spawnBTemplate, new Vector2(j, i), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    case 't':
                        tile = Instantiate(trapTemplate, new Vector2(j, i), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    case 'w':
                        tile = Instantiate(wallTemplate, new Vector2(j, i), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        if(j == 0 || j == 40 || i == 0 || i == 20){
                            tile.tag = "map";
                            tile.transform.GetChild(0).tag = "map";
                        }
                        break;
                    case 'W':
                        tile = Instantiate(wallTemplate, new Vector2(j, i), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        if(j == 0 || j == 40 || i == 0 || i == 20){
                            tile.tag = "map";
                            tile.transform.GetChild(0).tag = "map";
                        }
                        break;
                    case 'e':
                        tile = Instantiate(energyTemplate, new Vector2(j, i), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    case 'n':
                        tile = Instantiate(neutralZoneTemplate, new Vector2(j, i), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    default:
                        break;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
