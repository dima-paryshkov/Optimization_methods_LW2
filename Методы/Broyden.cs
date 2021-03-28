using System;
using System.Collections.Generic;
using System.IO;
using Лабораторная_работа__2_МО.Интерфейс;

namespace Лабораторная_работа__2_МО.Методы
{
    class Broyden
    {
        public int NumberOfIterationsObjectiveFunction = 0;
        
        public int iteration = 0;

        public Vector xk;

        Vector gradient;

        Matrix etta;

        public double Eps;

        DataTable Table;

        public Broyden()
        {
            Table = new DataTable();
        }

        public Vector minimization(Vector x0, double EPS = 1E-7, int maxIter = 50000)
        {
            Table.ClearTable(EPS);
            Eps = EPS;
            xk = x0;
            NumberOfIterationsObjectiveFunction = 0;
            bool flag = true;

            etta = new Matrix(1, 0, 0, 1);

            gradient = Function.Gradient(xk);

            iteration = 0;

            Table.Add(xk, etta * gradient, 0);

            while (flag && iteration++ < maxIter)
            {
                double lambda = GoldMethod(SearchSegment(0));
                Vector xklast = xk;
                xk = xk - (etta * gradient) * lambda;
                Vector gradLast = gradient;
                gradient = Function.Gradient(xk);
                Vector DifferenceOfGradients = gradient - gradLast;
                Vector DifferenceX = xk - xklast;
                Vector temp = DifferenceX - etta * DifferenceOfGradients;

                if (temp.norm() != 0)
                {
                    Matrix Detta = new Matrix(temp.x * temp.x, temp.x * temp.y, temp.x * temp.y, temp.y * temp.y) * (1.0 / (temp * DifferenceOfGradients));
                    etta += Detta;
                    double gradientNorm = gradient.norm();

                    if (Math.Abs(Function.Value(xk) - Function.Value(xklast)) < Eps && Math.Abs(DifferenceX.x - xk.x) < Eps * 2 && Math.Abs(DifferenceX.y - xk.y) < Eps * 2)
                        flag = false;

                    Table.Add(xk, etta * gradient, lambda);

                    if (iteration % 1000 == 0)
                    {
                        etta = new Matrix(1, 0, 0, 1);
                        gradient = Function.Gradient(xk);
                    }
                }
                else
                {
                    flag = false;
                }
            }
            return xk;
        }

        double oneFunction(double lambda)
        {
            NumberOfIterationsObjectiveFunction++;
            return Function.Value(xk - (etta * gradient) * lambda);
        }

        double GoldMethod(Vector ab, double Eps = 0.001)
        {
            double a = ab.x;
            double b = ab.y;
            double difference_ab = Math.Abs(b - a);
            double x1 = a + 0.381966011 * difference_ab;
            double x2 = a + (1 - 0.381966011) * difference_ab;
            double fx1 = oneFunction(x1);
            double fx2 = oneFunction(x2);

            while (difference_ab > Eps)
            {
                if (fx1 < fx2)
                {
                    b = x2;
                    x2 = x1;
                    fx2 = fx1;
                    difference_ab = Math.Abs(b - a);
                    x1 = a + 0.381966011 * difference_ab;
                    fx1 = oneFunction(x1);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    fx1 = fx2;
                    difference_ab = Math.Abs(b - a);
                    x2 = a + (1 - 0.381966011) * difference_ab;
                    fx2 = oneFunction(x2);
                }
                difference_ab = Math.Abs(b - a);
            }
            return (a + b) / 2;
        }

        Vector SearchSegment(double Xzero, double Delta = 1E-5)
        {
            List<double> x = new List<double>();

            List<double> fx = new List<double>();

            double a, b;

            double h = Delta;

            int k = 0;

            x.Add(Xzero);
            fx.Add(oneFunction(x[k]));

            if (fx[0] > oneFunction(x[k] + Delta))
            {
                x.Add(x[k] + Delta);
                h = Delta;
            }
            else
                if ((fx[0] > oneFunction(x[k] - Delta)))
                {
                    x.Add(x[k] - Delta);
                    h = -Delta;
                }
                else
                {
                a = x[0] - Delta;
                b = x[0] + Delta;
                return new Vector(a, b);
                }
            fx.Add(oneFunction(x[k + 1]));
            do
            {
                k++;
                h *= 2;
                x.Add(x[k] + h);
                fx.Add(oneFunction(x[k + 1]));

            } while (fx[k] > fx[k + 1]);

            a = x[k - 1];
            b = x[k + 1];

            return new Vector(a, b);
        }

        public void OutTable(StreamWriter sw, int mode = 1)
        {
            sw.WriteLine("Number of iterations objective function: " + NumberOfIterationsObjectiveFunction);
            sw.WriteLine("Number of iterations: " + iteration);
            Table.DrawTable(sw, mode);
        }
    }
}
