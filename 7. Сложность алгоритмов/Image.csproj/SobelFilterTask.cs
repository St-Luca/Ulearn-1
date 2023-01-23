using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] matrix)
        {
            int width = g.GetLength(0);
            int height = g.GetLength(1);
            double[,] result = new double[width, height];
            double[,] matrixT = Transpose(matrix);
            int bound = matrix.GetLength(0) / 2;

            Convolve(g, width, height, matrix, matrixT, result, bound);
            return result;
        }

        public static double[,] Transpose(double[,] matrix)
        {
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);
            double[,] matrixT = new double[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matrixT[i, j] = matrix[j, i];
                }
            }
            return matrixT;
        }

        public static void Convolve(double[,] g, int width, int height, double[,] matrix,
                                    double[,] matrixT, double[,] result, int bound)
        {
            int matrLen = matrix.GetLength(0);
            for (int x = bound; x < width - bound; x++)
            {
                for (int y = bound; y < height - bound; y++)
                {
                    double resX = 0.0;
                    double resY = 0.0;

                    for (int i = 0; i < matrLen; i++)
                    {
                        for (int j = 0; j < matrLen; j++)
                        {  //поэлементное умножение матриц matrix и полученной транспонированием
                            resX += matrix[i, j] * g[x + (i - bound), y + (j - bound)];
                            //из неё matrixT на окрестность точки(x, y)
                            resY += matrixT[i, j] * g[x + (i - bound), y + (j - bound)];
                        }
                    }

                    result[x, y] = Math.Sqrt(resX * resX + resY * resY);
                }
            }
        }
    }
}