namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            int length = SetLength(height);

            for (int i = 0; i < length;)
            {
                MakeASnake(robot, width, height);
                if (++i != length)
                {
                    robot.MoveTo(Direction.Down);
                    robot.MoveTo(Direction.Down);
                }
            }
        }

        //Задаем длину в зависимости от кол-ва необходимых итераций в методе MoveOut
        public static int SetLength(int height)
        {
            int length = 0;
            if (height > 5)
            {
                length = height / 4;
            }
            else
            {
                length = 1;
            }
            return length;
        }

        // Ведем робота до какой-либо горизонтальной границы
        public static void HorizontalMove(Robot robot, int width, Direction dir)
        {
            for (int i = 0; i < width - 3; i++)
            {
                robot.MoveTo(dir);
            }
        }

        //Делаем из методов змейку
        public static void MakeASnake(Robot robot, int width, int height)
        {
            HorizontalMove(robot, width, Direction.Right);
            robot.MoveTo(Direction.Down);
            robot.MoveTo(Direction.Down);
            HorizontalMove(robot, width, Direction.Left);
        }
    }
}