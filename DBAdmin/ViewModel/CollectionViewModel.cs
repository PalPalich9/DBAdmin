using DBAdmin.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using DBAdmin.ViewModel;
using DBAdmin.pages;
using System.Windows.Input;
using DBAdmin.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DBAdmin.ViewModel
{
    class CollectionViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<LegoCollection> LegoCollections { get; set; } = new ObservableCollection<LegoCollection>();
        RelayCommand? _navigateCommand;
        public RelayCommand NavigateCommand => _navigateCommand;

        public CollectionViewModel(RelayCommand relayCommand) {


            _navigateCommand = relayCommand;

            using (var context = new ApplicationContext())
            {
                var legoCollections = context.LegoCollections.ToList();
                foreach (var legoCollection in legoCollections)
                {
                    LegoCollections.Add(legoCollection);
                }
            }
        }
        public CollectionViewModel()
        {
            using (var context = new ApplicationContext())
            {
                var legoCollections = context.LegoCollections.ToList();
                foreach (var legoCollection in legoCollections)
                {
                    LegoCollections.Add(legoCollection);
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
