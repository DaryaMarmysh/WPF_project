using System.Linq;
using System.Windows;
using kurs.Context;
using kurs.Models;

namespace kurs
{
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
            var loginUser = new LoginUser();
            DataContext = loginUser;
        }

        private void ClickLogin(object sender, RoutedEventArgs e)
        {
            User user;
            using (var db = new DatabaseContext())
            {
                user = db.Users.FirstOrDefault(u => u.Username.Equals(LoginBox.Text));
            }

            if (user == null || !LoginBox.Text.Equals(user.Username) ||
                !PasswordBox.Password.Equals(user.Password))
            {
                SignInFailed.Content = "Неправильный логин или пароль!";
                return;
            }
            CurrentUser.User = user;
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void GoToSignUp(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            Close();
        }
    }
}
