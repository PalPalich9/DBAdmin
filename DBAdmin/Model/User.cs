using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DBAdmin.Model
{
    public class User : INotifyPropertyChanged
    {
        public int id { get; set; }
        public string name;
        public string surname;
        public string email;
        public string password;
        public string phone;

        

        
        public string  Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public User(string name, string surname, string email, string phone, string password)
        {
            this.name = name;
            this.surname = surname; 
            this.email = email; 
            this.phone = phone;
            this.password = password;
        }
        public User(string name, string surname, string email, string phone)
        {
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.phone = phone;
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
