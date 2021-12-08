using Dapper.Contrib.Extensions;
using System;

namespace GroupProject.Models
{
    [Table("ItemDesc")]
    public class ItemDescription
    {
        /// <summary>
        /// ItemDescription key
        /// </summary>
        [Key]
        public String ItemCode { get; set; }
        /// <summary>
        /// Description of Item
        /// </summary>

        public string ItemDesc { get; set; }
        /// <summary>
        /// Item Cost
        /// </summary>
        public Decimal Cost { get; set; }
    }
}
