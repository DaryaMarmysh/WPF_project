using System.Windows;
using kurs.Context;
using kurs.Models;
using System.Linq;

namespace kurs
{
    public partial class SignUp
    {
        public SignUp()
        {
            InitializeComponent();
            RegUser regUser = new RegUser();
            DataContext = regUser;
        }

        private void SignUpClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserName.Text) || string.IsNullOrEmpty(PasswordBox.Password) ||
                string.IsNullOrEmpty(ConfirmPasswordBox.Password) || IsUserExist(UserName.Text))
                return;
            if (PasswordBox.Password.Equals(ConfirmPasswordBox.Password))
            {
                using (var db = new DatabaseContext())
                {
                    User user = new User() {Username = UserName.Text, Password = PasswordBox.Password, Role = "User"};
                    db.Users.Add(user);
                    db.SaveChanges();
                    CurrentUser.User = user;
                }

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
                ConfirmFailed.Content = "ПАРОЛИ НЕ СОВПАДАЮТ.";
        }

        private bool IsUserExist(string username)
        {
            User user;
            using (var db = new DatabaseContext())
            {
                user = db.Users.FirstOrDefault(u => u.Username.Equals(username));
            }

            return user != null;
        }

        private void GoToLogin(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }
    }
}
