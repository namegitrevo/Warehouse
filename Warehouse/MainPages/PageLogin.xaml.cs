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
using Warehouse.AdminPages;
using Warehouse.ApplicationData;
using Warehouse.AssistanceClass;

namespace Warehouse.MainPages
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employeeObj = AppConnect.modelOdb.Employees.FirstOrDefault(x => x.Login == txbLogin.Text && x.Password == psbPassword.Password);
                var Obj = AppConnect.modelOdb.Employees.FirstOrDefault(x => x.Id == employeeObj.Id);
                var empObj = AppConnect.modelOdb.Employees.FirstOrDefault(x => x.Id == Obj.Id);
                if (employeeObj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    switch (employeeObj.RoleId)
                    {
                        case 1:
                            MessageBox.Show("Здравствуйте, Администратор " + empObj.Surname + " " + empObj.Name + " " + empObj.Patronymic + "!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.frameMain.Navigate(new PageMainMenu());
                            break;
                        case 2:
                            MessageBox.Show("Здравствуйте, " + empObj.Surname + " " + empObj.Name + " " + empObj.Patronymic + "!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.frameMain.Navigate(new PageMainMenu());
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка" + Ex.Message.ToString() + "Критическая работа приложения!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
