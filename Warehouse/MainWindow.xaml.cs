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
using Warehouse.AdminPages;
using Warehouse.ApplicationData;
using Warehouse.AssistanceClass;
using Warehouse.MainPages;

namespace Warehouse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConnect.modelOdb = new WarehouseEntities();
            AppFrame.frameMain = FrmMain;
            menupanel.Visibility = Visibility.Collapsed;
            FrmMain.Navigate(new PageLogin());
        }

        private void ButtonReceipt_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageReceipt());
        }

        private void ButtonRequest_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageRequests());
        }

        private void ButtonSupplies_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageSupplies());
        }

        private void ButtonUser_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageEmployees());
        }

        private void ButtonAssets_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageAssets());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageLogin());
            menupanel.Visibility = Visibility.Collapsed;
        }
    }
}
