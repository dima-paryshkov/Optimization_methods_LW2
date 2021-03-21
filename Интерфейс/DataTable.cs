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
                        sw.WriteLine("{0,3}     {1,32}     {2,10}     {3,24}     {4,10}     {5,10}     {6,10}     {7,10}     {8,10}     {9, 24}",
                                        "i",     "(x, y)", "f(x, y)",   "S",     "lambda", "angle", "delta(X)",  "delta(Y)", "delta(f)",  "Gradient");

                        for (int i = 0; i < Table.Count; i++)
                        {
                            //               i``````````` x              y                       f               s1                s2                lambda              angle         |x(i)-x(i-i1)|      |y(i)-y(i-i1)|      |f(i)-f(i-i1)|               gradient
                            sw.WriteLine("{0,3}     ({1:0.00000000E+00}, {2:0.00000000E+00})     {3:0.0000E+00}     ({4:0.0000E+00}, {5:0.0000E+00})     {6:0.0000E+00}     {7:0.0000E+00}     {8:0.0000E+00}     {9:0.0000E+00}     {10:0.0000E+00}     ({11:0.0000E+00}, {12:0.0000E+00})",
                                             i,     Table[i].coor.x, Table[i].coor.y,    Table[i].funcValue, Table[i].S.x,     Table[i].S.y,     Table[i].lambda,   Table[i].angle, Table[i].differenceX, Table[i].differenceY, Table[i].differenceF, Table[i].gradient.x, Table[i].gradient.y);
                        }
                        sw.WriteLine("______________________________________________________________________________________________________________________________________________________________________________");
                        sw.WriteLine();
                    }
                    break;
                case 2:
                    {
                        sw.WriteLine("Calculation accuracy - " + Eps);
                        sw.WriteLine("{0,3}     {1,32}     {2,10}     {3,24}     {4,10}     {5,10}     {6,10}     {7,10}     {8,24}     {9, 24}     {10, 40}",
                                       "i",    "(x, y)",    "f(x, y)", "S",      "lambda", "angle",   "delta(X)", "delta(Y)", "delta(f)", "Gradient", "Matrix Gesse");

                        for (int i = 0; i < Table.Count; i++)
                        {
                            //                   i``````````` x              y                       f               s1                s2                lambda              angle         |x(i)-x(i-i1)|      |y(i)-y(i-i1)|      |f(i)-f(i-i1)|               gradient
                            sw.WriteLine("{0,3}     ({1:0.00000000E+00}, {2:0.00000000E+00})     {3:0.0000E+00}     ({4:0.0000E+00}, {5:0.0000E+00})     {6:0.0000E+00}     {7:0.0000E+00}     {8:0.0000E+00}     {9:0.0000E+00}     {10:0.0000E+00}     ({11:0.0000E+00}, {12:0.0000E+00})     (({13:0.0000E+00}, {14:0.0000E+00}), ({15:0.0000E+00}, {16:0.0000E+00}))",
                                                  i, Table[i].coor.x, Table[i].coor.y, Table[i].funcValue, Table[i].S.x, Table[i].S.y, Table[i].lambda, Table[i].angle, Table[i].differenceX, Table[i].differenceY, Table[i].differenceF, Table[i].gradient.x, Table[i].gradient.y, Table[i].matrix.col1.x, Table[i].matrix.col1.y, Table[i].matrix.col2.x, Table[i].matrix.col2.y);
                        }
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
