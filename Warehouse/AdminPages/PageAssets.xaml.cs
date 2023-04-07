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
using Warehouse.MainPages;

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
                AppFrame.frameMain.Navigate(new PageEditAssets());
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
            AppFrame.frameMain.Navigate(new PageAddAssets());
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            list1 = list1.OrderBy(x => x.ItemNumber).ToList();
            AssetsList.ItemsSource = list1;
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            list1 = list1.OrderByDescending(x => x.ItemNumber).ToList();
            AssetsList.ItemsSource = list1;
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
                list1 = list1.Where
               (x => x.Name.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
                ||x.Name.Contains(TextBoxFind.Text)
                || x.ItemNumber.Contains(TextBoxFind.Text)).ToList();
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


    }
}
