using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
			var bestOrder = MakeTrivialPermutation(checkpoints.Length);
			int[] order = new int[checkpoints.Length];
			FindOrder(checkpoints, order, bestOrder, 1, 0);

			return bestOrder;
		}

		private static int[] MakeTrivialPermutation(int size)
		{
			var bestOrder = new int[size];
			for (int i = 0; i < bestOrder.Length; i++)
				bestOrder[i] = i;
			return bestOrder;
		}

		static void FindOrder(Point[] checkpoints, int[] permutation, int[] bestOrder, int currentCheckpoint, double path)
		{
			if (currentCheckpoint == permutation.Length)  //Конец перебора чекпоинтов, выход из рекурсии
			{
				for (int i = 0; i < permutation.Length; i++)
				{
					bestOrder[i] = permutation[i];
				}
				return;
			}

			for (int i = 0; i < permutation.Length; i++)
			{
				int checkpointIndex = Array.IndexOf(permutation, i, 0, currentCheckpoint);
				if (checkpointIndex != -1) //Поиск первого еще неиспользуемого чекпоинта
				{
					continue;
				}
				permutation[currentCheckpoint] = i;
				double resPath = path + PointExtensions.DistanceTo(checkpoints[permutation[currentCheckpoint - 1]], checkpoints[permutation[currentCheckpoint]]);
				if (resPath < checkpoints.GetPathLength(bestOrder)) //Выбор наименьшего пути 
				{
					FindOrder(checkpoints, permutation, bestOrder, currentCheckpoint + 1, resPath);
				}
			}
		}
	}
}
