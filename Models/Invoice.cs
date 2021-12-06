using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace GroupProject.Models
{
    public class Invoice
    {
        public Invoice()
        {

        }
        public Invoice(Invoice invoice)
        {
            InvoiceNum = invoice.InvoiceNum;
            InvoiceDate = invoice.InvoiceDate;
            TotalCost = invoice.TotalCost;
            LineItems = invoice.LineItems;
        }
        /// <summary>
        /// Invoice Key
        /// </summary>
        [Key]
        public int InvoiceNum { get; set; }
        /// <summary>
        /// User Set Invoice date
        /// </summary>
        public DateTime InvoiceDate { get; set; }
        /// <summary>
        /// TotalCost
        /// </summary>
        public Decimal TotalCost { get; set; }
        /// <summary>
        /// List of Related InvoiceLineItems
        /// </summary>
        public IList<ItemDescription> LineItems { get; set; }
    }
}