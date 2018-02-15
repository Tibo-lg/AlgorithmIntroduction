using AlgorithmIntroduction.Dijkstra;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SortingAlgorithms
{
    public partial class AlgorithmForm : Form
    {
        const string TabLabel1 = "Analysis";
        const string TabLabel2 = "Vizualization";
        const string TabLabel3 = "Dijkstra";

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Chart chart1;
        private Button clearButton;
        private Button startButton;
        private GroupBox groupBox1;
        private RadioButton OmegaRadioButton;
        private RadioButton BigORadioButton;
        private RadioButton ThetaRadioButton;
        private RadioButton HideRadioButton;
        private TrackBar trackBar1;
        private TrackBar trackBar2;
        private GroupBox Algorithm;
        private RadioButton SelectionSortRadio2;
        private RadioButton InsertionSortRadio2;
        private Button ShuffleButton;
        private Button StartButton2;
        private Chart visualSortChart;
        private GroupBox groupBox3;
        private RadioButton SelectionSortRadioButton;
        private RadioButton InsertionSortRadioButton;
        private RadioButton mergeSortRadioButton;
        private TabPage tabPage3;
        private ComboBox comboBox1;
        private Chart dijkstraChart;

        public AlgorithmForm()
        {
            InitializeComponent();
            InitializeAnalysis();
            InitializeVisualSort();
            InitializeDijkstra();
            HandleCreated += new EventHandler((sender, args) =>
            {
                HandleCreatedAnalysis();
                HandleCreatedVisualSort();
                tabControl1.TabPages[0].Name = TabLabel1;
                tabControl1.TabPages[1].Name = TabLabel2;
                tabControl1.TabPages[2].Name = TabLabel3;
                tabControl1.SelectedIndexChanged += SelectedIndexChanged;
            });
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[TabLabel2])
            {
                VisualSortTabSelected();
            }
            if (tabControl1.SelectedTab == tabControl1.TabPages[TabLabel3])
            {
                DijkstraTabSelected();
            }
        }

        private void UpdateButtonState(Button button, bool enabled)
        {
            void SetEnabled()
            {
                button.Enabled = enabled;
            }

            ExecuteInUI(SetEnabled);
        }

        private void UpdatePoint(DataPoint point, double value, Color color)
        {
            void UpdatePoint()
            {
                point.YValues[0] = value;
                point.Color = color;
            };

            ExecuteInUI(UpdatePoint);
        }

        private void ExecuteInUI(Action action)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    action();
                }));
            }
            else
            {
                action();
            }
        }

        private void RefreshChart(Chart chart)
        {
            void Refresh()
            {
                chart.Refresh();
            };

            ExecuteInUI(Refresh);
        }

        private int GetDelay(TrackBar trackBar)
        {
            int value = 0;
            
            void SetValue()
            {
                value = trackBar.Value;
            };

            ExecuteInUI(SetValue);

            return value;
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.mergeSortRadioButton = new System.Windows.Forms.RadioButton();
            this.SelectionSortRadioButton = new System.Windows.Forms.RadioButton();
            this.InsertionSortRadioButton = new System.Windows.Forms.RadioButton();
            this.clearButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OmegaRadioButton = new System.Windows.Forms.RadioButton();
            this.BigORadioButton = new System.Windows.Forms.RadioButton();
            this.ThetaRadioButton = new System.Windows.Forms.RadioButton();
            this.HideRadioButton = new System.Windows.Forms.RadioButton();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.Algorithm = new System.Windows.Forms.GroupBox();
            this.SelectionSortRadio2 = new System.Windows.Forms.RadioButton();
            this.InsertionSortRadio2 = new System.Windows.Forms.RadioButton();
            this.ShuffleButton = new System.Windows.Forms.Button();
            this.StartButton2 = new System.Windows.Forms.Button();
            this.visualSortChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dijkstraChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.Algorithm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.visualSortChart)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dijkstraChart)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(24, 23);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(927, 597);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.clearButton);
            this.tabPage1.Controls.Add(this.startButton);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.trackBar1);
            this.tabPage1.Controls.Add(this.chart1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(919, 571);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Analysis";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.mergeSortRadioButton);
            this.groupBox3.Controls.Add(this.SelectionSortRadioButton);
            this.groupBox3.Controls.Add(this.InsertionSortRadioButton);
            this.groupBox3.Location = new System.Drawing.Point(550, 523);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 42);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Algorithm";
            // 
            // mergeSortRadioButton
            // 
            this.mergeSortRadioButton.AutoSize = true;
            this.mergeSortRadioButton.Location = new System.Drawing.Point(194, 20);
            this.mergeSortRadioButton.Name = "mergeSortRadioButton";
            this.mergeSortRadioButton.Size = new System.Drawing.Size(77, 17);
            this.mergeSortRadioButton.TabIndex = 2;
            this.mergeSortRadioButton.TabStop = true;
            this.mergeSortRadioButton.Text = "Merge Sort";
            this.mergeSortRadioButton.UseVisualStyleBackColor = true;
            // 
            // SelectionSortRadioButton
            // 
            this.SelectionSortRadioButton.AutoSize = true;
            this.SelectionSortRadioButton.Location = new System.Drawing.Point(99, 20);
            this.SelectionSortRadioButton.Name = "SelectionSortRadioButton";
            this.SelectionSortRadioButton.Size = new System.Drawing.Size(88, 17);
            this.SelectionSortRadioButton.TabIndex = 1;
            this.SelectionSortRadioButton.Text = "SelectionSort";
            this.SelectionSortRadioButton.UseVisualStyleBackColor = true;
            // 
            // InsertionSortRadioButton
            // 
            this.InsertionSortRadioButton.AutoSize = true;
            this.InsertionSortRadioButton.Checked = true;
            this.InsertionSortRadioButton.Location = new System.Drawing.Point(7, 20);
            this.InsertionSortRadioButton.Name = "InsertionSortRadioButton";
            this.InsertionSortRadioButton.Size = new System.Drawing.Size(87, 17);
            this.InsertionSortRadioButton.TabIndex = 0;
            this.InsertionSortRadioButton.TabStop = true;
            this.InsertionSortRadioButton.Text = "Insertion Sort";
            this.InsertionSortRadioButton.UseVisualStyleBackColor = true;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(128, 518);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(111, 42);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(16, 518);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(96, 42);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OmegaRadioButton);
            this.groupBox1.Controls.Add(this.BigORadioButton);
            this.groupBox1.Controls.Add(this.ThetaRadioButton);
            this.groupBox1.Controls.Add(this.HideRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(846, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(67, 133);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Aymptotic Notation";
            // 
            // OmegaRadioButton
            // 
            this.OmegaRadioButton.AutoSize = true;
            this.OmegaRadioButton.Location = new System.Drawing.Point(7, 102);
            this.OmegaRadioButton.Name = "OmegaRadioButton";
            this.OmegaRadioButton.Size = new System.Drawing.Size(59, 17);
            this.OmegaRadioButton.TabIndex = 3;
            this.OmegaRadioButton.TabStop = true;
            this.OmegaRadioButton.Text = "Omega";
            this.OmegaRadioButton.UseVisualStyleBackColor = true;
            this.OmegaRadioButton.CheckedChanged += new System.EventHandler(this.OmegaRadioButton_CheckedChanged);
            // 
            // BigORadioButton
            // 
            this.BigORadioButton.AutoSize = true;
            this.BigORadioButton.Location = new System.Drawing.Point(7, 78);
            this.BigORadioButton.Name = "BigORadioButton";
            this.BigORadioButton.Size = new System.Drawing.Size(51, 17);
            this.BigORadioButton.TabIndex = 2;
            this.BigORadioButton.TabStop = true;
            this.BigORadioButton.Text = "Big O";
            this.BigORadioButton.UseVisualStyleBackColor = true;
            this.BigORadioButton.CheckedChanged += new System.EventHandler(this.BigORadioButton_CheckedChanged);
            // 
            // ThetaRadioButton
            // 
            this.ThetaRadioButton.AutoSize = true;
            this.ThetaRadioButton.Location = new System.Drawing.Point(7, 54);
            this.ThetaRadioButton.Name = "ThetaRadioButton";
            this.ThetaRadioButton.Size = new System.Drawing.Size(53, 17);
            this.ThetaRadioButton.TabIndex = 1;
            this.ThetaRadioButton.Text = "Theta";
            this.ThetaRadioButton.UseVisualStyleBackColor = true;
            this.ThetaRadioButton.CheckedChanged += new System.EventHandler(this.ThetaRadioButton_CheckedChanged);
            // 
            // HideRadioButton
            // 
            this.HideRadioButton.AutoSize = true;
            this.HideRadioButton.Checked = true;
            this.HideRadioButton.Location = new System.Drawing.Point(7, 30);
            this.HideRadioButton.Name = "HideRadioButton";
            this.HideRadioButton.Size = new System.Drawing.Size(47, 17);
            this.HideRadioButton.TabIndex = 0;
            this.HideRadioButton.TabStop = true;
            this.HideRadioButton.Text = "Hide";
            this.HideRadioButton.UseVisualStyleBackColor = true;
            this.HideRadioButton.CheckedChanged += new System.EventHandler(this.HideRadioButton_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(846, 207);
            this.trackBar1.Maximum = 1000000;
            this.trackBar1.Minimum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 310);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Value = 100;
            // 
            // chart1
            // 
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(16, 18);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(805, 494);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.trackBar2);
            this.tabPage2.Controls.Add(this.Algorithm);
            this.tabPage2.Controls.Add(this.ShuffleButton);
            this.tabPage2.Controls.Add(this.StartButton2);
            this.tabPage2.Controls.Add(this.visualSortChart);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(919, 571);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sorting";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(579, 520);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Minimum = 10;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(314, 45);
            this.trackBar2.TabIndex = 4;
            this.trackBar2.Value = 10;
            // 
            // Algorithm
            // 
            this.Algorithm.Controls.Add(this.SelectionSortRadio2);
            this.Algorithm.Controls.Add(this.InsertionSortRadio2);
            this.Algorithm.Location = new System.Drawing.Point(214, 520);
            this.Algorithm.Name = "Algorithm";
            this.Algorithm.Size = new System.Drawing.Size(359, 42);
            this.Algorithm.TabIndex = 3;
            this.Algorithm.TabStop = false;
            this.Algorithm.Text = "Algorithm";
            // 
            // SelectionSortRadio2
            // 
            this.SelectionSortRadio2.AutoSize = true;
            this.SelectionSortRadio2.Location = new System.Drawing.Point(99, 20);
            this.SelectionSortRadio2.Name = "SelectionSortRadio2";
            this.SelectionSortRadio2.Size = new System.Drawing.Size(91, 17);
            this.SelectionSortRadio2.TabIndex = 1;
            this.SelectionSortRadio2.Text = "Selection Sort";
            this.SelectionSortRadio2.UseVisualStyleBackColor = true;
            // 
            // InsertionSortRadio2
            // 
            this.InsertionSortRadio2.AutoSize = true;
            this.InsertionSortRadio2.Checked = true;
            this.InsertionSortRadio2.Location = new System.Drawing.Point(7, 20);
            this.InsertionSortRadio2.Name = "InsertionSortRadio2";
            this.InsertionSortRadio2.Size = new System.Drawing.Size(87, 17);
            this.InsertionSortRadio2.TabIndex = 0;
            this.InsertionSortRadio2.TabStop = true;
            this.InsertionSortRadio2.Text = "Insertion Sort";
            this.InsertionSortRadio2.UseVisualStyleBackColor = true;
            // 
            // ShuffleButton
            // 
            this.ShuffleButton.Location = new System.Drawing.Point(109, 520);
            this.ShuffleButton.Name = "ShuffleButton";
            this.ShuffleButton.Size = new System.Drawing.Size(99, 42);
            this.ShuffleButton.TabIndex = 2;
            this.ShuffleButton.Text = "Shuffle";
            this.ShuffleButton.UseVisualStyleBackColor = true;
            this.ShuffleButton.Click += new System.EventHandler(this.ShuffleButton_Click);
            // 
            // StartButton2
            // 
            this.StartButton2.Location = new System.Drawing.Point(7, 515);
            this.StartButton2.Name = "StartButton2";
            this.StartButton2.Size = new System.Drawing.Size(94, 42);
            this.StartButton2.TabIndex = 1;
            this.StartButton2.Text = "Start";
            this.StartButton2.UseVisualStyleBackColor = true;
            this.StartButton2.Click += new System.EventHandler(this.StartButton2_Click);
            // 
            // visualSortChart
            // 
            chartArea2.Name = "ChartArea1";
            this.visualSortChart.ChartAreas.Add(chartArea2);
            this.visualSortChart.Location = new System.Drawing.Point(7, 7);
            this.visualSortChart.Name = "visualSortChart";
            this.visualSortChart.Size = new System.Drawing.Size(886, 502);
            this.visualSortChart.TabIndex = 0;
            this.visualSortChart.Text = "chart2";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.comboBox1);
            this.tabPage3.Controls.Add(this.dijkstraChart);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(919, 571);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Dijkstra";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dijkstraChart
            // 
            chartArea3.Name = "ChartArea1";
            this.dijkstraChart.ChartAreas.Add(chartArea3);
            this.dijkstraChart.Location = new System.Drawing.Point(28, 27);
            this.dijkstraChart.Name = "dijkstraChart";
            this.dijkstraChart.Size = new System.Drawing.Size(761, 541);
            this.dijkstraChart.TabIndex = 0;
            this.dijkstraChart.Text = "chart3";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(795, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // AlgorithmForm
            // 
            this.ClientSize = new System.Drawing.Size(963, 632);
            this.Controls.Add(this.tabControl1);
            this.Name = "AlgorithmForm";
            this.Text = "Algorithms";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.Algorithm.ResumeLayout(false);
            this.Algorithm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.visualSortChart)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dijkstraChart)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
