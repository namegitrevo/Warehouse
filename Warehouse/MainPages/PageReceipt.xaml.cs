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
    /// Логика взаимодействия для PageReceipt.xaml
    /// </summary>
    public partial class PageReceipt : Page
    {
        static private List<DocumentItem> list1 = AppConnect.modelOdb.DocumentItems.ToList();
        public static WarehouseEntities DataEntities1 { get; set; }
        public ObservableCollection<DocumentItem> list2;
        public PageReceipt()
        {
            DataEntities1 = new WarehouseEntities();
            InitializeComponent();
            DocumentItemsList.ItemsSource = AppConnect.modelOdb.DocumentItems.ToList();
            list2 = new ObservableCollection<DocumentItem>();
            string Count = "Кол-во сотрудников: " + DocumentItemsList.Items.Count.ToString();
            TextBlockCount.Text = Count;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //if (DocumentItemsList.SelectedItems.Count > 0)
            //{
            //    for (int i = 0; i < DocumentItemsList.SelectedItems.Count; i++)
            //    {
            //        DocumentItem employeesObj = DocumentItemsList.SelectedItems[i] as DocumentItem;
            //        HelpClass.UId = employeesObj.Id;
            //    }
            //    AppFrame.frameMain.Navigate(new PageEditDocumentItems());
            //}
            //else
            //{
            //    MessageBox.Show("Выберите сотрудника!",
            //        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            //if (DocumentItemsList.SelectedItems.Count > 0)
            //{
            //    for (int i = 0; i < DocumentItemsList.SelectedItems.Count; i++)
            //    {
            //        DocumentItem employeeObj = DocumentItemsList.SelectedItems[i] as DocumentItem;
            //        AppConnect.modelOdb.DocumentItems.Remove(employeeObj);
            //    }
            //    AppConnect.modelOdb.SaveChanges();
            //    MessageBox.Show("Сотрудник успешно удалена!",
            //        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            //    DocumentItemsList.ItemsSource = AppConnect.modelOdb.DocumentItems.ToList();
            //}
            //else
            //{
            //    MessageBox.Show("Выберите сотрудника!",
            //        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void ComboBoxFind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //list1 = AppConnect.modelOdb.DocumentItems.ToList();
            //DataEntities1 = new WarehouseEntities();
            //list1.Clear();
            //Role str = ComboBoxFind.SelectedItem as Role;
            //DocumentItem employeeObj = DataEntities1.DocumentItems.FirstOrDefault(x => x.RoleId == str.Id);
            //var employees = DataEntities1.DocumentItems;
            //var queryDocumentItem = from DocumentItems in employees
            //                        where DocumentItems.RoleId == employeeObj.RoleId
            //                        select DocumentItems;
            //foreach (DocumentItem emp in queryDocumentItem)
            //{
            //    list1.Add(emp);
            //}
            //DocumentItemsList.ItemsSource = list1;
            //string Count = "Кол-во сотрудников: " + DocumentItemsList.Items.Count.ToString();
            //TextBlockCount.Text = Count;

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
            //AppFrame.frameMain.Navigate(new PageAddDocumentItems());
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            //list1 = list1.OrderBy(x => x.FullName).ToList();
            //DocumentItemsList.ItemsSource = list1;
            
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            //list1 = list1.OrderByDescending(x => x.FullName).ToList();
            //DocumentItemsList.ItemsSource = list1;
          
        }

        private void TextBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (TextBoxFind.Text == "")
            //{
            //    list1 = AppConnect.modelOdb.DocumentItems.ToList();
            //    DocumentItemsList.ItemsSource = list1;
            //    string Count = "Кол-во ценностей: " + DocumentItemsList.Items.Count.ToString();
            //    TextBlockCount.Text = Count;
            //}
            //else
            //{
            //    list1 = list1.Where
            //   (x => x.Surname.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
            //    || x.Name.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
            //    || x.Patronymic.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
            //    || x.Surname.Contains(TextBoxFind.Text)
            //    || x.Name.Contains(TextBoxFind.Text)
            //    || x.Patronymic.Contains(TextBoxFind.Text)).ToList();
            //    DocumentItemsList.ItemsSource = list1;
            //    string Count = "Кол-во сотрудников: " + DocumentItemsList.Items.Count.ToString();
            //    TextBlockCount.Text = Count;
            //}
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            list1 = AppConnect.modelOdb.DocumentItems.ToList();
            DocumentItemsList.ItemsSource = list1;
            string Count = "Кол-во сотрудников: " + DocumentItemsList.Items.Count.ToString();
            TextBlockCount.Text = Count;
        }

        private void ComboBoxReceipt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string receipt = ComboBoxReceipt.SelectedItem.ToString();
            //var receiptObj = AppConnect.modelOdb.Receipts.FirstOrDefault(x => receipt == x.Number);
            //list1 = AppConnect.modelOdb.DocumentItems.Where(x => receiptObj.DocumentId == x.DocumentId).ToList();
            //DocumentItemsList.ItemsSource = list1;
            //string Count = "Кол-во сотрудников: " + DocumentItemsList.Items.Count.ToString();
            //TextBlockCount.Text = Count;
        }
    }
}

