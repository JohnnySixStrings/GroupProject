using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using System.Reflection;

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
            try
            {
                InitializeComponent();
                ItemsLogic = new clsItemsLogic();
                DataContext = ItemsLogic;
                CancelObservable = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                    x => this.ExitButton.Click += x,
                    x => this.ExitButton.Click -= x);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
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
            try
            {
                ItemsLogic.UpdateItem(Code.Text, txtDesc.Text, txtCost.Text);
                dgItems.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
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
            try
            {
                ItemsLogic.DeleteItem(Code.Text);
                dgItems.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
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
            try
            {
                ItemsLogic.NewItem(Code.Text, txtDesc.Text, txtCost.Text);
                dgItems.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
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
