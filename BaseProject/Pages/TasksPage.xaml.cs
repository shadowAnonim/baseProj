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
    /// Interaction logic for TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        int _projectId;
        public TasksPage(int projectId)
        {
            InitializeComponent();
            _projectId = projectId;
        }

        void FillDataGrid()
        {
            try
            {
                taskDataGrid.ItemsSource = Utils.db.Project.FirstOrDefault(proj => proj.Id == _projectId).Task.ToList();
            }
            catch (Exception ex)
            {

                Utils.Error("Ошибка: " + ex.Message);
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TaskPage(_projectId, null));
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Task selected = taskDataGrid.SelectedItem as Task;
            if (selected == null)
            {
                Utils.Error("Выберите задачу");
                return;
            }
            NavigationService.Navigate(new TaskPage(_projectId, selected));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Task selected = taskDataGrid.SelectedItem as Task;
            if (selected == null)
            {
                Utils.Error("Выберите задачу");
                return;
            }
            Utils.db.Task.Remove(selected);
            FillDataGrid();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
    }
}
