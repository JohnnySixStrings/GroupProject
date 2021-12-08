using Dapper.Contrib.Extensions;

namespace GroupProject.Models
{
    public class LineItem
    {
        /// <summary>
        /// InvoiceLineItem Key
        /// </summary>
        public int LineItemNumber { get; set; }
        /// <summary>
        /// Invoice Foreign Key
        /// </summary>
        [ExplicitKey]
        public int InvoiceNum { get; set; }
        /// <summary>
        /// ItemDescription Foreign
        /// </summary>
        [ExplicitKey]
        public string ItemCode { get; set; }
        /// <summary>
        /// ItemDescription Related to enity
        /// </summary>
        [Write(false)]
        public ItemDescription ItemDescription { get; set; }
        /// <summary>
        /// Invoice related to this entity
        /// </summary>
        [Write(false)]
        public Invoice Invoice { get; set; }
    }
}