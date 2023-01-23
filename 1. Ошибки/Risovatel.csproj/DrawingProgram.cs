using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Drawer
    {
        static float x, y;
        static Graphics graphics;

        public static void Initialize(Graphics newGraphics)
        {
            graphics = newGraphics;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.Clear(Color.Black);
        }

        public static void Set_position(float x0, float y0)
        { x = x0; y = y0; }

        public static void MakeIt(Pen pen, double height, double angle)
        {
            //Делает шаг длиной dlina в направлении ugol и рисует пройденную траекторию
            var x1 = (float)(x + height * Math.Cos(angle));
            var y1 = (float)(y + height * Math.Sin(angle));
            graphics.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double height, double angle)
        {
            x = (float)(x + height * Math.Cos(angle));
            y = (float)(y + height * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, double angleOfRotation, Graphics graphics)
        {
            // ugolPovorota пока не используется, но будет использоваться в будущем
            Drawer.Initialize(graphics);

            var sz = Math.Min(width, height);

            var diagonal_length = Math.Sqrt(2) * (sz * 0.375f + sz * 0.04f) / 2;
            var x0 = (float)(diagonal_length * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonal_length * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;

            Drawer.Set_position(x0, y0);

            //Рисуем 1-ую сторону
            DrawTheSide1(sz);

            //Рисуем 2-ую сторону
            DrawTheSide2(sz);

            //Рисуем 3-ю сторону
            DrawTheSide3(sz);

            //Рисуем 4-ую сторону
            DrawTheSide4(sz);
        }

        private static void DrawTheSide4(int sz)
        {
            Drawer.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI / 2);
            Drawer.MakeIt(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), Math.PI / 2 + Math.PI / 4);
            Drawer.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI / 2 + Math.PI);
            Drawer.MakeIt(Pens.Yellow, sz * 0.375f - sz * 0.04f, Math.PI / 2 + Math.PI / 2);

            Drawer.Change(sz * 0.04f, Math.PI / 2 - Math.PI);
            Drawer.Change(sz * 0.04f * Math.Sqrt(2), Math.PI / 2 + 3 * Math.PI / 4);
        }

        private static void DrawTheSide3(int sz)
        {
            Drawer.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI);
            Drawer.MakeIt(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), Math.PI + Math.PI / 4);
            Drawer.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI + Math.PI);
            Drawer.MakeIt(Pens.Yellow, sz * 0.375f - sz * 0.04f, Math.PI + Math.PI / 2);

            Drawer.Change(sz * 0.04f, Math.PI - Math.PI);
            Drawer.Change(sz * 0.04f * Math.Sqrt(2), Math.PI + 3 * Math.PI / 4);
        }

        private static void DrawTheSide2(int sz)
        {
            Drawer.MakeIt(Pens.Yellow, sz * 0.375f, -Math.PI / 2);
            Drawer.MakeIt(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), -Math.PI / 2 + Math.PI / 4);
            Drawer.MakeIt(Pens.Yellow, sz * 0.375f, -Math.PI / 2 + Math.PI);
            Drawer.MakeIt(Pens.Yellow, sz * 0.375f - sz * 0.04f, -Math.PI / 2 + Math.PI / 2);

            Drawer.Change(sz * 0.04f, -Math.PI / 2 - Math.PI);
            Drawer.Change(sz * 0.04f * Math.Sqrt(2), -Math.PI / 2 + 3 * Math.PI / 4);
        }

        private static void DrawTheSide1(int sz)
        {
            Drawer.MakeIt(Pens.Yellow, sz * 0.375f, 0);
            Drawer.MakeIt(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), Math.PI / 4);
            Drawer.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI);
            Drawer.MakeIt(Pens.Yellow, sz * 0.375f - sz * 0.04f, Math.PI / 2);

            Drawer.Change(sz * 0.04f, -Math.PI);
            Drawer.Change(sz * 0.04f * Math.Sqrt(2), 3 * Math.PI / 4);
        }
    }
}
