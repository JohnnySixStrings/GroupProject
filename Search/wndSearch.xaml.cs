using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
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
        /// <summary>
        /// The DataContext that handles the logic behind the class
        /// </summary>
        private clsSearchLogic SearchLogic { get; set; }
        public wndSearch()
        {
            InitializeComponent();
            SearchLogic = new clsSearchLogic();
            DataContext = SearchLogic;
            CancelObservable = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                x => this.btnCancel.Click += x,
                x => this.btnCancel.Click -= x);

            LoadComboBoxes();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            //The selected invoice will be sent back to the main window. This will be sent by using a setter or function of some type.
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // wndSearch.Close(); this will be used to close the current window and show the MainWindow again. 
            this.Close();
        }


        /// <summary>
        /// This function will run anytime the user makes a change in selection for any of the comboboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            clsSearchLogic sl = new clsSearchLogic();

            dgResults.ItemsSource = sl.getUpdatedData(cbTotalCharge, cbInvoiceDate, cbInvoiceNum);
        }

        private void LoadComboBoxes()
        {
            clsSearchLogic sl = new clsSearchLogic();

            List<Invoice> InvoiceList = sl.getInvoices();

            foreach (Invoice iv in InvoiceList)
            {
                cbTotalCharge.Items.Add(iv.TotalCost);
                cbInvoiceDate.Items.Add(iv.InvoiceDate);
                cbInvoiceNum.Items.Add(iv.InvoiceDate);
            }


        }
    }
}
