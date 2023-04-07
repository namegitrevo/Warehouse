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
    /// Логика взаимодействия для PageAddAssets.xaml
    /// </summary>
    public partial class PageAddAssets : Page
    {
        public static WarehouseEntities DataEntities1 { get; set; }
        public PageAddAssets()
        {
            DataEntities1 = new WarehouseEntities();
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
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageAssets());
        }
    }
}
