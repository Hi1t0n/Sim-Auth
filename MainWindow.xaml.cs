using System;
using System.Collections.Generic;
using System.Data.SQLite;
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



namespace Training_Auth_DB_WPF
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        AppContext db;
        public MainWindow()
        {
            InitializeComponent();

            db = new AppContext();
        }
        public bool check_unique_login(string login)
        {
            string connectionString = "Data Source=DataBase.db;Version=3;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);

            string sql = "SELECT login FROM Users WHERE login = @login";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@login", login);

            connection.Open();
            SQLiteDataReader reader = command.ExecuteReader();
            bool userExists = reader.Read();
            connection.Close();
            return userExists;
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();
            string pass_2 = passBox_2.Password.Trim();
            string email = TextBoxEmail.Text.Trim().ToLower();
            string name = TextBoxName.Text.Trim();

            bool userExists = check_unique_login(login);

            if (login.Length < 5)
            {
                TextBoxLogin.ToolTip = "Длина логина должна быть не менее 5 символов!";
                TextBoxLogin.Background = Brushes.DarkRed;
            }else if(userExists)
            {
                TextBoxLogin.ToolTip = "Данный логин уже занят!!!";
                TextBoxLogin.Background = Brushes.DarkRed;
            } 
            else if(name.Length == 0) {
                TextBoxName.ToolTip = "Введите имя";
                TextBoxName.Background = Brushes.DarkRed;
            } else if(pass.Length < 5) {
                passBox.ToolTip = "Длина пароля должна быть не менее 5 символов!";
                passBox.Background = Brushes.DarkRed;
            } else if(pass != pass_2) {
                passBox_2.ToolTip = "Пароли не совпадают";
                passBox.Background = Brushes.DarkRed;
            } else if(email.Length == 0 || !email.Contains("@") || !email.Contains(".")) {
                TextBoxEmail.ToolTip = "Введен некорректный Email";
                TextBoxEmail.Background = Brushes.DarkRed;
            }
            else {
                TextBoxLogin.ToolTip = "";
                TextBoxLogin.Background = Brushes.Transparent;
                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;
                passBox_2.ToolTip = "";
                passBox_2.Background = Brushes.Transparent;
                TextBoxEmail.ToolTip = "";
                TextBoxEmail.Background = Brushes.Transparent;
                User user = new User(login,email,pass,name);
                db.Users.Add(user);
                db.SaveChanges();

                UserPageWindow userPageWindow = new UserPageWindow();
                userPageWindow.Show();
                this.Close();
            }
        }

        private void Button_Window_Auth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow= new AuthWindow();
            authWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
