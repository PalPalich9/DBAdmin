using DBAdmin.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DBAdmin.ViewModel
{
    public class BacketViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BacketSet> BacketSets { get; set; }

        RelayCommand? _incrementCommand;
        RelayCommand? _decrementCommand;
        RelayCommand? _deleteCommand;
        RelayCommand? _updateCommand;

        private string address;
        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private int summary;
        public int Summary
        {
            get {
                return summary;  
            }
            set {
                summary = value;
                OnPropertyChanged(nameof(Summary));
            }
        }

        public RelayCommand IncrementCommand => _incrementCommand;
        public RelayCommand DecrementCommand => _decrementCommand;
        public RelayCommand DeleteCommand => _deleteCommand;
        public RelayCommand UpdateCommand => _updateCommand;



        public BacketViewModel(LegoSet selectedSet)
        {
            
            var existingSet = BacketSets.FirstOrDefault(b => b.article == selectedSet.article);
            if (existingSet != null)
            {
                existingSet.InBacket++;
            }
            else
            {
                BacketSets.Add(new BacketSet(selectedSet, 1));
            }
        }
        public BacketViewModel(ObservableCollection<BacketSet> BacketSets, RelayCommand increment, RelayCommand decrement, RelayCommand delete, RelayCommand update)
        {
            _incrementCommand = increment;
            _decrementCommand = decrement;
            _deleteCommand = delete;
            _updateCommand = update;
            this.BacketSets = BacketSets;

        }
        public void UpdateSummary(int summary)
        {
            Summary = summary;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
