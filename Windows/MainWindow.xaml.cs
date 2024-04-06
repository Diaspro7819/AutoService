using AutoService.Pages;
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

namespace AutoService
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrmMain.Navigate(new Autho()); // переход на страницу авторизации
            FrmMain.ContentRendered += FrmMain_ContentRendered; // подписываемся на событие ContentRendered
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrmMain.GoBack();
        }

        private void FrmMain_Navigated(object sender, NavigationEventArgs e)
        {
            if (FrmMain.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
            }
        }

        private void FrmMain_ContentRendered(object sender, EventArgs e)
        {
            // Ваш код обработки события ContentRendered
        }
    }
}
