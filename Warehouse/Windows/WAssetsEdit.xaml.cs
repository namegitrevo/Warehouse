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

namespace Warehouse.Windows
{
    /// <summary>
    /// Логика взаимодействия для WAssetsEdit.xaml
    /// </summary>
    public partial class WAssetsEdit : Window
    {
        public WAssetsEdit()
        {
            InitializeComponent();
            var materialassetsObj = AppConnect.modelOdb.MaterialAssets.FirstOrDefault(x => x.Id == HelpClass.UnitId);
            TextBoxItemNumber.Text = materialassetsObj.ItemNumber;
            TextBoxName.Text = materialassetsObj.Name;
        }
              
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = AppConnect.modelOdb.MaterialAssets.FirstOrDefault(x => x.Id == HelpClass.UnitId);
                userObj.ItemNumber = TextBoxItemNumber.Text;
                userObj.Name = TextBoxName.Text;
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!",
                        "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frameMain.Navigate(new PageAssets());
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!",
                    "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
