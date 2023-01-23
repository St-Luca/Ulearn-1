namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            int one = height;
            int two = width;
            if (height < width)
            {
                one = width;
                two = height;
            }
            Direction onedir = (height > width) ? (Direction.Down) : (Direction.Right);
            Direction twodir = (height > width) ? (Direction.Right) : (Direction.Down);
            for (int i = 0; i < (two - 2); i++)
            {
                Move(robot, i, one, two, onedir, twodir);
            }
        }

        public static void Move(Robot robot, int width, int one, int two, Direction onedir, Direction twodir)
        {
            for (int i = 0; i < ((one - 2) / (two - 2)); i++)
            {
                robot.MoveTo(onedir);
            }
            if ((two - 2) > (width + 1))
            {
                robot.MoveTo(twodir);
            }
        }
    }
}