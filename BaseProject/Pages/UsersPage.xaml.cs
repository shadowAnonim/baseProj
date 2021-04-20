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
    /// Логика списка пользователей
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
        }

        void FillDataGrid()
        {
            try
            {
                userDataGrid.ItemsSource = Utils.db.User.ToList();
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
            NavigationService.Navigate(new UserPage(0));
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            User selected = userDataGrid.SelectedItem as User;
            if(selected == null)
            {
                Utils.Error("Выберите пользоаателя");
                return;
            }
            NavigationService.Navigate(new UserPage(selected.Id));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            User selected = userDataGrid.SelectedItem as User;
            if (selected == null)
            {
                Utils.Error("Выберите пользоаателя");
                return;
            }
            Utils.db.User.Remove(selected);
            Utils.db.SaveChanges();
            FillDataGrid();
        }
    }
}
