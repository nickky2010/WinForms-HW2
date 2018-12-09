using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsHW2_3
{
    public class Matrix
    {
        int[,] matrix;
        public int Col { get; }
        public int Row { get; }
        public Matrix(int col, int row)
        {
            matrix = new int[col, row];
            Col = col;
            Row = row;
        }
        public int this[int col, int row]
        {
            get
            {
                if (col >= 0 && col < Col &&
                    row >= 0 && row < Row)
                {
                    return matrix[col, row];
                }
                else
                    throw new Exception("Обращение к элементу класса Matrix за пределами массива");
            }
            set
            {
                if (col >= 0 && col < Col &&
                    row >= 0 && row < Row)
                {
                    matrix[col, row] = value;
                }
                else
                    throw new Exception("Установка значения элемента класса Matrix за пределами массива");
            }
        }
        public void ChangeNegCountPos()
        {
            int countPos = 0;
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (matrix[j, i]>0)
                        countPos++;
                }
            }
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (matrix[j, i] < 0)
                        matrix[j, i]=countPos;
                }
            }
        }
    }
}
