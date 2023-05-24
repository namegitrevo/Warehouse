﻿using System;
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
using Warehouse.AdminPages;
using Warehouse.ApplicationData;
using Warehouse.AssistanceClass;

namespace Warehouse.Windows
{
    /// <summary>
    /// Логика взаимодействия для WSuppliesAdd.xaml
    /// </summary>
    public partial class WSuppliesAdd : Window
    {
        public static WarehouseEntities DataEntities1 { get; set; }
        public WSuppliesAdd()
        {
            InitializeComponent();
            DataEntities1 = new WarehouseEntities();
            var storageObj = AppConnect.modelOdb.Storages.ToList();
            List<string> storages = new List<string>();
            for (int i = 0; i < storageObj.Count; i++)
            {
                storages.Add(storageObj[i].Name);
            }
            ComboBoxStorage.ItemsSource = storages;
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string storage = ComboBoxStorage.Text;
            if (AppConnect.modelOdb.MaterialAssets.Count(x => x.ItemNumber == TextBoxNumber.Text) > 0)
            {
                MessageBox.Show("Ценность с таким номером уже существует!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                string storages = ComboBoxStorage.SelectedItem.ToString();
                var storagesObj = AppConnect.modelOdb.Storages.FirstOrDefault(x => storages == x.Name);
                MaterialAsset assets = new MaterialAsset()
                {
                    Name = TextBoxName.Text,
                    ItemNumber = TextBoxNumber.Text,
                    MeasureUnitId = 1
                };
                AppConnect.modelOdb.MaterialAssets.Add(assets);
                Supply suppliesObj = new Supply()
                {
                    MaterialAsset = assets,
                    WarehousesId = storagesObj.Id,
                    PriceForUnit = decimal.Parse(TextBoxPrice.Text),
                    Amount = decimal.Parse(TextBoxAmount.Text)
                };
                AppConnect.modelOdb.Supplies.Add(suppliesObj);
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Данные успешно добавлены",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frameMain.Navigate(new PageSupplies());
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
