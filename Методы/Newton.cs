using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Лабораторная_работа__2_МО.Интерфейс;

namespace Лабораторная_работа__2_МО.Методы
{
    class Newton
    {
        public int NumberOfIterationsObjectiveFunction = 0;

        public int iteration = 0;

        public Vector xk;

        Vector gradient;

        Vector S;

        Matrix Hesse;

        Matrix HesseReverse;

        double Eps;

        double EpsN = 1E-7;

        DataTable Table;

        public Newton()
        {
            Table = new DataTable();
            HesseReverse = new Matrix();
        }

        public Vector minimization(Vector x0, double EPS = 1E-7, int maxIter = 50000)
        {
            Table.ClearTable(EPS);
            NumberOfIterationsObjectiveFunction = 0;
            Eps = EPS;
            xk = x0;

            double fk = Function.Value(xk);
            NumberOfIterationsObjectiveFunction++;

            double flast = 0;
            double differencef = Math.Abs(fk - flast);

            Vector ab = new Vector(0, 0);
            iteration = 0;

            Vector xpred = new Vector(0,0);

            while (differencef > Eps || Math.Abs(xk.x - xpred.x) > Eps || Math.Abs(xk.y - xpred.y) > Eps && iteration < maxIter)
            {
                flast = fk;
                xpred = xk;
                Hesse = Function.HesseMatrix(xk);
                HesseReverse = Hesse ^ -1;
                if (HesseReverse.col1.x < 0)
                    HesseReverse = new Matrix(1, 0, 0, 1);
                gradient = Function.Gradient(xk);
                S = HesseReverse * gradient * -1;

                ab = SearchIntervalWithMinimum(ab, xk, EPS);
                double lambda = GoldMethod(ab, xk);

                xk = xk + S * lambda;
                fk = Function.Value(xk);
                NumberOfIterationsObjectiveFunction++;
                iteration++;
                differencef = Math.Abs(fk - flast);
                Table.Add(xk, S, lambda);
            }
            return xk;
        }


        Vector SearchIntervalWithMinimum(Vector c, Vector xk, double Eps)
        {
            double lyampred = c.x;
            double lyamnext = c.y;
            int count_f_1 = 0;
            List<double> ltemp = new List<double>(2);
            List<double> ltemppred = new List<double>(2);
            List<double> ltempnext = new List<double>(2);

            for (int i = 0; i < 2; i++)
            {
                ltemp.Add(0);
                ltemppred.Add(0);
                ltempnext.Add(0);
            }

            double lyam;
            double deltaS = EpsN;
            lyam = 1;


            ltemp[0] = xk.x + lyam * S.x;
            ltemp[1] = xk.y + lyam * S.y;

            ltemppred[0] = xk.x + (lyam - deltaS) * S.x;
            ltemppred[1] = xk.y + (lyam - deltaS) * S.y;

            ltempnext[0] = xk.x + (lyam + deltaS) * S.x;
            ltempnext[1] = xk.y + (lyam + deltaS) * S.y;


            double f = Function.Value(new Vector(ltemp[0], ltemp[1]));
            double fdm = Function.Value(new Vector(ltemppred[0], ltemppred[1]));
            double fdp = Function.Value(new Vector(ltempnext[0], ltempnext[1]));
            NumberOfIterationsObjectiveFunction += 3;
            double fnext;

            if (f > fdp)
            {
                deltaS = deltaS;
                fnext = fdp;
            }
            else if (f > fdm)
            {
                deltaS = -deltaS;
                fnext = fdm;
            }
            else
            {
                lyampred = lyam - deltaS;
                lyamnext = lyam + deltaS;
                return new Vector(lyampred, lyamnext);
            }

            lyamnext = lyam + deltaS;
            double ft = f;

            do
            {
                deltaS = 2 * deltaS;
                lyampred = lyam;
                lyam = lyamnext;
                lyamnext = lyam + deltaS;

                ltemp[0] = xk.x + lyamnext * S.x;
                ltemp[1] = xk.y + lyamnext * S.y;

                f = fnext;
                fnext = Function.Value(new Vector(ltemp[0], ltemp[1]));
                NumberOfIterationsObjectiveFunction++;

            } while (fnext < f);

            if (lyampred > lyamnext)
            {
                double temp = lyampred;
                lyampred = lyamnext;
                lyamnext = temp;
            }
            return new Vector(lyampred, lyamnext);
        }

        double GoldMethod(Vector ab, Vector xk, double Eps = 1E-9)
        {
            double a = ab.x;
            double b = ab.y;
            List<double> s = new List<double>();
            s.Add(S.x);
            s.Add(S.y);

            List<double> ltemp1 = new List<double>();
            List<double> ltemp2 = new List<double>();

            double lyam1 = a + 0.381966011 * (b - a);
            double lyam2 = b - 0.381966011 * (b - a);

            ltemp1.Add(xk.x + lyam1 * s[0]);
            ltemp1.Add(xk.y + lyam1 * s[1]);
            double f1 = Function.Value(new Vector(ltemp1[0], ltemp1[1]));
            NumberOfIterationsObjectiveFunction++;

            ltemp2.Add(xk.x + lyam2 * s[0]);
            ltemp2.Add(xk.y + lyam2 * s[1]);
            double f2 = Function.Value(new Vector(ltemp2[0], ltemp2[1]));
            NumberOfIterationsObjectiveFunction++;

            while (Math.Abs(b - a) > Eps)
            {
                if (f1 < f2)
                {
                    b = lyam2;
                    lyam2 = lyam1;
                    lyam1 = a + 0.381966011 * (b - a);
                    //x1 = a + b - x2;
                    f2 = f1;
                    ltemp1[0] = xk.x + lyam1 * s[0];
                    ltemp1[1] = xk.y + lyam1 * s[1];
                    f1 = Function.Value(new Vector(ltemp1[0], ltemp1[1]));
                    NumberOfIterationsObjectiveFunction++;
                }
                else
                {
                    a = lyam1;
                    lyam1 = lyam2;
                    lyam2 = b - 0.381966011 * (b - a);
                    //x2 = a + b - x1;
                    f1 = f2;

                    ltemp2[0] = xk.x + lyam2 * s[0];
                    ltemp2[1] = xk.y + lyam2 * s[1];
                    f2 = Function.Value(new Vector(ltemp2[0], ltemp2[1]));
                    NumberOfIterationsObjectiveFunction++;
                }
            }
            return a;
        }

        public void OutTable(StreamWriter sw, int mode = 1)
        {
            sw.WriteLine("Number of iterations objective function: " + NumberOfIterationsObjectiveFunction);
            sw.WriteLine("Number of iterations: " + iteration);
            Table.DrawTable(sw, mode);
        }
    }
}
