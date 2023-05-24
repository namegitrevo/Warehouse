using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Warehouse.Windows;

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
                WEmployeesEdit wEmployeesEdit = new WEmployeesEdit();
                wEmployeesEdit.ShowDialog();
                
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

      

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WEmployeesAdd wEmployeesAdd = new WEmployeesAdd();
            wEmployeesAdd.ShowDialog();
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
                list1 = list1.Where(x => x.FullName.ToLower().Contains(TextBoxFind.Text.ToLower())).ToList();
                // list1 = list1.Where
                //(x => x.Surname.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
                // || x.Name.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
                // || x.Patronymic.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
                // || x.Surname.Contains(TextBoxFind.Text)
                // || x.Name.Contains(TextBoxFind.Text)
                // || x.Patronymic.Contains(TextBoxFind.Text)).ToList();
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

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void EmployeesList_Click(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }
        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(EmployeesList.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
    }
}
