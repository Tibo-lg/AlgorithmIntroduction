
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SortingAlgorithms
{
    public partial class AlgorithmForm
    {
        int[] visualSortData;
        Series VisualSortSeries;

        public void InitializeVisualSort()
        {
            VisualSortSeries = new Series
            {
                ChartType = SeriesChartType.Column,
            };
            visualSortData = Enumerable.Range(1, 31).ToArray();
        }

        public void HandleCreatedVisualSort()
        {
            LoadVisualSortData();
        }

        public void VisualSortTabSelected()
        {
            visualSortChart.Series.Add(VisualSortSeries);
        }

        private void LoadVisualSortData()
        {
            new Random().Shuffle(visualSortData);

            VisualSortSeries.Points.Clear();

            for (int i = 0; i < visualSortData.Length; i++)
            {
                VisualSortSeries.Points.AddXY(i, visualSortData[i]);
            }
        }

        #region Events
        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            LoadVisualSortData();
        }

        private void StartButton2_Click(object sender, EventArgs e)
        {
            StartButton2.Enabled = false;
            ShuffleButton.Enabled = false;
            Task task = null;
            if (InsertionSortRadio2.Checked)
            {
                task = Task.Run(() => VisualSort.InsertionSort(
                    VisualSortSeries,
                    () => GetDelay(trackBar2),
                    UpdatePoint,
                    () => RefreshChart(visualSortChart)));
            }
            else if (SelectionSortRadio2.Checked)
            {
                task = Task.Run(() => VisualSort.SelectionSort(
                    VisualSortSeries,
                    () => GetDelay(trackBar2),
                    UpdatePoint,
                    () => RefreshChart(visualSortChart)));
            }

            task?.ContinueWith((_) =>
            {
                UpdateButtonState(StartButton2, true);
                UpdateButtonState(ShuffleButton, true);
            });
        }
        #endregion
    }
}
