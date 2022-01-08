using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MapGeneratorOnline : MonoBehaviour
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

        if(PhotonNetwork.IsMasterClient){
            char [,] playerMap = MapManager.mapMatrix; 
            int mapH = 21;
            int mapW = 41;

            // 20hx40w
            char[,] genMatrix = new char[mapW, mapH];
            for(int x=1;x<mapW-1;x++){
                for(int y=1;y<mapH-1;y++){
                    genMatrix[x,y] = 'v';
                }
            }

            // Place Map Borders
            for(int i=0;i<mapW;i++){
                genMatrix[i,0] = 'w';
                genMatrix[i,mapH-1] = 'w';
            }
            for(int i=0;i<mapH;i++){
                genMatrix[0,i] = 'w';
                genMatrix[mapW-1,i] = 'w';
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
            Debug.Log(rw + " " + rh);
            for(int i=1; i<rh-1;i++){
                for(int j=1; j<rw-1;j++){
                    genMatrix[i,j] = playerMap[i,j];
                }
            }
            instantiateMap(genMatrix);
        }
    }

    void setBig(int x, int y, char[,] map, char tag){
        for(int i=-1;i<2;i++){
            for(int j=-1;j<2;j++){
                map[x-i,y-j] = tag;
            }
        }
    }

    void instantiateMap(char[,] map){
        for(int i = 0; i<map.GetUpperBound(0)+1;i++){
            for(int j=0; j<map.GetUpperBound(1)+1;j++){
                
                GameObject tile;
                switch(map[i,j]){
                    case 'a':
                        tile = PhotonNetwork.Instantiate(spawnATemplate.name, new Vector2(i, j), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    case 'b':
                        tile = PhotonNetwork.Instantiate(spawnBTemplate.name, new Vector2(i, j), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    case 't':
                        tile = PhotonNetwork.Instantiate(trapTemplate.name, new Vector2(i, j), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    case 'w':
                        tile = PhotonNetwork.Instantiate(wallTemplate.name, new Vector2(i, j), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        if(i == 0 || i == 40 || j == 0 || j == 20){
                            tile.tag = "map";
                            tile.transform.GetChild(0).tag = "map";
                        }
                        break;
                    case 'W':
                        tile = PhotonNetwork.Instantiate(wallTemplate.name, new Vector2(i, j), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        if(i == 0 || i == 40 || j == 0 || j == 20){
                            tile.tag = "map";
                            tile.transform.GetChild(0).tag = "map";
                        }
                        break;
                    case 'e':
                        tile = PhotonNetwork.Instantiate(energyTemplate.name, new Vector2(i, j), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    case 'n':
                        tile = PhotonNetwork.Instantiate(neutralZoneTemplate.name, new Vector2(i, j), Quaternion.identity);
                        tile.transform.SetParent(this.transform);
                        break;
                    case 'g':
                        tile = PhotonNetwork.Instantiate(energyGeneratorTemplate.name, new Vector2(i, j), Quaternion.identity);
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
