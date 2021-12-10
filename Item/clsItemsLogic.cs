using GroupProject.Models;
using GroupProject.Repositories;
using System.Collections.ObjectModel;
using GroupProject.Item;
using System;
using System.Globalization;
using System.Reflection;
using System.Data;
using System.Windows;

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
    public void DeleteItem(string iCode)
    {
        // Checks if the item is on an invoice
        // If not, delete it from the database
        try
        {
            clsDataAccess DB = new clsDataAccess();
            clsItemsSQL SQL = new clsItemsSQL();
            string sSQL = SQL.SelectInvoiceID(iCode);
            int iRetVal = 0;
            DataSet DS = DB.ExecuteSQLStatement(sSQL, ref iRetVal);
            if(DS.Tables[0].Rows.Count > 0)
            {
                string s = "Cannot delete as this item is listed on the following invoices: ";
                for(int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    var invoiceNo = DS.Tables[0].Rows[i].ItemArray[0];
                    if (invoiceNo != null) s += invoiceNo.ToString();
                    if (i < DS.Tables[0].Rows.Count - 1) s += ", ";
                }
                MessageBox.Show(s);
                return;
            }
            int alpa = 1;
            sSQL = SQL.DeleteItemDesc(iCode);
            DB.ExecuteNonQuery(sSQL);
            for(int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ItemCode == iCode)
                {
                    Items.RemoveAt(i);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// Updates the currently selected item with new description and cost
    /// </summary>
    public void UpdateItem(string iCode, string iDesc, string iCost)
    {
        // Checks if the new data is acceptable
        // If so, Updates the selected item with new data
        try
        {
            clsDataAccess DB = new clsDataAccess();
            clsItemsSQL SQL = new clsItemsSQL();
            string sSQL = SQL.UpdateItemDesc(iCode, iDesc, iCost);
            DB.ExecuteNonQuery(sSQL);
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ItemCode == iCode)
                {
                    Items[i].Cost = Decimal.Parse(iCost, NumberStyles.AllowDecimalPoint);
                    Items[i].ItemDesc = iDesc;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// Adds new item to the database
    /// </summary>
    public void NewItem(string iCode, string iDesc, string iCost)
    {
        // Checks if the entered data is acceptable
        // If so, adds new item to the database
        try
        {
            clsDataAccess DB = new clsDataAccess();
            clsItemsSQL SQL = new clsItemsSQL();
            string sSQL = SQL.InsertItemDesc(iCode, iDesc, iCost);
            DB.ExecuteNonQuery(sSQL);
            ItemDescription ID = new ItemDescription();
            ID.Cost = Decimal.Parse(iCost, NumberStyles.AllowDecimalPoint);
            ID.ItemDesc = iDesc;
            ID.ItemCode = iCode;
            Items.Add(ID);
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }
}
