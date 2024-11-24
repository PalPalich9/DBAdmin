using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace DBAdmin
{
    public class LegoSet : INotifyPropertyChanged
    {
        public int article { get; set; }
        public string name;
        public int price;
        public int count;
        public int pieceCount;
        public string collection;
        public byte[] image;
        
        public int Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }
        public int PieceCount
        {
            get { return pieceCount; }
            set
            {
                pieceCount = value;
                OnPropertyChanged("Count");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Collection
        {
            get { return collection; }
            set
            {
                collection = value;
                OnPropertyChanged("Collection");
            }
        }
        public byte[] Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }



        public LegoSet(int article, string name, int price, int pieceCount, int count, string collection, byte[] image)
        {
            this.article = article;
            this.name = name;
            this.price = price;
            this.pieceCount = pieceCount;
            this.count = count;
            this.collection = collection;
            this.image = image;
        }
       
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
