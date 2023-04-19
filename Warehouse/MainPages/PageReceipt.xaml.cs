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
        static private List<DocumentItem> list1 = AppConnect.modelOdb.DocumentItems.Where(x=> x.DocumentId==1).ToList();
        static private int helpint = 0;
        public static WarehouseEntities DataEntities1 { get; set; }
        public PageReceipt()
        {
            DataEntities1 = new WarehouseEntities();
            InitializeComponent();
            if (HelpClass.DocItId == 0)
            {
                list1 = AppConnect.modelOdb.DocumentItems.Where(x => x.DocumentId == 1).ToList();
                ComboBoxReceipt.SelectedIndex = 0;//////доработать
            }
            else
            {
                list1 = AppConnect.modelOdb.DocumentItems.Where(x => x.DocumentId == HelpClass.DocItId).ToList();

            }
            DocumentItemsList.ItemsSource = list1;
            string Count = "Кол-во строк: " + DocumentItemsList.Items.Count.ToString();
            TextBlockCount.Text = Count;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DocumentItemsList.SelectedItems.Count > 0)
            {
                for (int i = 0; i < DocumentItemsList.SelectedItems.Count; i++)
                {
                    DocumentItem employeesObj = DocumentItemsList.SelectedItems[i] as DocumentItem;
                    HelpClass.DocId = employeesObj.Id;
                }
                HelpClass.DocItemAddId = "Edit";
                AppFrame.frameMain.Navigate(new PageEditDocumentItems());
            }
            else
            {
                MessageBox.Show("Выберите товар!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (DocumentItemsList.SelectedItems.Count > 0)
            {
                for (int i = 0; i < DocumentItemsList.SelectedItems.Count; i++)
                {
                    DocumentItem employeeObj = DocumentItemsList.SelectedItems[i] as DocumentItem;
                    AppConnect.modelOdb.DocumentItems.Remove(employeeObj);
                }
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Товар успешно удалена!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                list1 = AppConnect.modelOdb.DocumentItems.Where(x => helpint == x.DocumentId).ToList();
                DocumentItemsList.ItemsSource = list1;
            }
            else
            {
                MessageBox.Show("Выберите товар!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            AppFrame.frameMain.Navigate(new PageAddDocumentItems());
            HelpClass.DocItId = helpint;
            HelpClass.DocItemAddId = "Edit";
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            list1 = list1.OrderBy(x => x.AssetsName).ToList();
            DocumentItemsList.ItemsSource = list1;

        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            list1 = list1.OrderByDescending(x => x.AssetsName).ToList();
            DocumentItemsList.ItemsSource = list1;

        }

        private void TextBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFind.Text == "")
            {
                list1 = AppConnect.modelOdb.DocumentItems.Where(x => helpint == x.DocumentId).ToList();
                DocumentItemsList.ItemsSource = list1;
                string Count = "Кол-во строк: " + DocumentItemsList.Items.Count.ToString();
                TextBlockCount.Text = Count;
            }
            else
            {
                list1 = list1.Where
               (x => x.AssetsName.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
                || x.AssetsName.Contains(TextBoxFind.Text)).ToList();
                DocumentItemsList.ItemsSource = list1;
                string Count = "Кол-во строк: " + DocumentItemsList.Items.Count.ToString();
                TextBlockCount.Text = Count;
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            list1 = AppConnect.modelOdb.DocumentItems.Where(x => helpint == x.DocumentId).ToList();
            DocumentItemsList.ItemsSource = list1;
            string Count = "Кол-во cтрок: " + DocumentItemsList.Items.Count.ToString();
            TextBlockCount.Text = Count;
        }

        private void ComboBoxReceipt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Document documentObj = ComboBoxReceipt.SelectedItem as Document;
            list1 = AppConnect.modelOdb.DocumentItems.Where(x => documentObj.Id == x.DocumentId).ToList();
            DocumentItemsList.ItemsSource = list1;
            helpint = documentObj.Id;                                                                                   //вспомогательная переменная
            var receiptObj = AppConnect.modelOdb.Receipts.FirstOrDefault(x => x.DocumentId == documentObj.Id);
            TextBoxNumber.Text = receiptObj.Number;
            DPDate.SelectedDate = receiptObj.Date;
            ComboBoxContractor.SelectedItem = receiptObj.Contractor.Name;
            var contractorObj = AppConnect.modelOdb.Contractors.ToList();
            List<string> contractor = new List<string>();
            for (int i = 0; i < contractorObj.Count; i++)
            {
                contractor.Add(contractorObj[i].Name);
            }
            ComboBoxContractor.ItemsSource = contractor;
            string Count = "Кол-во строк: " + DocumentItemsList.Items.Count.ToString();
            TextBlockCount.Text = Count;
        }

        private void AddReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageAddReceipt());
            HelpClass.DocumentAddId = 0;
        }

        private void SaveReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string date = DPDate.SelectedDate.ToString().Substring(0, 10);
                DateTime dateTime = DateTime.Parse(date);
                string contractor = ComboBoxContractor.SelectedItem.ToString();
                var contractorObj = AppConnect.modelOdb.Contractors.FirstOrDefault(x => contractor == x.Name);
                var receiptObj = AppConnect.modelOdb.Receipts.FirstOrDefault(x => x.DocumentId == helpint);
                receiptObj.Number = TextBoxNumber.Text;
                receiptObj.Date = dateTime;
                receiptObj.СontractorId = contractorObj.Id;
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

