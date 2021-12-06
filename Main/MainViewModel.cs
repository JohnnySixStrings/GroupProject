using GroupProject.Models;
using GroupProject.Repositories;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
    public class MainViewModel : ReactiveObject , IDisposable
    {
        private readonly InvoiceRepository _invoiceRepository;
        private readonly List<ItemDescription> _itemList;
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
            _itemList = _invoiceRepository.GetAllItems().ToList();
            Items = new ObservableCollection<ItemDescription>(_itemList);
            Invoice = _invoiceRepository.GetInvoive(5001);
            SelectedInvoiceItems = new ObservableCollection<ItemDescription>(Invoice?.LineItems);
            SelectedInvoiceItems.CollectionChanged += SelectedInvoiceItems_CollectionChanged;
        }

        private void SelectedInvoiceItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            var items = _itemList.Where(i => !SelectedInvoiceItems.Select(s=>s.ItemCode).ToList().Contains(i.ItemCode)).ToList();
            Items.Clear();
            foreach(var item in items)
            {
                Items.Add(item);
            }
            //update the total in the collection
            Invoice.TotalCost = SelectedInvoiceItems.Select(i => i.Cost).Sum();
            Invoice = new Invoice(Invoice);
        }

        internal void DeleteInvoice()
        {
            throw new NotImplementedException();
        }

        internal void NewInvoice()
        {
            throw new NotImplementedException();
        }

        internal void SaveInvoice()
        {
            throw new NotImplementedException();
        }

        public void AddItem(ItemDescription item)
        {
            SelectedInvoiceItems.Add(item);
        }

        public void Dispose()
        {
            SelectedInvoiceItems.CollectionChanged -= SelectedInvoiceItems_CollectionChanged;
        }
    }
}
