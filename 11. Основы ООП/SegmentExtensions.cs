using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;
using System.Drawing;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        public static Dictionary<Segment, Color> Colors = new Dictionary<Segment, Color>();

        public static Color GetColor(this Segment segment)
        {
            if (Colors.ContainsKey(segment))
            {
                return Colors[segment];
            }
            else
            {
                return Color.Black;
            }
        }

        public static void SetColor(this Segment segment, Color color)
        {
            if (Colors.ContainsKey(segment))
            {
                Colors[segment] = color;
            }
            else
            {
                Colors[segment] = new Color();
                Colors[segment] = color;
            }
        }
    }
}