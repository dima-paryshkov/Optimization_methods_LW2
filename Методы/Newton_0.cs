using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Лабораторная_работа__2_МО.Интерфейс;

namespace Лабораторная_работа__2_МО.Методы
{
    class Newton_0
    {
        public int NumberOfIterationsObjectiveFunction = 0;

        Vector xk;

        Vector gradient;

        Vector S;

        Matrix Hesse;

        Matrix HesseReverse;

        double Eps;

        DataTable Table;

        public Newton_0()
        {
            Table = new DataTable();
            HesseReverse = new Matrix();
        }

        public Vector minimization(Vector x0, double EPS = 1E-7, int maxIter = 50000)
        {
            Table.ClearTable(EPS);
            Eps = EPS;
            xk = x0;
            Vector ab = new Vector(10, 10);
            int iteration = 0;

            double fk = Function.Value(xk);

            double fpred = 0;

            bool Flag = true;

            Vector xpred = new Vector(-10, -10);

            while ((Math.Abs(fk - fpred) > Eps /*|| Math.Abs(xk.x - xpred.x) > Eps || Math.Abs(xk.y - xpred.y) > Eps*/) && iteration++ < maxIter)
            {
                fpred = fk;
                Hesse = Function.HesseMatrix(xk);
                HesseReverse = Hesse ^ -1;
                //if (HesseReverse.col1.x < 0 /*|| HesseReverse.col1.x * HesseReverse.col2.y - HesseReverse.col1.y * HesseReverse.col2.x < 0*/)
                 //   HesseReverse = new Matrix(1, 0, 0, 1);
                gradient = Function.Gradient(xk);
                S = HesseReverse * gradient;
                if (S * gradient > 0)
                    gradient *= -1;
                if (Flag) gradient *= -1; Flag = false;
                S = HesseReverse * gradient;
                ab = SearchSegment(ab, xk);
                double lambda = GoldMethod(ab, xk);
                xpred = xk;
                xk = xk + S * lambda;
                Table.Add(xk, S, lambda);
                fk = Function.Value(xk);
            }
            return xk;
        }

        public Vector minimizationSimple(Vector x0, double EPS = 1E-7, int maxIter = 50000)
        {
            Table.ClearTable(EPS);
            Vector xk = x0;
            Vector xpred;
            Vector d = Function.Gradient(xk) * -1;

            double r = GoldMethod2(xk);
            S = r * d;

            xpred = xk;
            xk = xk + S;
            Matrix H, Hrev;
            Vector g;
            double lambda = r;
            while (Math.Abs(Function.Value(xk) - Function.Value(xpred)) > EPS)
            {
                H = Function.HesseMatrix(xk);
                Hrev = H ^ -1;
                g = Function.Gradient(xk);
                S = Hrev * g;
                if (S * g < 0)
                    d = S;
                else
                    d = g * -1;
                lambda = GoldMethod2(xk + lambda * d);
                S = lambda * d;
                Table.Add(xk, this.S, lambda);
            }
            return xk;
        }


        double oneFunction(double lambda)
        {
            NumberOfIterationsObjectiveFunction++;
            return Function.Value(xk - (HesseReverse * gradient) * lambda);
        }

        Vector SearchSegment(Vector ab, Vector xk, double deltaS = 1E-9)
        {

            double lyampred = ab.x;
            double lyamnext = ab.y;
            List<double> ltemp = new List<double>();

            List<double> ltemppred = new List<double>();

            List<double> ltempnext = new List<double>();

            double lyam = 1;

            ltemp.Add(xk.x + lyam * S.x);
            ltemp.Add(xk.y + lyam * S.y);

            ltemppred.Add(xk.x + (lyam - deltaS) * S.x);
            ltemppred.Add(xk.y + (lyam - deltaS) * S.y);

            ltempnext.Add(xk.x + (lyam + deltaS) * S.x);
            ltempnext.Add(xk.y + (lyam + deltaS) * S.y);

            double f = Function.Value(new Vector(ltemp[0], ltemp[1]));
            double fdm = Function.Value(new Vector(ltemppred[0], ltemppred[1]));
            double fdp = Function.Value(new Vector(ltempnext[0], ltempnext[1]));
            
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
                if (lyampred < lyamnext)
                    return new Vector(lyampred, lyamnext);
                else
                    return new Vector(lyamnext, lyampred);
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
            } while (fnext < f);
            if (lyampred < lyamnext)
                return new Vector(lyampred, lyamnext);
            else
                return new Vector(lyamnext, lyampred);
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

            ltemp2.Add(xk.x + lyam2 * s[0]);
            ltemp2.Add(xk.y + lyam2 * s[1]);
            double f2 = Function.Value(new Vector(ltemp2[0], ltemp2[1]));

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
                }
            }
            return (a + b) / 2;
        }

        public void OutTable(StreamWriter sw, int mode = 1)
        {
            Table.DrawTable(sw, mode);
        }

        Vector SearchSegment2(double Xzero, double Delta = 1E-9)
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
                if (fx[0] > oneFunction(x[k] - Delta))
                {
                    x.Add(x[k] - Delta);
                    h = -Delta;
                }
                else
                {
                     return new Vector(x[k] - Delta, x[k] + Delta);
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
            if (a < b)
            {
                double tmp = a;
                a = b;
                b = tmp;
            }
            return new Vector(a, b);
        }

        double GoldMethod2(Vector ab, double Eps = 1E-9)
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
    }
}
