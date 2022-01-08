using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    public int maxEnergy = 30;
    public int maxTraps = 9;
    public int maxWalls = 9;
    public Text maxEnergyLabel;
    public Text maxTrapsLabel;
    public Text maxWallsLabel;
    public GameObject selector;
    public Camera mainCamera;
    public int xoffset = 18;
    public int yoffset = 10;
    public GameObject emptyTemplate;
    public GameObject spawnTemplate;
    private int spawnX = -2;
    private int spawnY = -2;
    public GameObject wallTemplate;
    public GameObject staticWallTemplate;
    public GameObject staticVoidTemplate;
    public GameObject energyTemplate;
    public GameObject trapTemplate;
    public GameObject neutralZoneTemplate;
    private GameObject selectedTemplate;
    private char code;
    private char[,] genMatrix;
    private GameObject [,] map;
    private int mousein = -1;

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
        */

        // Inverted, idk why
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
            genMatrix[i,0] = 'W';
            genMatrix[i,mapW-1] = 'V';
        }
        for(int i=0;i<mapW;i++){
            genMatrix[0,i] = 'W';
            genMatrix[mapH-1,i] = 'W';
        }
        instantiateMap(genMatrix);
        selectedTemplate = wallTemplate;
        code = 'w';
    }

    void setBig(int x, int y, char[,] map, char tag){
        for(int i=-1;i<2;i++){
            for(int j=-1;j<2;j++){
                map[x-i,y-j] = tag;
            }
        }
    }
    void instantiateBig(int x, int y, GameObject tag, float posx, float posy){
        for(int i=-1;i<2;i++){
            for(int j=-1;j<2;j++){
                Destroy(map[x-i,y-j]);
                Vector2 position = new Vector2(posx-i, posy-j);
                map[x-i,y-j] = Instantiate(selectedTemplate, position, Quaternion.identity);
                map[x-i,y-j].transform.SetParent(this.transform);   
            }
        }
    }

    void instantiateMap(char[,] matrix){
        float i, j;
        map = new GameObject[matrix.GetUpperBound(0)+1, matrix.GetUpperBound(1)+1];
        for(int x=0; x<map.GetUpperBound(0)+1;x++){
            for(int y=0; y<map.GetUpperBound(1)+1;y++){
                j = x + this.transform.position.x;
                i = y + this.transform.position.y;
                switch(matrix[y,x]){
                    case 's':
                        map[x,y] = Instantiate(spawnTemplate, new Vector2(j, i), Quaternion.identity);
                        map[x,y].transform.SetParent(this.transform);
                        break;
                    case 't':
                        map[x,y] = Instantiate(trapTemplate, new Vector2(j, i), Quaternion.identity);
                        map[x,y].transform.SetParent(this.transform);
                        break;
                    case 'w':
                        map[x,y] = Instantiate(wallTemplate, new Vector2(j, i), Quaternion.identity);
                        map[x,y].transform.SetParent(this.transform);
                        break;
                    case 'W':
                        map[x,y] = Instantiate(staticWallTemplate, new Vector2(j, i), Quaternion.identity);
                        map[x,y].transform.SetParent(this.transform);
                        break;
                    case 'e':
                        map[x,y] = Instantiate(energyTemplate, new Vector2(j, i), Quaternion.identity);
                        map[x,y].transform.SetParent(this.transform);
                        break;
                    case 'n':
                        map[x,y] = Instantiate(neutralZoneTemplate, new Vector2(j, i), Quaternion.identity);
                        map[x,y].transform.SetParent(this.transform);
                        break;
                    case 'v':
                        map[x,y] = Instantiate(emptyTemplate, new Vector2(j, i), Quaternion.identity);
                        map[x,y].transform.SetParent(this.transform);
                        break;
                    case 'V':
                        map[x,y] = Instantiate(staticVoidTemplate, new Vector2(j, i), Quaternion.identity);
                        map[x,y].transform.SetParent(this.transform);
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
        if (Input.GetMouseButtonDown(0))
            mousein = 0;
        if (Input.GetMouseButtonDown(1)) 
            mousein = 1;
        
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) 
            mousein = -1;
        
        if (mousein != -1) {
            Vector2 mouseMapPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseMapPosition, Vector2.zero);
 
            if(hit.collider != null)
            {
                GameObject obj = hit.collider.gameObject;
                int coorX = (int)obj.transform.position.x;
                int coorY = (int)obj.transform.position.y;
                coorX -= xoffset;
                coorY += yoffset;
                
                if(obj.tag == "palette") {
                    switch(obj.name){
                        case ("SpawnSelect"):
                            selector.transform.position = obj.transform.position;
                            selectedTemplate = spawnTemplate;
                            code = 's';
                            //Debug.Log("e");
                            break;
                        case ("EnergySelect"):
                            selector.transform.position = obj.transform.position;
                            selectedTemplate = energyTemplate;
                            code = 'e';
                            //Debug.Log("e");
                            break;
                        case ("TrapSelect"):
                            selector.transform.position = obj.transform.position;
                            selectedTemplate = trapTemplate;
                            code = 't';
                            //Debug.Log("t");
                            break;
                        case ("WallSelect"):
                            selector.transform.position = obj.transform.position;
                            selectedTemplate = wallTemplate;
                            code = 'w';
                            //Debug.Log("w");
                            break;
                    }
                }
                else if(obj.tag == "emptyTile" ) {
                    if (mousein == 0) {
                        if( (code == 'e' && maxEnergy > 0) ||
                            (code == 't' && maxTraps > 0) ||
                            (code == 'w' && maxWalls > 0) ||
                             code == 's') {
                            if (code == 'e') maxEnergy--;
                            if (code == 't') maxTraps--;
                            if (code == 'w') maxWalls--;
                            if(genMatrix[coorX,coorY] == 'e') maxEnergy++;
                            if(genMatrix[coorX,coorY] == 't') maxTraps++;
                            if(genMatrix[coorX,coorY] == 'w') maxWalls++;
                            if(code != 's' && genMatrix[coorX,coorY] == 's') {
                                spawnX = -2;
                                spawnY = -2;
                            }

                            Destroy(map[coorX,coorY]);
                            if(code != 's'){
                                if(genMatrix[coorX,coorY] == 's') {
                                    spawnX = -2;
                                    spawnY = -2;
                                    BotManager.spawnX = 5;
                                    BotManager.spawnY = 10;
                                }
                                map[coorX,coorY] = Instantiate(selectedTemplate, new Vector2(obj.transform.position.x, obj.transform.position.y), Quaternion.identity);
                                map[coorX,coorY].transform.SetParent(this.transform);
                                genMatrix[coorX,coorY] = code;
                            }
                            else{
                                if (spawnX != -2) {
                                    Destroy(map[spawnX,spawnY]);
                                    map[spawnX,spawnY] = Instantiate(emptyTemplate, new Vector2(obj.transform.position.x, obj.transform.position.y), Quaternion.identity);
                                    map[spawnX,spawnY].transform.SetParent(this.transform);
                                    genMatrix[spawnX,spawnX] = 'v';
                                }
                                map[coorX,coorY] = Instantiate(spawnTemplate, new Vector2(obj.transform.position.x, obj.transform.position.y), Quaternion.identity);
                                map[coorX,coorY].transform.SetParent(this.transform);
                                genMatrix[coorX,coorY] = code;
                                spawnX = coorX;
                                spawnY = coorY;
                                BotManager.spawnX = spawnX;
                                BotManager.spawnY = spawnY;
                            }
                        }
                    }
                    else {
                        if(genMatrix[coorX,coorY] == 'e') maxEnergy++;
                        if(genMatrix[coorX,coorY] == 't') maxTraps++;
                        if(genMatrix[coorX,coorY] == 'w') maxWalls++;
                        Destroy(map[coorX,coorY]);
                        Debug.Log(genMatrix[coorX,coorY] + "On " + coorX + ", " + coorY);
                        map[coorX,coorY] = Instantiate(emptyTemplate, new Vector2(obj.transform.position.x, obj.transform.position.y), Quaternion.identity);
                        map[coorX,coorY].transform.SetParent(this.transform);
                        genMatrix[coorX,coorY] = 'v';
                    }
                }
            }
        }

        maxEnergyLabel.text = "Energy: " +  maxEnergy;
        maxTrapsLabel.text = "Traps: " +  maxTraps;
        maxWallsLabel.text = "Walls: " +  maxWalls;
        // Vector2 coords = getClick();
        // Debug.Log(coords);
        MapManager.playerMapMatrix = genMatrix;
    }

    private Vector2 getClick(){
        Vector2 mouseMapPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return mouseMapPosition;
    }
}