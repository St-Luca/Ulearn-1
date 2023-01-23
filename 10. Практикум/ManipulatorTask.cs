using System;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        /// <summary>
        /// Возвращает массив углов (shoulder, elbow, wrist),
        /// необходимых для приведения эффектора манипулятора в точку x и y 
        /// с углом между последним суставом и горизонталью, равному alpha (в радианах)
        /// См. чертеж manipulator.png!
        /// </summary>
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {   //Находим координаты wrist снова через полярные координаты
            double wristX = x - Manipulator.Palm * Math.Cos(alpha);
            double wristY = y + Manipulator.Palm * Math.Sin(alpha);
            //Достраиваем треугольник, чтобы вычислить угол elbow
            double thirdSide = Math.Sqrt(wristX * wristX + wristY * wristY);
            double elbow = TriangleTask.GetABAngle(Manipulator.UpperArm, Manipulator.Forearm, thirdSide);

            double shoulder1 = TriangleTask.GetABAngle(Manipulator.UpperArm, thirdSide, Manipulator.Forearm);
            double shoulder2 = Math.Atan2(wristY, wristX);
            double shoulder = shoulder1 + shoulder2;
            double wrist = -alpha - shoulder - elbow;
            double[] result = { shoulder, elbow, wrist };

            for (int i = 0; i < result.Length; i++)
            {
                if (double.IsNaN(result[i]))
                {
                    return new[]
                    {
                        double.NaN,
                        double.NaN,
                        double.NaN
                    };
                }
            }
            return result;
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            Random rnd = new Random();
            double x = rnd.NextDouble();
            double y = rnd.NextDouble();
            double rndAlpha = rnd.NextDouble();
            double[] rndAngles = ManipulatorTask.MoveManipulatorTo(x, y, rndAlpha);
            var rndJoints = AnglesToCoordinatesTask.GetJointPositions(rndAngles[0], rndAngles[1], rndAngles[2]);

            Assert.AreEqual(rndJoints[2].X, x, 0.001);
            Assert.AreEqual(rndJoints[2].Y, y, 0.001);
        }
    }
}