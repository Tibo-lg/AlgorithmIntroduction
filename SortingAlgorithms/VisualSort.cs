using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SortingAlgorithms
{
    public static class VisualSort
    {
        public static void InsertionSort(
            Series series,
            Func<int> delay,
            Action<DataPoint, double, Color> updatePoint,
            Action refresh)
        {
            var points = series.Points;
            for (int i = 1; i < series.Points.Count; i++)
            {
                double value = points[i].YValues[0];
                updatePoint(points[i], value, Color.Red);
                int j = i - 1;
                while (j >= 0 && points[j].YValues[0] > value)
                {
                    updatePoint(
                        points[j + 1], points[j].YValues[0], Color.ForestGreen);
                    updatePoint(points[j], value, Color.Red);
                    Task.Delay(delay()).Wait();
                    refresh();
                    j--;
                }

                updatePoint(points[j + 1], value, Color.ForestGreen);
                Task.Delay(delay()).Wait();
                refresh();
            }
        }

        public static void SelectionSort(
            Series series,
            Func<int> delay,
            Action<DataPoint, double, Color> updatePoint,
            Action refresh)
        {
            var points = series.Points;
            Console.WriteLine(points[0].Color.ToString());
        
            for (int i = 0; i < points.Count; i++)
            {
                int min = i;
                for (int j = i + 1; j < points.Count; j++)
                {
                    if (points[j].YValues[0] < points[min].YValues[0])
                    {
                        min = j;
                    }
                }

                double tmp = points[min].YValues[0];
                updatePoint(points[min], points[i].YValues[0], Color.Empty);
                updatePoint(points[i], tmp, Color.ForestGreen);
                refresh();
                Task.Delay(delay() * 2).Wait();
            }
        }
    }
}
