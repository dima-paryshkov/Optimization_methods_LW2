using System;
using System.Collections.Generic;
using System.Text;

namespace Лабораторная_работа__2_МО.Интерфейс
{
    static class Function
    {
        //1 - квадратичная функция

        //2 - функция Розенброка

        //3 - функция по варианту

        public static int Func = 2;

        public static double Value(Vector c)
        {
            switch (Func)
            {
                case 1:
                    return 100 * (c.y - c.x) * (c.y - c.x) + (1 - c.x) * (1 - c.x);

                case 2:
                    return 100 * (c.y - c.x * c.x) * (c.y - c.x * c.x) + (1 - c.x) * (1 - c.x);

                case 3:
                    return 3 * Math.Exp(-(c.x - 2) * (c.x - 2) - (c.y - 3) * (c.y - 3) / 4) + Math.Exp(-(c.x - 1) * (c.x - 1) / 4 - (c.y - 1) * (c.y - 1));
            }
            return 0;
        }

        public static Vector Gradient(Vector c)
        {
            switch (Func)
            {
                case 1:
                    return new Vector(202 * c.x - 200 * c.y - 2, -200 * c.x + 200 * c.y);

                case 2:
                    return new Vector(400 * c.x * c.x * c.x - 400 * c.y * c.x + 2 * c.x - 2, 200 * (c.y - c.x * c.x));

                case 3:
                    double x = (-6 * c.x + 8) * Math.Exp(-(c.x - 2) * (c.x - 2) - (c.y - 3) * (c.y - 3) / 4) + (-c.x / 2 + 0.5) * Math.Exp(-(c.x - 1) * (c.x - 1) / 4 - (c.y - 1) * (c.y - 1));
                    double y = (-1.5 * c.y + 1.5) * Math.Exp(-(c.x - 2) * (c.x - 2) - (c.y - 3) * (c.y - 3) / 4) + (2 - 2 * c.y) * Math.Exp(-(c.x - 1) * (c.x - 1) / 4 - (c.y - 1) * (c.y - 1));
                    return new Vector(x,  y);
            }
            return new Vector(0,0);
        }


        public static Matrix HesseMatrix(Vector c)
        {
            switch (Func)
            {
                case 1:
                    return new Matrix(202, -200, -200, 200);

                case 2:
                    return new Matrix(1200 * c.x  * c.x - 400 * c.y + 2, -400 * c.x, -400 * c.x, 200);

                case 3:
                    double E1 = Math.Exp(-(c.x - 2) * (c.x - 2) - (c.y - 3) * (c.y - 3) / 4);
                    double E2 = Math.Exp(-(c.x - 1) * (c.x - 1) / 4 - (c.y - 1) * (c.y - 1));

                    double xx = (12 * (c.x - 2) * (c.x - 2)  - 6) * E1 + ((c.x - 1) * (c.x - 1) / 4 - 0.5) * E2;

                    double xy = 3 * (c.x -2) * (c.y - 3) * E1 + (c.x - 1) * (c.y - 1) * E2; 

                    double yy = (0.75 * (c.y - 3) * (c.y - 3) - 1.5) * E1 + (4 * (c.y - 1) * (c.y - 1) - 2) * E2;

                    return new Matrix(xx, xy, xy, yy);

                default:
                    return new Matrix(1, 0, 0, 1);
            }
            
        }
    }
}
