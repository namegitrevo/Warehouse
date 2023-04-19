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
    /// Логика взаимодействия для PageEditDocumentItems.xaml
    /// </summary>
    public partial class PageEditDocumentItems : Page
    {
        public PageEditDocumentItems()
        {
            InitializeComponent();
            if (HelpClass.DocItemAddId2=="Edit")
            {
                var documentitemsObj = AppConnect.modelOdb.DocumentItems.FirstOrDefault(x => x.Id == HelpClass.DocId);
                TextBoxAmount.Text = documentitemsObj.Amount.ToString();
                TextBoxPrice.Text = documentitemsObj.PriceForUnit.ToString();
                ComboBoxAssets.SelectedItem = documentitemsObj.AssetsName;
            }
            else if (HelpClass.DocItemAddId2 == "Add")
            {
                var documentitemsObj = AppConnect.modelOdb.DocumentItems.FirstOrDefault(x => x.Id == HelpClass.DocId2);
                TextBoxAmount.Text = documentitemsObj.Amount.ToString();
                TextBoxPrice.Text = documentitemsObj.PriceForUnit.ToString();
                ComboBoxAssets.SelectedItem = documentitemsObj.AssetsName;
            }
            var assetsObj = AppConnect.modelOdb.MaterialAssets.ToList();
            List<string> assets = new List<string>();
            for (int i = 0; i < assetsObj.Count; i++)
            {
                assets.Add(assetsObj[i].Name);
            }
            ComboBoxAssets.ItemsSource = assets;
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (HelpClass.DocItemAddId2 == "Edit") 
            {
                try
                {
                    string assets = ComboBoxAssets.SelectedItem.ToString();
                    var assetsObj = AppConnect.modelOdb.MaterialAssets.FirstOrDefault(x => assets == x.Name);
                    var documentitemObj = AppConnect.modelOdb.DocumentItems.FirstOrDefault(x => x.Id == HelpClass.DocId);
                    documentitemObj.PriceForUnit = decimal.Parse(TextBoxPrice.Text);
                    documentitemObj.Amount = decimal.Parse(TextBoxAmount.Text);
                    documentitemObj.MaterialAssetsId = assetsObj.Id;
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
            if (HelpClass.DocItemAddId2 == "Add")
            {
                try
                {
                    string assets = ComboBoxAssets.SelectedItem.ToString();
                    var assetsObj = AppConnect.modelOdb.MaterialAssets.FirstOrDefault(x => assets == x.Name);
                    var documentitemObj = AppConnect.modelOdb.DocumentItems.FirstOrDefault(x => x.Id == HelpClass.DocId2);
                    documentitemObj.PriceForUnit = decimal.Parse(TextBoxPrice.Text);
                    documentitemObj.Amount = decimal.Parse(TextBoxAmount.Text);
                    documentitemObj.MaterialAssetsId = assetsObj.Id;
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

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (HelpClass.DocItemAddId2 == "Edit")
            {
                AppFrame.frameMain.Navigate(new PageReceipt());
            }
            else if (HelpClass.DocItemAddId2 == "Add")
            {
                AppFrame.frameMain.Navigate(new PageAddReceipt());
            }
        }
    }
}
