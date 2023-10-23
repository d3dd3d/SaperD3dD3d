using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;

public class MapBuilder : MonoBehaviour
{
    public GameObject prefabCell;
    public GameObject Camera;
    // public GameObject prefabCellOpen;
    public GameObject prefabBomb;
    private GameObject[,] cellMatrix;
    public GameObject counterText;
    public int xSize = 20;
    public int ySize = 10;
    // private GameObject board;
    public bool isGen = false;

    void Start()
    {

        cellMatrix = new GameObject[xSize, ySize];
        // board = Instantiate(new GameObject("BoardEx"), new Vector3(0, 0, 0), Quaternion.identity);
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                GameObject cloneCell = Instantiate(prefabCell, new Vector3(i * 1.0f - 9.5f, 0, j * 1.0f - 4.5f), Quaternion.identity);
                Cell tmpCell = cloneCell.GetComponent<Cell>();
                tmpCell.mapBuilder = this;
                tmpCell.xPos = i;
                tmpCell.yPos = j;
                cellMatrix[i, j] = cloneCell;
                // cloneCell.transform.SetParent(board.transform);
            }
        }
    }
    void Update()
    {

    }

    public void GenerateMap(int xPos, int yPos)
    {
        Saper field = new Saper(xSize, ySize, 0.3);
        field.Gen(xPos, yPos);
        counterText.GetComponent<TextMeshPro>().SetText(field.bombsNum.ToString());
        // Destroy(board);
        // board = Instantiate(new GameObject("BoardEx"), new Vector3(0, 0, 0), Quaternion.identity);

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                Saper.Space cell = field.matrix[i, j];
                // if(xPos==i&&yPos==j){
                //     Destroy(cellMatrix[i,j]);
                //     cellMatrix[i,j] = Instantiate(prefabCellOpen, new Vector3(i * 1.0f - 9.5f, 0, j * 1.0f - 4.5f), Quaternion.identity);
                // }
                // else{
                Cell tmpCell = cellMatrix[i, j].GetComponent<Cell>();
                tmpCell.isBomb = cell.isBomb;
                tmpCell.countBomb = cell.number;
                // }
            }
        }
        OpenZeros(xPos, yPos);
    }
    public void OpenZeros(int xPos, int yPos)
    {
        Stack<GameObject> stack = new Stack<GameObject>();
        stack.Push(cellMatrix[xPos, yPos]);
        while (stack.Count > 0)
        {
            GameObject curCell = stack.Pop();
            Cell tmpCurCell = curCell.GetComponent<Cell>();
            for (int i = tmpCurCell.xPos - 1; i < tmpCurCell.xPos + 2; i++)
            {
                if (i >= 0 && i < xSize)
                {
                    for (int j = tmpCurCell.yPos - 1; j < tmpCurCell.yPos + 2; j++)
                    {
                        if (j >= 0 && j < ySize)
                        {
                            if (!(i == xPos && j == yPos))
                            {
                                Cell tmpCellMatrix = cellMatrix[i, j].GetComponent<Cell>();
                                if (!tmpCellMatrix.isOpen)
                                {
                                    OpenCell(i, j);
                                    if (tmpCellMatrix.countBomb != 0)
                                        cellMatrix[i, j].GetComponentInChildren<TextMeshPro>().SetText(tmpCellMatrix.countBomb.ToString());
                                    else
                                    {
                                        stack.Push(cellMatrix[i, j]);
                                    }
                                }
                            }
                            else
                            {
                                OpenCell(i, j);
                            }
                        }
                    }
                }
            }
        }
    }
    public void OpenCell(int xPos, int yPos)
    {
        cellMatrix[xPos, yPos].transform.GetChild(2).GameObject().SetActive(false);
        if (cellMatrix[xPos, yPos].GetComponent<Cell>().countBomb != 0)
            cellMatrix[xPos, yPos].GetComponentInChildren<TextMeshPro>().SetText(cellMatrix[xPos, yPos].GetComponent<Cell>().countBomb.ToString());
        cellMatrix[xPos, yPos].GetComponent<Cell>().isOpen = true;
    }

    public void OpenBomb(int xPos, int yPos){
        Destroy(cellMatrix[xPos, yPos]);
        Instantiate(prefabBomb,new Vector3(xPos * 1.0f - 9.5f, 0, yPos * 1.0f - 4.5f),Quaternion.identity);
    }

    public void OpenCellPlayer(int xPos, int yPos)
    {
        Cell tmpCurCell = cellMatrix[xPos, yPos].GetComponent<Cell>();
        bool isExplode = false;
        for (int i = tmpCurCell.xPos - 1; i < tmpCurCell.xPos + 2; i++)
        {
            if (i >= 0 && i < xSize)
            {
                for (int j = tmpCurCell.yPos - 1; j < tmpCurCell.yPos + 2; j++)
                {
                    if (j >= 0 && j < ySize)
                    {
                        if (!(i == xPos && j == yPos))
                        {
                            Cell tmpCell = cellMatrix[i, j].GetComponent<Cell>();
                            if(!tmpCell.isFlagged){
                                if(tmpCell.isBomb){
                                    isExplode=true;
                                    OpenBomb(i,j);
                                }
                                if(!isExplode){
                                    if(tmpCell.countBomb!=0)
                                        OpenCell(i,j);
                                    else
                                        OpenZeros(i,j);
                                }
                            }
                               
                        }
                    }
                }
            }
        }
    }

    public bool CheckAreaBomb(int xPos, int yPos){
        Cell tmpCurCell = cellMatrix[xPos, yPos].GetComponent<Cell>();
        int counterF=0;
        for (int i = tmpCurCell.xPos - 1; i < tmpCurCell.xPos + 2; i++)
        {
            if (i >= 0 && i < xSize)
            {
                for (int j = tmpCurCell.yPos - 1; j < tmpCurCell.yPos + 2; j++)
                {
                    if (j >= 0 && j < ySize)
                    {
                        if (!(i == xPos && j == yPos))
                        {
                            if(cellMatrix[i, j].GetComponent<Cell>().isFlagged){
                                counterF++;
                            }
                        }
                    }
                }
            }
        }
        return counterF>=tmpCurCell.countBomb;
    }


}
