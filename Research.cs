using System;
using System.IO;
using Лабораторная_работа__2_МО.Интерфейс;
using Лабораторная_работа__2_МО.Методы;

namespace Лабораторная_работа__2_МО
{
    class Research
    {

        static void researh_1()
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\dimon\OneDrive\NETI\VI семестр\Методы оптимизации\Лабораторная работа №2\Лабораторная работа №2 МО\Result.txt", false, System.Text.Encoding.Default);

            Broyden broyden = new Broyden();
            Newton Newton = new Newton();
            Function.Func = 2;
            double Eps = 1E-3;
            Vector x0 = new Vector(-1, -1);

            Console.WriteLine("Eps = " + Eps);

            x0 = new Vector(1.5, 1.6);
            broyden.minimization(x0, Eps);
            Newton.minimization(x0, Eps, 10000);
            Console.WriteLine("x0 = " + x0.x + " " + x0.y);
            Console.WriteLine("Method Broyden:");
            Console.WriteLine("xk = (" + broyden.xk.x + ", " + broyden.xk.y + ")");
            Console.WriteLine("fk = " + Function.Value(broyden.xk));
            Console.WriteLine("Number itererion: " + broyden.iteration);
            Console.WriteLine("Number itererion OF: " + broyden.NumberOfIterationsObjectiveFunction);
            Console.WriteLine("Method Newton:");
            Console.WriteLine("xk = (" + Newton.xk.x + ", " + Newton.xk.y + ")");
            Console.WriteLine("fk = " + Function.Value(Newton.xk));
            Console.WriteLine("Number itererion: " + Newton.iteration);
            Console.WriteLine("Number itererion OF: " + Newton.NumberOfIterationsObjectiveFunction);
            Console.WriteLine("");
            //broyden.OutTable(sw);
            Newton.OutTable(sw, 2);
            Console.WriteLine("");

            x0 = new Vector(-1, -1);
            broyden.minimization(x0, Eps);
            Newton.minimization(x0, Eps, 10000);
            Console.WriteLine("x0 = " + x0.x + " " + x0.y);
            Console.WriteLine("Method Broyden:");
            Console.WriteLine("xk = (" + broyden.xk.x + ", " + broyden.xk.y + ")");
            Console.WriteLine("fk = " + Function.Value(broyden.xk));
            Console.WriteLine("Number itererion: " + broyden.iteration);
            Console.WriteLine("Number itererion OF: " + broyden.NumberOfIterationsObjectiveFunction);
            Console.WriteLine("Method Newton:");
            Console.WriteLine("xk = (" + Newton.xk.x + ", " + Newton.xk.y + ")");
            Console.WriteLine("fk = " + Function.Value(Newton.xk));
            Console.WriteLine("Number itererion: " + Newton.iteration);
            Console.WriteLine("Number itererion OF: " + Newton.NumberOfIterationsObjectiveFunction);
            Console.WriteLine("");
            //broyden.OutTable(sw);
            Newton.OutTable(sw, 2);
            Console.WriteLine("");


            x0 = new Vector(-10, 10);
            broyden.minimization(x0, Eps);
            Newton.minimization(x0, Eps, 10000);
            Console.WriteLine("x0 = " + x0.x + " " + x0.y);
            Console.WriteLine("Method Broyden:");
            Console.WriteLine("xk = (" + broyden.xk.x + ", " + broyden.xk.y + ")");
            Console.WriteLine("fk = " + Function.Value(broyden.xk));
            Console.WriteLine("Number itererion: " + broyden.iteration);
            Console.WriteLine("Number itererion OF: " + broyden.NumberOfIterationsObjectiveFunction);
            Console.WriteLine("Method Newton:");
            Console.WriteLine("xk = (" + Newton.xk.x + ", " + Newton.xk.y + ")");
            Console.WriteLine("fk = " + Function.Value(Newton.xk));
            Console.WriteLine("Number itererion: " + Newton.iteration);
            Console.WriteLine("Number itererion OF: " + Newton.NumberOfIterationsObjectiveFunction);
            //broyden.OutTable(sw);
            Newton.OutTable(sw, 2);
            sw.Close();
        }


        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\dimon\OneDrive\NETI\VI семестр\Методы оптимизации\Лабораторная работа №2\Лабораторная работа №2 МО\Result.txt", false, System.Text.Encoding.Default);

            Broyden broyden = new Broyden();

            Vector x0 = new Vector(-5, 5);

            Function.Func = 5;

            double Eps = 1E-3;
            sw.WriteLine("Method Broyden:");
            broyden.minimization(x0, Eps);
            broyden.OutTable(sw);


            Newton Newton = new Newton();
            sw.WriteLine("Method Newton:");
            Newton.minimization(x0, Eps, 10000);
            Newton.OutTable(sw, 2);

            //researh_1();




            sw.Close();

        }


    }
}
