using System;
using System.Collections.Generic;
using System.Text;
using Лабораторная_работа__2_МО.Интерфейс;

namespace Лабораторная_работа__2_МО.Методы.Методы_одномерного_поиска
{
    class FibonacciMethod
    {
        public static double oneFunction(double lambda)
        {
            return Function();
        }

        public static double Do(double a, double b, double Eps = 0.001)
        {
            int NumberOfIterationsObjectiveFunction = 0;
            List<int> F = new List<int>();
            F.Add(1);
            F.Add(1);

            const int N = 1000;
            for (int i = 0; i < N; i++)
                F.Add(F[i] + F[i + 1]);

            double x1;

            double x2;

            double fx1;

            double fx2;

            double difference_ab = b - a;

            int n = 0;
            while (difference_ab / Eps > F[n++ + 2]) ;

            int k = 0;
            double temp = F[n - k];
            double temp_2 = F[n + 2];
            double temp_3 = temp / temp_2;
            x1 = a + temp_3 * difference_ab;
            temp = F[++n - k + 1];
            temp_3 = temp / temp_2;
            x2 = a + temp_3 * difference_ab;



            fx1 = oneFunction(x1);
            fx2 = oneFunction(x2);


            while (difference_ab > Eps)
            {
                k++;
                if (fx1 < fx2)
                {
                    b = x2;
                    x2 = x1;
                    fx2 = fx1;
                    temp = F[n - k];
                    temp_2 = F[n + 2];
                    temp_3 = temp / temp_2;
                    x1 = a + temp_3 * difference_ab;
                    fx1 = oneFunction(x1);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    fx1 = fx2;
                    temp = F[n - k + 1];
                    temp_3 = temp / temp_2;
                    x2 = a + temp_3 * difference_ab;
                    fx2 = oneFunction(x2);
                }
            }
            return (a + b) / 2;
        }

        
    }
}
