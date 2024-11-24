using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DBAdmin.Model;

namespace DBAdmin.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; set; }

        ApplicationContext db = new ApplicationContext();

        MainViewModel mainViewModel;

        private string textName;
        private string textSurname;
        private string textEmail;
        private string textPhone;
        private string textPassword;
        private string textRePassword;

        RelayCommand? addUserCommand;
        RelayCommand? toLoginCommand;


        public string TextName
        {
            get => textName;
            set
            {
                textName = value;
                OnPropertyChanged();
            }
        }
        public string TextSurname
        {
            get => textSurname;
            set
            {
                textSurname = value;
                OnPropertyChanged();
            }
        }
        public string TextEmail
        {
            get => textEmail;
            set
            {
                textEmail = value;
                OnPropertyChanged();
            }
        }
        public string TextPhone
        {
            get => textPhone;
            set
            {
                textPhone = value;
                OnPropertyChanged();
            }
        }
        public string TextPassword
        {
            get => textPassword;
            set
            {
                textPassword = value;
                OnPropertyChanged();
            }
        }
        public string TextRePassword
        {
            get => textRePassword;
            set
            {
                textRePassword = value;
                OnPropertyChanged();
            }
        }
        public RegistrationViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }
        public RelayCommand AddUserCommand
        {
            get
            {
                return addUserCommand ??
                  (addUserCommand = new RelayCommand((o) =>
                  {
                      
                      var checkUsers = db.Users.FirstOrDefault(cu => cu.Email == TextEmail);
                      if (checkUsers is null)
                      {
                          if (TextPassword.Length > 0)
                          {
                              if (TextPassword.Equals(TextRePassword))
                              {
                                  User user = new User(TextName, TextSurname, TextEmail, TextPhone, TextPassword);
                                  db.Users.Add(user);
                                  db.SaveChanges();
                                  TextName = "";
                                  TextSurname = "";
                                  TextEmail = "";
                                  TextPhone = "";
                                  TextPassword = "";
                                  TextRePassword = "";
                                  mainViewModel.CurrentPage = mainViewModel.loginPage;
                                  mainViewModel.OnSend(true, "Аккаунт создан");
                              }
                              else
                              {
                                  //Пароли не совпадают
                                  mainViewModel.OnSend(false, "Пароли не совпадают");
                              }
                          }
                          else
                          {
                              //Длина пароля больше 0
                              mainViewModel.OnSend(false, "Пустое поле пароля");
                          }
                      }
                      else
                      {
                          //Пользователь уже существует
                          mainViewModel.OnSend(false, "email занят");
                      }
                  }));
            }
        }
        public RelayCommand ToLoginCommand
        {
            get
            {
                return toLoginCommand ??
                  (toLoginCommand = new RelayCommand((o) =>
                  {
                      mainViewModel.CurrentPage = mainViewModel.loginPage;
                  }));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
