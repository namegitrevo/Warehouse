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
using Warehouse.AssistanceClass;

namespace Warehouse.MainPages
{
    /// <summary>
    /// Логика взаимодействия для PageMainMenu.xaml
    /// </summary>
    public partial class PageMainMenu : Page
    {
        public PageMainMenu()
        {
            InitializeComponent();
            if (HelpClass.RoleId == 2)
            {
                ButtonUser.Visibility = Visibility.Collapsed;
            }
            else if (HelpClass.RoleId==1)
            {
                ButtonUser.Visibility = Visibility.Visible;
            }
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
        }
    }
}
