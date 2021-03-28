using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Лабораторная_работа__2_МО.Интерфейс
{
    class DataTable
    {
        public List<Data> Table;

        double Eps;
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

        public void DrawTable(StreamWriter sw, int mode = 1)
        {
            // 1 - без матрицы гессе
            // 2 - с матрицей гессе
            switch (mode)
            {
                case 1:
                    {
                        sw.WriteLine("Calculation accuracy - " + Eps);
                        sw.WriteLine("{0,3}  {1,32}  {2,10}  {3,24}    {4,10}   {5,10}      {6,10}   {7,10}   {8,10}  {9, 24}",
                                        "i",     "(x, y)", "f(x, y)",   "S",     "lambda", "angle", "delta(X)",  "delta(Y)", "delta(f)",  "Gradient");

                        for (int i = 0; i < Table.Count; i++)
                        {
                            //               i``````````` x              y                       f               s1                s2                lambda              angle         |x(i)-x(i-i1)|      |y(i)-y(i-i1)|      |f(i)-f(i-i1)|               gradient
                            sw.Write("{0,3}  ", i);
                            if (Table[i].coor.x >= 0)        sw.Write("( {0:0.00000000E+00}, ",    Table[i].coor.x);         else sw.Write("({0:0.00000000E+00}, ",      Table[i].coor.x);
                            if (Table[i].coor.y >= 0)        sw.Write(" {0:0.00000000E+00})  ", Table[i].coor.y);         else sw.Write("{0:0.00000000E+00})  ",   Table[i].coor.y);
                            if (Table[i].funcValue >= 0)     sw.Write(" {0:0.0000E+00}  ",      Table[i].funcValue);      else sw.Write("{0:0.0000E+00}  ",        Table[i].funcValue);
                            if (Table[i].S.x >= 0)           sw.Write("( {0:0.0000E+00}, ",        Table[i].S.x);            else sw.Write("({0:0.0000E+00}, ",          Table[i].S.x);
                            if (Table[i].S.y >= 0)           sw.Write(" {0:0.0000E+00})  ",     Table[i].S.y);            else sw.Write("{0:0.0000E+00})  ",       Table[i].S.y);
                            if (Table[i].lambda >= 0)        sw.Write(" {0:0.0000E+00}  ",      Table[i].lambda);         else sw.Write("{0:0.0000E+00}  ",        Table[i].lambda);
                            if (Table[i].angle >= 0)         sw.Write(" {0:0.0000E+00}  ",      Table[i].angle);          else sw.Write("{0:0.0000E+00}  ",        Table[i].angle);
                            if (Table[i].differenceX >= 0)   sw.Write(" {0:0.0000E+00}  ",      Table[i].differenceX);    else sw.Write("{0:0.0000E+00}  ",        Table[i].differenceX);
                            if (Table[i].differenceY >= 0)   sw.Write(" {0:0.0000E+00}  ",      Table[i].differenceY);    else sw.Write("{0:0.0000E+00}  ",        Table[i].differenceY);
                            if (Table[i].differenceF >= 0)   sw.Write(" {0:0.0000E+00}  ",      Table[i].differenceF);    else sw.Write("{0:0.0000E+00}  ",        Table[i].differenceF);
                            if (Table[i].gradient.x >= 0)    sw.Write("( {0:0.0000E+00}, ",        Table[i].gradient.x);     else sw.Write("({0:0.0000E+00}, ",          Table[i].gradient.x);
                            if (Table[i].gradient.y >= 0)    sw.WriteLine(" {0:0.0000E+00})",      Table[i].gradient.y);     else sw.WriteLine("{0:0.0000E+00})",        Table[i].gradient.y);
                        }
                        sw.WriteLine("");
                        sw.WriteLine("______________________________________________________________________________________________________________________________________________________________________________");
                        sw.WriteLine();
                    }
                    break;
                case 2:
                    {
                        sw.WriteLine("Calculation accuracy - " + Eps);
                        sw.WriteLine("{0,3}  {1,32}  {2,10}  {3,24}      {4,10}   {5,10}    {6,10}    {7,10}   {8,10}   {9, 24}  {10, 15}",
                                        "i", "(x, y)", "f(x, y)", "S", "lambda", "angle", "delta(X)", "delta(Y)", "delta(f)", "Gradient", "Matrix Gesse");

                        for (int i = 0; i < Table.Count; i++)
                        {
                            //               i``````````` x              y                       f               s1                s2                lambda              angle         |x(i)-x(i-i1)|      |y(i)-y(i-i1)|      |f(i)-f(i-i1)|               gradient
                            sw.Write("{0,3}  ", i);
                            if (Table[i].coor.x >= 0)           sw.Write("( {0:0.00000000E+00}, ",  Table[i].coor.x);         else sw.Write("({0:0.00000000E+00}, ", Table[i].coor.x);
                            if (Table[i].coor.y >= 0)           sw.Write(" {0:0.00000000E+00})  ", Table[i].coor.y);          else sw.Write("{0:0.00000000E+00})  ", Table[i].coor.y);
                            if (Table[i].funcValue >= 0)        sw.Write(" {0:0.0000E+00} ", Table[i].funcValue);             else sw.Write("{0:0.0000E+00}  ", Table[i].funcValue);
                            if (Table[i].S.x >= 0)              sw.Write("( {0:0.0000E+00}, ", Table[i].S.x);                 else sw.Write("({0:0.0000E+00}, ", Table[i].S.x);
                            if (Table[i].S.y >= 0)              sw.Write(" {0:0.0000E+00})  ", Table[i].S.y);                 else sw.Write("{0:0.0000E+00})  ", Table[i].S.y);
                            if (Table[i].lambda >= 0)           sw.Write(" {0:0.0000E+00}  ", Table[i].lambda);               else sw.Write("{0:0.0000E+00}  ", Table[i].lambda);
                            if (Table[i].angle >= 0)            sw.Write(" {0:0.0000E+00}  ", Table[i].angle);                else sw.Write("{0:0.0000E+00}  ", Table[i].angle);
                            if (Table[i].differenceX >= 0)      sw.Write(" {0:0.0000E+00}  ", Table[i].differenceX);          else sw.Write("{0:0.0000E+00}  ", Table[i].differenceX);
                            if (Table[i].differenceY >= 0)      sw.Write(" {0:0.0000E+00}  ", Table[i].differenceY);          else sw.Write("{0:0.0000E+00}  ", Table[i].differenceY);
                            if (Table[i].differenceF >= 0)      sw.Write(" {0:0.0000E+00}  ", Table[i].differenceF);          else sw.Write("{0:0.0000E+00}  ", Table[i].differenceF);
                            if (Table[i].gradient.x >= 0)       sw.Write("( {0:0.0000E+00}, ", Table[i].gradient.x);          else sw.Write("({0:0.0000E+00}, ", Table[i].gradient.x);
                            if (Table[i].gradient.y >= 0)       sw.Write("{0:0.0000E+00})", Table[i].gradient.y);             else sw.Write("{0:0.0000E+00})", Table[i].gradient.y);
                            if (Table[i].matrix.col1.x >= 0)    sw.Write(" (( {0:0.0000E+00}, ",Table[i].matrix.col1.x);      else sw.Write(" (({0:0.0000E+00}, ", Table[i].matrix.col1.x);
                            if (Table[i].matrix.col1.y >= 0)    sw.WriteLine(" {0:0.0000E+00}), ", Table[i].matrix.col1.y);       else sw.WriteLine("{0:0.0000E+00}), ", Table[i].matrix.col1.y);
                            if (Table[i].matrix.col2.x >= 0)    sw.Write("{0, 173} ( {1:0.0000E+00}, ", " ",Table[i].matrix.col2.x); else sw.Write("{0, 173} ({1:0.0000E+00}, ", " ", Table[i].matrix.col2.x);
                            if (Table[i].matrix.col2.y >= 0)    sw.WriteLine(" {0:0.0000E+00}))", Table[i].matrix.col2.y);    else sw.WriteLine("{0:0.0000E+00}))", Table[i].matrix.col2.y);
                        }
                        sw.WriteLine("");
                        sw.WriteLine("______________________________________________________________________________________________________________________________________________________________________________");
                        sw.WriteLine();

                    }
                    break;
            }
        }

        public void ClearTable(double Eps)
        {
            Table.Clear();
            this.Eps = Eps;
        }

    }
}
