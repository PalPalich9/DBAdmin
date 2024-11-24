using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DBAdmin.Model;

namespace DBAdmin.ViewModel
{
    class DBViewModel : INotifyPropertyChanged
    {
        ApplicationContext db = new ApplicationContext();

        RelayCommand? addCommand;
        RelayCommand? addCollectionCommand;
        RelayCommand? loadImageCommand;

        private int textArticle;
        private string textName;
        private string textCollection;
        private int textPrice;
        private int textCount;
        private int textPieceCount;
        private BitmapImage selectedImage;

        public int TextArticle
        {
            get => textArticle;
            set
            {
                textArticle = value;
                OnPropertyChanged();
            }
        }
        public string TextName
        {
            get => textName;
            set
            {
                textName = value;
                OnPropertyChanged();
            }
        }
        public string TextCollection
        {
            get => textCollection;
            set
            {
                textCollection = value;
                OnPropertyChanged();
            }
        }
        public int TextPrice
        {
            get => textPrice;
            set
            {
                textPrice = value;
                OnPropertyChanged();
            }
        }
        public int TextCount
        {
            get => textCount;
            set
            {
                textCount = value;
                OnPropertyChanged();
            }
        }
        public int TextPieceCount
        {
            get => textPieceCount;
            set
            {
                textPieceCount = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage SelectedImage
        {
            get => selectedImage;
            set
            {
                selectedImage = value;
                OnPropertyChanged();
            }
        }
        public DBViewModel() { 
            
        }

        RelayCommand? _navigateCommand;
        public RelayCommand NavigateCommand => _navigateCommand;

        public DBViewModel(RelayCommand relayCommand)
        {
            _navigateCommand = relayCommand;
            
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {

                      LegoSet legoSet = new LegoSet(TextArticle, TextName, TextPrice, TextPieceCount, TextCount, TextCollection, SelectedImage.ToByteArray());
                      db.LegoSets.Add(legoSet);
                      db.SaveChanges();

                  }));
            }
        }
        public RelayCommand AddCollectionCommand
        {
            get
            {
                return addCollectionCommand ??
                  (addCollectionCommand = new RelayCommand((o) =>
                  {

                      LegoCollection legoCollection = new LegoCollection(TextName, SelectedImage.ToByteArray());
                      db.LegoCollections.Add(legoCollection);
                      db.SaveChanges();

                  }));
            }
        }

        public RelayCommand LoadImageCommand
        {
            get
            {
                return loadImageCommand ??
                  (loadImageCommand = new RelayCommand((o) =>
                  {
                      OpenFileDialog openFileDialog = new OpenFileDialog();
                      openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.jfif";
                      if (openFileDialog.ShowDialog() == true)
                      {
                          SelectedImage = new BitmapImage(new Uri(openFileDialog.FileName));
                      }
                  }));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this BitmapImage bitmapImage)
        {
            using (var stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder(); 
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
    }
}
