using System;
using System.Collections.Generic;
using System.Text;

namespace Лабораторная_работа__2_МО.Интерфейс
{
    class Matrix
    {
		public Vector col1;

		public Vector col2;

        public Matrix() 
		{
			col1 = new Vector(0, 0);
			col2 = new Vector(0, 0);
		}

		public Matrix(Vector vector1, Vector vector2)
        {
			col1 = vector1;
			col2 = vector2;
		}

		public Matrix(double a11, double a12, double a21, double a22)
        {
			col1 = new Vector(a11, a21);
			col2 = new Vector(a12, a22);
		}

		public static Vector operator *(Matrix matrix, Vector vector)
			=> new Vector(matrix.col1.x * vector.x + matrix.col2.x * vector.y, matrix.col1.y * vector.x + matrix.col2.y * vector.y);
		
		public static Vector operator *(Vector vector, Matrix matrix) 
			=> new Vector(matrix.col1.x * vector.x + matrix.col2.x * vector.y, matrix.col1.y * vector.x + matrix.col2.y * vector.y);
		
		public static Matrix operator *(Matrix M1, Matrix M2) 
			=> new Matrix(M1.col1.x * M2.col1.x + M1.col2.x * M2.col1.y, M1.col1.x * M2.col2.x + M1.col2.x * M2.col2.y, M1.col1.y * M2.col1.x + M1.col2.y * M2.col1.y, M1.col1.y * M2.col2.x + M1.col2.y * M2.col2.y);

		public static Matrix operator ^(Matrix M1, double N)
		{
			if (N == -1)
            {
				double det = M1.col1.x * M1.col2.y - M1.col1.y * M1.col2.x;
				return new Matrix(M1.col1.x / det, M1.col1.y / det, M1.col2.x / det, M1.col2.y / det) ;
			}
            else 
			{
				for (int i = 0; i < N; i++)
					M1 *= M1;
				return M1;
			}
		}

		public static Matrix operator +(Matrix M1, Matrix M2) 
			=> new Matrix(M1.col1 + M2.col1, M1.col2 + M2.col2);
        
		public static Matrix operator *(Matrix M1, double number) 
			=> new Matrix(M1.col1 * number, M1.col2 * number);
        
		public static Matrix operator +(Matrix M1, double number) 
			=> new Matrix(M1.col1 + number, M1.col2 + number);
		

	}
}
