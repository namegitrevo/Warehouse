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
    /// Логика взаимодействия для WEmployeesEdit.xaml
    /// </summary>
    public partial class WEmployeesEdit : Window
    {
        public WEmployeesEdit()
        {
            InitializeComponent();
            var employeesObj = AppConnect.modelOdb.Employees.FirstOrDefault(x => x.Id == HelpClass.UId);
            TextBoxSurname.Text = employeesObj.Surname;
            TextBoxName.Text = employeesObj.Name;
            TextBoxLastname.Text = employeesObj.Patronymic;
            TextBoxLogin.Text = employeesObj.Login;
            TextBoxPassword.Text = employeesObj.Password;
            var rolesObj = AppConnect.modelOdb.Roles.ToList();
            List<string> roles = new List<string>();
            for (int i = 0; i < rolesObj.Count; i++)
            {
                roles.Add(rolesObj[i].Name);
            }
            ComboBoxRole.ItemsSource = roles;
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            string role = ComboBoxRole.Text;
            try
            {
                string roles = ComboBoxRole.SelectedItem.ToString();
                var rolesObj = AppConnect.modelOdb.Roles.FirstOrDefault(x => roles == x.Name);
                var userObj = AppConnect.modelOdb.Employees.FirstOrDefault(x => x.Id == HelpClass.UId);
                userObj.Surname = TextBoxSurname.Text;
                userObj.Name = TextBoxName.Text;
                userObj.Patronymic = TextBoxLastname.Text;
                userObj.Login = TextBoxLogin.Text;
                userObj.Password = TextBoxPassword.Text;
                userObj.RoleId = rolesObj.Id;
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!",
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
