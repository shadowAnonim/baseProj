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
    /// Логика для редактирования и добавления данных проет
    /// </summary>
    public partial class ProjectPage : Page
    {
        Project project;
        bool edit = true;
        int errCount = 0;
        public ProjectPage(int autorId, int projectId)
        {
            InitializeComponent();
            try
            {
                if (projectId == 0)
                {
                    project = new Project();
                    project.AutorId = autorId;
                    edit = false;
                }
                else
                {
                    project = Utils.db.Project.FirstOrDefault(proj => proj.Id == projectId);
                }
                grid1.DataContext = project;
            }
            catch (Exception ex)
            {
                Utils.Error("Ошибка: " + ex.Message);
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки сохранения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (errCount != 0)
                {
                    Utils.Error("Данные имеют неправильный формат");
                    return;
                }
                if(!edit)
                    Utils.db.Project.Add(project);
                Utils.db.SaveChanges();
                NavigationService.GoBack();
            }
            catch (DbEntityValidationException ex)
            {
                foreach(var result in ex.EntityValidationErrors)
                {
                    Utils.Error(string.Join("\n", result.ValidationErrors.Select(x => x.ErrorMessage)));
                }    
            }
            catch (Exception ex)
            {

                Utils.Error("Ошибка: " + ex.Message);
            }
        }

        private void taskBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksPage(project.Id));
        }

        private void grid1_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                errCount++;
            else
                errCount--;
        }
    }
}
