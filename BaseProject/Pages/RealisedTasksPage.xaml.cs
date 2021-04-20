using BaseProject.Classes;
using BaseProject.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for RealisedTasksPage.xaml
    /// </summary>
    public partial class RealisedTasksPage : Page
    {
        public RealisedTasksPage()
        {
            InitializeComponent();
            chart.ChartAreas.Add(new ChartArea());
            VisualiseGraph();
        }

        void VisualiseGraph()
        {
            Dictionary<string, double> realisedTasks = new Dictionary<string, double>();
            Series s = new Series();
            s.ChartType = SeriesChartType.Column;
            s.IsValueShownAsLabel = true;
            s.LabelFormat = "{C2}";
            foreach (Project proj in Utils.db.Project)
            {
                realisedTasks[proj.Name] = (double)(proj.Task.Where(t => t.StatusId == 1).Count()) /  proj.Task.Count();
            }
            s.Points.DataBindXY(realisedTasks.Keys, realisedTasks.Values);
            chart.Series.Add(s);
        }
    }
}
