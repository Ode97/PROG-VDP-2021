using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSlider : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject emptyTemplate;
    public GameObject wallTemplate;
    public GameObject spawnATemplate;
    public GameObject spawnBTemplate;
    public GameObject energyTemplate;
    public GameObject trapTemplate;
    public GameObject energyGeneratorTemplate;
    public GameObject neutralZoneTemplate;
    private char[,] genMatrix;

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
        int mapW = 21;
        // 21hx41w
        genMatrix = new char[mapH, mapW];
        for(int x=1;x<mapH-1;x++){
            for(int y=1;y<mapW;y++){
                genMatrix[x,y] = 'v';
            }
        }

        // Place Map Borders
        for(int i=0;i<mapH;i++){
            genMatrix[i,0] = 'w';
        }
        for(int i=0;i<mapW;i++){
            genMatrix[0,i] = 'w';
            genMatrix[mapH-1,i] = 'w';
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
        float i, j; 
        for(int x=0; x<map.GetUpperBound(0)+1;x++){
            for(int y=0; y<map.GetUpperBound(1)+1;y++){
                GameObject tile;
                j = x + this.transform.position.x;
                i = y + this.transform.position.y;
                switch(map[y,x]){
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
                    case 'v':
                        tile = Instantiate(emptyTemplate, new Vector2(j, i), Quaternion.identity);
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
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mouseMapPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseMapPosition, Vector2.zero);
 
            if(hit.collider != null)
            {
                GameObject obj = hit.collider.gameObject;
                int coorX = (int)obj.transform.position.x;
                int coorY = (int)obj.transform.position.y;
                coorX -= 20;
                coorY += 10;
                if(obj.tag == "emptyTile") {
                    GameObject tile = Instantiate(wallTemplate, new Vector2(obj.transform.position.x, obj.transform.position.y), Quaternion.identity);
                    tile.transform.SetParent(this.transform);
                    genMatrix[coorX,coorY] = 'w';
                }
            }
        }
        // Vector2 coords = getClick();
        // Debug.Log(coords);
    }

    private Vector2 getClick(){
        Vector2 mouseMapPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return mouseMapPosition;
    }
}

