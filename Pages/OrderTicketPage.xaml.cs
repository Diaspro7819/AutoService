using AutoService.Entities;
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

namespace AutoService.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderTicketPage.xaml
    /// </summary>
    public partial class OrderTicketPage : Page
    {
        List<Product> productList = new List<Product>(); //Добавляем пустой лист, к которому можно будет обратиться в любом методе

        public OrderTicketPage(Order currentOrder, List<Product> products)
        {
            InitializeComponent();


            productList = products; //Передаем в пустой лист данные с прошлой страницы
            DataContext = currentOrder; //Привязываем контекст данных к оформленному заказу

            txtPickupPoint.Text = currentOrder.PickupPoint.address; //Выводим адрес пункта выдачи
            var result = "";

            foreach (var pl in productList)
                result += (result == "" ? "" : ", ") + pl.ProductName.ToString();
            txtProductList.Text = result.ToString();

            var total = productList.Sum(p => Convert.ToDouble(p.ProductCost) - Convert.ToDouble(p.ProductCost) * Convert.ToDouble(p.ProductDiscountAmount / 100.00));
            txtCost.Text = total.ToString() + " рублей"; //Выводим итоговую сумму заказа
            var discountSum = productList.Sum(p => p.ProductDiscountAmount);
            txtDiscountSum.Text = discountSum.ToString() + " %"; //Выводим сумму скидки заказа

        }

        private void btnSaveDocument_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource idp = flowDock;
                pd.PrintDocument(idp.DocumentPaginator, Title);

            }
        }       
    }
}
