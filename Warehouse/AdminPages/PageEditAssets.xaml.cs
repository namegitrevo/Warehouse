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
    /// Логика взаимодействия для PageEditAssets.xaml
    /// </summary>
    public partial class PageEditAssets : Page
    {
        public PageEditAssets()
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
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!",
                    "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageAssets());
        }
    }
}
