namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			VerticalMove(robot, height, Direction.Down);
			HorizontalMove(robot, width, Direction.Right);
		}
		//Ведем робота до нижней границы
		public static void VerticalMove(Robot robot, int height, Direction dir)
		{
			for (int i = 0; i < height - 3; i++)
				robot.MoveTo(dir);
		}
		//Ведем робота до правой границы
		public static void HorizontalMove(Robot robot, int width, Direction dir)
		{
			for (int i = 0; i < width - 3; i++)
				robot.MoveTo(dir);
		}
	}
}