using BaseProject.Classes;
using BaseProject.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BaseProject.Pages
{
    /// <summary>
    /// Interaction logic for GanttPage.xaml
    /// </summary>
    public partial class GanttPage : Page
    {
        public GanttPage()
        {
            InitializeComponent();
            chart.ChartAreas.Add(new ChartArea());

            projCb.ItemsSource = Utils.db.Project.ToList();
            projCb.SelectedIndex = 0;
        }

        void VisualiseGraph()
        {
            chart.Series.Clear();
            List<Task> tasks = (projCb.SelectedItem as Project).Task.OrderBy(el => el.PeriodStart).ToList();
            tasks.Reverse();
            chart.ChartAreas[0].AxisY.Minimum = tasks.Min(t => t.PeriodStart).ToOADate();
            chart.ChartAreas[0].AxisY.Maximum = tasks.Max(t => t.PeriodEnd).ToOADate();
            chart.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chart.ChartAreas[0].AxisY.Interval = 365;
            chart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chart.ChartAreas[0].AxisX.Interval = 1;
            //chart.ChartAreas[0].AxisX.MinorGrid.Enabled = true;

            Series s = new Series();
            s.ChartType = SeriesChartType.RangeBar;
            s.YValueType = ChartValueType.Date;
            chart.Series.Add(s);
            for (int i = 0; i < tasks.Count; i++)
            {
                s.Points.AddXY(tasks[i].User.Login, tasks[i].PeriodStart, tasks[i].PeriodEnd);
            }
        }

        private void projCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VisualiseGraph();
        }
    }
}
