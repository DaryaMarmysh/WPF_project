using System.ComponentModel;
using System.Linq;
using kurs.Models;

namespace kurs.Context
{
    public class RegUser : IDataErrorInfo
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "Username":

                        User user;
                        using (var db = new DatabaseContext())
                        {
                            user = db.Users.FirstOrDefault(u => u.Username.Equals(Username));
                        }

                        if (user != null)
                        {
                            error = "Такой пользователь уже существует.";
                        }
                        break;
                    case "Password":
                        if (!Password.Equals(ConfirmPassword))
                            error = "Пароли не совпадают.";
                        break;
                }
                return error;
            }
        }

        public string Error { get; }
    }
}
