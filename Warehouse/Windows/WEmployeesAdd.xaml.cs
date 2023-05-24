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
using System.Windows.Shapes;
using Warehouse.AdminPages;
using Warehouse.ApplicationData;
using Warehouse.AssistanceClass;

namespace Warehouse.Windows
{
    /// <summary>
    /// Логика взаимодействия для WEmployeesAdd.xaml
    /// </summary>
    public partial class WEmployeesAdd : Window
    {
        public WEmployeesAdd()
        {
            InitializeComponent();
            var rolesObj = AppConnect.modelOdb.Roles.ToList();
            List<string> roles = new List<string>();
            for (int i = 0; i < rolesObj.Count; i++)
            {
                roles.Add(rolesObj[i].Name);
            }
            ComboBoxRole.ItemsSource = roles;
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (AppConnect.modelOdb.Employees.Count(x => x.Login == TextBoxLogin.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                string roles = ComboBoxRole.SelectedItem.ToString();
                var rolesObj = AppConnect.modelOdb.Roles.FirstOrDefault(x => roles == x.Name);
                Employee employeeObj = new Employee()
                {
                    Surname = TextBoxSurname.Text,
                    Name = TextBoxName.Text,
                    Patronymic = TextBoxLastname.Text,
                    Login = TextBoxLogin.Text,
                    Password = TextBoxPassword.Text,
                    RoleId = rolesObj.Id
                };
                AppConnect.modelOdb.Employees.Add(employeeObj);
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Данные успешно добавлены",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frameMain.Navigate(new PageEmployees());
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
