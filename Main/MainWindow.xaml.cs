using GroupProject.Item;
using GroupProject.Main;
using GroupProject.Models;
using GroupProject.Search;
using System;
using System.Windows;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private wndSearch _searchWindow;
        private wndItems _itemsWindow;

        private readonly MainViewModel _mainViewModel;
        public MainWindow()
        {
            _itemsWindow = new wndItems();
            _searchWindow = new wndSearch();
            _mainViewModel = new MainViewModel();
            DataContext = _mainViewModel;

            InitializeComponent();
            _searchWindow.CancelObservable.Subscribe((x) =>
            {
                _searchWindow.Hide();
                this.Show();
            });

            _itemsWindow.CancelObservable.Subscribe(x =>
            {
                _itemsWindow.Hide();
                this.Show();
            });
        }

        private void NavigateToSearch(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this._searchWindow.Show();
        }
        private void NavigateToItems(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _itemsWindow.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.DeleteInvoice();
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NewInvoice();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.SaveInvoice();
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsComboBox.SelectedItem != null)
            {
                _mainViewModel.AddItem((ItemDescription)ItemsComboBox.SelectedItem);
            }
        }
    }
}
