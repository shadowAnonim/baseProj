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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BaseProject.Pages
{
    /// <summary>
    /// Логика для списка проектов
    /// </summary>
    public partial class ProjectsPage : Page
    {
        int _autorId;
        public ProjectsPage(int autorId)
        {
            InitializeComponent();
            _autorId = autorId;
        }

        void FillDataGrid()
        {
            try
            {
                projectDataGrid.ItemsSource = Utils.db.Project.ToList();
            }
            catch (Exception ex)
            {

                Utils.Error("Ошибка: " + ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectPage(_autorId, 0));
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Project selected = projectDataGrid.SelectedItem as Project;
            if (selected == null)
            {
                Utils.Error("Выберите проект");
                return;
            }
            NavigationService.Navigate(new ProjectPage(_autorId, selected.Id));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Project selected = projectDataGrid.SelectedItem as Project;
            if (selected == null)
            {
                Utils.Error("Выберите проект");
                return;
            }
            if(Utils.Question("Вы хотите удалить проект?") == MessageBoxResult.Yes)
            {
                Utils.db.Project.Remove(selected);
                Utils.db.SaveChanges();
                FillDataGrid();
            }
        }
    }
}
