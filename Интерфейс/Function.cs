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

        public static double Value(Vector coor)
        {
            switch (Func)
            {
                case 1:
                    return 100 * (coor.y - coor.x) * (coor.y - coor.x) + (1 - coor.x) * (1 - coor.x);

                case 2:
                    return 100 * (coor.y - coor.x * coor.x) * (coor.y - coor.x * coor.x) + (1 - coor.x) * (1 - coor.x);

                case 3:
                    return 3 * Math.Exp(-(coor.x - 2) * (coor.x - 2) - (coor.y - 3) * (coor.y - 3) / 4) + Math.Exp(-(coor.x - 1) * (coor.x - 1) / 4 - (coor.y - 1) * (coor.y - 1));
            }
            return 0;
        }

        public static Vector Gradient(Vector coor)
        {
            switch (Func)
            {
                case 1:
                    return new Vector(202 * coor.x - 200 * coor.y - 2, -200 * coor.x + 200 * coor.y);

                case 2:
                    return new Vector(400 * coor.x * coor.x * coor.x - 400 * coor.y * coor.x + 2 * coor.x - 2, 200 * (coor.y - coor.x * coor.x));

                case 3:
                    double x = (-6 * coor.x + 8) * Math.Exp(-(coor.x - 2) * (coor.x - 2) - (coor.y - 3) * (coor.y - 3) / 4) + (-coor.x / 2 + 0.5) * Math.Exp(-(coor.x - 1) * (coor.x - 1) / 4 - (coor.y - 1) * (coor.y - 1));
                    double y = (-1.5 * coor.y + 1.5) * Math.Exp(-(coor.x - 2) * (coor.x - 2) - (coor.y - 3) * (coor.y - 3) / 4) + (2 - 2 * coor.y) * Math.Exp(-(coor.x - 1) * (coor.x - 1) / 4 - (coor.y - 1) * (coor.y - 1));
                    return new Vector(x,  y);
            }
            return new Vector(0,0);
        }


        public static Matrix HesseMatrix(Vector coor)
        {
            switch (Func)
            {
                case 1:
                    return new Matrix(202, -200, -200, 200);

                case 2:
                    return new Matrix(1200 * coor.x  * coor.x - 400 * coor.y + 2, -400 * coor.x, -400 * coor.x, 200);

                case 3:
                    double E1 = Math.Exp(-(coor.x - 2) * (coor.x - 2) - (coor.y - 3) * (coor.y - 3) / 4);
                    double E2 = Math.Exp(-(coor.x - 1) * (coor.x - 1) / 4 - (coor.y - 1) * (coor.y - 1));

                    double xx = (12 * (coor.x - 2) * (coor.x - 2)  - 6) * E1 + ((coor.x - 1) * (coor.x - 1) / 4 - 0.5) * E2;

                    double xy = 3 * (coor.x -2) * (coor.y - 3) * E1 + (coor.x - 1) * (coor.y - 1) * E2; 

                    double yy = (0.75 * (coor.y - 3) * (coor.y - 3) - 1.5) * E1 + (4 * (coor.y - 1) * (coor.y - 1) - 2) * E2;

                    return new Matrix(xx, xy, xy, yy);

                default:
                    return new Matrix(1, 0, 0, 1);
            }
            
        }
    }
}
