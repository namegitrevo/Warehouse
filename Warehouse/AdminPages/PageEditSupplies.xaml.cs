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

namespace Warehouse.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для PageEditSupplies.xaml
    /// </summary>
    public partial class PageEditSupplies : Page
    {
        public PageEditSupplies()
        {
            InitializeComponent();
            var suppliesObj = AppConnect.modelOdb.Supplies.FirstOrDefault(x => x.Id == HelpClass.SuppId);
            var assetsObj = AppConnect.modelOdb.MaterialAssets.FirstOrDefault(x => x.Id == suppliesObj.MaterialAssetsId);
            TextBoxName.Text = assetsObj.Name;
            TextBoxNumber.Text = assetsObj.ItemNumber;
            TextBoxPrice.Text = suppliesObj.PriceForUnit.ToString();
            TextBoxAmount.Text = suppliesObj.Amount.ToString();
            ComboBoxStorage.SelectedItem = suppliesObj.WarehouseName;
            var storagesObj = AppConnect.modelOdb.Storages.ToList();
            List<string> storages = new List<string>();
            for (int i = 0; i < storagesObj.Count; i++)
            {
                storages.Add(storagesObj[i].Name);
            }
            ComboBoxStorage.ItemsSource = storages;
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string storages = ComboBoxStorage.SelectedItem.ToString();
                var storagesObj = AppConnect.modelOdb.Storages.FirstOrDefault(x => storages == x.Name);
                var suppliesObj = AppConnect.modelOdb.Supplies.FirstOrDefault(x => x.Id == HelpClass.SuppId);
                var assetsObj = AppConnect.modelOdb.MaterialAssets.FirstOrDefault(x => x.Id == suppliesObj.MaterialAssetsId);
                assetsObj.Name= TextBoxName.Text;
                assetsObj.ItemNumber= TextBoxNumber.Text;
                suppliesObj.PriceForUnit = decimal.Parse(TextBoxPrice.Text);
                suppliesObj.Amount = decimal.Parse(TextBoxAmount.Text);
                suppliesObj.WarehousesId =storagesObj.Id;
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

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageSupplies());
        }
    }
}
