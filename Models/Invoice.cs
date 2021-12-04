using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace GroupProject.Models
{
    public class Invoice
    {
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