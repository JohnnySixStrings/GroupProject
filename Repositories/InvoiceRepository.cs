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
                connection.Execute(@"UPDATE Invoices SET TotalCost = @TotalCost WHERE InvoiceNum = @InvoiceNum", new { invoice.TotalCost, invoice.InvoiceNum });

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
        public void DeleteInvoice(int invoiceNum)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                connection.Execute(@"DELETE FROM Invoices WHERE InvoiceNum = @invoiceNum", invoiceNum);
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
        public int AddInvoice(Invoice invoice)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);
                connection.Open();
                connection.Execute(@"INSERT INTO Invoices (InvoiceDate, TotalCost) Values (@InvoiceDate, @InvoiceNum)", new { InvoiceDate = invoice.InvoiceDate.ToShortDateString(), invoice.InvoiceNum });
                var invoiceId = connection.Query<int>(@"SELECT @@IDENTITY;").Single();
                return invoiceId;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public void DeleteLineItems(int invoiceNum)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                var inserted = connection.Execute(@"DELETE FROM LineItems WHERE InvoiceNum = @InvoiceNum", new { InvoiceNum = invoiceNum });

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
FROM (Invoices LEFT OUTER JOIN LineItems ON (Invoices.InvoiceNum = LineItems.InvoiceNum))
  LEFT OUTER JOIN ItemDesc ON (ItemDesc.ItemCode = LineItems.ItemCode) WHERE Invoices.[InvoiceNum] = @InvoiceNum",
                    (i, d) =>
                    {
                        Invoice retInvoice;

                        if (!dict.TryGetValue(i.InvoiceNum, out retInvoice))
                        {
                            retInvoice = i;
                            retInvoice.LineItems = new List<ItemDescription>();
                            dict.Add(i.InvoiceNum, retInvoice);
                        }

                        if (d is not null)
                            retInvoice.LineItems.Add(d);

                        return retInvoice;
                    },
                    new { InvoiceNum = invoiceId },
                    splitOn: "ItemCode"
                    );


                return invoice.Distinct()
                    .SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public void AddInvoices(List<LineItem> insert)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);
                foreach (var lineItem in insert)
                {
                    connection.Execute("INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values (@InvoiceNum,@LineItemNumber,@ItemCode)", new { lineItem.InvoiceNum, lineItem.LineItemNumber, lineItem.ItemCode });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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
        #region line items operations
        /// <summary>
        /// Deletes a lineitem from the database
        /// </summary>
        /// <param name="invoiceId"></param>
        public void DeleteLineItem(LineItem lineItem)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                //var deleted = connection.
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

                //var deleted = connection
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
        public void AddLineItem(LineItem lineItem)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);
                connection.Execute("INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values (@InvoiceNum,@LineItemNumber,@ItemCode)", new { lineItem.InvoiceNum, lineItem.LineItemNumber, lineItem.ItemCode });

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        //operations on items
        #region ItemDesc operations
        /// <summary>
        /// Deletes passed in item from the database
        /// </summary>
        /// <param name="item"></param>
        public void DeleteItem(ItemDescription item)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                //var deleted = connection
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
        public int AddItem(ItemDescription item)
        {
            try
            {
                using var connection = new OleDbConnection(_connectionString);

                //var insertedId = connection
                return 1;
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

                // var updated = connection
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
