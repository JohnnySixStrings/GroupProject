using Dapper.Contrib.Extensions;

namespace GroupProject.Models
{
    public class LineItem
    {
        /// <summary>
        /// InvoiceLineItem Key
        /// </summary>
        [Key]
        public int LineItemNumber { get; set; }
        /// <summary>
        /// Invoice Foreign Key
        /// </summary>
        public int InvoiceNum { get; set; }
        /// <summary>
        /// ItemDescription Foreign
        /// </summary>
        public char ItemCode { get; set; }
        /// <summary>
        /// ItemDescription Related to enity
        /// </summary>
        public ItemDescription ItemDescription { get; set; }
        /// <summary>
        /// Invoice related to this entity
        /// </summary>
        public Invoice Invoice { get; set; }
    }
}