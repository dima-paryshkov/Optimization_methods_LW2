using System;
using System.Collections.Generic;
using System.Text;

namespace Лабораторная_работа__2_МО.Интерфейс
{
    class DataTable
    {
        public List<Data> Table;

        public DataTable()
        {
            Table = new List<Data>();
        }

        public void Add(Vector coor, Vector S, double lambda)
        {
            if (Table.Count > 1)
                Table.Add(new Data(coor, S, lambda, Table[Table.Count - 1]));
            else 
                Table.Add(new Data(coor, S, lambda));
        }

        public void DrawTable()
        {
            double Eps = 0.0001; 
            Console.WriteLine("Calculation accuracy - " + Eps);
            Console.WriteLine("{0,3} {1,13} {2,13} {3,23} {4,23}    {5,13}     {6,13}      {7,13}    {8, 13}",
                                "i",   "x1",   "x2",   "fx1",  "fx2",  "ai",  "bi",  "bi - ai", "(b-a)_i-1 / (b-a)_i");

            for (int i = 0; i < Table.Count; i++)
            {
                Console.WriteLine("{0,3}   {1: 0.00000000}   {2: 0.00000000}   {3:0.000000000}   {4:0.000000000000000000}       {5:0.00000000}       {6:0.00000000}        {7:0.00000000}        {8:0.00000000}",
                        i, Table[i].x1, Table[i].x2, Table[i].fx1, Table[i].fx2, Table[i].a, Table[i].b, 
                                        Table[i].difference_ab, i == 0 ? 1 : Table[i].ratioOfIterations);
            }
            Console.WriteLine("Result: x = " + (Table[Table.Count - 1].a + Table[Table.Count - 1].b) / 2);
            Console.WriteLine("____________________________________________________________________________________________________________________");
            Console.WriteLine();
        }

        public void ClearTable()
        {
            Table.Clear();
        }

    }
}
