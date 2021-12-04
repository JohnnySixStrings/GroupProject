using Dapper;
using Dapper.Contrib.Extensions;
using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GroupProject.Repositories
{
    public class InvoiceRepository
    {

        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private readonly string _connectionString;

        // we can use a shared repository to access the data shared by the application
        // sharing code instead of repeating the logic throughout the project using the repository pattern
        /// <summary>
        /// 
        /// </summary>
        public InvoiceRepository()
        {
            _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
        }

        //operations on invoices 
        #region
        public void UpdateInvoice(Invoice invoice)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                var updated = connection.Update(invoice);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodBase.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Deletes the passed in invoice from the database
        /// </summary>
        /// <param name="invoice"></param>
        public void DeleteInvoice(Invoice invoice)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                var deleted = connection.Delete(invoice);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Inserts invoice into the database
        /// </summary>
        /// <param name="invoice"></param>
        public void AddInvoice(Invoice invoice)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                var updated = connection.Insert(invoice);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// returns an invoice by it is id if exists if it does not then returns null for you to handle
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public Invoice? GetInvoive(int invoiceId)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);
                var dict = new Dictionary<int, Invoice>();
                //too lazy to learn to the specifics of access.
                //Never have used it at any job in my 3 years being an engineer.
                var invoice = connection.Query<Invoice, ItemDescription, Invoice>(
                    @"SELECT DISTINCT Invoices.InvoiceNum, Invoices.InvoiceDate, Invoices.TotalCost, ItemDesc.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost
FROM ItemDesc INNER JOIN (Invoices INNER JOIN LineItems ON Invoices.[InvoiceNum] = LineItems.[InvoiceNum]) ON ItemDesc.[ItemCode] = LineItems.[ItemCode] WHERE Invoices.[InvoiceNum] = @InvoiceNum",
                    (i, d) =>
                    {
                        Invoice retInvoice;
                        
                        if(!dict.TryGetValue(i.InvoiceNum, out retInvoice))
                        {
                            retInvoice = i;
                            retInvoice.LineItems = new List<ItemDescription>();
                            dict.Add(i.InvoiceNum, retInvoice);
                        }
                        retInvoice.LineItems.Add(d);
                        return retInvoice;
                    }, 
                    new { InvoiceNum = invoiceId },
                    splitOn: "ItemCode"
                    )
                    .Distinct()
                    .SingleOrDefault();

                return invoice;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns a list of all invoices in the database
        /// </summary>
        /// <returns></returns>
        public IList<Invoice> GetAllInvoices()
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                var invoices = connection.GetAll<Invoice>().ToList();
                return invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        //operations on lineItems
        #region
        /// <summary>
        /// Deletes a list of lineitems from the database
        /// </summary>
        /// <param name="invoiceId"></param>
        public void DeleteLineItem(LineItem lineItem)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                var deleted = connection.Delete<LineItem>(lineItem);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes line Item from the database
        /// </summary>
        /// <param name="lineItems"></param>
        public void DeleteLineItems(IList<LineItem> lineItems)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                var deleted = connection.Delete(lineItems);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Inserts lineItem to the Database
        /// </summary>
        /// <param name="lineItem"></param>
        public long AddLineItem(LineItem lineItem)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                var insertedId = connection.Insert<LineItem>(lineItem);
                return insertedId;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        //operations on items
        #region
        /// <summary>
        /// Deletes passed in item from the database
        /// </summary>
        /// <param name="item"></param>
        public void DeleteItem(ItemDescription item)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                var deleted = connection.Delete(item);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Inserts passed in item to the database
        /// </summary>
        /// <param name="item"></param>
        public long AddItem(ItemDescription item)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                var insertedId = connection.Insert(item);
                return insertedId;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates passed in item in the databasee
        /// </summary>
        /// <param name="item"></param>
        public void UpdateItem(ItemDescription item)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                var updated = connection.Update(item);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of all items in the database
        /// </summary>
        /// <returns></returns>
        public IList<ItemDescription> GetAllItems()
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                var items = connection.GetAll<ItemDescription>().ToList();
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
