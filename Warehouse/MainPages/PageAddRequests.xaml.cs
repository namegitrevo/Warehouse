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
using Warehouse.ApplicationData;
using Warehouse.AssistanceClass;

namespace Warehouse.MainPages
{
    /// <summary>
    /// Логика взаимодействия для PageAddRequests.xaml
    /// </summary>
    public partial class PageAddRequests : Page
    {
        public static WarehouseEntities DataEntities1 { get; set; }
        public PageAddRequests()
        {
            DataEntities1 = new WarehouseEntities();
            InitializeComponent();
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
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {    
            if (AppConnect.modelOdb.Documents.Count(x => x.Name == TextBoxDocyment.Text) > 0)
            {
                MessageBox.Show("Документ с таким номером уже существует!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
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
                string assets = ComboBoxAssets.SelectedItem.ToString();
                var assetsObj = AppConnect.modelOdb.MaterialAssets.FirstOrDefault(x => assets == x.Name);
                var suppliesObj = AppConnect.modelOdb.Supplies.FirstOrDefault(x => x.MaterialAssetsId == assetsObj.Id);
                Document document = new Document()
                {
                    DocumentTypeId = 2,
                    WerehouseId = 1,
                    Name = TextBoxDocyment.Text,
                    Date = dateTime
                };
                AppConnect.modelOdb.Documents.Add(document);
                DocumentItem documentItem = new DocumentItem()
                {
                    Document = document,
                    MaterialAssetsId = assetsObj.Id,
                    Amount = 1,
                    PriceForUnit = suppliesObj.PriceForUnit
                };
                AppConnect.modelOdb.DocumentItems.Add(documentItem);
                suppliesObj.Amount -= 1;
                Request requestObj = new Request()
                {
                    StatusId = statusObj.Id,
                    Date = dateTime,
                    Deadline = dateTime2,
                    ThemeId = themeObj.Id,
                    Customer = TextBoxCustomer.Text,
                    ExecutorId = executorObj.Id,
                    PriorityId = priorityObj.Id,
                    СompanyId = companyObj.Id,
                    Creator = TextBoxCreator.Text,
                    Address = TextBoxAddress.Text,
                    Content = TextBoxContent.Text,
                    DocumentId = document.Id
                };
                AppConnect.modelOdb.Requests.Add(requestObj);
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Данные успешно добавлены",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageRequests());
        }
    }
}
