using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class Saper
{
    public class Space
    {
        public int number;
        public bool isBomb;
        public bool isProtected;
        public Space()
        {
            number = 0;
            isBomb = false;
            isProtected = false;
        }
    }
    public Space[,] matrix;
    public int bombsNum;
    int spaceNum;
    int flunct = 50;
    int rows;
    int columns;
    public Saper(int n, int m, double mode)
    {
        rows = n;
        columns = m;
        matrix = new Space[n, m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                matrix[i, j] = new Space();
        spaceNum = n * m;
        bombsNum = Convert.ToInt32(spaceNum * mode);
    }
    public void Gen(int x, int y)
    {
        int averBombsPerColumn = bombsNum / columns;
        // double rowPercent = 7.5;
        // double columnPercent = 50;
        // double minusPercent = rows / averBombsPerColumn;
        // double plusPercent = rows / (rows - averBombsPerColumn);
        // double percent;
        Random rand = new Random();
        matrix[x, y].isProtected = true;
        for (int i = x - 1; i < x + 2; i++)
        {
            if (i >= 0 && i < rows)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (j >= 0 && j < columns)
                    {
                        if (!(i == x && j == y))
                        {
                            matrix[i, j].isProtected = true;
                        }
                    }
                }
            }
        }
        int fluctNum = 0;
        List<int> rowB;
        int realBombsNum = 0;
        for (int j = 0; j < columns; j++)
        {
            rowB = new List<int>();
            int averB = 0;
            while (averB != averBombsPerColumn)
            {
                int rad = rand.Next(rows);
                if ((!rowB.Contains(rad)) && (!matrix[rad, j].isProtected))
                {
                    averB++;
                    rowB.Add(rad);
                }
            }
            averB = 0;
            if (rand.Next(100) < flunct)
            {
                if (j == columns - 1)
                {
                    if (fluctNum % 2 == 1)
                    {
                        fluctNum++;
                        rowB.Remove(rowB[rowB.Count - 1]);
                    }
                }
                else if (fluctNum % 2 == 0)
                {
                    fluctNum++;
                    while (rowB.Count != 5)
                    {
                        int rad = rand.Next(rows);
                        if (!rowB.Contains(rad))
                            rowB.Add(rad);
                    }
                }
                else
                {
                    fluctNum++;
                    rowB.Remove(rowB[rowB.Count - 1]);
                }
            }
            for (int bom = 0; bom < rowB.Count; bom++)
            {
                matrix[rowB[bom], j].isBomb = true;
                realBombsNum++;
                for (int i1 = rowB[bom] - 1; i1 < rowB[bom] + 2; i1++)
                {
                    if (i1 >= 0 && i1 < rows)
                    {
                        for (int j1 = j - 1; j1 < j + 2; j1++)
                        {
                            if (j1 >= 0 && j1 < columns)
                            {
                                if (!(i1 == rowB[bom] && j1 == j))
                                {
                                    if (!matrix[i1, j1].isBomb)
                                        matrix[i1, j1].number++;
                                }
                            }
                        }
                    }
                }
            }

        }
        bombsNum=realBombsNum;
        // while (realBombsNum < bombsNum)
        // {
        //     for (int i = 0; i < columns; i++)
        //     {
        //         int bombs = 0;
        //         for (int j = 0; j < rows; j++)
        //         {
        //             if (!matrix[j, i].isProtected)
        //             {
        //                 if (realBombsNum < bombsNum)
        //                 {
        //                     int rs = rand.Next(0, 100);
        //                     if (bombs >= averBombsPerColumn)
        //                         columnPercent = 25;
        //                     percent = columnPercent + rowPercent;
        //                     if (rs <= percent)
        //                     {
        //                         matrix[j, i].isBomb = true;
        //                         // //Говноверка
        //                         // if (j == 0)
        //                         // {
        //                         //     if (i == 0)
        //                         //     {
        //                         //         matrix[j, i + 1].number++;
        //                         //         matrix[j + 1, i].number++;
        //                         //         matrix[j + 1, i + 1].number++;
        //                         //     }
        //                         //     else if (i == columns - 1)
        //                         //     {
        //                         //         matrix[j, i - 1].number++;
        //                         //         matrix[j + 1, i].number++;
        //                         //         matrix[j + 1, i - 1].number++;
        //                         //     }
        //                         //     else
        //                         //     {
        //                         //         matrix[j, i - 1].number++;
        //                         //         matrix[j + 1, i].number++;
        //                         //         matrix[j + 1, i - 1].number++;
        //                         //         matrix[j + 1, i + 1].number++;
        //                         //         matrix[j, i + 1].number++;
        //                         //     }
        //                         // }
        //                         // else if (j == rows - 1)
        //                         // {
        //                         //     if (i == 0)
        //                         //     {
        //                         //         matrix[j - 1, i].number++;
        //                         //         matrix[j - 1, i + 1].number++;
        //                         //         matrix[j, i + 1].number++;
        //                         //     }
        //                         //     else if (i == columns - 1)
        //                         //     {
        //                         //         matrix[j, i - 1].number++;
        //                         //         matrix[j - 1, i - 1].number++;
        //                         //         matrix[j - 1, i].number++;
        //                         //     }
        //                         //     else
        //                         //     {
        //                         //         matrix[j, i - 1].number++;
        //                         //         matrix[j - 1, i].number++;
        //                         //         matrix[j - 1, i - 1].number++;
        //                         //         matrix[j - 1, i + 1].number++;
        //                         //         matrix[j, i + 1].number++;
        //                         //     }
        //                         // }
        //                         // else
        //                         // {
        //                         //     if (i == 0)
        //                         //     {
        //                         //         matrix[j - 1, i].number++;
        //                         //         matrix[j - 1, i + 1].number++;
        //                         //         matrix[j, i + 1].number++;
        //                         //         matrix[j + 1, i + 1].number++;
        //                         //         matrix[j + 1, i].number++;
        //                         //     }
        //                         //     else if (i == columns - 1)
        //                         //     {
        //                         //         matrix[j - 1, i].number++;
        //                         //         matrix[j - 1, i - 1].number++;
        //                         //         matrix[j, i - 1].number++;
        //                         //         matrix[j + 1, i - 1].number++;
        //                         //         matrix[j + 1, i].number++;
        //                         //     }
        //                         //     else
        //                         //     {
        //                         //         matrix[j, i - 1].number++;
        //                         //         matrix[j + 1, i].number++;
        //                         //         matrix[j + 1, i - 1].number++;
        //                         //         matrix[j + 1, i + 1].number++;
        //                         //         matrix[j, i + 1].number++;
        //                         //         matrix[j - 1, i].number++;
        //                         //         matrix[j - 1, i + 1].number++;
        //                         //         matrix[j - 1, i - 1].number++;
        //                         //     }
        //                         // }
        //                         // //Говноверка
        //                         for (int i1 = i - 1; i1 < i + 2; i1++)
        //                         {
        //                             if (i1 >= 0 && i1 < rows)
        //                             {
        //                                 for (int j1 = j - 1; j1 < j + 2; j1++)
        //                                 {
        //                                     if (j1 >= 0 && j1 < columns)
        //                                     {
        //                                         if (!(i1 == i && j1 == j))
        //                                         {
        //                                             if(!matrix[j1, i1].isBomb)
        //                                                 matrix[j1, i1].number++;
        //                                         }
        //                                     }
        //                                 }
        //                             }
        //                         }
        //                         rowPercent -= minusPercent;
        //                         realBombsNum++;
        //                         bombs++;
        //                     }
        //                 }
        //                 else
        //                 {
        //                     rowPercent += plusPercent;
        //                 }
        //             }
        //             else
        //             {
        //                 rowPercent += plusPercent;
        //             }
        //         }
        //         if (realBombsNum <= bombsNum)
        //         {
        //             columnPercent = 50;
        //             if (bombs > averBombsPerColumn)
        //                 columnPercent -= 15;
        //             if (bombs < averBombsPerColumn)
        //                 columnPercent += 15;
        //             rowPercent = 7.5;
        //         }
        //         else
        //         {
        //             columnPercent = 0;
        //             rowPercent = 0;
        //         }
        //     }
        // }
        // int realBombsNum = 0;
        // for (int i = 0; i < columns; i++)
        // {
        //     int bombs = 0;
        //     for (int j = 0; j < rows; j++)
        //     {
        //         if (!matrix[j, i].isProtected)
        //         {
        //             int rs = rand.Next(0, 100);
        //             if (bombs >= averBombsPerColumn)
        //                 columnPercent = 10;
        //             percent = columnPercent + rowPercent;
        //             if (rs <= percent)
        //             {
        //                 matrix[j, i].isBomb = true;
        //                 //Говноверка
        //                 if (j == 0)
        //                 {
        //                     if (i == 0)
        //                     {
        //                         matrix[j, i + 1].number++;
        //                         matrix[j + 1, i].number++;
        //                         matrix[j + 1, i + 1].number++;
        //                     }
        //                     else if (i == columns - 1)
        //                     {
        //                         matrix[j, i - 1].number++;
        //                         matrix[j + 1, i].number++;
        //                         matrix[j + 1, i - 1].number++;
        //                     }
        //                     else
        //                     {
        //                         matrix[j, i - 1].number++;
        //                         matrix[j + 1, i].number++;
        //                         matrix[j + 1, i - 1].number++;
        //                         matrix[j + 1, i + 1].number++;
        //                         matrix[j, i + 1].number++;
        //                     }
        //                 }
        //                 else if (j == rows - 1)
        //                 {
        //                     if (i == 0)
        //                     {
        //                         matrix[j - 1, i].number++;
        //                         matrix[j - 1, i + 1].number++;
        //                         matrix[j, i + 1].number++;
        //                     }
        //                     else if (i == columns - 1)
        //                     {
        //                         matrix[j, i - 1].number++;
        //                         matrix[j - 1, i - 1].number++;
        //                         matrix[j - 1, i].number++;
        //                     }
        //                     else
        //                     {
        //                         matrix[j, i - 1].number++;
        //                         matrix[j - 1, i].number++;
        //                         matrix[j - 1, i - 1].number++;
        //                         matrix[j - 1, i + 1].number++;
        //                         matrix[j, i + 1].number++;
        //                     }
        //                 }
        //                 else
        //                 {
        //                     if (i == 0)
        //                     {
        //                         matrix[j - 1, i].number++;
        //                         matrix[j - 1, i + 1].number++;
        //                         matrix[j, i + 1].number++;
        //                         matrix[j + 1, i + 1].number++;
        //                         matrix[j + 1, i].number++;
        //                     }
        //                     else if (i == columns - 1)
        //                     {
        //                         matrix[j - 1, i].number++;
        //                         matrix[j - 1, i - 1].number++;
        //                         matrix[j, i - 1].number++;
        //                         matrix[j + 1, i - 1].number++;
        //                         matrix[j + 1, i].number++;
        //                     }
        //                     else
        //                     {
        //                         matrix[j, i - 1].number++;
        //                         matrix[j + 1, i].number++;
        //                         matrix[j + 1, i - 1].number++;
        //                         matrix[j + 1, i + 1].number++;
        //                         matrix[j, i + 1].number++;
        //                         matrix[j - 1, i].number++;
        //                         matrix[j - 1, i + 1].number++;
        //                         matrix[j - 1, i - 1].number++;
        //                     }
        //                 }
        //                 //Говноверка
        //                 rowPercent -= minusPercent;
        //                 realBombsNum++;
        //                 bombs++;
        //             }
        //             else
        //             {
        //                 rowPercent += plusPercent;
        //             }
        //         }
        //         else
        //         {
        //             rowPercent += plusPercent;
        //         }
        //     }
        //     if (realBombsNum <= bombsNum)
        //     {
        //         columnPercent = 50;
        //         if (bombs > averBombsPerColumn)
        //             columnPercent -= 5;
        //         if (bombs < averBombsPerColumn)
        //             columnPercent += 5;
        //         rowPercent = 7.5;
        //     }
        //     else
        //     {
        //         columnPercent = 0;
        //         rowPercent = 0;
        //     }
        // }
        // bool ischanged = true;
        // while (ischanged)
        // {
        //     ischanged = false;
        //     for (int i = 0; i < columns; i++)
        //     {
        //         for (int j = 0; j < rows; j++)
        //         {
        //             //Говноверка
        //             if ((matrix[j, i].isHidden) && (!matrix[j, i].isBomb))
        //             {
        //                 if (j == 0)
        //                 {
        //                     if (i == 0)
        //                     {
        //                         if (((matrix[j, i + 1].number == 0) && (!matrix[j, i + 1].isHidden)) || ((matrix[j + 1, i].number == 0) && (!matrix[j + 1, i].isHidden)) || ((matrix[j + 1, i + 1].number == 0) && (!matrix[j + 1, i + 1].isHidden)))
        //                         {
        //                             matrix[j, i].isHidden = false;
        //                             ischanged = true;
        //                         }
        //                     }
        //                     else if (i == columns - 1)
        //                     {
        //                         if (((matrix[j, i - 1].number == 0) && (!matrix[j, i - 1].isHidden)) || ((matrix[j + 1, i].number == 0) && (!matrix[j + 1, i].isHidden)) || ((matrix[j + 1, i - 1].number == 0) && (!matrix[j + 1, i - 1].isHidden)))
        //                         {
        //                             matrix[j, i].isHidden = false;
        //                             ischanged = true;
        //                         }
        //                     }
        //                     else
        //                     {
        //                         if (((matrix[j, i - 1].number == 0) && (!matrix[j, i - 1].isHidden)) || ((matrix[j + 1, i].number == 0) && (!matrix[j + 1, i].isHidden)) || ((matrix[j + 1, i + 1].number == 0) && (!matrix[j + 1, i + 1].isHidden)) || ((matrix[j + 1, i - 1].number == 0) && (!matrix[j + 1, i - 1].isHidden)) || ((matrix[j, i + 1].number == 0) && (!matrix[j, i + 1].isHidden)))
        //                         {
        //                             matrix[j, i].isHidden = false;
        //                             ischanged = true;
        //                         }
        //                     }
        //                 }
        //                 else if (j == rows - 1)
        //                 {
        //                     if (i == 0)
        //                     {
        //                         if (((matrix[j - 1, i].number == 0) && (!matrix[j - 1, i].isHidden)) || ((matrix[j - 1, i + 1].number == 0) && (!matrix[j - 1, i + 1].isHidden)) || ((matrix[j, i + 1].number == 0) && (!matrix[j, i + 1].isHidden)))
        //                         {
        //                             matrix[j, i].isHidden = false;
        //                             ischanged = true;
        //                         }
        //                     }
        //                     else if (i == columns - 1)
        //                     {
        //                         if (((matrix[j, i - 1].number == 0) && (!matrix[j, i - 1].isHidden)) || ((matrix[j - 1, i - 1].number == 0) && (!matrix[j - 1, i - 1].isHidden)) || ((matrix[j - 1, i].number == 0) && (!matrix[j - 1, i].isHidden)))
        //                         {
        //                             matrix[j, i].isHidden = false;
        //                             ischanged = true;
        //                         }
        //                     }
        //                     else
        //                     {
        //                         if (((matrix[j, i - 1].number == 0) && (!matrix[j, i - 1].isHidden)) || ((matrix[j - 1, i].number == 0) && (!matrix[j - 1, i].isHidden)) || ((matrix[j - 1, i - 1].number == 0) && (!matrix[j - 1, i - 1].isHidden)) || ((matrix[j - 1, i + 1].number == 0) && (!matrix[j - 1, i + 1].isHidden)) || ((matrix[j, i + 1].number == 0) && (!matrix[j, i + 1].isHidden)))
        //                         {
        //                             matrix[j, i].isHidden = false;
        //                             ischanged = true;
        //                         }
        //                     }
        //                 }
        //                 else
        //                 {
        //                     if (i == 0)
        //                     {
        //                         if (((matrix[j + 1, i].number == 0) && (!matrix[j + 1, i].isHidden)) || ((matrix[j - 1, i].number == 0) && (!matrix[j - 1, i].isHidden)) || ((matrix[j + 1, i + 1].number == 0) && (!matrix[j + 1, i + 1].isHidden)) || ((matrix[j - 1, i + 1].number == 0) && (!matrix[j - 1, i + 1].isHidden)) || ((matrix[j, i + 1].number == 0) && (!matrix[j, i + 1].isHidden)))
        //                         {
        //                             matrix[j, i].isHidden = false;
        //                             ischanged = true;
        //                         }
        //                     }
        //                     else if (i == columns - 1)
        //                     {
        //                         if (((matrix[j - 1, i].number == 0) && (!matrix[j - 1, i].isHidden)) || ((matrix[j - 1, i - 1].number == 0) && (!matrix[j - 1, i - 1].isHidden)) || ((matrix[j, i - 1].number == 0) && (!matrix[j, i - 1].isHidden)) || ((matrix[j + 1, i - 1].number == 0) && (!matrix[j + 1, i - 1].isHidden)) || ((matrix[j + 1, i].number == 0) && (!matrix[j + 1, i].isHidden)))
        //                         {
        //                             matrix[j, i].isHidden = false;
        //                             ischanged = true;
        //                         }
        //                     }
        //                     else
        //                     {
        //                         if (((matrix[j, i - 1].number == 0) && (!matrix[j, i - 1].isHidden)) || ((matrix[j - 1, i].number == 0) && (!matrix[j - 1, i].isHidden)) || ((matrix[j - 1, i - 1].number == 0) && (!matrix[j - 1, i - 1].isHidden)) || ((matrix[j - 1, i + 1].number == 0) && (!matrix[j - 1, i + 1].isHidden)) || ((matrix[j, i + 1].number == 0) && (!matrix[j, i + 1].isHidden)) || ((matrix[j + 1, i - 1].number == 0) && (!matrix[j + 1, i - 1].isHidden)) || ((matrix[j + 1, i].number == 0) && (!matrix[j + 1, i].isHidden)) || ((matrix[j + 1, i + 1].number == 0) && (!matrix[j + 1, i + 1].isHidden)))
        //                         {
        //                             matrix[j, i].isHidden = false;
        //                             ischanged = true;
        //                         }
        //                     }
        //                 }
        //             }
        //             //Говноверка
        //         }
        //     }
        // }
        // bombsNum = realBombsNum;
    }

}


