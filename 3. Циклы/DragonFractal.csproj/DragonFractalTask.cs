using System.Drawing;
using System;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
			/*Начните с точки (1, 0)
			Создайте генератор рандомных чисел с сидом seed*/
			Random rnd = new Random(seed);
			double x = 1.0;
			double y = 0.0;
			for (int i = 0; i < iterationsCount; i++)
			{
				double x1 = x;

				if (rnd.Next(2) == 0) //Вариант 1
				{
					x = (x * Math.Cos(Math.PI / 4) - y * Math.Sin(Math.PI / 4)) / Math.Sqrt(2);
					y = (x1 * Math.Sin(Math.PI / 4) + y * Math.Cos(Math.PI / 4)) / Math.Sqrt(2);
				}
				else //Вариант 2
				{
					x = (x * Math.Cos(Math.PI * 3 / 4) - y * Math.Sin(Math.PI * 3 / 4)) / Math.Sqrt(2) + 1;
					y = (x1 * Math.Sin(Math.PI * 3 / 4) + y * Math.Cos(Math.PI * 3 / 4)) / Math.Sqrt(2);
				}
				pixels.SetPixel(x, y);
			}
		}
	}
}
