using GroupProject.Item;
using GroupProject.Models;
using GroupProject.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Windows;

public class clsItemsLogic
{
    clsDataAccess DB = new clsDataAccess();
    clsItemsSQL SQL = new clsItemsSQL();
    string sSQL;
    int iRetVal = 0;
    decimal iRet;
    string s;

    private readonly InvoiceRepository _invoiceRepository;
    public ObservableCollection<ItemDescription> Items { get; set; }
    /// <summary>
    /// Constructor for clsItemsLogic class
    /// </summary>
    public clsItemsLogic()
    {
        try
        {
            // Insert constructor code here.
            _invoiceRepository = new InvoiceRepository();
            var items = _invoiceRepository.GetAllItems();
            Items = new ObservableCollection<ItemDescription>(items);
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// Checks if input is not valid. Returns true if not valid
    /// </summary>
    private bool NotValid(string iCode = "A", string iDesc = "A", string iCost = "1.00")
    {
        try
        {
            if (iCode == "" || iDesc == "" || iCost == "") return true;
            else if (iCode.Length != 1) return true;
            else if (!Decimal.TryParse(iCost, out iRet)) return true;
            else if ((iCode + iDesc + iCost).IndexOf('\'') != -1) return true;
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// Checks if an item exists in the database. returns true if there is.
    /// </summary>
    private bool CodeExists(string iCode)
    {
        try
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ItemCode == iCode)
                {
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
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
            if (NotValid(iCode) || !CodeExists(iCode)) return;
            sSQL = SQL.SelectInvoiceID(iCode);
            DataSet DS = DB.ExecuteSQLStatement(sSQL, ref iRetVal);
            if (DS.Tables[0].Rows.Count > 0)
            {
                s = "Cannot delete as this item is listed on the following invoices: ";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    var invoiceNo = DS.Tables[0].Rows[i].ItemArray[0];
                    if (invoiceNo != null) s += invoiceNo.ToString();
                    if (i < DS.Tables[0].Rows.Count - 1) s += ", ";
                }
                MessageBox.Show(s);
                return;
            }
            sSQL = SQL.DeleteItemDesc(iCode);
            DB.ExecuteNonQuery(sSQL);
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ItemCode == iCode)
                {
                    Items.RemoveAt(i);
                    return;
                }
            }
            RefreshItems();
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
            if (NotValid(iCode, iDesc, iCost) || !CodeExists(iCode)) return;
            sSQL = SQL.UpdateItemDesc(iCode, iDesc, iCost);
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
            RefreshItems();
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
            if (NotValid(iCode, iDesc, iCost) || CodeExists(iCode)) return;
            sSQL = SQL.InsertItemDesc(iCode, iDesc, iCost);
            DB.ExecuteNonQuery(sSQL);
            ItemDescription ID = new ItemDescription();
            ID.Cost = Decimal.Parse(iCost, NumberStyles.AllowDecimalPoint);
            ID.ItemDesc = iDesc;
            ID.ItemCode = iCode;
            Items.Add(ID);
            RefreshItems();
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// Refreshes the item list from the database
    /// </summary>
    private void RefreshItems()
    {
        try
        {
            var items = _invoiceRepository.GetAllItems();
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }
}
