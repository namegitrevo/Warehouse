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

namespace Warehouse.MainPages
{
    /// <summary>
    /// Логика взаимодействия для PageRequests.xaml
    /// </summary>
    public partial class PageRequests : Page
    {
        static private List<Request> list1 = AppConnect.modelOdb.Requests.ToList();
        public static WarehouseEntities DataEntities1 { get; set; }
        public ObservableCollection<Request> list2;
        public PageRequests()
        {
            DataEntities1 = new WarehouseEntities();
            InitializeComponent();
            RequestsList.ItemsSource = AppConnect.modelOdb.Requests.ToList();
            list2 = new ObservableCollection<Request>();
            string Count = "Кол-во заявок: " + RequestsList.Items.Count.ToString();
            TextBlockCount.Text = Count;

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsList.SelectedItems.Count > 0)
            {
                for (int i = 0; i < RequestsList.SelectedItems.Count; i++)
                {
                    Request employeesObj = RequestsList.SelectedItems[i] as Request;
                    HelpClass.reqId = employeesObj.Id;
                }
                AppFrame.frameMain.Navigate(new PageEditRequests());
            }
            else
            {
                MessageBox.Show("Выберите заявку!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (RequestsList.SelectedItems.Count > 0)
            {
                for (int i = 0; i < RequestsList.SelectedItems.Count; i++)
                {
                    Request employeeObj = RequestsList.SelectedItems[i] as Request;
                    AppConnect.modelOdb.Requests.Remove(employeeObj);
                }
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Заявка успешно удалена!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                RequestsList.ItemsSource = AppConnect.modelOdb.Requests.ToList();
            }
            else
            {
                MessageBox.Show("Выберите заявку",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ComboBoxFind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            list1 = AppConnect.modelOdb.Requests.ToList();
            DataEntities1 = new WarehouseEntities();
            list1.Clear();
            Priority str = ComboBoxFind.SelectedItem as Priority;
            Request employeeObj = DataEntities1.Requests.FirstOrDefault(x => x.PriorityId == str.Id);
            var employees = DataEntities1.Requests;
            var queryRequest = from Requests in employees
                               where Requests.PriorityId == employeeObj.PriorityId
                               select Requests;
            foreach (Request emp in queryRequest)
            {
                list1.Add(emp);
            }
            RequestsList.ItemsSource = list1;
            string Count = "Кол-во заявок: " + RequestsList.Items.Count.ToString();
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
            AppFrame.frameMain.Navigate(new PageAddRequests());
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {

            list1 = list1.OrderBy(x => x.StatusId).ToList();
            RequestsList.ItemsSource = list1;

        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            list1 = list1.OrderByDescending(x => x.StatusId).ToList();
            RequestsList.ItemsSource = list1;
        }

        private void TextBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFind.Text == "")
            {
                list1 = AppConnect.modelOdb.Requests.ToList();
                RequestsList.ItemsSource = list1;
                string Count = "Кол-во заявок: " + RequestsList.Items.Count.ToString();
                TextBlockCount.Text = Count;
            }
            else
            {
                list1 = list1.Where
               (x => x.Customer.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
                || x.Customer.Contains(TextBoxFind.Text)).ToList();
                RequestsList.ItemsSource = list1;
                string Count = "Кол-во заявок: " + RequestsList.Items.Count.ToString();
                TextBlockCount.Text = Count;
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            list1 = AppConnect.modelOdb.Requests.ToList();
            RequestsList.ItemsSource = list1;
            string Count = "Кол-во заявок: " + RequestsList.Items.Count.ToString();
            TextBlockCount.Text = Count;
        }
    }
}
