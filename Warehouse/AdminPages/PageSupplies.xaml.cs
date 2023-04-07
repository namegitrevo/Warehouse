﻿using System;
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
            string Count = "Кол-во сотрудников: " + SuppliesList.Items.Count.ToString();
            TextBlockCount.Text = Count;
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
                AppFrame.frameMain.Navigate(new PageEditSupplies());
            }
            else
            {
                MessageBox.Show("Выберите сотрудника!",
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
            string Count = "Кол-во сотрудников: " + SuppliesList.Items.Count.ToString();
            TextBlockCount.Text = Count;

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
            AppFrame.frameMain.Navigate(new PageAddSupplies());
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            list1 = list1.OrderBy(x => x.Amount).ToList();
            SuppliesList.ItemsSource = list1;

        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            list1 = list1.OrderByDescending(x => x.Amount).ToList();
            SuppliesList.ItemsSource = list1;

        }

        private void TextBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFind.Text == "")
            {
                list1 = AppConnect.modelOdb.Supplies.ToList();
                SuppliesList.ItemsSource = list1;
                string Count = "Кол-во ценностей: " + SuppliesList.Items.Count.ToString();
                TextBlockCount.Text = Count;
            }
            else
            {
                list1 = list1.Where
               (x => x.AssetsName.StartsWith(TextBoxFind.Text, StringComparison.CurrentCultureIgnoreCase)
                || x.AssetsName.Contains(TextBoxFind.Text)).ToList();
                SuppliesList.ItemsSource = list1;
                string Count = "Кол-во ценностей: " + SuppliesList.Items.Count.ToString();
                TextBlockCount.Text = Count;
            }
        }
        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            list1 = AppConnect.modelOdb.Supplies.ToList();
            SuppliesList.ItemsSource = list1;
            string Count = "Кол-во сотрудников: " + SuppliesList.Items.Count.ToString();
            TextBlockCount.Text = Count;
        } 
    }
}