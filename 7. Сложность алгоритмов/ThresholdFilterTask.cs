using System.Collections.Generic;
namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			int xPixels = original.GetLength(0);
			int yPixels = original.GetLength(1);
			double[,] result = new double[xPixels, yPixels];
			double threshold = GetThreshold(original, whitePixelsFraction, xPixels, yPixels);

			for (int i = 0; i < xPixels; i++)
			{
				for (int j = 0; j < yPixels; j++)
				{
					if (original[i, j] >= threshold)
					{
						result[i, j] = 1.0;
					}
					else
					{
						result[i, j] = 0.0;
					}
				}
			}

			return result;
		}

		public static double GetThreshold(double[,] original, double whitePixelsFraction, int x, int y)
		{
			int n = x * y;
			int minWhitePixelsCount = (int)(whitePixelsFraction * n);
			List<double> allPixels = new List<double>();
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					allPixels.Add(original[i, j]);
				}
			}
			allPixels.Sort();

			double threshold;
			if (minWhitePixelsCount == 0)
			{
				threshold = 124.0; //Делаем все пиксели черными
			}
			else
			{
				threshold = allPixels[original.Length - minWhitePixelsCount];//Берем этот элемент, т.к это точно гарантирует, что белых будет не меньше необходимого минимума(из-за сортировки allPixels)
																			 //и при последующем равенстве в цикле ТhresholdFilter белыми будет как можно меньше пикселей, опять же из-за сортировки allPixels
			}
			return threshold;
		}
	}
}