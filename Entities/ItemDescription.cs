using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Entities
{
    public class ItemDescription : IEntityTypeConfiguration<ItemDescription>
    {
        /// <summary>
        /// ItemDescription key
        /// </summary>
        public int ItemDescriptionId { get; set; }
        /// <summary>
        /// Description of Item
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Item Cost
        /// </summary>
        public Decimal Cost { get; set; }
        /// <summary>
        /// InvoiceItems that Relate to this enity
        /// </summary>
        public IList<InvoiceLineItem> InvoiceLineItems { get; set; }
        /// <summary>
        /// Setting up of the relationships and properties
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ItemDescription> builder)
        {
            builder.HasKey(x => x.ItemDescriptionId);
            builder.Property(x => x.Cost)
                .HasPrecision(19, 4);
        }
    }
}
