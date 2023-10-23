using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject thisCell;
    public Material cellMat;
    public Material bombMat;
    // public GameObject prefabCell;
    public GameObject prefabBomb;
    public MapBuilder mapBuilder;
    public bool isFlagged;
    public bool isBomb;
    public bool isOpen = false;
    public int countBomb;
    public int xPos;
    public int yPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver () {
        if(Input.GetMouseButtonDown(1)){
            if (!mapBuilder.isGen){
                mapBuilder.isGen = true;
                mapBuilder.GenerateMap(xPos,yPos);
            }
            if(!isFlagged){
                thisCell.transform.GetChild(2).GetComponent<MeshRenderer>().material=bombMat;
                isFlagged=true;
            }
            else{
                thisCell.transform.GetChild(2).GetComponent<MeshRenderer>().material=cellMat;
                isFlagged=false;
            }
                
        }
        else if(Input.GetMouseButtonDown(0)&&!isFlagged){
            if (!mapBuilder.isGen){
                mapBuilder.isGen = true;
                mapBuilder.GenerateMap(xPos,yPos);
            }
            float x1Pos = thisCell.transform.position.x;
            float z1Pos = thisCell.transform.position.z;
            if(!isOpen){
                isOpen=true;
                if(isBomb){
                    Destroy(thisCell);
                    Instantiate(prefabBomb,new Vector3(x1Pos,0,z1Pos),Quaternion.identity);
                }
                else{
                    if(countBomb==0) 
                        mapBuilder.OpenZeros(xPos,yPos);
                    else
                        mapBuilder.OpenCell(xPos,yPos);
                }
            }
            else if(!isBomb){
                if(mapBuilder.CheckAreaBomb(xPos,yPos)){
                    mapBuilder.OpenCellPlayer(xPos,yPos);
                }
            }
        }
    }
}
