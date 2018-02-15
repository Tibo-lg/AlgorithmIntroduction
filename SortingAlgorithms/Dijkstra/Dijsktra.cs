using LinkedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace AlgorithmIntroduction.Dijkstra
{
    public static class Dijsktra
    {
        static readonly Color AddedColor = Color.AliceBlue;
        static readonly Color VisitedColor = Color.Chocolate;

        public static IEnumerable<Point> FindShortestPath(
            Map map, Point source, Point dest, Action<Point, Color> UpdateColor = null)
        {
            // For each point we keep their distance to the destination.
            int[,] dist = new int[map.Height, map.Width];
            // For each point we keep the predecessor from source.
            Point[,] prev = new Point[map.Height, map.Width];

            // Initialize all distance to infinity.
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    dist[i, j] = int.MaxValue;
                }
            }

            // Set distance of source to 0.
            dist[source.Y, source.X] = 0;

            // .Net does not provide a priority queue, we emulate
            // with a sorted set.
            var q = new MinPriorityQueue<Point>();
            q.Enqueue(source, 0);

            while (prev[dest.Y, dest.X] == null && q.Count != 0)
            {
                // Get the next element in the queue.
                var current = q.Dequeue();
                UpdateColor(current, VisitedColor);
                // Get all the neighbors of the element.
                var neighbors = GetNeighbors(
                    map.Points, current, map.Height, map.Width);
                var currentDist = dist[current.Y, current.X];
                var cost = GetCost(current);

                foreach (var point in neighbors)
                {
                    // If the distance to the current element + distance to the
                    // neighbor is less than the distance recorded for neighbor,
                    // the shortest pass to the neighbor is through the current
                    // element. We add the neighbor to the queue.
                    int newDist = currentDist + cost;
                    if (dist[point.Y, point.X] > newDist)
                    {
                        dist[point.Y, point.X] = newDist;
                        prev[point.Y, point.X] = current;
                        q.Enqueue(point, newDist);
                        UpdateColor(point, AddedColor);
                    }
                }
            }

            // Get the path from dest to source.
            if (prev[dest.Y, dest.X] != null)
            {
                var prevList = new List<Point>();
                var current = dest;
                do
                {
                    prevList.Add(current);
                    current = prev[current.Y, current.X];
                } while (current != null);
                return prevList;
            }

            return Array.Empty<Point>();
        }

        static List<Point> GetNeighbors(
            Point[,] points, Point p, int height, int width)
        {
            var pList = new List<Point>();
            if (p.X < width && CheckType(points[p.Y, p.X + 1]))
            {
                pList.Add(points[p.Y, p.X + 1]);
            }

            if (p.X > 0 && CheckType(points[p.Y, p.X - 1]))
            {
                pList.Add(points[p.Y, p.X - 1]);
            }

            if (p.Y < height && CheckType(points[p.Y + 1, p.X]))
            {
                pList.Add(points[p.Y + 1, p.X]);
            }

            if (p.Y > 0 && CheckType(points[p.Y - 1, p.X]))
            {
                pList.Add(points[p.Y - 1, p.X]);
            }

            return pList;
        }

        static bool CheckType(Point p)
        {
            switch (p.Type)
            {
                case PointType.Passable1:
                case PointType.Passable2:
                case PointType.Swamp:
                    return true;
            }

            return false;
        }

        static int GetCost(Point p)
        {
            switch (p.Type)
            {
                case PointType.Passable1:
                case PointType.Passable2:
                    return 1;
                case PointType.Swamp:
                    return 2;
            }

            throw new ArgumentException();
        }

        static T Pop<T>(SortedSet<T> set)
        {
            var popped = set.Min;
            set.Remove(popped);
            return popped;
        }

        class PointComparer : IComparer<Point>
        {
            int[,] dist;

            public PointComparer(int[,] dist)
            {
                this.dist = dist;
            }

            public int Compare(Point x, Point y)
            {
                return dist[x.Y, x.X] - dist[y.Y, y.X];
            }
        }
    }
}
