﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GroupProject.Item
{
    public partial class wndItems : Window
    {
        // clsItemsLogic Logic = new clsItemsLogic();

        public wndItems()
        {
            InitializeComponent();
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
