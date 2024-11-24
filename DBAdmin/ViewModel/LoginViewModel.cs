using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DBAdmin.Model;

namespace DBAdmin.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        ApplicationContext db = new ApplicationContext();

        MainViewModel mainViewModel;

        private string textEmail;
        private string textPassword;
        RelayCommand? loginCommand;
        RelayCommand? toRegistrationCommand;


        public string TextEmail
        {
            get => textEmail;
            set
            {
                textEmail = value;
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
        public LoginViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                  (loginCommand = new RelayCommand((o) =>
                  {

                      var checkUsers = db.Users.FirstOrDefault(cu => cu.Email == TextEmail);
                      if (checkUsers is not null)
                      {
                          if (checkUsers.Password == TextPassword)
                          {
                              
                              mainViewModel.User = checkUsers;
                              mainViewModel.isUser = true;
                              mainViewModel.userViewModel.User = checkUsers;
                              TextEmail = "";
                              TextPassword = "";
                              mainViewModel.CurrentPage = mainViewModel.userPage;
                              mainViewModel.ToUserPage();
                              mainViewModel.OnSend(true, $"Добро пожаловать, {checkUsers.Name}");
                          }
                          else
                          {
                              mainViewModel.OnSend(false, "Другой пароль");
                          }
                      }
                      else
                      {
                          mainViewModel.OnSend(false, "The user does not exist");
                      }
                  }));
            }
        }
        public RelayCommand ToRegistrationCommand
        {
            get
            {
                return toRegistrationCommand ??
                  (toRegistrationCommand = new RelayCommand((o) =>
                  {
                        mainViewModel.CurrentPage = mainViewModel.registrationPage;         
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
