using System.Linq;
using System.Collections.Generic;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		/* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
		public static double[,] MedianFilter(double[,] original)
		{
			int x = original.GetLength(0);
			int y = original.GetLength(1);

			return CreateMedianArray(original, x, y);
		}

		public static double[,] CreateMedianArray(double[,] original, int x, int y)
		{
			double[,] result = new double[x, y];
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					result[i, j] = GetMedian(original, x, y, i, j);
				}
			}
			return result;
		}

		public static double GetMedian(double[,] original, int x, int y, int i, int j)
		{
			List<double> listOfMedians = new List<double>();
			for (int k = i - 1; k < i + 2; k++)
			{
				for (int n = j - 1; n < j + 2; n++)
				{
					if (k >= 0 && k < x && n >= 0 && n < y)   //Проверка на вхождение в границы
					{
						listOfMedians.Add(original[k, n]);
					}
				}
			}
			listOfMedians.Sort();

			if (listOfMedians.Count % 2 > 0)
			{
				return listOfMedians[listOfMedians.Count / 2];
			}
			else
			{
				return (listOfMedians[listOfMedians.Count / 2] + listOfMedians[listOfMedians.Count / 2 - 1]) / 2.0;
			}
		}
	}
}