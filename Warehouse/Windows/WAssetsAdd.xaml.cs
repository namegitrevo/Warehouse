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
using System.Windows.Shapes;
using Warehouse.AdminPages;
using Warehouse.ApplicationData;
using Warehouse.AssistanceClass;
using Warehouse.MainPages;

namespace Warehouse.Windows
{
    /// <summary>
    /// Логика взаимодействия для WAssetsAdd.xaml
    /// </summary>
    public partial class WAssetsAdd : Window
    {
        public WAssetsAdd()
        {
            InitializeComponent();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (AppConnect.modelOdb.MaterialAssets.Count(x => x.ItemNumber == TextBoxItemNumber.Text) > 0)
            {
                MessageBox.Show("Ценность с таким номером уже существует!",
                    "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                MaterialAsset materialassetsObj = new MaterialAsset()
                {
                    ItemNumber = TextBoxItemNumber.Text,
                    Name = TextBoxName.Text,
                    MeasureUnitId = 1
                };
                AppConnect.modelOdb.MaterialAssets.Add(materialassetsObj);
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Данные успешно добавлены",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                switch (HelpClass.AssetsId)
                {
                    case 1:
                        AppFrame.frameMain.Navigate(new PageAssets());
                        this.Close();
                        break;
                    case 2:
                        AppFrame.frameMain.Navigate(new PageAddDocumentItems());
                        this.Close();
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
