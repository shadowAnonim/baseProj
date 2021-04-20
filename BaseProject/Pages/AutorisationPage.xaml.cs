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
    /// Interaction logic for AutorisationPage.xaml
    /// </summary>
    public partial class AutorisationPage : Page
    {
        public AutorisationPage()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = Utils.db.User.FirstOrDefault(
                 u => u.Login == loginTb.Text &&
                u.Password == passwordTb.Password);
                if (user == null)
                {
                    Utils.Error("Неверный логин/пароль");
                    return;
                }
                switch(user.RoleId)
                {
                    case 1:
                        NavigationService.Navigate(new DirectorPage(user.Id));
                        break;
                    case 2:
                        NavigationService.Navigate(new ExecutorPage());
                         break;
                }
            }
            catch (Exception ex)
            {

                Utils.Error("Ошибка: " + ex.Message);
            }
        }
    }
}
