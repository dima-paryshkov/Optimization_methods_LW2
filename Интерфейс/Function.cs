using System;
using System.Collections.Generic;
using System.Text;

namespace Лабораторная_работа__2_МО.Интерфейс
{
    static class Function
    {
        //1 - квадратичная функция

        //2 - функция Розенброка

        //3 - функция по варианту мод

        //4 - функция другого варинта (для проверки)

        //5 - функция по варианту

        public static int Func = 4;

        public static double Value(Vector c)
        {
            switch (Func)
            {
                case 1:
                    return 100 * (c.y - c.x) * (c.y - c.x) + (1 - c.x) * (1 - c.x);

                case 2:
                    return 100 * (c.y - c.x * c.x) * (c.y - c.x * c.x) + (1 - c.x) * (1 - c.x);

                case 3:
                    return 3 / (1 + (c.x - 2) * (c.x - 2) + (c.y - 3) * (c.y - 3) / 4) + 1 / (1 + (c.x - 1) * (c.x - 1) / 4 + (c.y - 1) * (c.y - 1));

                case 4:
                    return -(3.0 / (1 + (c.x - 3) * (c.x - 3) / 4 + (c.y - 2) * (c.y - 2)) + 1.0 / (1 + (c.x - 3) * (c.x - 3) + (c.y - 1) * (c.y - 1) / 9));
                
                case 5:
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
                    double E1 = (c.y - 1) * (c.y - 1) + (c.x - 1) * (c.x - 1) / 4 + 1;
                    double E2 = (c.y - 3) * (c.y - 3) / 4 + (c.x - 2) * (c.x - 2) + 1;
                    return new Vector((0.5 - c.x / 2) / (E1 * E1) + (12 - 6 * c.x) / (E2 * E2), 3 * (1.5 - c.y / 2) / (E2 * E2) + (2 - 2 * c.y) / (E1 * E1));

                case 4:
                    double f1 = (c.x - 3) * (c.x - 3) + (c.y - 1) * (c.y - 1) / 9 + 1;
                    double f2 = (c.x - 3) * (c.x - 3) / 4 + (c.y - 2) * (c.y - 2) + 1;
                    double f3 = 9 * c.x * c.x - 54 * c.x + c.y * c.y - 2 * c.y + 91;
                    return new Vector(-((c.x - 3) * (-3.0 / (f2 * f2) - 4.0 / (f1 * f1)) / 2), -(-(6 * (c.y - 2) / (f2 * f2)) - (2 * (c.y - 1) / (9 * f1 * f1))));

                case 5:
                    E1 = Math.Exp(-(c.x - 2) * (c.x - 2) - (c.y - 3) * (c.y - 3) / 4);
                    E2 = Math.Exp(-(c.x - 1) * (c.x - 1) / 4 - (c.y - 1) * (c.y - 1));
                    double x = (-6 * c.x + 12) * E1 + (-c.x / 2 + 0.5) * E2;
                    double y = (-1.5 * c.y + 4.5) * E1 + (2 - 2 * c.y) * E2;
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
                    double E1 = 4 * (c.x - 2) * (c.x - 2) + (c.y - 3) * (c.y - 3) + 4;
                    double E2 = (c.x - 1) * (c.x - 1) + 4 * (c.y - 1) * (c.y - 1) + 4;
                    double xx = 8 * (192 * (c.x - 2) * (c.x - 2) / (E1 * E1 * E1) + 4 * (c.x - 1) * (c.x - 1) / (E2 * E2 * E2) - 1 / (E2 * E2) - 12 / (E1 * E1));
                    double xy = 128 * (3 * (c.x - 2) * (c.y - 3) / (E1 * E1 * E1) + (c.x - 1) * (c.y - 1) / (E2 * E2 * E2));
                    double yy = 8 * (12 * (c.y - 3) * (c.y - 3) / (E1 * E1 * E1) + 64 * (c.y - 1) * (c.y - 1) / (E2 * E2 * E2) - 4 / (E2 * E2) - 3 / (E1 * E1));
                    return new Matrix(xx, xy, xy, yy);

                case 4:
                    double f1 = (c.x - 3) * (c.x - 3) + (c.y - 1) * (c.y - 1) / 9 + 1;
                    double f2 = (c.x - 3) * (c.x - 3) / 4 + (c.y - 2) * (c.y - 2) + 1;
                    double f3 = 9 * c.x * c.x - 54 * c.x + c.y * c.y - 2 * c.y + 91; ;
                    xx = -(((c.x - 3) * (c.x - 3) * (16.0 / (f1 * f1 * f1) + 3.0 / (f2 * f2 * f2)) - 3.0 / (f2 * f2) - 4.0 / (f1 * f1))) / 2;
                    xy = -((c.x - 3) * (12.0 * (c.y - 2) / (f2 * f2 * f2) + 16.0 * (c.y - 1) / (9 * f1 * f1 * f1))) / 2;
                    yy = -(72.0 * (c.y - 1) * (c.y - 1) / (f3 * f3 * f3) + 24.0 * (c.y - 2) * (c.y - 2) / (f2 * f2 * f2) - 6.0 / (f2 * f2) - 2.0 / (9 * f1 * f1));

                    return new Matrix(xx, xy, xy, yy);

                case 5:
                    E1 = Math.Exp(-(c.x - 2) * (c.x - 2) - (c.y - 3) * (c.y - 3) / 4);
                    E2 = Math.Exp(-(c.x - 1) * (c.x - 1) / 4 - (c.y - 1) * (c.y - 1));

                    xx = (12 * (c.x - 2) * (c.x - 2) - 6) * E1 + ((c.x - 1) * (c.x - 1) / 4 - 0.5) * E2;

                    xy = 3 * (c.x - 2) * (c.y - 3) * E1 + (c.x - 1) * (c.y - 1) * E2;

                    yy = (0.75 * (c.y - 3) * (c.y - 3) - 1.5) * E1 + (4 * (c.y - 1) * (c.y - 1) - 2) * E2;

                    return new Matrix(xx, xy, xy, yy);


                default:
                    return new Matrix(1, 0, 0, 1);
            }
            
        }
    }
}
