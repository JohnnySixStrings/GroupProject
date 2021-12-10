using GroupProject.Item;
using GroupProject.Main;
using GroupProject.Models;
using GroupProject.Search;
using System;
using System.Windows;
using System.Windows.Input;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        private readonly wndSearch _searchWindow;
        private readonly wndItems _itemsWindow;
        private readonly IDisposable CancelDisposableSearch;
        private readonly IDisposable CancelDisposableItem;
        private readonly MainViewModel _mainViewModel;
        public MainWindow() 
        {
            _itemsWindow = new wndItems();
            _searchWindow = new wndSearch();
            _mainViewModel = new MainViewModel();


            DataContext = _mainViewModel;

            InitializeComponent();
            CancelDisposableSearch = _searchWindow.CancelObservable.Subscribe((x) =>
            {
                _searchWindow.Hide();
                this.Show();
            });

            CancelDisposableItem = _itemsWindow.CancelObservable.Subscribe(x =>
            {
                _itemsWindow.Hide();
                this.Show();
            });
            _searchWindow.InvoiceSelected += HandleInvoiceSelected;

        }

        private void NavigateToSearch(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this._searchWindow.Show();
            _searchWindow.Owner = this;
        }
        private void NavigateToItems(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _itemsWindow.Show();
            _itemsWindow.Owner = this;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.DeleteInvoice();
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NewInvoice();
            InvoiceDatePicker.IsEnabled = true;
            TotalCostTextBox.IsEnabled = true;
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
        private void HandleInvoiceSelected(object sender, RoutedEventArgs e)
        {
            _mainViewModel.ChangeInvoice((Invoice)e.Source);
            _searchWindow.Hide();
            Show();
        }
        public void Dispose()
        {
            CancelDisposableItem?.Dispose();
            CancelDisposableSearch?.Dispose();
            _searchWindow.InvoiceSelected -= HandleInvoiceSelected;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var valid = int.TryParse(e.Text, out _);
            e.Handled = !valid;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
           InvoiceDatePicker.IsEnabled = true;
           InvoiceIdTextBox.IsEnabled = true;
           TotalCostTextBox.IsEnabled = true;
        }
    }
}
