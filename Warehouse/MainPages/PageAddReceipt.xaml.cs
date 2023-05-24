using System;
using System.Collections.Generic;
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
                list1 = AppConnect.modelOdb.DocumentItems.Where(x => HelpClass.DocumentAddId == x.DocumentId).ToList();
                DocumentItemsList.ItemsSource = list1;
            }
            else
            {
                MessageBox.Show("Выберите товар!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            list1 = AppConnect.modelOdb.DocumentItems.Where(x => HelpClass.DocumentAddId == x.DocumentId).ToList();
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
                    СontractorId = contractorObj.Id,
                    DocumentId = HelpClass.DocumentAddId
                };
                AppConnect.modelOdb.Receipts.Add(receiptObj);
                receiptObj.Document.Name = "Поступление " + TextBoxNumber.Text + " от " + date;
                receiptObj.Document.Date = dateTime;
                var supplylist = AppConnect.modelOdb.Supplies.ToList();
                //MessageBox.Show(supplylist.Count.ToString());
                var documentitemObj = AppConnect.modelOdb.DocumentItems.Where(x => x.DocumentId == HelpClass.DocumentAddId).ToList();//начало добавления товаров в запасы
                for (int i = 0; i < supplylist.Count; i++)
                {
                    int id = supplylist[i].Id;
                    Supply supplyObj = AppConnect.modelOdb.Supplies.FirstOrDefault(x => x.Id == id);
                    //MessageBox.Show(supplyObj.AssetsName, "из таблицы запасы");
                    for (int j = 0; j < documentitemObj.Count; j++)
                    {
                        //MessageBox.Show(documentitemObj[j].AssetsName, "из таблицы документ, не равное значение");
                        if (supplylist[i].MaterialAssetsId == documentitemObj[j].MaterialAssetsId)
                        {
                            //MessageBox.Show(documentitemObj[j].AssetsName, "из таблицы документ, равное значение");
                            supplyObj.Amount += documentitemObj[j].Amount;//нужно поработать с ценой и удаления документа в случае отмены или закрытия приложения
                            AppConnect.modelOdb.SaveChanges();
                        }
                    }
                }
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
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void DocumentItemsList_Click(object sender, RoutedEventArgs e)
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
              CollectionViewSource.GetDefaultView(DocumentItemsList.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void DocumentItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
