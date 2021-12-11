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
    public class clsMainLogic : ReactiveObject, IDisposable
    {
        /// <summary>
        /// repository for getting object out of the database
        /// </summary>
        private readonly InvoiceRepository _invoiceRepository;

        /// <summary>
        /// Invoices currently in the database 
        /// </summary>
        public List<Invoice> Invoices { get; set; }
        public ObservableCollection<ItemDescription> Items { get; set; }
        /// <summary>
        /// The Selected invoice's line items
        /// </summary>
        public ObservableCollection<ItemDescription> SelectedInvoiceItems { get; set; }
        private Invoice _invoice;
        /// <summary>
        /// Flag for if the current invoice is new 
        /// </summary>
        private bool IsNewInvoice { get; set; }
        /// <summary>
        /// Binding target for Invoice
        /// </summary>
        public Invoice Invoice
        {
            get { return _invoice; }
            set { this.RaiseAndSetIfChanged(ref _invoice, value); }
        }
        /// <summary>
        /// Constructor for setting initial state
        /// </summary>
        public clsMainLogic()
        {
            _invoiceRepository = new InvoiceRepository();
            Invoices = _invoiceRepository.GetAllInvoices().ToList();
            var items = _invoiceRepository.GetAllItems().ToList();
            Items = new ObservableCollection<ItemDescription>(items);
            IsNewInvoice = true;
            Invoice = new Invoice();
            SelectedInvoiceItems = new ObservableCollection<ItemDescription>(Invoice?.LineItems);
            SelectedInvoiceItems.CollectionChanged += SelectedInvoiceItems_CollectionChanged;
        }

        /// <summary>
        /// Upddates the data contained.
        /// </summary>
        public void UpdateContext()
        {
            Invoices = _invoiceRepository.GetAllInvoices().ToList();
            var items = _invoiceRepository.GetAllItems().ToList();
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        /// <summary>
        /// Whenever an item is removed or added to the collection it runs this funcion update the total cost and refresh the state of the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedInvoiceItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            //update the total in the collection
            Invoice.TotalCost = SelectedInvoiceItems.Select(i => i.Cost).Sum();
            Invoice = new Invoice(Invoice);
        }

        /// <summary>
        /// Deleted the invoice and it's related items
        /// </summary>
        public void DeleteInvoice()
        {
            _invoiceRepository.DeleteInvoice(Invoice.InvoiceNum);

        }

        /// <summary>
        /// Set the invoice to a blank invoice to be created
        /// </summary>
        public void NewInvoice()
        {
            Invoice = new Invoice();
            SelectedInvoiceItems.Clear();
            IsNewInvoice = true;
        }

        /// <summary>
        /// Handles the logic for saving an invoice and its data
        /// </summary>
        public void SaveInvoice()
        {
            if (IsNewInvoice)
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
        /// <summary>
        /// Switches the state to the passed in invoice
        /// </summary>
        /// <param name="source"></param>
        public void ChangeInvoice(Invoice source)
        {
            Invoice = _invoiceRepository.GetInvoive(source.InvoiceNum);
            IsNewInvoice = false;
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
        /// Dispose of listners or objects at the end of the object.
        /// </summary>
        public void Dispose()
        {
            SelectedInvoiceItems.CollectionChanged -= SelectedInvoiceItems_CollectionChanged;
        }
    }
}
