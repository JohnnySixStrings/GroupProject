using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;


namespace GroupProject.Models
{
    [Table("ItemDesc")]
    public class ItemDescription
    {
        /// <summary>
        /// ItemDescription key
        /// </summary>
        [Key]
        public Char ItemCode { get; set; }
        /// <summary>
        /// Description of Item
        /// </summary>
        
        public string ItemDesc { get; set; }
        /// <summary>
        /// Item Cost
        /// </summary>
        public Decimal Cost { get; set; }
        /// <summary>
        /// InvoiceItems that Relate to this enity
        /// </summary>
        public IList<LineItem> LineItems { get; set; }
    }
}
