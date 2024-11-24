using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DBAdmin.Model;
using DBAdmin.pages;
using DBAdmin.ViewModel;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Hardcodet.Wpf.TaskbarNotification;
using Flattinger.Core.MVVM;
using Flattinger.UI.ToastMessage;
using Flattinger.UI.ToastMessage.Controls;
using Flattinger.Core.Interface.ToastMessage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Windows;
using System.Xml.Linq;


namespace DBAdmin.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<BacketSet> BacketSets { get; set; } = new ObservableCollection<BacketSet>();
        public ObservableCollection<LegoSet> LegoSets { get; set; } = new ObservableCollection<LegoSet>();
        public ObservableCollection<LegoSet> SelectedSets { get; set; } = new ObservableCollection<LegoSet>();

        public bool isUser = false;

        public BacketPage backetPage;
        public BacketViewModel backetViewModel;

        public CatalogSetsPage catalogSetsPage;
        public CatalogSetsViewModel catalogSetsViewModel;

        public ItemPage itemPage;
        public ItemViewModel itemViewModel;

        public UserPage userPage;
        public UserViewModel userViewModel;

        public RegistrationPage registrationPage;
        public RegistrationViewModel registrationViewModel;

        public LoginPage loginPage;
        public LoginViewModel loginViewModel;

        public CollectionPage collectionPage;

        private bool isCollectionPage = true;
        private bool isCatalogSetsPage = false;
        private bool isBacketPage = false;
        private bool isUserPage = false;
        private bool isLoginPage = false;
        private bool isRegistrationPage = false;

        private string currentCollection;

        private ToastProvider toastProvider;

        RelayCommand? navigateCommand;
        RelayCommand? backCommand;
        RelayCommand? addToBacketCommand;
        RelayCommand? toBacketCommand;
        RelayCommand? homeCommand;
        RelayCommand? incrementCommand;
        RelayCommand? decrementCommand;
        RelayCommand? deleteCommand;
        RelayCommand? updateCommand;
        RelayCommand? checkItemCommand;
        RelayCommand toUserCommand;


        private Frame frame;
        private Visibility backButtonVisibility;


        private object _currentpage;
        public object CurrentPage
        {
            get => _currentpage;
            set
            {
                _currentpage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public event Action<string> FilterTextChanged;

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                OnPropertyChanged(nameof(FilterText));
                FilterData2(filterText);
            }
        }
        private LegoCollection selectedItem;
        public LegoCollection SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private LegoSet selectedSet;
        public LegoSet SelectedSet
        {
            get { return selectedSet; }
            set
            {
                selectedSet = value;
                OnPropertyChanged(nameof(SelectedSet));
            }
        }
        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public Visibility BackButtonVisibility
        {

            get => backButtonVisibility;
            set
            {
                backButtonVisibility = value;
                OnPropertyChanged(nameof(BackButtonVisibility));
            }
        }
        private int summary;
        public int Summary
        {
            get => summary;
            set
            {
                summary = value;
                OnPropertyChanged(nameof(Summary));
            }
        }
        public void ToCollectionPage()
        {
            isCollectionPage = true;
            isCatalogSetsPage = false;
            isBacketPage = false;
            isLoginPage = false;
            isRegistrationPage = false;
            isUserPage = false;

        }
        public void ToCatalogSetsPage(string currentCollection)
        {
            this.currentCollection = currentCollection;
            isCollectionPage = false;
            isCatalogSetsPage = true;
            isBacketPage = false;
            isLoginPage = false;
            isRegistrationPage = false;
            isUserPage = false;
        }
        public void ToBacketPage()
        {
            isCollectionPage = false;
            isCatalogSetsPage = false;
            isBacketPage = true;
            isLoginPage = false;
            isRegistrationPage = false;
            isUserPage = false;
        }
        public void ToUserPage()
        {
            isCollectionPage = false;
            isCatalogSetsPage = false;
            isBacketPage = false;
            isLoginPage = false;
            isRegistrationPage = false;
            isUserPage = true;
        }
        public void ToRegistrationPage()
        {
            isCollectionPage = false;
            isCatalogSetsPage = false;
            isBacketPage = false;
            isLoginPage = false;
            isRegistrationPage = true;
            isUserPage = false;
        }
        public void ToLoginPage()
        {
            isCollectionPage = false;
            isCatalogSetsPage = false;
            isBacketPage = false;
            isLoginPage = true;
            isRegistrationPage = false;
            isUserPage = false;

        }
        public MainViewModel(Frame frame, NotificationContainer notificationContainer)
        {
            toastProvider = new ToastProvider(notificationContainer);
            this.frame = frame;

            backetViewModel = new BacketViewModel(BacketSets, IncrementCommand, DecrementCommand, DeleteCommand, UpdateCommand);
            backetPage = new BacketPage { DataContext = backetViewModel };

            catalogSetsViewModel = new CatalogSetsViewModel(SelectedSets, AddToBacketCommand, CheckItemCommand);
            catalogSetsPage = new CatalogSetsPage { DataContext = catalogSetsViewModel };

            registrationViewModel = new RegistrationViewModel(this);
            registrationPage = new RegistrationPage { DataContext = registrationViewModel};

            loginViewModel = new LoginViewModel(this);
            loginPage = new LoginPage { DataContext = loginViewModel };

            userViewModel = new UserViewModel(this, User);
            userPage = new UserPage { DataContext = userViewModel };

            collectionPage = new CollectionPage { DataContext = new CollectionViewModel(NavigateCommand) };
            BackButtonVisibility = Visibility.Collapsed;
            GetDataFromDB();
            CurrentPage = collectionPage;

            // CurrentPage = new DBPage { DataContext = new DBViewModel(NavigateCommand) };
        }
        public void OnSend(bool type, string toastText)
        {
            if (type)
            {
                toastProvider.NotificationService.AddNotification(Flattinger.Core.Enums.ToastType.SUCCESS, "", toastText, 1, animationConfig: new AnimationConfig { });

            }
            else
            {
                toastProvider.NotificationService.AddNotification(Flattinger.Core.Enums.ToastType.WARNING, "", toastText, 1, animationConfig: new AnimationConfig { });

            }
        }
        public RelayCommand NavigateCommand
        {
            get
            {
                return navigateCommand ??
                  (navigateCommand = new RelayCommand((o) =>
                  {
                      selectedItem = o as LegoCollection;
                      FilterData(selectedItem.Name);
                      CurrentPage = catalogSetsPage;
                      BackButtonVisibility = Visibility.Visible;
                      ToCatalogSetsPage(selectedItem.Name);
                  }));
            }
        }
        public void FilterData(string filterText)
        {
            SelectedSets.Clear();
            foreach (var filtredSet in LegoSets.Where(fs => fs.Collection.Contains(filterText, StringComparison.OrdinalIgnoreCase))
                
                .ToList())
            {
                SelectedSets.Add(filtredSet);
                
            }
        }
        public void FilterData2(string filterText)
        {
            string trimmedFilterText = filterText.Replace(" ", string.Empty);
            if (string.IsNullOrEmpty(trimmedFilterText))
            {
                if (isCollectionPage)
                {
                    CurrentPage = collectionPage;
                    catalogSetsViewModel.SelectedComboBox = catalogSetsViewModel.SortComboBox[0];

                }
                if (isCatalogSetsPage)
                {
                    FilterData(currentCollection);

                }
                if (isBacketPage)
                {
                    CurrentPage = backetPage;
                    catalogSetsViewModel.SelectedComboBox = catalogSetsViewModel.SortComboBox[0];

                }
                if (isLoginPage)
                {
                    CurrentPage = loginPage;
                    catalogSetsViewModel.SelectedComboBox = catalogSetsViewModel.SortComboBox[0];

                }
                if (isUserPage)
                {
                    CurrentPage = userPage;
                    catalogSetsViewModel.SelectedComboBox = catalogSetsViewModel.SortComboBox[0];

                }
            }
            else
            {
                if (isCollectionPage)
                {
                    CurrentPage = catalogSetsPage;
                    SelectedSets.Clear();
                    var filteredSets = LegoSets.Where(fs => fs.Name.Replace(" ", string.Empty).Contains(trimmedFilterText, StringComparison.OrdinalIgnoreCase)).ToList();
                    if (filteredSets is null || filteredSets.Count == 0)
                    {
                        foreach (var filteredSet in LegoSets.Where(fs => fs.Collection.Replace(" ", string.Empty).Contains(trimmedFilterText, StringComparison.OrdinalIgnoreCase)).ToList())
                        {
                            SelectedSets.Add(filteredSet);
                        }
                    }
                    else
                    {
                        foreach (var filteredSet in filteredSets)
                        {
                            SelectedSets.Add(filteredSet);
                        }
                    }
                    
                    BackButtonVisibility = Visibility.Visible;
                }
                if (isCatalogSetsPage)
                {
                    CurrentPage = catalogSetsPage;
                    SelectedSets.Clear();
                    foreach (var filteredSet in LegoSets.Where(fs => fs.Name.Replace(" ", string.Empty).Contains(trimmedFilterText, StringComparison.OrdinalIgnoreCase)
                    && fs.Collection == currentCollection).ToList())

                    {
                        SelectedSets.Add(filteredSet);
                    }
                    BackButtonVisibility = Visibility.Visible;
                }
                else
                {
                    CurrentPage = catalogSetsPage;
                    SelectedSets.Clear();
                    var filteredSets = LegoSets.Where(fs => fs.Name.Replace(" ", string.Empty).Contains(trimmedFilterText, StringComparison.OrdinalIgnoreCase)).ToList();
                    if (filteredSets is null || filteredSets.Count == 0)
                    {
                        foreach (var filteredSet in LegoSets.Where(fs => fs.Collection.Replace(" ", string.Empty).Contains(trimmedFilterText, StringComparison.OrdinalIgnoreCase)).ToList())
                        {
                            SelectedSets.Add(filteredSet);
                        }
                    }
                    else
                    {
                        foreach (var filteredSet in filteredSets)
                        {
                            SelectedSets.Add(filteredSet);
                        }
                    }
                    BackButtonVisibility = Visibility.Visible;
                }


            }
        }
        public void FilterSelectedSets()
        {

        }
        public RelayCommand ToUserCommand
        {
            get
            {
                return toUserCommand ??
                  (toUserCommand = new RelayCommand((o) =>
                  {
                      if (isUser)
                      {
                          CurrentPage = userPage;
                          ToUserPage();
                      }
                      else
                      {
                          CurrentPage = loginPage;
                          ToLoginPage();
                      }


                  }));
            }
        }
        
        public RelayCommand AddToBacketCommand
        {
            get
            {
                return addToBacketCommand ??
                  (addToBacketCommand = new RelayCommand((o) =>
                  {
                      if (CurrentPage is not ItemPage)
                      {
                          selectedSet = o as LegoSet;
                          AddToBacket();

                      }
                      else
                      {
                          AddToBacket();
                      }
                      
                  }));
            }
        }
        public void AddToBacket()
        {
            BacketSet backetSet = new BacketSet(selectedSet, 0);
            var existingSet = backetSet;
            existingSet = BacketSets.FirstOrDefault(b => b.article == selectedSet.article);
            if (existingSet != null)
            {
                if (existingSet.InBacket < existingSet.Count)
                {
                    existingSet.InBacket++;
                    UpdateValueSummary();
                    OnSend(true, "Успешно дабавлен в корзину");
                }
                else
                {
                    OnSend(false, "Больше нету(");
                }
            }
            else
            {
                if (backetSet.Count > 0)
                {
                    BacketSets.Add(new BacketSet(selectedSet, 1));
                    UpdateValueSummary();
                    OnSend(true, "Успешно дабавлен в корзину");
                }
                else
                {
                    OnSend(false, "Нет в наличии");
                }
            }
        }
        public RelayCommand ToBacketCommand
        {
            get
            {
                return toBacketCommand ??
                  (toBacketCommand = new RelayCommand((o) =>
                  {
                      BackButtonVisibility = Visibility.Collapsed;
                      CurrentPage = backetPage;
                      ToBacketPage();
                  }));
            }
        }
        public RelayCommand HomeCommand
        {
            get
            {
                return homeCommand ??
                  (homeCommand = new RelayCommand((o) =>
                  {
                      BackButtonVisibility = Visibility.Collapsed;
                      CurrentPage = collectionPage;
                      catalogSetsViewModel.SelectedComboBox = catalogSetsViewModel.SortComboBox[0];

                      ToCollectionPage();
                  }));
            }
        }
        public RelayCommand IncrementCommand
        {
            get
            {
                return incrementCommand ??
                  (incrementCommand = new RelayCommand((o) =>
                  {
                      var backetSelected = o as BacketSet;
                      if (backetSelected.InBacket < backetSelected.Count)
                      {
                          backetSelected.InBacket++;
                          UpdateValueSummary();
                          OnSend(true, "Успешно дабавлен в корзину");
                      }
                      else
                      {
                          OnSend(false, "Больше нету(");
                      }

                  }));
            }
        }
        public RelayCommand DecrementCommand
        {
            get
            {
                return decrementCommand ??
                  (decrementCommand = new RelayCommand((o) =>
                  {
                      var backetSelected = o as BacketSet;
                      if (backetSelected.InBacket != 1)
                      {
                          backetSelected.InBacket--;
                          UpdateValueSummary();
                      }
                      else
                      {
                          BacketSets.Remove(backetSelected);
                          UpdateValueSummary();
                          //OnSend(true, "Набор удален из корзины");
                          OnSend(true, $"{User.Name}");

                      }
                  }));
            }
        }
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((o) =>
                  {
                      var backetSelected = o as BacketSet;
                      BacketSets.Remove(backetSelected);
                      UpdateValueSummary();
                      OnSend(true, "Набор удален из корзины");
                  }));
            }
        }
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                  (updateCommand = new RelayCommand((o) =>
                  {
                      if (isUser)
                      {
                          if (BacketSets.Count != 0)
                          {
                              if (!string.IsNullOrWhiteSpace(backetViewModel.Address))
                              {
                                  using (var context = new ApplicationContext())
                                  {
                                      foreach (var backetSet in BacketSets)
                                      {
                                          var legoSet = LegoSets.FirstOrDefault(ls => ls.article == backetSet.article);
                                          if (legoSet != null)
                                          {
                                              legoSet.Count -= backetSet.InBacket;

                                              var existingLegoSet = context.LegoSets.Find(legoSet.article);
                                              if (existingLegoSet != null)
                                              {
                                                  existingLegoSet.Count = legoSet.Count;
                                              }
                                          }
                                      }
                                      context.SaveChanges();
                                  }
                                  /*var order = new Order
                                  {
                                      UserId = currentUserId, // Установите ID текущего пользователя
                                      InBacket = backetViewModel.BacketSets.Sum(b => b.InBacket),
                                      LegoSets = backetViewModel.BacketSets.Select(b => b.article).ToList(),
                                      SumOrder = backetViewModel.Summary,
                                      Address = backetViewModel.Address,
                                      CreatedAt = DateTime.Now
                                  };*/

                                  // Сохраните заказ в базе данных
                                  //SaveOrder(order);
                                  backetViewModel.Address = "";
                                  BacketSets.Clear();
                                  UpdateValueSummary();
                                  OnSend(true, "Заказ оформлен");
                              }
                              else
                              {
                                  OnSend(false, "Введите адрес");
                              }
                          }
                         else
                          {
                              OnSend(false, "Корзина пуста");
                          }

                      }
                      else
                      {
                          CurrentPage = loginPage;
                          ToLoginPage();
                          OnSend(false, "Войдите в аккаунт");
                      }
                      
                  }));
            }
        }
       /* public void SaveOrder(Order order)
        {
            using (var context = new ApplicationContext())
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
        }*/
        public RelayCommand CheckItemCommand
        {
            get
            {
                return checkItemCommand ??
                  (checkItemCommand = new RelayCommand((o) =>
                  {
                      selectedSet = o as LegoSet;
                      itemViewModel = new ItemViewModel(AddToBacketCommand, selectedSet);
                      itemPage = new ItemPage { DataContext = itemViewModel };
                      CurrentPage = itemPage;
                  }));
            }
        }
        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ??
                  (backCommand = new RelayCommand((o) =>
                  {
                      if (frame.CanGoBack)
                      {
                          CurrentPage = collectionPage;
                          catalogSetsViewModel.SelectedComboBox = catalogSetsViewModel.SortComboBox[0];
                          FilterText = "";
                          BackButtonVisibility = Visibility.Collapsed;
                          ToCollectionPage();
                      }                   
                  }));
            }
        }
        public void UpdateValueSummary()
        {
            Summary = BacketSets.Sum(item => item.Price * item.InBacket);
            backetViewModel.UpdateSummary(Summary);
        }
        public void GetDataFromDB()
        {
            using (var context = new ApplicationContext())
            {
                var legoSets = context.LegoSets
                    .ToList();
                foreach (var legoSet in legoSets)
                {
                    LegoSets.Add(legoSet);
                }
            }
        }

        public void OnFilterTextChanged(string filterText)
        {
            FilterTextChanged?.Invoke(filterText);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
