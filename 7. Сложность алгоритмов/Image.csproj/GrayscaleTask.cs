namespace Recognizer
{
	public static class GrayscaleTask
	{
		public static double[,] ToGrayscale(Pixel[,] original)
		{
			int width = original.GetLength(0);
			int height = original.GetLength(1);

			double[,] grayscaleArray = new double[width, height];
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					grayscaleArray[x, y] = (original[x, y].R * 0.299 + original[x, y].G * 0.587 + original[x, y].B * 0.114) / 255;
				}
			}
			return grayscaleArray;
		}
	}
}
