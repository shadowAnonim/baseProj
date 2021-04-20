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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BaseProject.Pages
{
    /// <summary>
    /// Interaction logic for BoardPage.xaml
    /// </summary>
    public partial class BoardPage : Page
    {
        int _userId;
        public BoardPage(int userId)
        {
            InitializeComponent();
            _userId = userId;
            try
            {
                var tasks = Utils.db.Task.Where(task => task.ExecutorId == userId).GroupBy(task => task.ProjectId).Select(t => t.Key).ToList();
                var projects =Utils.db.Project.Where(proj => tasks.Contains(proj.Id));
                projectCb.ItemsSource = projects.ToList();
                projectCb.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                Utils.Error("Ошибка: " + ex.Message);
            }
        }

        void FillListView()
        {
            try
            {
                panel.Children.Clear();
                Project project = projectCb.SelectedItem as Project;
                for (int i = 0; i < Utils.db.Status.Count(); i++)
                {
                    TaskTemplate tt = new TaskTemplate();
                    tt.DataContext = Utils.db.Task.Where(task =>
                         task.ExecutorId == _userId &&
                         task.ProjectId == project.Id &&
                         task.StatusId == i + 1).Select(task => new
                         {
                            Items = task,
                            btnText = i == 0 ? "В работе" : "Выполнено",
                            statusId = i == 0 ? 2 : 1, 
                            visibility = i == 3 ? Visibility.Hidden : Visibility.Visible
                         }).ToList();
                    tt.Width = 200;
                    tt.StatusChanged += sttusChanged;
                    
                    panel.Children.Add(tt);
                }
            }
            catch (Exception ex)
            {

                Utils.Error("Ошибка: " + ex.Message);
            }
        }

        private void projectCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillListView();
        }

        private void sttusChanged(object sender, EventArgs e)
        {
            FillListView();
        }
    }
}
