using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Warehouse.ApplicationData;
using Warehouse.AssistanceClass;
using Warehouse.MainPages;

namespace Warehouse.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для PageUsers.xaml
    /// </summary>
    public partial class PageEmployees : Page
    {
        static private List<Employee> list1 = AppConnect.modelOdb.Employees.ToList();
        public static WarehouseEntities DataEntities1 { get; set; }
        public ObservableCollection<Employee> list2;
        public PageEmployees()
        {
            DataEntities1 = new WarehouseEntities();
            InitializeComponent();
            EmployeesList.ItemsSource = AppConnect.modelOdb.Employees.ToList();
            list2 = new ObservableCollection<Employee>();
            string Count = "Кол-во сотрудников: " + EmployeesList.Items.Count.ToString();
            TextBlockCount.Text = Count;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesList.SelectedItems.Count > 0)
            {
                for (int i = 0; i < EmployeesList.SelectedItems.Count; i++)
                {
                    Employee employeesObj = EmployeesList.SelectedItems[i] as Employee;
                    HelpClass.UId = employeesObj.Id;
                }
                AppFrame.frameMain.Navigate(new PageEditEmployees());
            }
            else
            {
                MessageBox.Show("Выберите сотрудника!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (EmployeesList.SelectedItems.Count > 0)
            {
                for (int i = 0; i < EmployeesList.SelectedItems.Count; i++)
                {
                    Employee employeeObj = EmployeesList.SelectedItems[i] as Employee;
                    AppConnect.modelOdb.Employees.Remove(employeeObj);
                }
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Сотрудник успешно удалена!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                EmployeesList.ItemsSource = AppConnect.modelOdb.Employees.ToList();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ComboBoxFind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            list1 = AppConnect.modelOdb.Employees.ToList();
            DataEntities1 = new WarehouseEntities();
            list1.Clear();
            Role str = ComboBoxFind.SelectedItem as Role;
            Employee employeeObj = DataEntities1.Employees.FirstOrDefault(x => x.RoleId == str.Id);
            var employees = DataEntities1.Employees;
            var queryEmployee = from Employees in employees
                                where Employees.RoleId == employeeObj.RoleId
                                select Employees;
            foreach (Employee emp in queryEmployee)
            {
                list1.Add(emp);
            }
            EmployeesList.ItemsSource = list1;
            string Count = "Кол-во сотрудников: " + EmployeesList.Items.Count.ToString();
            TextBlockCount.Text = Count;

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageLogin());
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageMainMenu());
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageAddEmployees());
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            //list1.Clear();
            list1 = list1.OrderBy(x => x.FullName).ToList();
            EmployeesList.ItemsSource = list1;
            ///////
            //List1.Clear();
            //var employeeObj = AppConnect.modelOdb.Employees.OrderBy(x => x.RoleId).ToList();
            //EmployeesList.ItemsSource = employeeObj;
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            //list1.Clear();
            list1 = list1.OrderByDescending(x => x.FullName).ToList();
            EmployeesList.ItemsSource = list1;
            ///////
            //List1.Clear();
            //var employeeObj = AppConnect.modelOdb.Employees.OrderByDescending(x => x.RoleId).ToList();
            //EmployeesList.ItemsSource = employeeObj;
        }

        private void TextBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFind.Text == "")
            {
                list1 = AppConnect.modelOdb.Employees.ToList();
                EmployeesList.ItemsSource = list1;
                string Count = "Кол-во ценностей: " + EmployeesList.Items.Count.ToString();
                TextBlockCount.Text = Count;
            }
            else
            {
                list1 = list1.Where
               (x => x.Surname.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
                || x.Name.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
                || x.Patronymic.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
                || x.Surname.Contains(TextBoxFind.Text)
                || x.Name.Contains(TextBoxFind.Text)
                || x.Patronymic.Contains(TextBoxFind.Text)).ToList();
                EmployeesList.ItemsSource = list1;
                string Count = "Кол-во сотрудников: " + EmployeesList.Items.Count.ToString();
                TextBlockCount.Text = Count;
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            list1 = AppConnect.modelOdb.Employees.ToList();
            EmployeesList.ItemsSource = list1;
            string Count = "Кол-во сотрудников: " + EmployeesList.Items.Count.ToString();
            TextBlockCount.Text = Count;
        }
    }
}
