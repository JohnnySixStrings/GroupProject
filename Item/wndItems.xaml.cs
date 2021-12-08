using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;

namespace GroupProject.Item
{
    public partial class wndItems : Window
    {
        /// <summary>
        /// An Observable that watches the cancel button click and can be subscirbed to
        /// </summary>
        public IObservable<EventPattern<RoutedEventArgs>> CancelObservable { get; }
        private clsItemsLogic ItemsLogic;

        // clsItemsLogic Logic = new clsItemsLogic();

        public wndItems()
        {
            InitializeComponent();
            ItemsLogic = new clsItemsLogic();
            DataContext = ItemsLogic;
            CancelObservable = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                x => this.ExitButton.Click += x,
                x => this.ExitButton.Click -= x);
        }

        /// <summary>
        /// Updates selected item and updates UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Runs clsItemsLogic.UpdateItem
            // If it fails, display error message
            // If it succeeds, update UI and display success message
        }

        /// <summary>
        /// Deletes selected item from database and updates UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Runs clsItemsLogic.DeleteItem
            // If it fails, display error message
            // If it succeeds, update UI and display success message
        }

        /// <summary>
        /// Adds new item to the database and updates UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            // Runs clsItemsLogic.NewItem
            // If it fails, display error message
            // If it succeeds, update UI and display success message
        }

        /// <summary>
        /// Closes this window and returns to main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            // Return to main window
        }
    }
}
