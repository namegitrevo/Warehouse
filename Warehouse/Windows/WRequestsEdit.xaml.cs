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
using Warehouse.ApplicationData;
using Warehouse.AssistanceClass;
using Warehouse.MainPages;

namespace Warehouse.Windows
{
    /// <summary>
    /// Логика взаимодействия для WRequestsEdit.xaml
    /// </summary>
    public partial class WRequestsEdit : Window
    {
        public WRequestsEdit()
        {
            InitializeComponent();
            var requestsObj = AppConnect.modelOdb.Requests.FirstOrDefault(x => x.Id == HelpClass.reqId);

            ComboBoxStatus.SelectedItem = requestsObj.StatusName;
            DPDate.SelectedDate = requestsObj.Deadline;
            DPDeadline.SelectedDate = requestsObj.Date;
            ComboBoxTheme.SelectedItem = requestsObj.ThemeName;
            TextBoxCustomer.Text = requestsObj.Customer;
            ComboBoxExecutor.SelectedItem = requestsObj.ExecutorName;
            ComboBoxPriority.SelectedItem = requestsObj.PriorityName;
            ComboBoxCompany.SelectedItem = requestsObj.CompanyName;
            //ComboBoxAssets.SelectedItem = requestsObj.
            TextBoxCreator.Text = requestsObj.Creator;
            TextBoxAddress.Text = requestsObj.Address;
            TextBoxContent.Text = requestsObj.Content;
            TextBoxDocyment.Text = requestsObj.DocumentName;
            var statusObj = AppConnect.modelOdb.Status.ToList();
            List<string> statuses = new List<string>();
            for (int i = 0; i < statusObj.Count; i++)
            {
                statuses.Add(statusObj[i].Name);
            }
            ComboBoxStatus.ItemsSource = statuses;
            var themeObj = AppConnect.modelOdb.Themes.ToList();
            List<string> theme = new List<string>();
            for (int i = 0; i < themeObj.Count; i++)
            {
                theme.Add(themeObj[i].Name);
            }
            ComboBoxTheme.ItemsSource = theme;
            var executorObj = AppConnect.modelOdb.Employees.ToList();
            List<string> executor = new List<string>();
            for (int i = 0; i < executorObj.Count; i++)
            {
                executor.Add(executorObj[i].Surname + " " + executorObj[i].Name + " " + executorObj[i].Patronymic);
            }
            ComboBoxExecutor.ItemsSource = executor;
            var priorityObj = AppConnect.modelOdb.Priorities.ToList();
            List<string> priority = new List<string>();
            for (int i = 0; i < priorityObj.Count; i++)
            {
                priority.Add(priorityObj[i].Name);
            }
            ComboBoxPriority.ItemsSource = priority;
            var companyObj = AppConnect.modelOdb.Companies.ToList();
            List<string> company = new List<string>();
            for (int i = 0; i < companyObj.Count; i++)
            {
                company.Add(companyObj[i].Name);
            }
            ComboBoxCompany.ItemsSource = company;
            var assetsObj = AppConnect.modelOdb.MaterialAssets.ToList();
            List<string> assets = new List<string>();
            for (int i = 0; i < assetsObj.Count; i++)
            {
                assets.Add(assetsObj[i].Name);
            }
            ComboBoxAssets.ItemsSource = assets;
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string date = DPDate.SelectedDate.ToString().Substring(0, 10);
                DateTime dateTime = DateTime.Parse(date);
                string date2 = DPDeadline.SelectedDate.ToString().Substring(0, 10);
                DateTime dateTime2 = DateTime.Parse(date2);
                string status = ComboBoxStatus.SelectedItem.ToString();
                var statusObj = AppConnect.modelOdb.Status.FirstOrDefault(x => status == x.Name);
                string theme = ComboBoxTheme.SelectedItem.ToString();
                var themeObj = AppConnect.modelOdb.Themes.FirstOrDefault(x => theme == x.Name);
                string executor = ComboBoxExecutor.SelectedItem.ToString();
                var executorObj = AppConnect.modelOdb.Employees.FirstOrDefault(x => executor.Contains(x.Surname) && executor.Contains(x.Name) && executor.Contains(x.Patronymic));
                string priority = ComboBoxPriority.SelectedItem.ToString();
                var priorityObj = AppConnect.modelOdb.Priorities.FirstOrDefault(x => priority == x.Name);
                string company = ComboBoxCompany.SelectedItem.ToString();
                var companyObj = AppConnect.modelOdb.Companies.FirstOrDefault(x => company == x.Name);
                var requestsObj = AppConnect.modelOdb.Requests.FirstOrDefault(x => x.Id == HelpClass.reqId);
                var documentsObj = AppConnect.modelOdb.Documents.FirstOrDefault(x => x.Id == requestsObj.DocumentId);
                requestsObj.StatusId = statusObj.Id;
                requestsObj.Date = dateTime;
                requestsObj.Deadline = dateTime2;
                requestsObj.ThemeId = themeObj.Id;
                requestsObj.ExecutorId = executorObj.Id;
                requestsObj.PriorityId = priorityObj.Id;
                requestsObj.СompanyId = companyObj.Id;
                requestsObj.Creator = TextBoxCreator.Text;
                requestsObj.Customer = TextBoxCustomer.Text;
                requestsObj.Address = TextBoxAddress.Text;
                requestsObj.Content = TextBoxContent.Text;
                documentsObj.Name = TextBoxDocyment.Text;
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Данные успешно изменены!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frameMain.Navigate(new PageRequests());
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при изменение данных!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
