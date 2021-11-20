using GroupProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GroupProject.Repositories
{
    public class InvoiceRepository
    {
       
        // we can use a shared repository to access the data shared by the application
        // sharing code instead of repeating the logic throughout the project using the repository pattern
        private readonly InvoiceDbContext _invoiceDb;
        /// <summary>
        /// Setups the DbContext to be used for the life of the repository.
        /// </summary>
        public InvoiceRepository()
        {
            _invoiceDb = new InvoiceDbContext();
        }

        //operations on invoices 
        #region
        public void UpdateInvoice(Invoice invoice)
        {
            _invoiceDb.Update(invoice);
            _invoiceDb.SaveChanges();
        }
        /// <summary>
        /// Deletes the passed in invoice from the database
        /// </summary>
        /// <param name="invoice"></param>
        public void DeleteInvoice(Invoice invoice)
        {
            _invoiceDb.Remove(invoice);
            _invoiceDb.SaveChanges();
        }
        /// <summary>
        /// Inserts invoice into the database
        /// </summary>
        /// <param name="invoice"></param>
        public void AddInvoice(Invoice invoice)
        {
            _invoiceDb.Add(invoice);
            _invoiceDb.SaveChanges();
        }
        /// <summary>
        /// returns an invoice by it is id if exists if it does not then returns null for you to handle
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public Invoice? GetInvoive(int invoiceId)
        {
            var invoice = _invoiceDb.Invoices
               .Where(x => x.InvoiceId == invoiceId)
               .Include(x => x.InvoiceLineItems)
               .ThenInclude(x => x.ItemDescription)
               .FirstOrDefault();

            return invoice;
        }

        /// <summary>
        /// returns a list of all invoices in the database
        /// </summary>
        /// <returns></returns>
        public IList<Invoice> GetAllInvoices()
        {
            var invoices = _invoiceDb.Invoices
                .Include(x => x.InvoiceLineItems)
                .ThenInclude(x => x.ItemDescription)
                .ToList();
            return invoices;
        }
        #endregion

        //operations on lineItems
        #region
        /// <summary>
        /// Deletes a list of lineitems from the database
        /// </summary>
        /// <param name="invoiceId"></param>
        public void DeleteLineItems(int invoiceId)
        {
            var lineItems = _invoiceDb.InvoiceLineItems
                 .Where(x => x.InvoiceId == invoiceId)
                 .ToList();
            _invoiceDb.RemoveRange(lineItems);
            _invoiceDb.SaveChanges();
        }

        /// <summary>
        /// Deletes line Item from the database
        /// </summary>
        /// <param name="lineItems"></param>
        public void DeleteLineItems(IList<InvoiceLineItem> lineItems)
        {
            _invoiceDb.RemoveRange(lineItems);
            _invoiceDb.SaveChanges();
        }
        /// <summary>
        /// Inserts lineItem to the Database
        /// </summary>
        /// <param name="lineItem"></param>
        public void AddLineItem(InvoiceLineItem lineItem)
        {
            _invoiceDb.Add(lineItem);
            _invoiceDb.SaveChanges();
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
            _invoiceDb.Remove(item);
            _invoiceDb.SaveChanges();
        }
        /// <summary>
        /// Inserts passed in item to the database
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ItemDescription item)
        {
            _invoiceDb.Add(item);
            _invoiceDb.SaveChanges();
        }

        /// <summary>
        /// Updates passed in item in the databasee
        /// </summary>
        /// <param name="item"></param>
        public void UpdateItem(ItemDescription item)
        {
            _invoiceDb.Update(item);
            _invoiceDb.SaveChanges();
        }
        
        /// <summary>
        /// Gets a list of all items in the database
        /// </summary>
        /// <returns></returns>
        public IList<ItemDescription> GetAllItems()
        {
            var items = _invoiceDb.ItemDescriptions
                .Include(x=> x.InvoiceLineItems)
                .ThenInclude(x=> x.Invoice)
                .ToList();
            return items;
        }
        #endregion
    }
}
