using GroupProject.Models;
using GroupProject.Repositories;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
    public class MainViewModel : ReactiveObject
    {
        private readonly InvoiceRepository _invoiceRepository;
        public ObservableCollection<Invoice> Invoices { get; set;}
        public ObservableCollection<ItemDescription> Items { get; set;}
        public ObservableCollection<ItemDescription> SelectedInvoiceItems { get; set;}
        private Invoice _invoice;
        public Invoice Invoice { 
            get { return _invoice; } 
            set { this.RaiseAndSetIfChanged(ref _invoice, value); }
        }
        public MainViewModel()
        {
            _invoiceRepository = new InvoiceRepository();
            Invoices = new ObservableCollection<Invoice>(_invoiceRepository.GetAllInvoices());
            Items = new ObservableCollection<ItemDescription>( _invoiceRepository.GetAllItems());
            Invoice = _invoiceRepository.GetInvoive(5001);
            SelectedInvoiceItems = new ObservableCollection<ItemDescription>(Invoice?.LineItems);
        }

        public void AddItem(ItemDescription item)
        {
            SelectedInvoiceItems.Add(item);
        }
    }
}
