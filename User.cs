using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Training_Auth_DB_WPF
{
    class User
    {
        public int id { get; set; }

        private string login, email, pass, name;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Pass
        {
            get { return pass; }
            set{pass = value;}
        }

        public string Name
        {
            get { return name; }
            set{name = value;}
        }
        public User() { }

        public User(string login, string email, string pass, string name)
        {
            this.login = login;
            this.email = email;
            this.pass = pass;
            this.name = name;
        }
        /*
        public override string ToString()
        {
            return "Имя: " + Name + ". Email: " + Email;
        }*/
    }
}
