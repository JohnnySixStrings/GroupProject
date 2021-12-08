﻿using GroupProject.Models;
using GroupProject.Repositories;
using System.Collections.ObjectModel;

public class clsItemsLogic
{
    // clsItemsSQL SQL = new clsItemsSQL();
    // char Code;

    private readonly InvoiceRepository _invoiceRepository;
    public ObservableCollection<ItemDescription> Items { get; set; }
    /// <summary>
    /// Constructor for clsItemsLogic class
    /// </summary>
    public clsItemsLogic()
    {
        // Insert constructor code here.
        _invoiceRepository = new InvoiceRepository();
        var items = _invoiceRepository.GetAllItems();
        Items = new ObservableCollection<ItemDescription>(items);
    }

    /// <summary>
    /// Deletes selected item from database
    /// </summary>
    private void DeleteItem()
    {
        // Checks if the item is on an invoice
        // If not, delete it from the database
    }

    /// <summary>
    /// Updates the currently selected item with new description and cost
    /// </summary>
    private void UpdateItem()
    {
        // Checks if the new data is acceptable
        // If so, Updates the selected item with new data
    }

    /// <summary>
    /// Adds new item to the database
    /// </summary>
    private void NewItem()
    {
        // Checks if the entered data is acceptable
        // If so, adds new item to the database
    }
}
