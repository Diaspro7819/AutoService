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
using static System.Net.Mime.MediaTypeNames;

namespace AutoService.Pages
{
    // loginDErfg2018    5ovb1N
    public partial class Autho : Page
    {
        Model1 tradeEntities = new Model1();

        private int countUnsuccessful = 0;
        public Autho()
        {
            InitializeComponent();
            textcaptcha.Visibility = Visibility.Hidden;
            textBlockCaptcha.Visibility = Visibility.Hidden;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            User user = tradeEntities.User.FirstOrDefault(p => p.UserLogin == login && p.UserPassword == password);
            int userCount = tradeEntities.User.Where(p=> p.UserLogin == login && p.UserPassword == password).Count();
            if (countUnsuccessful < 1) {
                if (userCount > 0)
                {
                    MessageBox.Show("Вы вошли под: " + user.Role.RoleName.ToString());
                    LoadForm(user.Role.RoleName.ToString(), user);
                }
                else
                {
                    MessageBox.Show("Вы ввели неверно логин или пароль!");
                    countUnsuccessful++;
                    if (countUnsuccessful == 1) //Если количество неверных попыток равно 1
                    {
                        GenerateCaptcha();
                    } 
                }
            }
            else
            {
                if (userCount > 0 && textBlockCaptcha.Text == textcaptcha.Text)
                {

                    MessageBox.Show("Вы вошли под: " + user.Role.RoleName.ToString());
                    LoadForm(user.Role.RoleName.ToString(), user);
                }
                else
                {
                    MessageBox.Show("Введите данные заново!");
                }

            }

        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null)); // переход на страницу клиента неавторизованного пользователя

         
        }

        private void GenerateCaptcha()
        {
            textcaptcha.Visibility = Visibility.Visible;// Показываем надпись и
            textBlockCaptcha.Visibility = Visibility.Visible;// поле для ввода капчи

            Random random = new Random();
            int randomnum = random.Next(0, 3); //Генерируем случайное число от 1 до 3

            switch (randomnum)
            {
                case 1:
                    textBlockCaptcha.Text = "ju2sT8Cbs"; //Eсли случайное число равно 1, выводим капчу в TextBlock
                    textBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case 2:
                    textBlockCaptcha.Text = "iNmk2cl"; //Если случайное число равно 2, выводим капчу в TextBlock
                    textBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case 3:
                    textBlockCaptcha.Text = "u0ozGk95"; //Eсли случайное число равно 3, выводим капчу в TextBlock
                    textBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
            }      
        }

        private void LoadForm(string _role, User user)
        {
            switch (_role)
            {
                case "клиент":

                    NavigationService.Navigate(new Client(user)); //Если роль пользователя "Клиент", переходим на страницу клиента
                    break;
                case "менеджер":
                    NavigationService.Navigate(new Client(user));
                    break;
                case "администратор":
                    NavigationService.Navigate(new Admin(user));
                    break;
            }
        }

    }
}
