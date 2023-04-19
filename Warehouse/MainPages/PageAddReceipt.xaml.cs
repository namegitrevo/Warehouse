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
    /// Логика взаимодействия для PageAddReceipt.xaml
    /// </summary>
    public partial class PageAddReceipt : Page
    {
        
        static private List<DocumentItem> list1 = AppConnect.modelOdb.DocumentItems.Where(x => x.DocumentId == HelpClass.DocumentAddId).ToList();
        static private int helpint = 0;
        public static WarehouseEntities DataEntities1 { get; set; }
        public PageAddReceipt()
        {
            DataEntities1 = new WarehouseEntities();
            InitializeComponent();
            if (HelpClass.DocumentAddId != 0)
            {
                list1 = AppConnect.modelOdb.DocumentItems.Where(x => x.DocumentId == HelpClass.DocumentAddId).ToList();
            }
            DocumentItemsList.ItemsSource = list1;
            string Count = "Кол-во строк: " + DocumentItemsList.Items.Count.ToString();
            TextBlockCount.Text = Count;
            var contractorObj = AppConnect.modelOdb.Contractors.ToList();
            List<string> contractor = new List<string>();
            for (int i = 0; i < contractorObj.Count; i++)
            {
                contractor.Add(contractorObj[i].Name);
            }
            ComboBoxContractor.ItemsSource = contractor;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DocumentItemsList.SelectedItems.Count > 0)
            {
                for (int i = 0; i < DocumentItemsList.SelectedItems.Count; i++)
                {
                    DocumentItem employeesObj = DocumentItemsList.SelectedItems[i] as DocumentItem;
                    HelpClass.DocId2 = employeesObj.Id;
                }
                HelpClass.DocItemAddId2 = "Add";
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
            HelpClass.DocumentAddId = 0;
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageMainMenu());
            HelpClass.DocumentAddId = 0;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageAddDocumentItems());
            HelpClass.DocItId = helpint;
            if (HelpClass.DocumentAddId==0)
            {
                HelpClass.DocItemAddId = "Add";
            }
            else if (HelpClass.DocumentAddId!=0)
            {
                HelpClass.DocItemAddId = "Edit2";
            }
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
        private void SaveReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string date = DPDate.SelectedDate.ToString().Substring(0, 10);
                DateTime dateTime = DateTime.Parse(date);
                string contractor = ComboBoxContractor.SelectedItem.ToString();
                var contractorObj = AppConnect.modelOdb.Contractors.FirstOrDefault(x => contractor == x.Name);
                Receipt receiptObj = new Receipt()
                {
                    Number = TextBoxNumber.Text,
                    Date = dateTime,
                    СontractorId= contractorObj.Id,
                    DocumentId=  HelpClass.DocumentAddId
                };
                AppConnect.modelOdb.Receipts.Add(receiptObj);
                receiptObj.Document.Name = "Поступление " + TextBoxNumber.Text + " от " + date;
                receiptObj.Document.Date = dateTime;
                //var documentitemObj = AppConnect.modelOdb.DocumentItems.Where(x=>x.DocumentId == HelpClass.DocumentAddId).ToList();
                //var supplyObj = AppConnect.modelOdb.Supplies.ToList();
                //supplyObj.Clear();
                //for (int i = 0; i < documentitemObj.Count; i++)
                //{
                //    for (int j = 0; j <supplyObj.Count; j++)
                //    {
                //        var supp = AppConnect.modelOdb.Supplies.First(x => x[j].MaterialAssetsId == documentitemObj[i].MaterialAssetsId);
                //    }
                //}
                AppConnect.modelOdb.SaveChanges();
                HelpClass.DocumentAddId = 0;
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
