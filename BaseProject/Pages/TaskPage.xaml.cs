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
    /// Логика формы содания задач
    /// </summary>
    public partial class TaskPage : Page
    {
        Task task;
        bool edit = true;
        public TaskPage(int projectId, Task t)
        {
            InitializeComponent();
            try
            {
                executorComboBox.ItemsSource = Utils.db.User.ToList();
                roleComboBox.ItemsSource = Utils.db.TaskRole.ToList();
                statusComboBox.ItemsSource = Utils.db.Status.ToList();
                if (t == null)
                {
                    task = new Task();
                    Utils.db.Project.FirstOrDefault(proj => proj.Id == projectId).Task.Add(task);
                    edit = false;
                }
                else
                {
                    task = t;
                }
                grid1.DataContext = task;
            }
            catch (Exception ex)
            {
                Utils.Error("Ошибка: " + ex.Message);
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!edit)
                 Utils.db.Task.Add(task);
            NavigationService.GoBack();
        }
    }
}
