using AlgorithmIntroduction.Dijkstra;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SortingAlgorithms
{
    using DijkstraPoint = AlgorithmIntroduction.Dijkstra.Point;

    partial class AlgorithmForm
    {
        Map map;
        bool isWalking;
        Series dijkstraSeries;

        public void InitializeDijkstra()
        {
            dijkstraChart.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
            dijkstraChart.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
            dijkstraSeries = new Series()
            {
                ChartType = SeriesChartType.Point,
            };
            dijkstraChart.Series.Add(dijkstraSeries);

            var dir = new DirectoryInfo(@".\Dijkstra");
            map = new Map();
            var maps = dir.EnumerateFiles()
                .Where(
                x => x.Extension == ".map")
                .Select(x => x.Name.Replace(".map", string.Empty));

            foreach (var map in maps)
            {
                comboBox1.Items.Add(map);
            }

            comboBox1.SelectedIndex = 0;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

            LoadMap();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadMap();
            ResetMap();
        }

        public void DijkstraTabSelected()
        {
            Task.Run(() =>
            {
                ResetMap();
                dijkstraChart.MouseClick += DijkstraChart_MouseClick;
            });
        }

        void LoadMap()
        {
            string fileName = (string)comboBox1.SelectedItem + ".map";
            using (var file = File.OpenRead($@"Dijkstra\{fileName}"))
            {
                map.Load(file);
            }
        }

        void ResetMap()
        {
            void Clear()
            {
                dijkstraSeries.Points.Clear();
            }

            ExecuteInUI(Clear);
            ExecuteInUI(FillSeries);
        }

        public void FillSeries()
        {
            dijkstraSeries["PointWidth"] = "0.2";

            foreach (var point in map.Points)
            {
                var p = new DataPoint(point.X, point.Y)
                {
                    Color = DijkstraPoint.TypeToColor(point.Type),
                    MarkerSize = 7,
                    MarkerStyle = MarkerStyle.Square,
                };
                dijkstraSeries.Points.Add(p);
            }

            int index = (map.CurrentPosition.Y * map.Width) +
                map.CurrentPosition.X;
            dijkstraSeries.Points[index].Color = Color.Red;
        }

        private void DisplayRoute(
            IEnumerable<DijkstraPoint> route)
        {
            foreach (var point in route)
            {
                UpdatePointColor(point, Color.DarkGray);
            }
            RefreshChart(dijkstraChart);
        }

        private void WalkRoute(
            IEnumerable<DijkstraPoint> route)
        {
            foreach (var point in route)
            {
                var prevColor = GetDijkstraPointColor(point);
                UpdatePointColor(
                    point,
                    Color.Green);
                RefreshChart(dijkstraChart);
                Task.Delay(80).Wait();
                UpdatePointColor(point, prevColor);
                RefreshChart(dijkstraChart);
            }
        }

        private void UpdatePointColor(DijkstraPoint p, Color color)
        {
            int index = (p.Y * map.Width) + p.X;
            void UpdatePointColor()
            {
                dijkstraChart.Series[0].Points[index].Color = color;
            };

            ExecuteInUI(UpdatePointColor);
        }

        private Color GetDijkstraPointColor(DijkstraPoint p)
        {
            int index = (p.Y * map.Width) + p.X;
            return dijkstraChart.Series[0].Points[index].Color;

        }

        #region Events
        private void DijkstraChart_MouseClick(object sender, MouseEventArgs e)
        {
            if (isWalking)
            {
                return;
            }

            var results = dijkstraChart.HitTest(
                e.X, e.Y, false, ChartElementType.PlottingArea);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.PlottingArea)
                {
                    var xVal = result.ChartArea.AxisX.PixelPositionToValue(e.X);
                    var yVal = result.ChartArea.AxisY.PixelPositionToValue(e.Y);
                    var dest = map.Points[(int)yVal, (int)xVal];
                    if (DijkstraPoint.IsPassable(dest))
                    {
                        isWalking = true;
                        Task.Run(() => DoDijkstra(dest))
                            .ContinueWith((_) => isWalking = false);
                    }
                }
            }
        }

        private void UpdateColor(DijkstraPoint point, Color color)
        {
            void UpdateColor()
            {
                dijkstraChart.Series[0].Points[point.Y * map.Width + point.X].Color =
                    color;
            }

            ExecuteInUI(UpdateColor);

            //RefreshChart(dijkstraChart);
        }

        private void DoDijkstra(DijkstraPoint dest)
        {
            var route = Dijsktra.FindShortestPath(
                map, map.CurrentPosition, dest, UpdateColor);
            route = route.Reverse();
            DisplayRoute(route.Skip(1));
            WalkRoute(route.Take(route.Count() - 1));
            UpdatePointColor(route.Last(), Color.Red);
            map.CurrentPosition = route.Last();
            ResetMap();
        }
        #endregion
    }
}
