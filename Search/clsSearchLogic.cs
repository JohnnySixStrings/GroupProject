using GroupProject.Models;
using GroupProject.Repositories;
using System.Collections.ObjectModel;

namespace GroupProject.Search
{
    class clsSearchLogic
    {
        public ObservableCollection<Invoice> Invoices { get; set; }
        private readonly InvoiceRepository _invoiceRepository;
        public clsSearchLogic()
        {
            _invoiceRepository = new InvoiceRepository();
            var invoices = _invoiceRepository.GetAllInvoices();
            Invoices = new ObservableCollection<Invoice>(invoices);
        }
        //This class will have a local variable for InvoiceID that will be sent over to the other windows with a function like a setter.
        //private int iInvoiceID;


    }
}
