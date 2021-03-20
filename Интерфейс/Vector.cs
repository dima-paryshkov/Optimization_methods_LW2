using System;
using System.Collections.Generic;
using System.Text;

namespace Лабораторная_работа__2_МО.Интерфейс
{
    class Vector
    {
        public double x;

        public double y;

        public Vector() { }

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double norm()
        {
            return Math.Sqrt(x * x + y * y);
        }

        public static double norm(Vector vector1, Vector vector2)
        {
            return Math.Sqrt(vector1.x * vector2.x + vector1.y * vector2.y);
        }

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.x + vector2.x, vector1.y + vector2.y);
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.x - vector2.x, vector1.y - vector2.y);
        }

        public static double operator *(Vector vector1, Vector vector2)
        {
            return vector1.x * vector2.x + vector1.y * vector2.y;
        }

        public static Vector operator *(Vector vector, double number)
        {
            return new Vector(vector.x * number, vector.y * number);
        }

        public static Vector operator *(double number, Vector vector)
        {
            return new Vector(vector.x * number, vector.y * number);
        }

        public static Vector operator +(double number, Vector vector)
        {
            return new Vector(vector.x + number, vector.y + number);
        }

        public static Vector operator +(Vector vector, double number)
        {
            return new Vector(vector.x + number, vector.y + number);
        }
    }
}
