using GroupProject.Models;
using GroupProject.Repositories;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace GroupProject.Main
{
    public class MainViewModel : ReactiveObject, IDisposable
    {
        private readonly InvoiceRepository _invoiceRepository;
        private readonly List<ItemDescription> _itemList;
        public List<Invoice> Invoices { get; set; }
        public ObservableCollection<ItemDescription> Items { get; set; }
        public ObservableCollection<ItemDescription> SelectedInvoiceItems { get; set; }
        private Invoice _invoice;
        private bool _newInvoice { get; set; }
        public Invoice Invoice
        {
            get { return _invoice; }
            set { this.RaiseAndSetIfChanged(ref _invoice, value); }
        }
        public MainViewModel()
        {
            _invoiceRepository = new InvoiceRepository();
            Invoices = _invoiceRepository.GetAllInvoices().ToList();
            _itemList = _invoiceRepository.GetAllItems().ToList();
            Items = new ObservableCollection<ItemDescription>(_itemList);
            _newInvoice = true;
            Invoice = new Invoice();
            SelectedInvoiceItems = new ObservableCollection<ItemDescription>(Invoice?.LineItems);
            SelectedInvoiceItems.CollectionChanged += SelectedInvoiceItems_CollectionChanged;
        }

        private void SelectedInvoiceItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            //update the total in the collection
            Invoice.TotalCost = SelectedInvoiceItems.Select(i => i.Cost).Sum();
            Invoice = new Invoice(Invoice);
        }

        public void DeleteInvoice()
        {
            _invoiceRepository.DeleteInvoice(Invoice.InvoiceNum);
        }

        public void NewInvoice()
        {
            Invoice = new Invoice();
            SelectedInvoiceItems.Clear();
            _newInvoice = true;

        }

        public void SaveInvoice()
        {


            if (_newInvoice)
            {
                if (Invoices.Select(i => i.InvoiceNum).Any(i => i == Invoice.InvoiceNum))
                {
                    return;
                }
                var invoiceNum = _invoiceRepository.AddInvoice(Invoice);
                Invoice = _invoiceRepository.GetInvoive(invoiceNum);
            }
            else
            {
                _invoiceRepository.UpdateInvoice(Invoice);
            }

            _invoiceRepository.DeleteLineItems(Invoice.InvoiceNum);
            var insert = SelectedInvoiceItems
                .Select((s, i) => new LineItem()
                {
                    InvoiceNum = Invoice.InvoiceNum,
                    LineItemNumber = i,
                    ItemCode = s.ItemCode
                })
                .ToList();
            _invoiceRepository.AddInvoices(insert);

            Invoices = _invoiceRepository.GetAllInvoices().ToList();
            Invoice = _invoiceRepository.GetInvoive(Invoice.InvoiceNum);
        }

        public void ChangeInvoice(Invoice source)
        {
            Invoice = _invoiceRepository.GetInvoive(source.InvoiceNum);
            _newInvoice = false;
            SelectedInvoiceItems.Clear();
            foreach (var item in Invoice.LineItems)
            {
                SelectedInvoiceItems.Add(item);
            }
        }

        /// <summary>
        /// Adds an item to the selected to items tied to the invoice.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ItemDescription item)
        {
            SelectedInvoiceItems.Add(item);

        }

        /// <summary>
        /// Dispose of anything you tell it to dispose at the end of the object
        /// </summary>
        public void Dispose()
        {
            SelectedInvoiceItems.CollectionChanged -= SelectedInvoiceItems_CollectionChanged;
        }
    }
}
