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
using Excel = Microsoft.Office.Interop.Excel;

namespace Warehouse.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для PageSupplies.xaml
    /// </summary>
    public partial class PageSupplies : Page
    {
        static private List<Supply> list1 = AppConnect.modelOdb.Supplies.ToList();
        public static WarehouseEntities DataEntities1 { get; set; }
        public ObservableCollection<Supply> list2;
        public PageSupplies()
        {
            
            DataEntities1 = new WarehouseEntities();
            InitializeComponent();
            SuppliesList.ItemsSource = AppConnect.modelOdb.Supplies.ToList();
            list2 = new ObservableCollection<Supply>();
            string Count = "Кол-во строк: " + SuppliesList.Items.Count.ToString();
            TextBlockCount.Text = Count;
            if (HelpClass.RoleId == 2)
            {
                AddButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
            }
            else if (HelpClass.RoleId == 1)
            {
                AddButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
                EditButton.Visibility = Visibility.Visible;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliesList.SelectedItems.Count > 0)
            {
                for (int i = 0; i < SuppliesList.SelectedItems.Count; i++)
                {
                    Supply suppliesObj = SuppliesList.SelectedItems[i] as Supply;
                    HelpClass.SuppId = suppliesObj.Id;
                }
                WSuppliesEdit wSuppliesEdit = new WSuppliesEdit();
                wSuppliesEdit.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите ценность!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (SuppliesList.SelectedItems.Count > 0)
            {
                for (int i = 0; i < SuppliesList.SelectedItems.Count; i++)
                {
                    Supply suppliesObj = SuppliesList.SelectedItems[i] as Supply;
                    AppConnect.modelOdb.Supplies.Remove(suppliesObj);
                }
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Ценность успешно удалена!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                SuppliesList.ItemsSource = AppConnect.modelOdb.Supplies.ToList();
            }
            else
            {
                MessageBox.Show("Выберите ценность!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ComboBoxFind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            list1 = AppConnect.modelOdb.Supplies.ToList();
            DataEntities1 = new WarehouseEntities();
            list1.Clear();
            Storage str = ComboBoxFind.SelectedItem as Storage;
            Supply suppliesObj = DataEntities1.Supplies.FirstOrDefault(x => x.WarehousesId == str.Id);
            var supplies = DataEntities1.Supplies;
            var querySupply = from Supplies in supplies
                              where Supplies.WarehousesId == suppliesObj.WarehousesId
                              select Supplies;
            foreach (Supply emp in querySupply)
            {
                list1.Add(emp);
            }
            SuppliesList.ItemsSource = list1;
            string Count = "Кол-во строк: " + SuppliesList.Items.Count.ToString();
            TextBlockCount.Text = Count;

        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WSuppliesAdd wSuppliesAdd = new WSuppliesAdd();
            wSuppliesAdd.ShowDialog();
        }


        private void TextBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFind.Text == "")
            {
                list1 = AppConnect.modelOdb.Supplies.ToList();
                SuppliesList.ItemsSource = list1;
                string Count = "Кол-во строк: " + SuppliesList.Items.Count.ToString();
                TextBlockCount.Text = Count;
            }
            else
            {
                list1 = list1.Where(x => x.AssetsName.ToLower().Contains(TextBoxFind.Text.ToLower())).ToList();
               // list1 = list1.Where
               //(x => x.AssetsName.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
               // || x.AssetsName.Contains(TextBoxFind.Text)).ToList();
                SuppliesList.ItemsSource = list1;
                string Count = "Кол-во строк: " + SuppliesList.Items.Count.ToString();
                TextBlockCount.Text = Count;
            }
        }
        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            list1 = AppConnect.modelOdb.Supplies.ToList();
            SuppliesList.ItemsSource = list1;
            string Count = "Кол-во строк: " + SuppliesList.Items.Count.ToString();
            TextBlockCount.Text = Count;
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            switch (ComboBoxReport.SelectedIndex)
            {
                case -1:
                    MessageBox.Show("Выберите вид отчета!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case 0:
                    DateTime date11 = DateTime.Today;
                    DateTime date12 = date11.AddMonths(-1);
                    var Spisok1 = AppConnect.modelOdb.DocumentItems.Where(x => x.Document.DocumentTypeId == 2 && (x.Document.Date >= date12 && x.Document.Date <= date11)).ToList();
                    var application1 = new Excel.Application();
                    //application.SheetsInNewWorkbook = Spisok.Count();
                    Excel.Workbook workbook1 = application1.Workbooks.Add(Type.Missing);
                    Excel.Worksheet worksheet1 = application1.Worksheets.Item[1];
                    int RowIndex1 = 5;
                    Excel.Range header1 = worksheet1.Range[worksheet1.Cells[1, 1], worksheet1.Cells[4, 5]];
                    header1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    header1.Font.Bold = true;
                    header1.ColumnWidth = 19.6;
                    worksheet1.Cells[2][1] = "Расходы за месяц";
                    worksheet1.Cells[3][1] = "ГАУ РХ \"ЦИНТ\"";
                    worksheet1.Cells[5][2] = DateTime.Now;
                    worksheet1.Cells[1][4] = "инвентарный номер";
                    worksheet1.Cells[2][4] = "Наименование";
                    worksheet1.Cells[3][4] = "Кол-во ед.";
                    worksheet1.Cells[4][4] = "Стоимость (руб.)";
                    for (int i = 0; i < Spisok1.Count(); i++)
                    {
                        worksheet1.Cells[1][RowIndex1] = Spisok1[i].MaterialAsset.ItemNumber;
                        worksheet1.Cells[2][RowIndex1] = Spisok1[i].AssetsName;
                        worksheet1.Cells[3][RowIndex1] = Spisok1[i].Amount;
                        worksheet1.Cells[4][RowIndex1] = Spisok1[i].PriceForUnit;
                        RowIndex1++;
                    }
                    application1.Visible = true;
                    break;
                case 1:
                    var Spisok2 = AppConnect.modelOdb.Supplies.Where(x => x.Amount>0).ToList();
                    var application2 = new Excel.Application();
                    //application.SheetsInNewWorkbook = Spisok.Count();
                    Excel.Workbook workbook2 = application2.Workbooks.Add(Type.Missing);
                    Excel.Worksheet worksheet2 = application2.Worksheets.Item[1];
                    int RowIndex2 = 5;
                    Excel.Range header2 = worksheet2.Range[worksheet2.Cells[1, 1], worksheet2.Cells[4, 5]];
                    header2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    header2.Font.Bold = true;
                    header2.ColumnWidth = 19.6;
                    worksheet2.Cells[2][1] = "Остатки";
                    worksheet2.Cells[3][1] = "ГАУ РХ \"ЦИНТ\"";
                    worksheet2.Cells[5][2] = DateTime.Now;
                    worksheet2.Cells[1][4] = "инвентарный номер";
                    worksheet2.Cells[2][4] = "Наименование";
                    worksheet2.Cells[3][4] = "Склад";
                    worksheet2.Cells[4][4] = "Кол-во ед.";
                    worksheet2.Cells[5][4] = "Стоимость (руб.)";
                    for (int i = 0; i < Spisok2.Count(); i++)
                    {
                        worksheet2.Cells[1][RowIndex2] = Spisok2[i].MaterialAsset.ItemNumber;
                        worksheet2.Cells[2][RowIndex2] = Spisok2[i].AssetsName;
                        worksheet2.Cells[3][RowIndex2] = Spisok2[i].WarehouseName;
                        worksheet2.Cells[4][RowIndex2] = Spisok2[i].Amount;
                        worksheet2.Cells[5][RowIndex2] = Spisok2[i].PriceForUnit;
                        RowIndex2++;
                    }
                    application2.Visible = true;
                    break;
            }
        }
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void SuppliesList_Click(object sender, RoutedEventArgs e)
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
              CollectionViewSource.GetDefaultView(SuppliesList.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
    }
}
