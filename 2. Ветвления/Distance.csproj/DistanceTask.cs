using System;
namespace DistanceTask
{
    public static class DistanceTask
    {
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {   //Если точки совпадают
            if (ax == bx && ay == by)
                return Math.Sqrt(Math.Pow(x - ax, 2) + Math.Pow(y - ay, 2));
            //Скалярное произведение. Если отрицательное - возвращает наименьшую длину от точки до конца отрезка, если положительное или ноль - вычисление длины по площадям
            else if ((x - ax) * (bx - ax) + (y - ay) * (by - ay) < 0 ||
                (x - bx) * (ax - bx) + (y - by) * (ay - by) < 0)
                return Math.Min(Math.Sqrt(Math.Pow(x - ax, 2) + Math.Pow(y - ay, 2)),
                    Math.Sqrt(Math.Pow(x - bx, 2) + Math.Pow(y - by, 2)));
            else
                return Math.Abs((bx - ax) * (y - ay) - (by - ay) * (x - ax)) /
                    Math.Sqrt(Math.Pow(bx - ax, 2) + Math.Pow(by - ay, 2));
        }
    }
}