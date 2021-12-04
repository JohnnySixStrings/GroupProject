using GroupProject.Models;
using GroupProject.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
    public class MainViewModel
    {
        private readonly InvoiceRepository _invoiceRepository;
        public ObservableCollection<Invoice> Invoices { get; set;}
        public ObservableCollection<ItemDescription> Items { get; set;}
        public MainViewModel()
        {
            _invoiceRepository = new InvoiceRepository();
            Invoices = new ObservableCollection<Invoice>(_invoiceRepository.GetAllInvoices());
            Items = new ObservableCollection<ItemDescription>( _invoiceRepository.GetAllItems());
        }
    }
}
