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
    /// Логика взаимодействия для PageAssets.xaml
    /// </summary>
    public partial class PageAssets : Page
    {
        static private List<MaterialAsset> list1 = AppConnect.modelOdb.MaterialAssets.ToList();
        public static WarehouseEntities DataEntities1 { get; set; }
        public ObservableCollection<MaterialAsset> list2;
        public PageAssets()
        {
            DataEntities1 = new WarehouseEntities();
            InitializeComponent();
            AssetsList.ItemsSource = AppConnect.modelOdb.MaterialAssets.ToList();
            list2 = new ObservableCollection<MaterialAsset>();
            string Count = "Кол-во ценностей: " + AssetsList.Items.Count.ToString();
            TextBlockCount.Text = Count;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (AssetsList.SelectedItems.Count > 0)
            {
                for (int i = 0; i < AssetsList.SelectedItems.Count; i++)
                {
                    MaterialAsset materialassetsObj = AssetsList.SelectedItems[i] as MaterialAsset;
                    HelpClass.UnitId = materialassetsObj.Id;
                }
                WAssetsEdit wAssetsEdit = new WAssetsEdit();
                wAssetsEdit.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Выберите ценность!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (AssetsList.SelectedItems.Count > 0)
            {
                for (int i = 0; i < AssetsList.SelectedItems.Count; i++)
                {
                    MaterialAsset materialassetObj = AssetsList.SelectedItems[i] as MaterialAsset;
                    AppConnect.modelOdb.MaterialAssets.Remove(materialassetObj);
                }
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Ценность успешно удалена!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AssetsList.ItemsSource = AppConnect.modelOdb.MaterialAssets.ToList();
            }
            else
            {
                MessageBox.Show("Выберите ценность!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      
       
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            HelpClass.AssetsId = 1;
            WAssetsAdd wAssetsAdd = new WAssetsAdd();
            wAssetsAdd.ShowDialog();
        }

        

        private void TextBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFind.Text == "")
            {
                list1 = AppConnect.modelOdb.MaterialAssets.ToList();
                AssetsList.ItemsSource = list1;
                string Count = "Кол-во ценностей: " + AssetsList.Items.Count.ToString();
                TextBlockCount.Text = Count;
            }
            else
            {
                list1 = list1.Where(x => x.Name.ToLower().Contains(TextBoxFind.Text.ToLower())|| x.ItemNumber.ToLower().Contains(TextBoxFind.Text.ToLower())).ToList();
                // list1 = list1.Where
                //(x => x.Name.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
                // ||x.Name.Contains(TextBoxFind.Text)
                // || x.ItemNumber.Contains(TextBoxFind.Text)).ToList();
                AssetsList.ItemsSource = list1;
                string Count = "Кол-во ценностей: " + AssetsList.Items.Count.ToString();
                TextBlockCount.Text = Count;
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            list1 = AppConnect.modelOdb.MaterialAssets.ToList();
            AssetsList.ItemsSource = list1;
            string Count = "Кол-во ценностей: " + AssetsList.Items.Count.ToString();
            TextBlockCount.Text = Count;
        }

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void AssetsList_Click(object sender, RoutedEventArgs e)
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
              CollectionViewSource.GetDefaultView(AssetsList.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
    }
}
