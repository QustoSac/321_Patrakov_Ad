using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace _321_Patrakov_Ad.Pages
{
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин или пароль!");
                return;
            }

            using (var db = new Entities())
            {
                var user = db.Users
                    .AsNoTracking()
                    .FirstOrDefault(u => u.login == LoginTextBox.Text && u.password == PasswordBox.Password);

                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден!");
                    return;
                }
                else
                {
                    MessageBox.Show("Авторизация прошла успешно");
                    var adsPage = new AdsPage();
                    adsPage.SetAuthenticatedUser(user);
                    NavigationService?.Navigate(adsPage);
                }
            }
        }
    }
}
