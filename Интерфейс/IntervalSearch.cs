using System;
using System.Collections.Generic;
using System.Text;

namespace Лабораторная_работа__1_МО.Интерфейс
{
    class IntervalSearch
    {
        List<double> x;

        List<double> fx;

        Function Func;

        double a, b;

        public IntervalSearch()
        {
            Func = new Function();

            x = new List<double>();

            fx = new List<double>();
        }

        public void Search(double Xzero, double Delta)
        {
            x.Clear();
            fx.Clear();

            double h = Delta;

            int k = 0;

            x.Add(Xzero);
            fx.Add(Func.Value(x[k]));

            if (fx[0] > Func.Value(x[k] + Delta))
            {
                x.Add(x[k] + Delta);
                h = Delta;
            }
            else
                if ((fx[0] > Func.Value(x[k] - Delta)))
            {
                x.Add(x[k] - Delta);
                h = -Delta;
            }
            fx.Add(Func.Value(x[k + 1]));
            do
            {
                k++;
                h *= 2;
                x.Add(x[k] + h);
                fx.Add(Func.Value(x[k + 1]));

            } while (fx[k] > fx[k + 1]);

            a = x[k - 1];
            b = x[k + 1];
        }

        public void ShowResult()
        {
            Console.WriteLine("{0,3}    {1,12}     {2,12}", "i", "x", "f(x)");
            for (int i = 0; i < x.Count; i++)
                Console.WriteLine("{0,3}   {1: 0.0000000000}   {2: 0.0000000000}", i, x[i], fx[i]);
            Console.WriteLine("The interval containing the minimum of the function: [{0:0.00000},{1:0.00000}]", x[x.Count - 3], x[x.Count - 1]);
        }
    }
}
