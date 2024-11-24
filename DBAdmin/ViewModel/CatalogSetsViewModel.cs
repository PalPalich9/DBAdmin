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
    public class CatalogSetsViewModel : INotifyPropertyChanged
    {
        
        public ObservableCollection<LegoSet> LegoSets { get; set; }
        public ObservableCollection<string> SortComboBox { get; set; }
        

        RelayCommand? _addToBacketCommand;
        public RelayCommand AddToBacketCommand => _addToBacketCommand;

        RelayCommand? _checkItemCommand;
        public RelayCommand CheckItemCommand => _checkItemCommand;

        private string selectedComboBox;
        public string SelectedComboBox
        {
            get => selectedComboBox;
            set
            {
                selectedComboBox = value;
                OnPropertyChanged(nameof(SelectedComboBox));      
                SortLegoSets();
            }
        }

        public CatalogSetsViewModel(ObservableCollection<LegoSet> LegoSets, RelayCommand relayCommand, RelayCommand checkItemCommand)
        {
            this.LegoSets = LegoSets;
            _addToBacketCommand = relayCommand;
            _checkItemCommand = checkItemCommand;
            SortComboBox = new ObservableCollection<string>
            {
                { "In Stock" },
                { "Price: High to Low" },
                { "Price: Low to High" },
                { "Piece Count: High to Low" }
            };
            SelectedComboBox = SortComboBox[0];

        }
        public void SortLegoSets()
        {
            var selectedSets = LegoSets.ToList();
            LegoSets.Clear();

            switch (SelectedComboBox)
            {
                
                case "In Stock":
                    var items1 = selectedSets.OrderByDescending(s => s.Count).ToList(); 
                    foreach (var item in items1)
                    {
                        LegoSets.Add(item);
                    }
                    break;
                case "Price: High to Low":
                    var items2 = selectedSets.OrderByDescending(s => s.Price).ToList();
                    foreach (var item in items2)
                    {
                        LegoSets.Add(item);
                    }
                    break;
                case "Price: Low to High":
                    var items3 = selectedSets.OrderBy(s => s.Price).ToList();
                    foreach (var item in items3)
                    {
                        LegoSets.Add(item);
                    }
                    break;
                case "Piece Count: High to Low":
                    var items4 = selectedSets.OrderByDescending(s => s.PieceCount).ToList();
                    foreach (var item in items4)
                    {
                        LegoSets.Add(item);
                    }
                    break;
            }
            

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
