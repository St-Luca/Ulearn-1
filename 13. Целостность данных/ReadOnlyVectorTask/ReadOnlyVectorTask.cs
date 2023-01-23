using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadOnlyVectorTask
{
	public class ReadOnlyVector
	{
		public readonly double X;
		public readonly double Y;

		public ReadOnlyVector(double x, double y)
		{
			X = x;
			Y = y;
		}

		public ReadOnlyVector Add(ReadOnlyVector other)
		{
			return new ReadOnlyVector(other.X + X, other.Y + Y);
		}

		public ReadOnlyVector WithY(double y)
		{
			return new ReadOnlyVector(X, y);
		}

		public ReadOnlyVector WithX(double x)
		{
			return new ReadOnlyVector(x, Y);
		}
	}
}