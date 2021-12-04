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

        char [,] playerMap = MapManager.playerMapMatrix; 
        int mapH = 21;
        int mapW = 41;

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
        // Set map Seed/Type
        // * * * Preset Map
        // Place spawns 3x3
        setBig(10,38,genMatrix,'b');

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

        // Place Energy generators 3x3
        // setBig(10,20,genMatrix,'g');
        
        // * * * Randomized Map
        // Place spawns 3x3
        // Place traps
        // Place walls
        // Place energys
        // Place neutrals
        // Place Energy generators 3x3

        // Draw map with objects

        int rh = playerMap.GetUpperBound(0)+1;
        int rw = playerMap.GetUpperBound(1)+1;
        for(int i=1; i<rh-1;i++){
            for(int j=0; j<rw-1;j++){
                genMatrix[i,j] = playerMap[j,i];
            }
        }
        instantiateMap(genMatrix);
    }

    void setBig(int x, int y, char[,] map, char tag){
        for(int i=-1;i<2;i++){
            for(int j=-1;j<2;j++){
                map[x-i,y-j] = tag;
            }
        }
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
                        break;
                    case 'W':
                        tile = Instantiate(wallTemplate, new Vector2(j, i), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    case 'e':
                        tile = Instantiate(energyTemplate, new Vector2(j, i), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    case 'n':
                        tile = Instantiate(neutralZoneTemplate, new Vector2(j, i), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    case 'g':
                        tile = Instantiate(energyGeneratorTemplate, new Vector2(j, i), Quaternion.identity);
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
