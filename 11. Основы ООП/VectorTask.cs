using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector v)
        {
            return Geometry.Add(v, this);
        }
        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            double x = vector.X;
            double y = vector.Y;
            return Math.Sqrt(x*x + y*y);
        }

        public static Vector Add(Vector v1, Vector v2)
        {
            double x = v1.X + v2.X;
            double y = v1.Y + v2.Y;
            return new Vector { X = x, Y = y };
        }

        public static double GetLength(Segment segment)
        {
            double x = segment.End.X - segment.Begin.X;
            double y = segment.End.Y - segment.Begin.Y;
            return Math.Sqrt(x*x + y*y);
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            double segmentLength = GetLength(segment);

            double beginX = vector.X - segment.Begin.X;
            double beginY = vector.Y - segment.Begin.Y;
            double endX = vector.X - segment.End.X;
            double endY = vector.Y - segment.End.Y;

            double beginLength = Math.Sqrt(beginX* beginX + beginY * beginY);
            double endLength = Math.Sqrt(endX * endX + endY * endY);

            return Math.Abs(beginLength + endLength) <= segmentLength;
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public static double GetLength(Vector v)
        {
            return Geometry.GetLength(v);
        }
        public bool Contains(Vector v)
        {
            return Geometry.IsVectorInSegment(v, this);
        }
    }
}
