using BaseProject.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BaseProject.Classes
{
    public static class Utils
    {
       public static EducationProjectsEntities db = new EducationProjectsEntities();
       public static void Error(string text)
       {
            MessageBox.Show(text, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
       }

       public static MessageBoxResult Question(string text)
       {
            return MessageBox.Show(text, "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
       }
    }
}
