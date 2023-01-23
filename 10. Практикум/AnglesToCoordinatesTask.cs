using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {   //Находим координаты точек через полярные координаты
            double elbowX = Manipulator.UpperArm * Math.Cos(shoulder);
            double elbowY = Manipulator.UpperArm * Math.Sin(shoulder);
            PointF elbowPos = new PointF((float)elbowX, (float)elbowY);
            //К углу добавляем пи, т.к. угол должен быть в пределах от -пи до пи
            double wristX = elbowX + Manipulator.Forearm * Math.Cos(elbow + shoulder + Math.PI);
            double wristY = elbowY + Manipulator.Forearm * Math.Sin(elbow + shoulder + Math.PI);
            PointF wristPos = new PointF((float)wristX, (float)wristY);
            //Здесь прибавляем два пи, но это полный поворот на 360 градусов, поэтому его можно опустить
            double palmEndX = wristX + Manipulator.Palm * Math.Cos(elbow + shoulder + wrist);
            double palmEndY = wristY + Manipulator.Palm * Math.Sin(elbow + shoulder + wrist);
            PointF palmEndPos = new PointF((float)palmEndX, (float)palmEndY);

            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(0, Math.PI, Math.PI, Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm, 0)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI / 2, Manipulator.Forearm, Manipulator.UpperArm - Manipulator.Palm)]
        [TestCase(Math.PI / 2, 3 * Math.PI / 2, 3 * Math.PI / 2, -Manipulator.Forearm, Manipulator.UpperArm - Manipulator.Palm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        }
    }
}
