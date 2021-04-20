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
    /// Interaction logic for DirectorPage.xaml
    /// </summary>
    public partial class DirectorPage : Page
    {
        int _autorId;
        public DirectorPage(int autorId)
        {
            InitializeComponent();
            _autorId = autorId;
        }

        private void usersBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersPage());
        }

        private void projectBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectsPage(_autorId));
        }

        private void graphBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RealisedTasksPage());
        }
    }
}
