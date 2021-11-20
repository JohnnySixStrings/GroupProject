using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace GroupProject.Entities
{
    public class InvoiceLineItem : IEntityTypeConfiguration<InvoiceLineItem>
    {
        /// <summary>
        /// InvoiceLineItem Key
        /// </summary>
        public int InvoiceLineItemId { get; set; }
        /// <summary>
        /// Invoice Foreign Key
        /// </summary>
        public int InvoiceId { get; set;}
        /// <summary>
        /// ItemDescription Foreign
        /// </summary>
        public int ItemDescriptionId { get; set; }
        /// <summary>
        /// ItemDescription Related to enity
        /// </summary>
        public ItemDescription ItemDescription { get; set; }
        /// <summary>
        /// Invoice related to this entity
        /// </summary>
        public Invoice Invoice { get; set; }
        /// <summary>
        /// for setting up relationships and properties of entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<InvoiceLineItem> builder)
        {
            builder.HasKey(x => x.InvoiceLineItemId);
            builder.HasOne(x => x.ItemDescription)
                .WithMany( x=>x.InvoiceLineItems);
            builder.HasOne(x => x.Invoice)
                .WithMany(x => x.InvoiceLineItems);
        }
    }
}