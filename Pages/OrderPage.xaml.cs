using AutoService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
    public partial class OrderPage : Page
    {
        Model1 tradeEntities = new Model1();

        List<Product> productList = new List<Product>(); //Создаем пустой лист, к которому можно будет обращаться во всех методах
        int i = 0;

        public OrderPage(List<Product> products, User user)
        {
            InitializeComponent();
            DataContext = this; //Привязываем контекст данных к коду
            productList = products; //Передаем список с товарами в пустой лист
            //cmbPickupPoint.ItemsSource = productList;
            
            cmbPickupPoint.ItemsSource = tradeEntities.PickupPoint.ToList(); //Выводим в ComboBox список пункт о выдачи

            if (user != null)
            {
                txtUser.Text = user.UserSurname.ToString() + user.UserName.ToString() + " " + user.UserPatronymic.ToString(); //Вводим в TextBox ФИО
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот элемент?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                productList.Remove(lViewOrder.SelectedItem as Product);
        }

        private void btnOrderSave_Click(object sender, RoutedEventArgs e)
        {
            var productArticle = productList.Select(p => p.ProductArticleNumber).ToArray(); //Производим поиск товаров по артикулу, добавляя каждый отдельным элементом массива
            Random random = new Random(); //Добавляем рандом, для добавления случайного числа в поле "Код получения"
            var date = DateTime.Now; //Добавляем переменную, в которой хранится сегодняшняя дата
            if (productList.Any(p => p.ProductQuantityInStock < 3)) //
                date = date.AddDays(6);
            else
                date = date.AddDays(3);

            if (cmbPickupPoint.SelectedItem == null)
            {
                MessageBox.Show("Выберите пункт выдачи!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Order newOrder = new Order()
            {
                OrderStatus = "Новый",
                DateOrders = DateTime.Now,
                OrderPickupPoint = cmbPickupPoint.SelectedIndex + 1,
                OrderDeliveryDate = date,
                Code = random.Next(100, 1000).ToString(),
                NameClient = txtUser.Text,
            };
            MessageBox.Show(newOrder.NameClient);
            tradeEntities.Order.Add(newOrder);
            //Добавляем счетчик, который будет добавлять записи до того момента, как не закончатся артикулы
            for (i = 0; i < productArticle.Count(); i++)
            {
                OrderProduct newOrderProduct = new OrderProduct()
                {
                    OrderID = newOrder.OrderID,
                    ProductArticleNumber = productArticle[i],
                    Count = "1",

                };
                tradeEntities.OrderProduct.Add(newOrderProduct);
            }
            tradeEntities.SaveChanges(); //Сохраняем добавленные в БД записи
            MessageBox.Show("Заказ оформлен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new OrderTicketPage(newOrder, productList)); //Переходим на страницу талона заказа

        }
        public string Total
        {
            get
            {
                var total = productList.Sum(p => Convert.ToDouble(p.ProductCost) - Convert.ToDouble(p.ProductCost) * Convert.ToDouble(p.ProductDiscountAmount / 100.00));
                return total.ToString();
            }
        }

    }
}
