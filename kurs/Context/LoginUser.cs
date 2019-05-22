using System.ComponentModel;
using System.Linq;
using kurs.Models;

namespace kurs.Context
{
    public class LoginUser : IDataErrorInfo
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;
                switch (columnName)
                {
                    case "Username":

                        User user;
                        using (var db = new DatabaseContext())
                        {
                            user = db.Users.FirstOrDefault(u => u.Username.Equals(Username));
                        }

                        try
                        {
                            if (!string.IsNullOrEmpty(Username) &&
                                (user == null || !Username.Equals(user.Username) || !Password.Equals(user.Password)))
                            {
                                error = "Неправильный логин или пароль";
                            }
                        }
                        catch { }
                        break;
                }
                return error;
            }
        }

        public string Error { get; }
    }
}
