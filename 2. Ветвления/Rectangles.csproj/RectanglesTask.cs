using System;

namespace Rectangles
{
    public static class RectanglesTask
    {
        // Пересечение + границы
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            return Math.Min(r1.Bottom, r2.Bottom) >= Math.Max(r1.Top, r2.Top) &&
                    Math.Min(r1.Right, r2.Right) >= Math.Max(r1.Left, r2.Left);
        }

        // Площадь пересечения
        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            if (AreIntersected(r1, r2))
            {
                return (Math.Min(r1.Bottom, r2.Bottom) - Math.Max(r1.Top, r2.Top)) * (Math.Min(r1.Right, r2.Right) - Math.Max(r1.Left, r2.Left));
            }
            else
            {
                return 0;
            }
        }


        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if ((r1.Top >= r2.Top) && (r1.Bottom <= r2.Bottom) && (r1.Left >= r2.Left) && (r1.Right <= r2.Right))
            {
                return 0;
            }
            else if ((r1.Top <= r2.Top) && (r1.Bottom >= r2.Bottom) && (r1.Left <= r2.Left) && (r1.Right >= r2.Right))
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
}