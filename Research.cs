using System;
using System.IO;
using Лабораторная_работа__2_МО.Интерфейс;
using Лабораторная_работа__2_МО.Методы;

namespace Лабораторная_работа__2_МО
{
    class Research
    {
        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\dimon\OneDrive\NETI\VI семестр\Методы оптимизации\Лабораторная работа №2\Лабораторная работа №2 МО\Result.txt", false, System.Text.Encoding.Default) ;

            Broyden broyden = new Broyden();

            Vector x0 = new Vector(-5, 5);

            Function.Func = 1;

            double Eps = 1E-7;

            //broyden.minimization(x0, Eps);
            //broyden.OutTable(sw);


            Newton Newton = new Newton();

            Newton.minimization(x0, Eps, 10000);
            Newton.OutTable(sw, 2);


            sw.Close();

        }


    }
}
