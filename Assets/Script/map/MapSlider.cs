using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSlider : MonoBehaviour
{
    public GameObject selector;
    public Camera mainCamera;
    public GameObject emptyTemplate;
    public GameObject wallTemplate;
    public GameObject spawnTemplate;
    public GameObject energyTemplate;
    public GameObject trapTemplate;
    public GameObject energyGeneratorTemplate;
    public GameObject neutralZoneTemplate;
    private GameObject selectedTemplate;
    private char code;
    private char[,] genMatrix;
    private GameObject [,] map;

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
                    case 'a':
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
                    case 'e':
                        map[x,y] = Instantiate(energyTemplate, new Vector2(j, i), Quaternion.identity);
                        map[x,y].transform.SetParent(this.transform);
                        break;
                    case 'n':
                        map[x,y] = Instantiate(neutralZoneTemplate, new Vector2(j, i), Quaternion.identity);
                        map[x,y].transform.SetParent(this.transform);
                        break;
                    case 'g':
                        map[x,y] = Instantiate(energyGeneratorTemplate, new Vector2(j, i), Quaternion.identity);
                        map[x,y].transform.SetParent(this.transform);
                        break;
                    case 'v':
                        map[x,y] = Instantiate(emptyTemplate, new Vector2(j, i), Quaternion.identity);
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
                
                if(obj.tag == "palette") {
                    switch(obj.name){
                        case ("Energy"):
                            selector.transform.position = obj.transform.position;
                            selectedTemplate = energyTemplate;
                            code = 'e';
                            Debug.Log("e");
                            break;
                        case ("Spawn"):
                            selector.transform.position = obj.transform.position;
                            selectedTemplate = spawnTemplate;
                            code = 's';
                            Debug.Log("s");
                            break;
                        case ("Trap"):
                            selector.transform.position = obj.transform.position;
                            selectedTemplate = trapTemplate;
                            code = 't';
                            Debug.Log("t");
                            break;
                        case ("Wall"):
                            selector.transform.position = obj.transform.position;
                            selectedTemplate = wallTemplate;
                            code = 'w';
                            Debug.Log("w");
                            break;
                    }
                }
                else if(obj.tag == "emptyTile") {
                    Destroy(map[coorX,coorY]);
                    Debug.Log(genMatrix[coorX,coorY] + "On " + coorX + ", " + coorY);
                    if(code != 's'){
                        map[coorX,coorY] = Instantiate(selectedTemplate, new Vector2(obj.transform.position.x, obj.transform.position.y), Quaternion.identity);
                        map[coorX,coorY].transform.SetParent(this.transform);
                        genMatrix[coorX,coorY] = code;
                    }
                    else{
                        setBig(coorX, coorY, genMatrix, code);
                        instantiateBig(coorX, coorY, selectedTemplate, obj.transform.position.x, obj.transform.position.y);
                    }
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