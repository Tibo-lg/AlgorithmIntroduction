
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SortingAlgorithms
{
    partial class AlgorithmForm
    {
        IEnumerable<int[]> data;
        Series ExecutionTime;
        Series UpperBound;
        Series LowerBound;
        const string ExecutionTimeName = "Execution Time";
        const string UpperBoundName = "Upper Bound";
        const string LowerBoundName = "Lower Bound";

        private static IEnumerable<int[]> GetData()
        {
            List<int[]> data = new List<int[]>();
            using (var stream = new StreamReader("input.txt"))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine().Split(',');
                    var values = line.Take(line.Count() - 1)
                        .Select(s => int.Parse(s)).ToArray();
                    data.Add(values);
                }
            }
            return data;
        }

        private void InitializeAnalysis()
        {
            ExecutionTime = new Series
            {
                Name = ExecutionTimeName,
                ChartType = SeriesChartType.Spline,
            };

            UpperBound = new Series
            {
                Name = UpperBoundName,
                ChartType = SeriesChartType.Spline,
                Enabled = false,
            };

            LowerBound = new Series
            {
                Name = LowerBoundName,
                ChartType = SeriesChartType.Spline,
                Enabled = false,
            };
        }

        private void HandleCreatedAnalysis()
        {
            chart1.Series.Add(ExecutionTime);
            chart1.Series.Add(UpperBound);
            chart1.Series.Add(LowerBound);
            LoadData();
            trackBar1.ValueChanged += new EventHandler(
                (_, __) => InitStaticSeries());
        }

        private void LoadData()
        {
            Task.Run(() => { data = GetData(); })
                .ContinueWith((_) =>
                {
                    UpdateButtonState(startButton, true);
                    InitStaticSeries();
                });
        }


        private Action<int[]> SelectAlgorithm()
        {
            if (InsertionSortRadioButton.Checked)
                return InsertionSort.Sort;
            else if (SelectionSortRadioButton.Checked)
                return SelectionSort.Sort;
            else if (mergeSortRadioButton.Checked)
                return MergeSort.Sort;

            throw new InvalidOperationException();
        }

        private void DisplayData()
        {
            AddExecutionTimePoint(0, 0);
            foreach (var line in data)
            {
                var stopWatch = Stopwatch.StartNew();
                InsertionSort.Sort(line);
                AddExecutionTimePoint(line.Length, stopWatch.ElapsedMilliseconds);
            }
        }

        private void AddExecutionTimePoint(double x, double y)
        {
            void AddPoint()
            {
                ExecutionTime.Points.AddXY(x, y);
            };

            ExecuteInUI(AddPoint);
        }

        private void ClearExecutionTime()
        {
            ExecutionTime.Points.Clear();
        }

        private void SetBounds(bool upper, bool lower)
        {
            void SetBounds()
            {
                UpperBound.Enabled = upper;
                LowerBound.Enabled = lower;
            };

            ExecuteInUI(SetBounds);
        }


        private void InitStaticSeries()
        {
            void InitSeries()
            {
                int length = data.Last().Length;
                int x1 = length / 2;
                int x2 = length;
                int c1 = trackBar1.Value * 10;
                int c2 = c1 - c1 / 5;
                UpperBound.Points.Clear();
                LowerBound.Points.Clear();
                UpperBound.Points.AddXY(0, 0);
                UpperBound.Points.AddXY(x1, Math.Pow(x1, 2) / c1);
                UpperBound.Points.AddXY(x2, Math.Pow(x2, 2) / c1);
                LowerBound.Points.AddXY(0, 0);
                LowerBound.Points.AddXY(x1, Math.Pow(x1, 2) / c2);
                LowerBound.Points.AddXY(x2, Math.Pow(x2, 2) / c2);
            };

            ExecuteInUI(InitSeries);
        }

        #region Events
        private void StartButton_Click(object sender, EventArgs e)
        {
            UpdateButtonState(startButton, false);
            Task.Run(() => DisplayData())
                .ContinueWith((_) => { UpdateButtonState(clearButton, true); });
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            UpdateButtonState(clearButton, false);
            ClearExecutionTime();
            LoadData();
        }

        private void HideRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (HideRadioButton.Checked)
            {
                SetBounds(false, false);
            }
        }

        private void ThetaRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ThetaRadioButton.Checked)
            {
                SetBounds(true, true);
            }
        }

        private void BigORadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (BigORadioButton.Checked)
            {
                SetBounds(true, false);
            }
        }

        private void OmegaRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (OmegaRadioButton.Checked)
            {
                SetBounds(false, true);
            }
        }

        #endregion
    }
}
