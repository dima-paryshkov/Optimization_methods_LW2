using System;
using System.Collections.Generic;
using System.Text;
using Лабораторная_работа__2_МО.Интерфейс;

namespace Лабораторная_работа__2_МО.Методы
{
    class Newton
    {
        public int NumberOfIterationsObjectiveFunction = 0;

        Vector xk;

        Vector gradient;

        Matrix Hesse;

        Matrix HesseReverse;

        double Eps;

        DataTable Table;

        public Newton(double Eps)
        {
            this.Eps = Eps;
            Table = new DataTable();
        }

        public Vector minimization(Vector x0, int maxIter = 50000)
        {
            xk = x0;

            int iteration = 0;

            Table.Add(xk, HesseReverse * gradient, 0);

            Vector xklast = xk + 10; // чтобы прошла первая итерация

            while (Function.Value(xk - xklast) < Eps && iteration++ < maxIter)
            {

                double lambda = FibonacciMethod(xk);
                Hesse = Function.HesseMatrix(xk);
                HesseReverse = Hesse ^ -1;
                if (HesseReverse.col1.x < 0) HesseReverse = new Matrix(1, 0, 0, 1);
                gradient = Function.Gradient(xk);
                xklast = xk;
                xk = xk - (HesseReverse * gradient) * lambda;
                Table.Add(xk, HesseReverse * gradient, lambda);
            }
            return xk;
        }

        double oneFunction(double lambda)
        {
            NumberOfIterationsObjectiveFunction++;
            return Function.Value(xk - (HesseReverse * gradient) * lambda);
        }

        double FibonacciMethod(Vector ab, double Eps = 0.001)
        {
            double a = ab.x;
            double b = ab.y;
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
