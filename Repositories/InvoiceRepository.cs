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

        public void DeleteInvoice(Invoice invoice)
        {
            _invoiceDb.Remove(invoice);
            _invoiceDb.SaveChanges();
        }

        public void AddInvoice(Invoice invoice)
        {
            _invoiceDb.Add(invoice);
            _invoiceDb.SaveChanges();
        }

        public Invoice? GetInvoive(int invoiceId)
        {
            var invoice = _invoiceDb.Invoices
               .Where(x => x.InvoiceId == invoiceId)
               .Include(x => x.InvoiceLineItems)
               .ThenInclude(x => x.ItemDescription)
               .FirstOrDefault();

            return invoice;
        }

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
        public void DeleteLineItems(int invoiceId)
        {
            var lineItems = _invoiceDb.InvoiceLineItems
                 .Where(x => x.InvoiceId == invoiceId)
                 .ToList();
            _invoiceDb.RemoveRange(lineItems);
            _invoiceDb.SaveChanges();
        }

        public void DeleteLineItems(IList<InvoiceLineItem> lineItems)
        {
            _invoiceDb.RemoveRange(lineItems);
            _invoiceDb.SaveChanges();
        }

        public void AddLineItem(InvoiceLineItem lineItem)
        {
            _invoiceDb.Add(lineItem);
            _invoiceDb.SaveChanges();
        }
        #endregion

        //operations on items
        #region
        public void DeleteItem(ItemDescription item)
        {
            _invoiceDb.Remove(item);
            _invoiceDb.SaveChanges();
        }
        
        public void AddItem(ItemDescription item)
        {
            _invoiceDb.Add(item);
            _invoiceDb.SaveChanges();
        }

        public void UpdateItem(ItemDescription item)
        {
            _invoiceDb.Update(item);
            _invoiceDb.SaveChanges();
        }
        
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
