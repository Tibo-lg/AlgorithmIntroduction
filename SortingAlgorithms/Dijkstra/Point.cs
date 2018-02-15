
using System;
using System.Drawing;

namespace AlgorithmIntroduction.Dijkstra
{
    public enum PointType
    {
        Passable1,
        Passable2,
        OutOfBound1,
        OutOfBound2,
        Trees,
        Swamp,
        Water,
    }

    public class Point
    {
        public Point(int x, int y, PointType type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public int X { get; }
        public int Y { get; }
        public PointType Type { get; }

        public static PointType CharToType(char c)
        {
            switch (c)
            {
                case 'T':
                    return PointType.Trees;
                case '.':
                    return PointType.Passable1;
                case 'G':
                    return PointType.Passable2;
                case '@':
                    return PointType.OutOfBound1;
                case 'O':
                    return PointType.OutOfBound2;
                case 'S':
                    return PointType.Swamp;
                case 'W':
                    return PointType.Water;
            }

            throw new ArgumentException("Unrecognized type.");
        }

        public static Color TypeToColor(PointType type)
        {
            switch (type)
            {
                case PointType.Passable1:
                    return Color.Beige;
                case PointType.Passable2:
                    return Color.Olive;
                case PointType.OutOfBound1:
                    return Color.Black;
                case PointType.OutOfBound2:
                    return Color.Gray;
                case PointType.Trees:
                    return Color.Green;
                case PointType.Swamp:
                    return Color.Khaki;
                case PointType.Water:
                    return Color.Blue;
            }

            throw new ArgumentException();
        }

        public static bool IsPassable(Point p)
        {
            return p.Type == PointType.Passable1 ||
                p.Type == PointType.Passable2;
        }
    }
}
