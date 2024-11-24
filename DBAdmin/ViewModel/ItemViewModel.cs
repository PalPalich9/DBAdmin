using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAdmin.ViewModel
{
    public class ItemViewModel
    {
        public LegoSet LegoSet { get; set; }
        RelayCommand? _addToBacketCommand;
        public RelayCommand AddToBacketCommand => _addToBacketCommand;
        public string Name => LegoSet?.Name;
        public int Price => LegoSet?.Price ?? 0;
        public int PieceCount => LegoSet?.PieceCount ?? 0;
        public byte[] Image => LegoSet?.Image;

        public ItemViewModel(RelayCommand addToBacket, LegoSet legoSet)
        {
            _addToBacketCommand = addToBacket;
            this.LegoSet = legoSet;
        }
    }
}
