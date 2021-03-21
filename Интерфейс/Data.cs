using System;
using System.Collections.Generic;
using System.Text;

namespace Лабораторная_работа__2_МО.Интерфейс
{
    class Data
    {
        public Vector coor;

        public double funcValue;

        public Vector S;

        public double lambda;

        public double angle;

        public double differenceX;

        public double differenceY;

        public double differenceF;

        public Vector gradient;

        public Matrix matrix;

        public Data(Vector coor, Vector S, double lambda, Data DataLast)
        {
            this.coor = coor;
            funcValue = Function.Value(coor);
            this.S = S;
            this.lambda = lambda;
            gradient = Function.Gradient(coor);
            matrix = Function.HesseMatrix(coor);
            differenceX = coor.x - DataLast.coor.x;
            differenceY = coor.y - DataLast.coor.y;
            differenceF = funcValue - DataLast.funcValue;
            angle = Angle();
        }

        public Data(Vector coor, Vector S, double lambda)
        {
            this.coor = coor;
            funcValue = Function.Value(coor);
            this.S = S;
            this.lambda = lambda;
            gradient = Function.Gradient(coor);
            matrix = Function.HesseMatrix(coor);
            differenceX = 0;
            differenceY = 0;
            differenceF = 0;
            angle = Angle();
        }

        double Angle() 
            => Math.Acos((coor.x * S.x + coor.y * S.y) / (coor.norm() * S.norm() + 1E-10));

    }
}
