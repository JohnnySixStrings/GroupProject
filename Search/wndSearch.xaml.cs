using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace GroupProject.Search
{

    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// Obserable that observes the cancel button click and can be subscribed
        /// </summary>        
        public IObservable<EventPattern<RoutedEventArgs>> CancelObservable { get; }
        public event EventHandler<RoutedEventArgs> InvoiceSelected;
        public Invoice SelectedInvoice { get; set; }
        /// <summary>
        /// The DataContext that handles the logic behind the class
        /// </summary>
        private clsSearchLogic SearchLogic { get; set; }

        /// <summary>
        /// Boolean used to flag if the program is clearing all the comboboxes to prevent lag and increase efficiency.
        /// </summary>
        private bool bisClearingSelection = false;

        /// <summary>
        /// Constructor for the Search window. Loads in the Combo boxes with unique items.
        /// </summary>
        public wndSearch()
        {
            try
            {
                InitializeComponent();
                SearchLogic = new clsSearchLogic();
                DataContext = SearchLogic;
                CancelObservable = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                    x => this.btnCancel.Click += x,
                    x => this.btnCancel.Click -= x);

                LoadComboBoxes();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Runs when the user clicks the "Select" button. Sends the selected invoice back to the main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is not null && dgResults.SelectedItem is not null)
                {
                    e.Source = dgResults.SelectedItem;
                    InvoiceSelected?.Invoke(this, e);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Runs when the user clicks the "Cancel" button. Closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Runs when the user clicks the "Clear Selection" button. clears all selections in all the comboboxes. refreshes dataGrid automatically
        /// I have an bisClearingSelection variable that will override the SelectionChanged() method, so that the method
        /// doesn't run 3 times (each time a ComboBox is set to null), only the last time, when all are null.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bisClearingSelection = true;
                cbInvoiceDate.SelectedItem = null;
                cbInvoiceNum.SelectedItem = null;
                bisClearingSelection = false;
                cbTotalCharge.SelectedItem = null;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// This function will run anytime the user makes a change in selection for any of the comboboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                clsSearchLogic sl = new clsSearchLogic();

                dgResults.ItemsSource = sl.getUpdatedData(cbTotalCharge, cbInvoiceDate, cbInvoiceNum);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Function to load all the comboBoxes with unique values at the beginning of this program/window.
        /// </summary>
        private void LoadComboBoxes()
        {
            try
            {
                clsSearchLogic sl = new clsSearchLogic();

                List<Invoice> InvoiceList = sl.getInvoices();

                List<Decimal> Costs = new List<Decimal>();
                List<int> IDs = new List<int>();
                List<DateTime> Dates = new List<DateTime>();


                foreach (Invoice iv in InvoiceList)
                {
                    Costs.Add(iv.TotalCost);
                    IDs.Add(iv.InvoiceNum);
                    Dates.Add(iv.InvoiceDate);
                }

                //Found this code online to delete duplicates from a list. 
                Costs = Costs.Distinct().ToList();
                IDs = IDs.Distinct().ToList();
                Dates = Dates.Distinct().ToList();

                cbTotalCharge.ItemsSource = Costs;
                cbInvoiceDate.ItemsSource = Dates;
                cbInvoiceNum.ItemsSource = IDs;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end of LoadComboBoxes()

        
        /// <summary>
        /// This function handles errors that arise from all the TryCatch blocks.
        /// </summary>
        /// <param name="sClass">String of the name of the class where the error occurred</param>
        /// <param name="sMethod">String of the name of the method where the error occurred</param>
        /// <param name="sMessage">String of the message of the error that occurred</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }//End of HandleError
    }
}
