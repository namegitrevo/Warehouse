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
    /// Логика взаимодействия для PageAddDocumentItems.xaml
    /// </summary>
    public partial class PageAddDocumentItems : Page
    {
        public static WarehouseEntities DataEntities1 { get; set; }
        public PageAddDocumentItems()
        {
            DataEntities1 = new WarehouseEntities();
            InitializeComponent();
            var assetsObj = AppConnect.modelOdb.MaterialAssets.ToList();
            List<string> assets = new List<string>();
            for (int i = 0; i < assetsObj.Count; i++)
            {
                assets.Add(assetsObj[i].Name);
            }
            ComboBoxAssets.ItemsSource = assets;
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (HelpClass.DocItemAddId == "Edit")
            {
                List<DocumentItem> list1 = AppConnect.modelOdb.DocumentItems.Where(x => HelpClass.DocItId == x.DocumentId).ToList();
                if (list1.Count(x => x.AssetsName == ComboBoxAssets.SelectedItem.ToString()) > 0)
                {
                    MessageBox.Show("Этот товар уже существует!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                try
                {
                    string assets = ComboBoxAssets.SelectedItem.ToString();
                    var assetsObj = AppConnect.modelOdb.MaterialAssets.FirstOrDefault(x => assets == x.Name);
                    DocumentItem documentitemObj = new DocumentItem()
                    {
                        DocumentId = HelpClass.DocItId,
                        MaterialAssetsId = assetsObj.Id,
                        Amount = decimal.Parse(TextBoxAmount.Text),
                        PriceForUnit = decimal.Parse(TextBoxPrice.Text)
                    };
                    AppConnect.modelOdb.DocumentItems.Add(documentitemObj);
                    AppConnect.modelOdb.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены",
                            "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Ошибка при добавлении данных!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (HelpClass.DocItemAddId == "Edit2")//костыль
            {
                List<DocumentItem> list1 = AppConnect.modelOdb.DocumentItems.Where(x => HelpClass.DocumentAddId == x.DocumentId).ToList();
                if (list1.Count(x => x.AssetsName == ComboBoxAssets.SelectedItem.ToString()) > 0)
                {
                    MessageBox.Show("Этот товар уже существует!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                try
                {
                    string assets = ComboBoxAssets.SelectedItem.ToString();
                    var assetsObj = AppConnect.modelOdb.MaterialAssets.FirstOrDefault(x => assets == x.Name);
                    DocumentItem documentitemObj = new DocumentItem()
                    {
                        DocumentId = HelpClass.DocumentAddId,
                        MaterialAssetsId = assetsObj.Id,
                        Amount = decimal.Parse(TextBoxAmount.Text),
                        PriceForUnit = decimal.Parse(TextBoxPrice.Text)
                    };
                    AppConnect.modelOdb.DocumentItems.Add(documentitemObj);
                    AppConnect.modelOdb.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены",
                            "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Ошибка при добавлении данных!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (HelpClass.DocItemAddId == "Add")
            {
                try
                {
                    string assets = ComboBoxAssets.SelectedItem.ToString();
                    var assetsObj = AppConnect.modelOdb.MaterialAssets.FirstOrDefault(x => assets == x.Name);
                    Document documentObj = new Document()
                    {
                        DocumentTypeId = 1,
                        WerehouseId = 1,
                        Name = "Поступление",
                        Date = DateTime.Now,
                    };
                    AppConnect.modelOdb.Documents.Add(documentObj);
                    DocumentItem documentitemObj = new DocumentItem()
                    {
                        DocumentId = documentObj.Id,
                        MaterialAssetsId = assetsObj.Id,
                        Amount = decimal.Parse(TextBoxAmount.Text),
                        PriceForUnit = decimal.Parse(TextBoxPrice.Text)
                    };
                    AppConnect.modelOdb.DocumentItems.Add(documentitemObj);
                    AppConnect.modelOdb.SaveChanges();
                    HelpClass.DocumentAddId = documentObj.Id;
                    MessageBox.Show("Данные успешно добавлены",
                            "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.frameMain.Navigate(new PageAddReceipt()); //костыль
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
            if (HelpClass.DocItemAddId == "Edit")
            {
                AppFrame.frameMain.Navigate(new PageReceipt());
            }
            else if (HelpClass.DocItemAddId == "Edit2")
            {
                AppFrame.frameMain.Navigate(new PageAddReceipt());
            }
            else if (HelpClass.DocItemAddId == "Add") 
            {
                AppFrame.frameMain.Navigate(new PageAddReceipt());
            }
        }

    }
}
