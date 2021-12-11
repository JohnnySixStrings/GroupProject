using GroupProject.Item;
using GroupProject.Main;
using GroupProject.Models;
using GroupProject.Search;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        /// <summary>
        /// Instance of the searchWindow
        /// </summary>
        private readonly wndSearch _searchWindow;
        /// <summary>
        /// Instance of the item window
        /// </summary>
        private readonly wndItems _itemsWindow;
        /// <summary>
        /// Disposable for subscription to the SearchCancelEvent
        /// </summary>
        private readonly IDisposable CancelDisposableSearch;
        /// <summary>
        /// Disposable for subscription to the Item exit/ cancel event
        /// </summary>
        private readonly IDisposable CancelDisposableItem;
        /// <summary>
        /// Business logic class 
        /// </summary>
        private readonly clsMainLogic _mainViewModel;
        public MainWindow()
        {
            _itemsWindow = new wndItems();
            _searchWindow = new wndSearch();
            _mainViewModel = new clsMainLogic();


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
                _mainViewModel.UpdateContext();
            });

            _searchWindow.InvoiceSelected += HandleInvoiceSelected;
            _searchWindow.Closed += ChildWindow_Closing;
            _itemsWindow.Closed += ChildWindow_Closing;
        }

        /// <summary>
        /// Handles the closing of all the windows if one is closed to not leave the program running in an un interactable state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildWindow_Closing(object? sender, EventArgs e)
        {
            try
            {
                _searchWindow?.Close();
                _itemsWindow?.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Logic for navigating to the search window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToSearch(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                this._searchWindow.Show();
                _searchWindow.Owner = this;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// logic for navigating to the items window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToItems(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                _itemsWindow.Show();
                _itemsWindow.Owner = this;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Deletes the currently selected invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _mainViewModel.DeleteInvoice();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Creates a new invoice to be saved 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _mainViewModel.NewInvoice();
                InvoiceDatePicker.IsEnabled = true;
                InvoiceIdTextBox.IsEnabled = false;
                TotalCostTextBox.IsEnabled = true;
                LineItemsDataGrid.IsEnabled = true;
                AddItemButton.IsEnabled = true;
                InvoiceDeleteButton.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        ///  Saves current item wether that is an update or a create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _mainViewModel.SaveInvoice();
                InvoiceDatePicker.IsEnabled = false;
                InvoiceIdTextBox.IsEnabled = false;
                TotalCostTextBox.IsEnabled = false;
                LineItemsDataGrid.IsEnabled = false;
                AddItemButton.IsEnabled = false;
                InvoiceDeleteButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        ///  Handles the adding of an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ItemsComboBox.SelectedItem != null)
                {
                    _mainViewModel.AddItem((ItemDescription)ItemsComboBox.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles the Invoice selected event on the search window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleInvoiceSelected(object? sender, RoutedEventArgs e)
        {
            try
            {
                _mainViewModel.ChangeInvoice((Invoice)e.Source);
                _searchWindow.Hide();
                Show();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handling disposing of releasing resources
        /// </summary>
        public void Dispose()
        {
            CancelDisposableItem?.Dispose();
            CancelDisposableSearch?.Dispose();
            _searchWindow.InvoiceSelected -= HandleInvoiceSelected;
            _searchWindow.Closing -= ChildWindow_Closing;
            _itemsWindow.Closing -= ChildWindow_Closing;
        }

        /// <summary>
        /// Makes sure only numbers are allowed in cost and id field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var valid = int.TryParse(e.Text, out _);
            e.Handled = !valid;
        }

        /// <summary>
        /// Updating state for when the edit button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InvoiceDatePicker.IsEnabled = true;
                InvoiceIdTextBox.IsEnabled = true;
                TotalCostTextBox.IsEnabled = true;
                LineItemsDataGrid.IsEnabled = true;
                AddItemButton.IsEnabled = true;
                InvoiceDeleteButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Method for handling the errors and displaying them
        /// </summary>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="message"></param>
        private static void HandleError(string className, string methodName, string message)
        {
            try
            {
                MessageBox.Show(className + "." + methodName + " -> " + message);
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
