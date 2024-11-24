using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DBAdmin.Model;
//using Microsoft.VisualBasic.ApplicationServices;

namespace DBAdmin.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        MainViewModel mainViewModel;

        private User user;
        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Surname));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Name => User?.Name;
        public string Surname => User?.Surname;
        public string Email => User?.Email;
        public string Phone => User?.Phone;


        RelayCommand? logoutCommand;

 
        
        public UserViewModel(MainViewModel mainViewModel, User user)
        {
            this.mainViewModel = mainViewModel;
            this.User = user;
        }
        public RelayCommand LogoutCommand
        {
            get
            {
                return logoutCommand ??
                  (logoutCommand = new RelayCommand((o) =>
                  {

                      mainViewModel.isUser = false;
                      mainViewModel.CurrentPage = mainViewModel.loginPage;
                      mainViewModel.OnSend(true, $"Успешный выход");
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
