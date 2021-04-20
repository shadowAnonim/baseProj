using BaseProject.Classes;
using BaseProject.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        User user;
        bool edit = true;
        public UserPage(int id)
        {
            InitializeComponent();
            try
            {
                if(id == 0)
                {
                    user = new User();
                    user.RoleId = 2;
                    edit = false;
                }
                else
                {
                    user = Utils.db.User.FirstOrDefault(u => u.Id == id);
                }
                grid1.DataContext = user;
            }
            catch (Exception ex)
            {
                Utils.Error("Ошибка: " + ex.Message);
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!edit)
                    Utils.db.User.Add(user);
                Utils.db.SaveChanges();
                NavigationService.GoBack();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var el in ex.EntityValidationErrors)
                {
                    Utils.Error(string.Join("\n", el.ValidationErrors.Select(x => x.ErrorMessage)));
                }
            }
            catch (Exception ex)
            {
                Utils.Error("Ошибка: " + ex.Message);
            }
        }
    }
}
